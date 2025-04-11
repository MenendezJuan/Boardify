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
    public class RemoveLabelFromBoardCommand : IRemoveLabelFromBoardCommand
    {
        private readonly ICommandRepository<BoardLabel> _boardLabelRepository;
        private readonly IQueryRepository<Board> _boardQueryRepository;
        private readonly IBoardLabelRepository _labelRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public RemoveLabelFromBoardCommand(
            ICommandRepository<BoardLabel> boardLabelRepository,
            IQueryRepository<Board> boardQueryRepository,
            IBoardLabelRepository labelRepository,
            IMapper mapper,
            IResultFactory resultFactory)
        {
            _boardLabelRepository = boardLabelRepository ?? throw new ArgumentNullException(nameof(boardLabelRepository));
            _boardQueryRepository = boardQueryRepository ?? throw new ArgumentNullException(nameof(boardQueryRepository));
            _labelRepository = labelRepository ?? throw new ArgumentNullException(nameof(labelRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<bool>> RemoveAsync(RemoveLabelFromBoardModel model, int requesterUserId)
        {
            var board = await _boardQueryRepository.GetByIdAsync(model.BoardId);

            if (board == null)
            {
                return _resultFactory.Failure<bool>("Board not found");
            }

            var isMember = await _labelRepository.IsMemberOfBoardAsync(model.BoardLabelId, model.BoardId);
            if (!isMember)
            {
                return _resultFactory.Failure<bool>("Label is not in the board.");
            }

            var boardLabel = _mapper.Map<BoardLabel>(model);

            await _boardLabelRepository.DeleteAsync(boardLabel);

            return _resultFactory.Success(true);
        }
    }
}