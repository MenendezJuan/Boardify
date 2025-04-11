using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.CardLabels.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.CardLabels.Commands
{
    public class CreateCardLabelCommand : ICreateCommand<CreateCardLabelModel, CardLabelModel>
    {
        private readonly ICommandRepository<CardLabel> _commandRepository;
        private readonly IQueryRepository<Card> _cardQueryRepository;
        private readonly IQueryRepository<BoardLabel> _boardLabelQueryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public CreateCardLabelCommand(
            ICommandRepository<CardLabel> commandRepository,
            IMapper mapper,
            IQueryRepository<Card> cardQueryRepository,
            IQueryRepository<BoardLabel> boardLabelQueryRepository,
            IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cardQueryRepository = cardQueryRepository ?? throw new ArgumentNullException(nameof(cardQueryRepository));
            _boardLabelQueryRepository = boardLabelQueryRepository ?? throw new ArgumentNullException(nameof(boardLabelQueryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<CardLabelModel>> Create(CreateCardLabelModel model)
        {
            var cardExists = await _cardQueryRepository.GetByIdAsync(model.CardId);
            if (cardExists == null)
            {
                return _resultFactory.Failure<CardLabelModel>("The specified card does not exist.");
            }

            var boardLabelExists = await _boardLabelQueryRepository.GetByIdAsync(model.LabelId);
            if (boardLabelExists == null)
            {
                return _resultFactory.Failure<CardLabelModel>("The specified board label does not exist.");
            }

            var cardLabel = _mapper.Map<CardLabel>(model);
            await _commandRepository.InsertAsync(cardLabel);
            var cardLabelModel = _mapper.Map<CardLabelModel>(cardLabel);

            return _resultFactory.Success(cardLabelModel);
        }
    }
}