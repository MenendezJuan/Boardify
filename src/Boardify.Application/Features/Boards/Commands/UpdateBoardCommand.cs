using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Boards.Commands
{
    public class UpdateBoardCommand : IUpdateBoardCommand<UpdateBoardModel, UpdateBoardResponseModel>
    {
        private readonly ICommandRepository<Board> _boardRepository;
        private readonly IQueryRepository<Board> _boardQueryRepository;
        private readonly IUserWorkspaceRepository _userWorkspaceRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateBoardCommand(
            ICommandRepository<Board> boardRepository,
            IUserWorkspaceRepository userWorkspaceRepository,
            IMapper mapper,
            IQueryRepository<Board> boardQueryRepository,
            IResultFactory resultFactory)
        {
            _boardRepository = boardRepository ?? throw new ArgumentNullException(nameof(boardRepository));
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardQueryRepository = boardQueryRepository ?? throw new ArgumentNullException(nameof(boardQueryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateBoardResponseModel>> Update(int boardId, UpdateBoardModel model, int userId)
        {
            var board = await _boardQueryRepository.GetByIdAsync(boardId);
            if (board == null)
            {
                return _resultFactory.Failure<UpdateBoardResponseModel>("Board not found");
            }

            var isOwner = await _userWorkspaceRepository.IsOwnerOfWorkspace(userId, board.WorkspaceId);
            if (!isOwner)
            {
                return _resultFactory.Failure<UpdateBoardResponseModel>("No tienes permisos para actualizar este board.");
            }

            _mapper.Map(model, board);
            await _boardRepository.UpdateAsync(board);

            var responseModel = _mapper.Map<UpdateBoardResponseModel>(board);
            return _resultFactory.Success(responseModel);
        }
    }
}