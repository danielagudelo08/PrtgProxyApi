using Microsoft.AspNetCore.Mvc;
using PrtgProxyApi.Contracts.Services;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.DTOs.Sensors;
using PrtgProxyApi.Domain.Mappers;
using PrtgProxyApi.PrtgAPISatrack;
using PrtgProxyApi.Request;

namespace PrtgProxyApi.Controllers
{
    [ApiController]
    [Route("api/v2/sensors")]
    public class SensorsDomainController : ControllerBase
    {
        private readonly ISensorsServiceDomain _sensorsService;
        private readonly ILogger<SensorsController> _logger;

        public SensorsDomainController(ISensorsServiceDomain sensorsService, ILogger<SensorsController> logger)
        {
            _sensorsService = sensorsService;
            _logger = logger;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetSensorById(int id)
        {
            try
            {
                var sensor = await _sensorsService.GetSensorByIdAsync(id);

                if (sensor == null)
                    return NotFound($"Sensor con ID {id} no encontrado.");

                return Ok(sensor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener el sensor con ID {id}");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

        [HttpGet("name")]
        public async Task<IActionResult> GetSensorByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("El parámetro 'name' es obligatorio.");
            }

            var sensors = await _sensorsService.GetSensorsByNameAsyncDomain(name);

            if (sensors == null || sensors.Count == 0)
            {
                return NotFound($"No se encontraron sensores que coincidan con el nombre '{name}'.");
            }

            return Ok(sensors);
        }

        [HttpPost("create-sensor")]
        public IActionResult CreateHttpSensorDomain([FromBody] CreateHttpSensorDTO request)
        {
            try
            {
                var sensorDomain = SensorMapper.ConvertRequestSensorHTTPToDtoDomain(request);
                var sensorId = _sensorsService.CreateHttpSensorAsync(sensorDomain);
                return Ok(new { Message = "Sensor HTTP creado con éxito", SensorId = sensorId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al crear el sensor", Details = ex.Message });
            }
        }

    }
}
