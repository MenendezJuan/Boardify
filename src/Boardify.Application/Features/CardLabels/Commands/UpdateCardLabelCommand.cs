using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.CardLabels.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.CardLabels;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.CardLabels.Commands
{
    public class UpdateCardLabelCommand : IUpdateCommand<UpdateCardLabelModel, CardLabelModel>
    {
        private readonly ICommandRepository<CardLabel> _commandRepository;
        private readonly IMapper _mapper;
        private readonly ICardLabelRepository _cardLabelQueryRepository;
        private readonly IResultFactory _resultFactory;

        public UpdateCardLabelCommand(
            ICommandRepository<CardLabel> commandRepository,
            IMapper mapper,
            ICardLabelRepository cardLabelQueryRepository,
            IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cardLabelQueryRepository = cardLabelQueryRepository ?? throw new ArgumentNullException(nameof(cardLabelQueryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<CardLabelModel>> Update(UpdateCardLabelModel model)
        {
            var existingCardLabel = await _cardLabelQueryRepository.GetByCardIdAndLabelIdAsync(model.CardId, model.LabelId);
            if (existingCardLabel == null)
            {
                return _resultFactory.Failure<CardLabelModel>("CardLabel not found.");
            }

            await _commandRepository.DeleteAsync(existingCardLabel);

            var newCardLabel = new CardLabel
            {
                CardId = model.CardId,
                LabelId = model.NewLabelId
            };

            await _commandRepository.InsertAsync(newCardLabel);

            var reloadedCardLabel = await _cardLabelQueryRepository.GetByIdWithIncludes(model.CardId, model.NewLabelId);
            var cardLabelModel = _mapper.Map<CardLabelModel>(reloadedCardLabel);

            return _resultFactory.Success(cardLabelModel);
        }
    }
}