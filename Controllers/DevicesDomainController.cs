using Microsoft.AspNetCore.Mvc;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.DTOs.Devices;
using PrtgProxyApi.Domain.Mappers;
using PrtgProxyApi.Request.Devices;

namespace PrtgProxyApi.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class DevicesDomainController : ControllerBase
    {
        private readonly IDeviceServiceDomain _devicesService;
        private readonly ILogger<DevicesDomainController> _logger;

        public DevicesDomainController(IDeviceServiceDomain devicesService, ILogger<DevicesDomainController> logger)
        {
            _devicesService = devicesService;
            _logger = logger;
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
                var requestDomain = DeviceMapper.ToDomainRequest(request);
                var deviceId = await _devicesService.CreateDeviceAsync(requestDomain);
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
