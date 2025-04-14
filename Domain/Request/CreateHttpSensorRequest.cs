using PrtgAPI;

namespace PrtgProxyApi.Domain.Request
{
    public class CreateHttpSensorRequest
    {
        // Propiedades obligatorias
        public string Name { get; set; } = string.Empty; // Nombre del sensor
        public string Url { get; set; } = string.Empty;  // URL a monitorear
        public int DeviceId { get; set; } // ID del dispositivo donde se creará el sensor
        public HttpRequestMethod HttpRequestMethod { get; set; } = HttpRequestMethod.GET;

        // Propiedades opcionales
        public string? RequestMethod { get; set; } // GET, POST, PUT, DELETE
        public List<string>? Tags { get; set; }
        public Priority Priority { get; set; } = Priority.Three;
        public string? Comments { get; set; }
        public int? ExpectedResponseCode { get; set; } = 200; // Por defecto 200
        public int? Timeout { get; set; } // Tiempo de espera en segundos
        public string? ContentMatch { get; set; } // Texto que debe estar en la respuesta
        public string? ContentNotMatch { get; set; } // Texto que no debe estar en la respuesta
        public string? UserAgent { get; set; } // User-Agent personalizado
        public Dictionary<string, string>? CustomHeaders { get; set; } // Encabezados HTTP adicionales
        public string? AuthenticationUser { get; set; } // Usuario para autenticación HTTP
        public string? AuthenticationPassword { get; set; } // Contraseña para autenticación HTTP
        public bool? IgnoreSSLErrors { get; set; } // Omitir errores de certificado SSL
        public string? Proxy { get; set; } // Dirección del proxy si es necesario
        public int? IntervalMinutes { get; set; } // Intervalo de escaneo
        public int? IntervalSeconds { get; set; }
    }


}
