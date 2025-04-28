using Microsoft.AspNetCore.Mvc;
using PrtgAPI; // Asegúrate de que tienes esta librería instalada
using PrtgProxyApi.Contracts.Services;
using PrtgProxyApi.Request.Devices;
using PrtgProxyApi.Services;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace PrtgProxyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevicesController : ControllerBase
    {
        private readonly IDevicesService _devicesService;
        private readonly ILogger<DevicesService> _logger;

        public DevicesController(IDevicesService devicesService, ILogger<DevicesService> logger)
        {
            _devicesService = devicesService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetDevices()
        {
            try
            {
                var devices = await _devicesService.GetDevicesAsync();
                return Ok(devices);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error en la solicitud HTTP al obtener dispositivos.");
                return StatusCode(503, "Error al comunicarse con el servicio externo.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado en GetDevices.");
                return StatusCode(500, "Ocurrió un error interno.");
            }
        }

        [HttpGet("select")]
        public async Task<IActionResult> GetDevicesForSelect()
        {
            try
            {
                var devices = await _devicesService.GetDevicesForSelectAsync();
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
                var device = await _devicesService.GetDeviceByIdAsync(id);

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
        public async Task<IActionResult> CreateDevice([FromBody] CreateDeviceRequest request)
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
