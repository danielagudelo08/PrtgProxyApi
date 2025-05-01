
using PrtgProxyApi.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PrtgProxyApi.Request.Devices
{
    public class CreateDeviceRequestDomain
    {
        // Obligatorios
        [Required(ErrorMessage = "El nombre del dispositivo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El nombre no debe superar los 100 caracteres.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "El host del dispositivo es obligatorio.")]
        [StringLength(100, ErrorMessage = "El host no debe superar los 100 caracteres.")]
        public string Host { get; set; } = null!;

        [Required(ErrorMessage = "El ID del padre es obligatorio.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ID del padre debe ser un número positivo.")]
        public int ParentId { get; set; }

        // Opcionales de creación
        public string[]? Tags { get; set; }

        public DeviceDiscoveryMode? AutoDiscoveryMode { get; set; }

        public DeviceDiscoverySchedule? AutoDiscoverySchedule { get; set; }

        // Opcionales vía SetObjectProperty
        [StringLength(250, ErrorMessage = "La ubicación no debe superar los 250 caracteres.")]
        public string? Location { get; set; }

        [StringLength(500, ErrorMessage = "Los comentarios no deben superar los 500 caracteres.")]
        public string? Comments { get; set; }

        [Range(0, 5, ErrorMessage = "La prioridad debe estar entre 0 y 5.")]
        public int? Priority { get; set; }
    }


}
