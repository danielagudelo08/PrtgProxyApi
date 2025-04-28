using PrtgAPI;
using PrtgProxyApi.DTOs.Groups;

namespace PrtgProxyApi.Contracts.Services
{
    public interface IGroupsService
    {
        //Task<List<Group>> GetAllGroupsAsync();
        List<GroupSummaryDto> GetGroupSummaries();
        List<GroupOptionDto> GetAvailableGroups();
        Task<List<GroupOptionDto>> SearchGroupsByNameAsync(string name);
    }

}
