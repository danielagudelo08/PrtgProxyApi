using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain.DTOs.Sensors
{
    public class CreateExecSensorDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string ScriptName { get; set; } // Nombre del script a ejecutar

        public string? Parameters { get; set; } // Parámetros para el script

        [Range(1, 5)]
        public int Priority { get; set; } = 3;

        public string? Comments { get; set; }

        [Range(1, 60)]
        public int Timeout { get; set; } = 60;

        public int DeviceId { get; set; }
    }
}
