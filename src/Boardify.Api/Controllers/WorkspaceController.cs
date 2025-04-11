using Boardify.Api.Controllers.Base;
using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Boardify.Api.Controllers
{
    public class WorkspaceController : TrelloControllerBase
    {
        private readonly IWorkspaceBusinessLogic _workspaceBusinessLogic;

        public WorkspaceController(IWorkspaceBusinessLogic workspaceBusinessLogic)
        {
            _workspaceBusinessLogic = workspaceBusinessLogic ?? throw new ArgumentNullException(nameof(workspaceBusinessLogic));
        }

        /// <summary>
        /// Creates a new workspace.
        /// </summary>
        /// <param name="workspaceModel">The Workspace model.</param>
        /// <returns>The created workspace.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateWorkspaceModel workspaceModel)
        {
            var creatorUserId = CreatorUserId;
            var result = await _workspaceBusinessLogic.CreateWorkspace(workspaceModel, creatorUserId);

            if (result.IsSuccess())
            {
                return CreatedAtAction(nameof(GetWorkspaceMembers), new { workspaceId = result.SuccessValue.Id }, result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates an existing workspace.
        /// </summary>
        /// <param name="id">The workspace ID.</param>
        /// <param name="workspaceModel">The workspace model.</param>
        /// <returns>The updated workspace.</returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateWorkspaceModel workspaceModel)
        {
            var creatorUserId = CreatorUserId;
            var result = await _workspaceBusinessLogic.UpdateWorkspace(id, workspaceModel, creatorUserId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Adds an existing user to a workspace.
        /// </summary>
        [HttpPost("add-member")]
        public async Task<IActionResult> AddMemberToWorkspace([FromBody] AddMemberToWorkspaceModel addMemberToWorkspaceModel)
        {
            var creatorUserId = CreatorUserId;
            var result = await _workspaceBusinessLogic.AddMemberToWorkspace(addMemberToWorkspaceModel, creatorUserId);

            if (result.IsSuccess())
            {
                return Created("", result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Removes a member from a workspace.
        /// </summary>
        /// <param name="removeMemberFromWorkspace">The model containing information about the member to be removed from the workspace.</param>
        /// <returns>The result of the removal operation.</returns>
        [HttpDelete("remove/members")]
        public async Task<IActionResult> RemoveMemberFromWorkspace([FromBody] RemoveMemberFromWorkspaceModel removeMemberFromWorkspace)
        {
            var result = await _workspaceBusinessLogic.RemoveMemberFromWorkspace(removeMemberFromWorkspace);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }

            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets the members of a workspace.
        /// </summary>
        /// <param name="workspaceId">The workspace ID.</param>
        /// <returns>The workspace members.</returns>
        [HttpGet("{workspaceId}/members")]
        public async Task<IActionResult> GetWorkspaceMembers(int workspaceId)
        {
            var result = await _workspaceBusinessLogic.GetWorkspaceMembersAsync(workspaceId);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets the workspaces owned by the current user.
        /// </summary>
        /// <returns>The user's owned workspaces.</returns>
        [HttpGet("owned")]
        public async Task<IActionResult> GetOwnedWorkspaces()
        {
            var creatorUserId = CreatorUserId;
            var result = await _workspaceBusinessLogic.GetOwnedWorkspaces(creatorUserId);
            return result.IsSuccess() ? Ok(result.SuccessValue) : BadRequest(new {result.Errors,result.Message});
        }
    }
}