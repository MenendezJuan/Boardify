﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Boards.Queries
{
    public class PermissionCheckQuery : IPermissionCheckQuery
    {
        private readonly IBoardRepository _boardRepository;
        private readonly IResultFactory _resultFactory;

        public PermissionCheckQuery(IBoardRepository boardQueryRepository, IResultFactory resultFactory)
        {
            _boardRepository = boardQueryRepository ?? throw new ArgumentNullException(nameof(boardQueryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<PermissionCheckResponseModel>> CheckPermissions(int boardId, int userId)
        {
            var board = await _boardRepository.GetBoardWithMembersAndWorkspacesAsync(boardId);

            if (board == null)
            {
                return _resultFactory.Failure<PermissionCheckResponseModel>("Board not found");
            }

            var isMember = board.BoardMembers.Any(bm => bm.UserId == userId) ||
                           board.Workspace.UserWorkspaces.Any(uw => uw.UserId == userId);

            var canView = (board.Visibility == VisibilityEnum.Public) ||
                          (board.Visibility == VisibilityEnum.Private && board.BoardMembers.Any(bm => bm.UserId == userId)) ||
                          (board.Visibility == VisibilityEnum.Workspace && board.Workspace.UserWorkspaces.Any(uw => uw.UserId == userId));

            var canEdit = isMember;

            var responseModel = new PermissionCheckResponseModel
            {
                CanView = canView,
                CanEdit = canEdit,
                BoardVisibility = board.Visibility
            };

            return _resultFactory.Success(responseModel);
        }
    }
}