using Microsoft.AspNetCore.Mvc;
using PrtgAPI;
using PrtgProxyApi.Contracts.Services;
using PrtgProxyApi.DTOs.Groups;
using PrtgProxyApi.Services;

namespace PrtgProxyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupsController : ControllerBase
    {
        private readonly IGroupsService _groupsService;
        private readonly ILogger<GroupsController> _logger;

        public GroupsController(IGroupsService groupsService, ILogger<GroupsController> logger)
        {
            _groupsService = groupsService;
            _logger = logger;
        }

        /*[HttpGet]
        public async Task<ActionResult<List<Group>>> GetGroups()
        {
            var groups = await _groupsService.GetAllGroupsAsync();
            return Ok(groups);
        }*/

        [HttpGet("summary")]
        public ActionResult<List<GroupSummaryDto>> GetGroupSummaries()
        {
            var summaries = _groupsService.GetGroupSummaries();
            return Ok(summaries);
        }

        [HttpGet("available")]
        public ActionResult<List<GroupOptionDto>> GetAvailableGroups()
        {
            var groups = _groupsService.GetAvailableGroups();
            return Ok(groups);
        }

        [HttpGet("search-groups")]
        public async Task<IActionResult> SearchGroupsByName([FromQuery] string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return BadRequest("El nombre no puede estar vacío.");
            }

            try
            {
                List<GroupOptionDto> groups = await _groupsService.SearchGroupsByNameAsync(name);
                return Ok(groups);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al buscar grupos por nombre.");
                return StatusCode(500, "Error interno del servidor.");
            }
        }

    }
}
