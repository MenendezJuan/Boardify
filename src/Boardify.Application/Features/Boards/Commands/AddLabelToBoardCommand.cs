using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Boards;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Boards.Commands
{
    public class AddLabelToBoardCommand : IAddLabelToBoardCommand
    {
        private readonly IMapper _mapper;
        private readonly ICommandRepository<BoardLabel> _boardLabelRepository;
        private readonly IQueryRepository<Board> _boardQueryRepository;
        private readonly IResultFactory _resultFactory;

        public AddLabelToBoardCommand(
            IMapper mapper,
            ICommandRepository<BoardLabel> boardLabelRepository,
            IQueryRepository<Board> boardQueryRepository,
            IResultFactory resultFactory)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _boardLabelRepository = boardLabelRepository ?? throw new ArgumentNullException(nameof(boardLabelRepository));
            _boardQueryRepository = boardQueryRepository ?? throw new ArgumentNullException(nameof(boardQueryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<AddLabelToBoardResponseModel>> Create(AddLabelToBoardModel model, int creatorUserId)
        {
            var board = await _boardQueryRepository.GetByIdAsync(model.BoardId);

            if (board == null)
            {
                return _resultFactory.Failure<AddLabelToBoardResponseModel>("Board not found");
            }

            var boardLabel = _mapper.Map<BoardLabel>(model);

            await _boardLabelRepository.InsertAsync(boardLabel);

            var response = _mapper.Map<AddLabelToBoardResponseModel>(boardLabel);

            return _resultFactory.Success(response);
        }
    }
}