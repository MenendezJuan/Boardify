using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Cards.Commands
{
    public class MoveCardCommand : IMoveCardCommand
    {
        private readonly ICommandRepository<Card> _commandRepository;
        private readonly IQueryRepository<Card> _queryRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public MoveCardCommand(ICommandRepository<Card> commandRepository, IMapper mapper, IQueryRepository<Card> queryRepository, ICardRepository cardRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<CardResponseModel>> MoveCardAsync(MoveCardRequestModel model)
        {
            var card = await _queryRepository.GetByIdAsync(model.CardId);

            if (card == null)
            {
                return _resultFactory.Failure<CardResponseModel>("Card not found.");
            }

            var sourceColumnCards = await _cardRepository.GetCardsByColumnIdAsync(model.SourceColumnId);
            var destinationColumnCards = await _cardRepository.GetCardsByColumnIdAsync(model.DestinationColumnId);

            sourceColumnCards.Remove(card);

            for (int i = 0; i < sourceColumnCards.Count; i++)
            {
                await _commandRepository.UpdateAsync(sourceColumnCards[i]);
            }

            destinationColumnCards.Insert(model.DestinationCardOrder.IndexOf(model.CardId), card);

            for (int i = 0; i < destinationColumnCards.Count; i++)
            {
                await _commandRepository.UpdateAsync(destinationColumnCards[i]);
            }

            card.ColumnId = model.DestinationColumnId;
            await _commandRepository.UpdateAsync(card);

            var response = _mapper.Map<CardResponseModel>(card);
            return _resultFactory.Success(response);
        }
    }
}