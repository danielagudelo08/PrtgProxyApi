using Microsoft.AspNetCore.Mvc;
using PrtgProxyApi.Domain;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.DTOs.Sensors;

namespace PrtgProxyApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class SensorsController : ControllerBase
    {
        private readonly ISensorsService _sensorsService;
        private readonly ILogger<SensorsController> _logger;

        public SensorsController(ISensorsService sensorsService, ILogger<SensorsController> logger)
        {
            _sensorsService = sensorsService ?? throw new ArgumentNullException(nameof(sensorsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sensorId = _sensorsService.CreateHttpSensorAsync(request);
                return Ok(new { Message = "Sensor HTTP creado con éxito", SensorId = sensorId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "Error al crear el sensor", Details = ex.Message });
            }
        }

        [HttpPost("create-exec-script")]
        public async Task<IActionResult> CreateExecScriptSensor([FromBody] CreateExecSensorDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var sensorId = await _sensorsService.CreateExecScriptSensorAsync(request);
                return Ok(new { SensorId = sensorId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el sensor de tipo Exec Script.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

    }
}
