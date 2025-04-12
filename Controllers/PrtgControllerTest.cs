using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PrtgAPI;
using PrtgAPI.Parameters;
using System;

namespace PrtgProxyApi.Controllers
{
    [Route("api/prtg")]
    [ApiController]
    public class PrtgControllerTest : ControllerBase
    {
        private readonly PrtgClient _client;
        private readonly int _deviceId;

        public PrtgControllerTest(IConfiguration config)
        {
            _client = new PrtgClient(
                config["PrtgConfig:Url"],
                config["PrtgConfig:Username"],
                config["PrtgConfig:Password"]
            );

            _deviceId = 9763; // ID del dispositivo en PRTG donde se creará el sensor
        }

        [HttpPost("sensor")]
        public IActionResult CrearSensor([FromBody] SensorRequest request)
        {
            try
            {
                var parameters = new HttpSensorParameters(request.Url)
                {
                    Name = request.Name,
                    Timeout = int.Parse(request.Timeout ?? "60"),
                    Interval = ScanningInterval.Parse(request.Interval ?? "300"), // Conversión correcta
                    Priority = (Priority)int.Parse(request.Priority ?? "3"),
                    Tags = request.Tags?.Split(','),
                    HttpRequestMethod = request.HttpMethod == "POST" ? HttpRequestMethod.POST : HttpRequestMethod.GET

                };

                _client.AddSensor(_deviceId, parameters);

                return Ok(new { message = "Sensor creado exitosamente" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }

    public class SensorRequest
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string HttpMethod { get; set; }
        public string Timeout { get; set; }
        public string Interval { get; set; }
        public string Priority { get; set; }
        public string Tags { get; set; }
    }
}
