using Microsoft.AspNetCore.Mvc;
using PrtgAPI; // Asegúrate de que tienes esta librería instalada
using PrtgProxyApi.Domain.Contracts;
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
    }

}
