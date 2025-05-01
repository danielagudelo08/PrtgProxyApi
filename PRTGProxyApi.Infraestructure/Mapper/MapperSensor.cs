using PrtgAPI;
using PrtgAPI.Parameters;
using PrtgProxyApi.Domain;
using PrtgProxyApi.Domain.DTOs.Sensors;
using PrtgProxyApi.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PrtgProxyApi.PrtgAPISatrack.Mapper
{
    public class MapperSensor
    {

        public static SensorEntity MapToDomainSensor(Sensor prtgSensor)
        {
            if (prtgSensor == null)
            {
                throw new ArgumentNullException(nameof(prtgSensor), "El sensor de PRTG no puede ser null");
            }

            return new SensorEntity
            {
                Id = prtgSensor.Id != 0 ? prtgSensor.Id : 0, // Si es nullable, usa .Value, de lo contrario, asigna 0
                ParentId = prtgSensor.ParentId != 0 ? prtgSensor.ParentId : 0, // Si ParentId es null, asignar 0
                Name = prtgSensor.Name ?? "Desconocido", // Si Name es null, asignar valor por defecto
                Device = prtgSensor.Device ?? "Desconocido", // Valor por defecto para Device
                Group = prtgSensor.Group ?? "Desconocido", // Valor por defecto para Group
                Probe = prtgSensor.Probe ?? "Desconocido", // Valor por defecto para Probe
                Status = prtgSensor.Status.ToString() ?? "Desconocido", // Si Status es null, asignar "Desconocido"
                Message = prtgSensor.Message ?? "No hay mensaje", // Valor por defecto para Message
                Active = prtgSensor.Active, // Si Active es null, asignar false
                Uptime = prtgSensor.Uptime.GetValueOrDefault(), // Si Uptime es null, asignar 0
                Downtime = prtgSensor.Downtime.GetValueOrDefault(), // Si Downtime es null, asignar 0
                LastCheck = prtgSensor.LastCheck ?? DateTime.MinValue, // Valor por defecto para LastCheck
                LastUp = prtgSensor.LastUp ?? DateTime.MinValue, // Valor por defecto para LastUp
                LastDown = prtgSensor.LastDown ?? DateTime.MinValue, // Valor por defecto para LastDown
                Comments = prtgSensor.Comments ?? "Sin comentarios", // Valor por defecto para Comments
                DisplayType = prtgSensor.DisplayType ?? "Desconocido", // Valor por defecto para DisplayType
                Tags = prtgSensor.Tags?.ToList() ?? new List<string>() // Si Tags es null, asignar lista vacía
            };
        }

        public static HttpSensorParameters ToPrtgParameters(CreateSensorEntity request)
        {
            return new HttpSensorParameters
            {
                Name = request.Name,
                Url = request.Url,
                HttpRequestMethod = MapHttpMethod(request.HttpRequestMethod),
                SensorType = SensorType.Http,
                Tags = new[] { "httpsensor" },
                Priority = MapPriority(request.Priority),
                Timeout = request.Timeout ?? 60,
                InheritInterval = false,
                Interval = MapInterval(request.IntervalSeconds)
            };
        }

        private static ScanningInterval MapInterval(int? seconds)
        {
            return seconds switch
            {
                30 => ScanningInterval.ThirtySeconds,
                60 => ScanningInterval.SixtySeconds,
                300 => ScanningInterval.FiveMinutes,
                600 => ScanningInterval.TenMinutes,
                _ => ScanningInterval.FiveMinutes
            };
        }

        private static HttpRequestMethod MapHttpMethod(HttpRequestMethodEnum method)
        {
            return method switch
            {
                HttpRequestMethodEnum.GET => PrtgAPI.HttpRequestMethod.GET,
                HttpRequestMethodEnum.POST => PrtgAPI.HttpRequestMethod.POST,
                HttpRequestMethodEnum.HEAD => PrtgAPI.HttpRequestMethod.HEAD,
                _ => throw new ArgumentOutOfRangeException(nameof(method), $"Método HTTP no soportado: {method}")
            };
        }

        private static Priority MapPriority(PriorityEnum priority)
        {
            return (Priority)(int)priority;
        }

    }
}
