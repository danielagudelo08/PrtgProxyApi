using PrtgProxyApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain.Entities
{
    public class CreateSensorEntity
    {
        // Propiedades obligatorias
        public string Name { get; set; } = string.Empty; // Nombre del sensor
        public string Url { get; set; } = string.Empty;  // URL a monitorear
        public int DeviceId { get; set; } // ID del dispositivo donde se creará el sensor
        public HttpRequestMethodEnum HttpRequestMethod { get; set; } = HttpRequestMethodEnum.GET;
        public PriorityEnum Priority { get; set; } = PriorityEnum.Three;

        // Propiedades opcionales
        public string? RequestMethod { get; set; } // GET, POST, PUT, DELETE
        public List<string>? Tags { get; set; }
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
