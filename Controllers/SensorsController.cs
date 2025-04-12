using Microsoft.AspNetCore.Mvc;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.Request;

namespace PrtgProxyApi.Controllers
{
    [ApiController]
    [Route("api/sensors")]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorsService _sensorsService;

        public SensorsController(ISensorsService sensorsService)
        {
            _sensorsService = sensorsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSensors()
        {
            var sensors = await _sensorsService.GetSensorsAsync();
            return Ok(sensors);
        }

        [HttpPost("create-http-sensor")]
        public IActionResult CreateHttpSensor([FromBody] CreateHttpSensorRequest request)
        {
            try
            {
                var sensorId = _sensorsService.CreateHttpSensor(request);
                return Ok(new { Message = "Sensor HTTP creado con éxito", SensorId = sensorId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al crear el sensor", Details = ex.Message });
            }
        }
    }
}
