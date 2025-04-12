using PrtgAPI;
using PrtgProxyApi.Domain.DTOs.Groups;

namespace PrtgProxyApi.Domain.Contracts
{
    public interface IGroupsService
    {
        Task<List<Group>> GetAllGroupsAsync();
        List<GroupSummaryDto> GetGroupSummaries();
        List<GroupOptionDto> GetAvailableGroups();
        Task<List<GroupOptionDto>> SearchGroupsByNameAsync(string name);
    }

}
