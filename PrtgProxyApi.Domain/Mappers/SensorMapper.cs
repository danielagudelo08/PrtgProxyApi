using PrtgProxyApi.Domain.DTOs.Sensors;
using PrtgProxyApi.PrtgAPISatrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain.Mappers
{
    public static class SensorMapper
    {
        public static CreateSensorEntity ConvertRequestSensorHTTPToDtoDomain(this CreateHttpSensorDTO request)
        {
            return new CreateSensorEntity
            {
                Name = request.Name,
                Url = request.Url,
                DeviceId = request.DeviceId,
                HttpRequestMethod = request.HttpRequestMethod,
                Priority = request.Priority,
                RequestMethod = request.RequestMethod,
                Tags = request.Tags,
                Comments = request.Comments,
                ExpectedResponseCode = request.ExpectedResponseCode,
                Timeout = request.Timeout,
                ContentMatch = request.ContentMatch,
                ContentNotMatch = request.ContentNotMatch,
                UserAgent = request.UserAgent,
                CustomHeaders = request.CustomHeaders,
                AuthenticationUser = request.AuthenticationUser,
                AuthenticationPassword = request.AuthenticationPassword,
                IgnoreSSLErrors = request.IgnoreSSLErrors,
                Proxy = request.Proxy,
                IntervalMinutes = request.IntervalMinutes,
                IntervalSeconds = request.IntervalSeconds
            };
        }
    }
}
