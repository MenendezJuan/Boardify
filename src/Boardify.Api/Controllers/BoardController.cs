using Boardify.Api.Controllers.Base;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Boards.Models.Testing;
using Boardify.Application.Interfaces.Business;
using Microsoft.AspNetCore.Mvc;

namespace Boardify.Api.Controllers
{
    public class BoardController : TrelloControllerBase
    {
        private readonly IBoardBusinessLogic _boardBusinessLogic;

        public BoardController(IBoardBusinessLogic boardBusinessLogic)
        {
            _boardBusinessLogic = boardBusinessLogic ?? throw new ArgumentNullException(nameof(boardBusinessLogic));
        }

        /// <summary>
        /// Creates a new board.
        /// </summary>
        /// <param name="boardModel">The board model.</param>
        /// <returns>The created board.</returns>
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CreateBoardModel boardModel)
        {
            var creatorUserId = CreatorUserId;
            var result = await _boardBusinessLogic.CreateBoard(boardModel, creatorUserId);

            if (result.IsSuccess())
            {
                return Created("", result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Retrieves the columns and cards for a specific board.
        /// </summary>
        /// <param name="boardId">The board ID.</param>
        /// <returns>The columns and cards of the board.</returns>
        [HttpGet("get-columnscards/{boardId}")]
        public async Task<IActionResult> GetColumnsCards(int boardId)
        {
            var result = await _boardBusinessLogic.GetColumnsAndCards(boardId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Checks the permissions for a board.
        /// </summary>
        /// <param name="boardId">The board ID.</param>
        /// <returns>The board permissions.</returns>
        [HttpGet("permissions/{boardId}")]
        public async Task<IActionResult> CheckPermissions(int boardId)
        {
            var userId = CreatorUserId;
            var result = await _boardBusinessLogic.CheckPermissions(boardId, userId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates an existing board.
        /// </summary>
        /// <param name="id">The board ID.</param>
        /// <param name="model">The updated board model.</param>
        /// <returns>The updated board.</returns>
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBoardModel model)
        {
            var userId = CreatorUserId;
            var result = await _boardBusinessLogic.UpdateBoard(id, model, userId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Deletes a board by ID.
        /// </summary>
        /// <param name="id">The board ID.</param>
        /// <returns>A confirmation of deletion.</returns>
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = CreatorUserId;
            var result = await _boardBusinessLogic.DeleteBoard(id, userId);
            return result.IsSuccess() ? NoContent() : NotFound(new {result.Errors, result.Message});
        }

        /// <summary>
        /// Removes a member from a board.
        /// </summary>
        /// <param name="removeMemberFromBoardModel">The model containing information about the member to be removed.</param>
        /// <returns>The result of the removal operation.</returns>
        [HttpDelete("remove/members")]
        public async Task<IActionResult> RemoveMemberFromBoard([FromBody] RemoveMemberFromBoardModel removeMemberFromBoardModel)
        {
            var requesterUserId = CreatorUserId;

            var result = await _boardBusinessLogic.RemoveMemberFromBoard(removeMemberFromBoardModel, requesterUserId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Adds a member to a board.
        /// </summary>
        /// <param name="addMemberToBoardModel">The model containing information about the member to be added.</param>
        /// <returns>The result of the addition operation.</returns>
        [HttpPost("add/members")]
        public async Task<IActionResult> AddMemberToBoard([FromBody] AddMemberToBoardModel addMemberToBoardModel)
        {
            var requesterUserId = CreatorUserId;

            var result = await _boardBusinessLogic.AddMemberToBoard(addMemberToBoardModel, requesterUserId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets the members of a board.
        /// </summary>
        /// <param name="boardId">The board ID.</param>
        /// <returns>The board members.</returns>
        [HttpGet("{boardId}/members")]
        public async Task<IActionResult> GetBoardMembers(int boardId)
        {
            var result = await _boardBusinessLogic.GetBoardMembersAsync(boardId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets the boards associated with the current user's workspaces.
        /// </summary>
        /// <returns>The user's boards by workspace.</returns>
        [HttpGet("user/workspaces")]
        public async Task<IActionResult> GetUserBoardsByWorkspace()
        {
            var creatorUserId = CreatorUserId;

            var result = await _boardBusinessLogic.GetUserBoardsByWorkspace(new GetUserBoardsByWorkspaceQueryModel { UserId = creatorUserId });

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Adds a label to a board.
        /// </summary>
        /// <param name="addLabelToBoardModel">The model containing information about the member to be added.</param>
        /// <returns>The result of the addition operation.</returns>
        [HttpPost("add/labels")]
        public async Task<IActionResult> AddLabelToBoard([FromBody] AddLabelToBoardModel addLabelToBoardModel)
        {
            var requesterUserId = CreatorUserId;

            var result = await _boardBusinessLogic.AddLabelToBoard(addLabelToBoardModel, requesterUserId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Updates an existing board label.
        /// </summary>
        /// <param name="id">The board label ID.</param>
        /// <param name="model">The updated board label model.</param>
        /// <returns>The updated board.</returns>
        [HttpPut("update/label/{id}")]
        public async Task<IActionResult> UpdateLabel(int id, [FromBody] UpdateBoardLabelModel model)
        {
            var userId = CreatorUserId;

            var result = await _boardBusinessLogic.UpdateBoardLabel(id, model, userId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Removes a label from a board.
        /// </summary>
        /// <param name="removeLabelFromBoardModel">The model containing information about the member to be removed.</param>
        /// <returns>The result of the removal operation.</returns>
        [HttpDelete("remove/labels")]
        public async Task<IActionResult> RemoveLabelFromBoard([FromBody] RemoveLabelFromBoardModel removeLabelFromBoardModel)
        {
            var requesterUserId = CreatorUserId;

            var result = await _boardBusinessLogic.RemoveLabelFromBoard(removeLabelFromBoardModel, requesterUserId);

            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }

        /// <summary>
        /// Gets the labels of a board.
        /// </summary>
        /// <param name="boardId">The board ID.</param>
        /// <returns>The board labels.</returns>
        [HttpGet("{boardId}/labels")]
        public async Task<IActionResult> GetBoardLabels(int boardId)
        {
            var result = await _boardBusinessLogic.GetBoardLabelsAsync(boardId);
            if (result.IsSuccess())
            {
                return Ok(result.SuccessValue);
            }
            return BadRequest(new { result.Errors, result.Message });
        }
    }
}