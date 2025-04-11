using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Features.Boards.Models.GetBoardWithColumnsAndCardsAsync;
using Boardify.Application.Features.Boards.Models.Testing;
using Boardify.Application.Interfaces.Business;
using Boardify.Application.Interfaces.Specific.Boards;
using FluentValidation;

namespace Boardify.Application.Business
{
    public class BoardBusinessLogic : IBoardBusinessLogic
    {
        private readonly ICreateBoardCommand<CreateBoardModel, CreateBoardResponseModel> _createBoardCommand;
        private readonly IUpdateBoardCommand<UpdateBoardModel, UpdateBoardResponseModel> _updateBoardCommand;
        private readonly IDeleteBoardCommand _deleteBoardCommand;
        private readonly IPermissionCheckQuery _permissionCheckQuery;
        private readonly IAddMemberToBoardCommand _addMemberToBoardCommand;
        private readonly IRemoveMemberFromBoardCommand _removeMemberFromBoardCommand;
        private readonly IGetBoardMembersQuery _getBoardMembersQuery;
        private readonly IGetUserBoardsByWorkspaceQuery _getUserBoardsByWorkspaceQuery;
        private readonly IAddLabelToBoardCommand _addLabelToBoardCommand;
        private readonly IUpdateBoardLabelCommand<UpdateBoardLabelModel, UpdateBoardLabelResponseModel> _updateBoardLabelCommand;
        private readonly IRemoveLabelFromBoardCommand _removeLabelFromBoardCommand;
        private readonly IGetBoardLabelsQuery _getBoardLabelsQuery;
        private readonly IGetBoardWithColumnsAndCardsQuery _getBoardWithColumnsAndCardsQuery;
        private readonly IValidator<CreateBoardModel> _createBoardValidator;
        private readonly IValidator<UpdateBoardModel> _updateBoardValidator;
        private readonly IValidator<AddMemberToBoardModel> _addMemberToBoardValidator;
        private readonly IValidator<RemoveMemberFromBoardModel> _removeMemberFromBoardValidator;
        private readonly IValidator<AddLabelToBoardModel> _addLabelToBoardValidator;
        private readonly IValidator<UpdateBoardLabelModel> _updateBoardLabelValidator;
        private readonly IValidator<RemoveLabelFromBoardModel> _removeLabelFromBoardValidator;
        private readonly IResultFactory _resultFactory;

        public BoardBusinessLogic(ICreateBoardCommand<CreateBoardModel, CreateBoardResponseModel> createBoardCommand,
            IUpdateBoardCommand<UpdateBoardModel, UpdateBoardResponseModel> updateBoardCommand,
            IDeleteBoardCommand deleteBoardCommand,
            IPermissionCheckQuery permissionCheckQuery,
            IAddMemberToBoardCommand addMemberToBoardCommand,
            IRemoveMemberFromBoardCommand removeMemberFromBoardCommand,
            IGetBoardMembersQuery getBoardMembersQuery,
            IGetUserBoardsByWorkspaceQuery getUserBoardsByWorkspaceQuery,
            IAddLabelToBoardCommand addLabelToBoardCommand,
            IUpdateBoardLabelCommand<UpdateBoardLabelModel, UpdateBoardLabelResponseModel> updateBoardLabelCommand,
            IRemoveLabelFromBoardCommand removeLabelFromBoardCommand,
            IGetBoardLabelsQuery getBoardLabelsQuery,
            IValidator<CreateBoardModel> createBoardValidator,
            IValidator<UpdateBoardModel> updateBoardValidator,
            IValidator<AddMemberToBoardModel> addMemberToBoardValidator,
            IValidator<RemoveMemberFromBoardModel> removeMemberFromBoardValidator,
            IValidator<AddLabelToBoardModel> addLabelToBoardValidator,
            IValidator<UpdateBoardLabelModel> updateBoardLabelValidator,
            IValidator<RemoveLabelFromBoardModel> removeLabelFromBoardValidator,
            IGetBoardWithColumnsAndCardsQuery getBoardWithColumnsAndCardsQuery,
            IResultFactory resultFactory)

