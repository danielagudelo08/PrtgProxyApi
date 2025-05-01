using PrtgProxyApi.Domain.DTOs.Sensors;
using PrtgProxyApi.Domain.Entities;

namespace PrtgProxyApi.Domain.Mappers
{
    public static class SensorMapper
    {
        public static CreateSensorEntity ConvertRequestSensorHTTPToEntity(this CreateHttpSensorDTO request)
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

        public static CreateSensorEntity ConvertRequestSensorExecToEntity(this CreateHttpSensorDTO request)
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
