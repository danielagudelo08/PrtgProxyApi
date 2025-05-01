using PrtgProxyApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain.DTOs.Sensors
{
    public class CreateHttpSensorDTO
    {
        // Propiedades obligatorias

        [Required(ErrorMessage = "El nombre del sensor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La URL a monitorear es obligatoria.")]
        [Url(ErrorMessage = "La URL no tiene un formato válido.")]
        public string Url { get; set; } = string.Empty;

        [Required(ErrorMessage = "El ID del dispositivo es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del dispositivo debe ser mayor que 0.")]
        public int DeviceId { get; set; }

        [Required(ErrorMessage = "El método HTTP es obligatorio.")]
        public HttpRequestMethodEnum HttpRequestMethod { get; set; } = HttpRequestMethodEnum.GET;

        [Required(ErrorMessage = "La prioridad es obligatoria.")]
        public PriorityEnum Priority { get; set; } = PriorityEnum.Three;

        // Propiedades opcionales

        [RegularExpression("GET|POST|PUT|DELETE", ErrorMessage = "El método debe ser GET, POST, PUT o DELETE.")]
        public string? RequestMethod { get; set; }

        public List<string>? Tags { get; set; }

        [StringLength(500, ErrorMessage = "Los comentarios no deben superar los 500 caracteres.")]
        public string? Comments { get; set; }

        [Range(100, 599, ErrorMessage = "El código de respuesta esperado debe estar entre 100 y 599.")]
        public int? ExpectedResponseCode { get; set; } = 200;

        [Range(1, 300, ErrorMessage = "El tiempo de espera debe estar entre 1 y 300 segundos.")]
        public int? Timeout { get; set; }

        [StringLength(500, ErrorMessage = "El texto de coincidencia no debe superar los 500 caracteres.")]
        public string? ContentMatch { get; set; }

        [StringLength(500, ErrorMessage = "El texto de no coincidencia no debe superar los 500 caracteres.")]
        public string? ContentNotMatch { get; set; }

        [StringLength(200, ErrorMessage = "El User-Agent no debe superar los 200 caracteres.")]
        public string? UserAgent { get; set; }

        public Dictionary<string, string>? CustomHeaders { get; set; }

        [StringLength(100, ErrorMessage = "El nombre de usuario no debe superar los 100 caracteres.")]
        public string? AuthenticationUser { get; set; }

        [StringLength(100, ErrorMessage = "La contraseña no debe superar los 100 caracteres.")]
        public string? AuthenticationPassword { get; set; }

        public bool? IgnoreSSLErrors { get; set; }

        [StringLength(200, ErrorMessage = "La dirección del proxy no debe superar los 200 caracteres.")]
        public string? Proxy { get; set; }

        [Range(1, 1440, ErrorMessage = "El intervalo en minutos debe estar entre 1 y 1440.")]
        public int? IntervalMinutes { get; set; }

        [Range(1, 1600, ErrorMessage = "El intervalo en segundos debe estar entre 1 y 1600.")]
        public int? IntervalSeconds { get; set; }
    }
}
