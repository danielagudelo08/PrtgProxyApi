using Microsoft.AspNetCore.Mvc;
using PrtgProxyApi.Contracts.Services;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Request.Devices;
using PrtgProxyApi.Services;

namespace PrtgProxyApi.Controllers
{
    [ApiController]
    [Route("api/v2/[controller]")]
    public class DevicesDomainController : ControllerBase
    {
        private readonly IDeviceServiceDomain _devicesService;
        private readonly ILogger<DevicesService> _logger;

        public DevicesDomainController(IDeviceServiceDomain devicesService, ILogger<DevicesService> logger)
        {
            _devicesService = devicesService;
            _logger = logger;
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


    }
}
