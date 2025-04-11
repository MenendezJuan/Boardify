using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Exceptions.Custom;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Boards.Commands
{
    public class DeleteBoardCommand : IDeleteBoardCommand
    {
        private readonly ICommandRepository<Board> _boardRepository;
        private readonly IQueryRepository<Board> _boardQueryRepository;
        private readonly IUserWorkspaceRepository _userWorkspaceRepository;
        private readonly IResultFactory _resultFactory;

        public DeleteBoardCommand(IUserWorkspaceRepository userWorkspaceRepository,
            ICommandRepository<Board> boardRepository,
            IQueryRepository<Board> boardQueryRepository,
            IResultFactory resultFactory)
        {
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _boardRepository = boardRepository ?? throw new ArgumentNullException(nameof(boardRepository));
            _boardQueryRepository = boardQueryRepository ?? throw new ArgumentNullException(nameof(boardQueryRepository));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(int boardId, int userId)
        {
            var board = await _boardQueryRepository.GetByIdAsync(boardId);
            if (board == null)
            {
                return _resultFactory.Failure<bool>("No se encontro el tablero");
            }

            var isOwner = await _userWorkspaceRepository.IsOwnerOfWorkspace(userId, board.WorkspaceId);
            if (!isOwner)
            {
                return _resultFactory.Failure<bool>("No tienes permisos para eliminar este tablero");
            }

            await _boardRepository.DeleteAsync(board);
            return _resultFactory.Success(true);
        }
    }
}