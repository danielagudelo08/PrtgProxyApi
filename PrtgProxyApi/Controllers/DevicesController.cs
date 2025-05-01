using Microsoft.AspNetCore.Mvc;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.DTOs.Devices;

namespace PrtgProxyApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDeviceService _devicesService;
        private readonly ILogger<DevicesController> _logger;

        public DevicesController(IDeviceService devicesService, ILogger<DevicesController> logger)
        {
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        [HttpGet("select")]
        public async Task<IActionResult> GetDevicesForSelect()
        {
            try
            {
                var devices = await _devicesService.GetDevicesForSelect();
                return Ok(devices);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener dispositivos para el select.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("devices/{id}")]
        public async Task<IActionResult> GetDeviceById(int id)
        {
            try
            {
                var device = await _devicesService.GetDeviceById(id);

                if (device == null)
                    return NotFound($"No se encontró un dispositivo con ID {id}.");

                return Ok(device);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el dispositivo por ID.");
                return StatusCode(500, "Error al obtener el dispositivo.");
            }
        }

        [HttpPost("CreateDevices")]
        public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceDTO request)
        {
            if (request == null)
                return BadRequest("La solicitud no puede ser nula.");

            try
            {
                var deviceId = await _devicesService.CreateDeviceAsync(request);
                return CreatedAtAction(nameof(GetDeviceById), new { id = deviceId }, new { Id = deviceId });
            }
            catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Solicitud inválida para crear dispositivo.");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al crear el dispositivo.");
                return StatusCode(500, "Ocurrió un error inesperado al crear el dispositivo.");
            }
        }


    }
}
