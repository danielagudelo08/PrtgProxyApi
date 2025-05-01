using Microsoft.Extensions.Logging;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.DTOs.Devices;
using PrtgProxyApi.DTOs.Devices;
using PrtgProxyApi.Request.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrtgProxyApi.Domain
{
    public class DeviceServiceDomain: IDeviceServiceDomain
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ILogger<DeviceServiceDomain> _logger;

        public DeviceServiceDomain(IDeviceRepository deviceRepository, ILogger<DeviceServiceDomain> logger)
        {
            _deviceRepository = deviceRepository ?? throw new ArgumentNullException(nameof(deviceRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<List<SelectDeviceDto>> GetDevicesForSelect()
        {
            try
            {
                _logger.LogInformation("Obteniendo dispositivos (solo id y nombre) desde PRTG.");

                // Llamada al repositorio que ya mapea y retorna el DTO
                var devices = await _deviceRepository.GetDevicesForSelectAsync();

                _logger.LogInformation($"Se encontraron {devices.Count} dispositivos.");
                return devices;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener dispositivos desde PRTG.");
                throw;
            }
        }

        public async Task<DeviceEntity?> GetDeviceById(int id)
        {
            try
            {
                _logger.LogInformation("Obteniendo dispositivo con Id : {DeviceId}.", id);
                return await _deviceRepository.GetDeviceByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el dispositivo desde PRTG.");
                throw;
            }   
        }

        public async Task<int> CreateDeviceAsync(CreateDeviceEntity request)
        {
            try
            {
                _logger.LogInformation("Intentando crear dispositivo.");
                return await _deviceRepository.CreateDeviceAsync(request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al intentar crear un disposito.");
                throw;
            }
            
        }
    }

}
