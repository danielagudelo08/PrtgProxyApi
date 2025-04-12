using Microsoft.Extensions.Options;
using PrtgAPI;
using PrtgProxyApi.Domain.Contracts;
using PrtgProxyApi.Domain.DTOs.Groups;
using PrtgProxyApi.Domain.Helpers;
using PrtgProxyApi.Settings;
using System.Text.RegularExpressions;

namespace PrtgProxyApi.Services
{
    public partial class GroupsService : IGroupsService
    {
        private readonly PrtgClient _client;
        private readonly ILogger<GroupsService> _logger;

        public GroupsService(IOptions<PrtgSettings> settings, ILogger<GroupsService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            var prtgSettings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));

            if (string.IsNullOrEmpty(prtgSettings.Server) ||
                string.IsNullOrEmpty(prtgSettings.Username) ||
                string.IsNullOrEmpty(prtgSettings.Password))
            {
                throw new InvalidOperationException("Faltan valores en la configuración de PRTG.");
            }

            _client = new PrtgClient(
                prtgSettings.Server,
                prtgSettings.Username,
                prtgSettings.Password,
                AuthMode.Password,
                prtgSettings.IgnoreSSL
            );
        }

        public Task<List<PrtgAPI.Group>> GetAllGroupsAsync()
        {
            try
            {
                var groups = _client.GetGroups().ToList();
                return Task.FromResult(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los grupos.");
                throw;
            }
        }

        public List<GroupSummaryDto> GetGroupSummaries()
        {
            return _client.GetGroups()
                .Select(g => new GroupSummaryDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    TotalSensors = g.TotalSensors,
                    TotalDevices = g.TotalDevices,
                    Priority = (int)g.Priority,
                    Comments = g.Comments,
                    Status = g.Status.ToString()
                })
                .ToList();
        }

        public List<GroupOptionDto> GetAvailableGroups()
        {
            return _client.GetGroups()
                .Where(g => g.Status == Status.Up)
                .Select(g => new GroupOptionDto
                {
                    Id = g.Id,
                    Name = g.Name
                })
                .ToList();
        }

        public async Task<List<GroupOptionDto>> SearchGroupsByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("El nombre no puede estar vacío", nameof(name));
            }

            try
            {
                // Obtener todos los grupos
                var allGroups = await _client.GetGroupsAsync();

                // Filtrar por nombre (sin importar mayúsculas y minúsculas)
                var filteredGroups = allGroups
                    .Where(g => TextNormalizer.Normalize(g.Name).Contains(TextNormalizer.Normalize(name)))
                    .Select(g => new GroupOptionDto
                    {n
                        Id = g.Id,
                        Name = g.Name
                    })
                    .ToList();

                return filteredGroups;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar grupos por nombre.");
                throw;
            }
        }
    }
}