        {
            _createBoardCommand = createBoardCommand ?? throw new ArgumentNullException(nameof(createBoardCommand));
            _updateBoardCommand = updateBoardCommand ?? throw new ArgumentNullException(nameof(updateBoardCommand));
            _deleteBoardCommand = deleteBoardCommand ?? throw new ArgumentNullException(nameof(deleteBoardCommand));
            _permissionCheckQuery = permissionCheckQuery ?? throw new ArgumentNullException(nameof(permissionCheckQuery));
            _addMemberToBoardCommand = addMemberToBoardCommand ?? throw new ArgumentNullException(nameof(addMemberToBoardCommand));
            _removeMemberFromBoardCommand = removeMemberFromBoardCommand ?? throw new ArgumentNullException(nameof(removeMemberFromBoardCommand));
            _getBoardMembersQuery = getBoardMembersQuery ?? throw new ArgumentNullException(nameof(getBoardMembersQuery));
            _getUserBoardsByWorkspaceQuery = getUserBoardsByWorkspaceQuery ?? throw new ArgumentNullException(nameof(getUserBoardsByWorkspaceQuery));
            _addLabelToBoardCommand = addLabelToBoardCommand ?? throw new ArgumentNullException(nameof(addLabelToBoardCommand));
            _updateBoardLabelCommand = updateBoardLabelCommand ?? throw new ArgumentNullException(nameof(updateBoardLabelCommand));
            _removeLabelFromBoardCommand = removeLabelFromBoardCommand ?? throw new ArgumentNullException(nameof(removeLabelFromBoardCommand));
            _getBoardLabelsQuery = getBoardLabelsQuery ?? throw new ArgumentNullException(nameof(getBoardLabelsQuery));
            _createBoardValidator = createBoardValidator ?? throw new ArgumentNullException(nameof(createBoardValidator));
            _updateBoardValidator = updateBoardValidator ?? throw new ArgumentNullException(nameof(updateBoardValidator));
            _addMemberToBoardValidator = addMemberToBoardValidator ?? throw new ArgumentNullException(nameof(addMemberToBoardValidator));
            _removeMemberFromBoardValidator = removeMemberFromBoardValidator ?? throw new ArgumentNullException(nameof(removeMemberFromBoardValidator));
            _addLabelToBoardValidator = addLabelToBoardValidator ?? throw new ArgumentNullException(nameof(addMemberToBoardValidator));
            _updateBoardLabelValidator = updateBoardLabelValidator ?? throw new ArgumentNullException(nameof(updateBoardLabelValidator));
            _removeLabelFromBoardValidator = removeLabelFromBoardValidator ?? throw new ArgumentNullException(nameof(removeLabelFromBoardValidator));
            _getBoardWithColumnsAndCardsQuery = getBoardWithColumnsAndCardsQuery ?? throw new ArgumentNullException(nameof(getBoardWithColumnsAndCardsQuery));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        #region Board

        public async Task<IResult<BoardResponseDto>> GetColumnsAndCards(int boardId)
        {
            return await _getBoardWithColumnsAndCardsQuery.GetBoardColumnsAndCards(boardId);
        }

        public async Task<IResult<PermissionCheckResponseModel>> CheckPermissions(int boardId, int userId)
        {
            return await _permissionCheckQuery.CheckPermissions(boardId, userId);
        }

        public async Task<IResult<UpdateBoardResponseModel>> UpdateBoard(int boardId, UpdateBoardModel model, int userId)
        {
            var validation = await _updateBoardValidator.ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return _resultFactory.Failure<UpdateBoardResponseModel>(errorDictionary);
            }

            return await _updateBoardCommand.Update(boardId, model, userId);
        }


        public async Task<IResult<CreateBoardResponseModel>> CreateBoard(CreateBoardModel model, int creatorUserId)
        {
            var validation = await _createBoardValidator.ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errors = validation.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage);
                return _resultFactory.Failure<CreateBoardResponseModel>(errors);
            }

            return await _createBoardCommand.Create(model, creatorUserId);
        }

        public async Task<IResult<bool>> DeleteBoard(int boardId, int userId)
        {
                return await _deleteBoardCommand.Delete(boardId, userId);
        }

        #endregion Board

        #region BoardMembers

        public async Task<IResult<GetUserBoardsByWorkspaceQueryResult>> GetUserBoardsByWorkspace(GetUserBoardsByWorkspaceQueryModel getUserBoardsByWorkspaceQuery)
        {
            return await _getUserBoardsByWorkspaceQuery.ExecuteAsync(getUserBoardsByWorkspaceQuery);
        }

        public async Task<IResult<IEnumerable<BoardMemberModel>>> GetBoardMembersAsync(int boardId)
        {
            var boardMembersResult = await _getBoardMembersQuery.GetBoardMembersAsync(boardId);
            return boardMembersResult;
        }

        public async Task<IResult<AddMemberToBoardResponseModel>> AddMemberToBoard(AddMemberToBoardModel model, int creatorUserId)
        {
            var validation = await _addMemberToBoardValidator.ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return _resultFactory.Failure<AddMemberToBoardResponseModel>(errorDictionary);
            }

            var result = await _addMemberToBoardCommand.Create(model, creatorUserId);
            return result;
        }
        public async Task<IResult<bool>> RemoveMemberFromBoard(RemoveMemberFromBoardModel model, int requesterUserId)
        {
            return await _removeMemberFromBoardCommand.RemoveAsync(model, requesterUserId);
        }

        #endregion

        #region BoardLabels

        public async Task<IResult<AddLabelToBoardResponseModel>> AddLabelToBoard(AddLabelToBoardModel model, int creatorUserId)
        {
            var validation = await _addLabelToBoardValidator.ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return _resultFactory.Failure<AddLabelToBoardResponseModel>(errorDictionary);
            }
            return await _addLabelToBoardCommand.Create(model, creatorUserId);
        }

        public async Task<IResult<UpdateBoardLabelResponseModel>> UpdateBoardLabel(int boardLabelId, UpdateBoardLabelModel model, int userId)
        {
            var validation = await _updateBoardLabelValidator.ValidateAsync(model);

            if (!validation.IsValid)
            {
                var errorDictionary = validation.Errors
                    .ToDictionary(e => e.PropertyName, e => e.ErrorMessage);

                return _resultFactory.Failure<UpdateBoardLabelResponseModel>(errorDictionary);
            }

            return await _updateBoardLabelCommand.Update(boardLabelId, model, userId);
        }

        public async Task<IResult<bool>> RemoveLabelFromBoard(RemoveLabelFromBoardModel model, int requesterUserId)
        {
            return await _removeLabelFromBoardCommand.RemoveAsync(model, requesterUserId);
        }

        public async Task<IResult<IEnumerable<BoardLabelModel>>> GetBoardLabelsAsync(int boardId)
        {
            var boardLabels = await _getBoardLabelsQuery.GetBoardLabelsAsync(boardId);
            return boardLabels;
        }

        #endregion BoardLabels
    }
}