﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync;
using Boardify.Application.Features.Boards.Models.Testing;

namespace Boardify.Application.Interfaces.Business
{
    public interface IBoardBusinessLogic
    {
        Task<IResult<CreateBoardResponseModel>> CreateBoard(CreateBoardModel model, int creatorUserId);

        Task<IResult<UpdateBoardResponseModel>> UpdateBoard(int boardId, UpdateBoardModel model, int userId);

        Task<IResult<bool>> DeleteBoard(int boardId, int userId);

        Task<IResult<PermissionCheckResponseModel>> CheckPermissions(int boardId, int userId);

        Task<IResult<AddMemberToBoardResponseModel>> AddMemberToBoard(AddMemberToBoardModel model, int creatorUserId);

        Task<IResult<bool>> RemoveMemberFromBoard(RemoveMemberFromBoardModel model, int requesterUserId);

        Task<IResult<IEnumerable<BoardMemberModel>>> GetBoardMembersAsync(int boardId);

        Task<IResult<GetUserBoardsByWorkspaceQueryResult>> GetUserBoardsByWorkspace(GetUserBoardsByWorkspaceQueryModel getUserBoardsByWorkspaceQuery);

        Task<IResult<AddLabelToBoardResponseModel>> AddLabelToBoard(AddLabelToBoardModel model, int creatorUserId);

        Task<IResult<UpdateBoardLabelResponseModel>> UpdateBoardLabel(int boardLabelId, UpdateBoardLabelModel model, int userId);

        Task<IResult<bool>> RemoveLabelFromBoard(RemoveLabelFromBoardModel model, int requesterUserId);

        Task<IResult<IEnumerable<BoardLabelModel>>> GetBoardLabelsAsync(int boardId);

        Task<IResult<BoardResponseDto>> GetColumnsAndCards(int boardId);
    }
}