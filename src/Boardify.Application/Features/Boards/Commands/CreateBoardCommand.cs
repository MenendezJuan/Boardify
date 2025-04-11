using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Application.Interfaces.Specific.Workspaces;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Boards.Commands
{
    public class CreateBoardCommand : ICreateBoardCommand<CreateBoardModel, CreateBoardResponseModel>
    {
        private readonly ICommandRepository<Board> _boardRepository;
        private readonly ICommandRepository<BoardMember> _boardMemberRepository;
        private readonly IUserWorkspaceRepository _userWorkspaceRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public CreateBoardCommand(
            ICommandRepository<Board> boardRepository,
            IUserWorkspaceRepository userWorkspaceRepository,
            IMapper mapper,
            ICommandRepository<BoardMember> boardMemberRepository,
            IResultFactory resultFactory)
        {
            _boardRepository = boardRepository ?? throw new ArgumentNullException(nameof(boardRepository));
            _userWorkspaceRepository = userWorkspaceRepository ?? throw new ArgumentNullException(nameof(userWorkspaceRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardMemberRepository = boardMemberRepository ?? throw new ArgumentNullException(nameof(boardMemberRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<CreateBoardResponseModel>> Create(CreateBoardModel model, int creatorUserId)
        {
            var isOwner = await _userWorkspaceRepository.IsOwnerOfWorkspace(creatorUserId, model.WorkspaceId);
            if (!isOwner)
            {
                return _resultFactory.Failure<CreateBoardResponseModel>("No tienes permisos para crear un tablero en este espacio de trabajo.");
            }

            var board = _mapper.Map<Board>(model);
            await _boardRepository.InsertAsync(board);

            var boardMember = new BoardMember
            {
                BoardId = board.Id,
                UserId = creatorUserId
            };
            await _boardMemberRepository.InsertAsync(boardMember);

            var responseModel = _mapper.Map<CreateBoardResponseModel>(board);
            return _resultFactory.Success(responseModel);
        }
    }
}