using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Specific.Cards;
using System.Transactions;

namespace Boardify.Application.Features.Cards.Commands
{
    public class UpdateCardOrderCommand : IUpdateCardOrderCommand
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateCardOrderCommand(ICardRepository cardRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _cardRepository = cardRepository ?? throw new ArgumentNullException(nameof(cardRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<List<UpdateCardOrderResponseModel>>> UpdateCardOrder(UpdateCardOrderModel model)
        {
            if (model == null || model.CardOrder == null || !model.CardOrder.Any())
            {
                return _resultFactory.Failure<List<UpdateCardOrderResponseModel>>("Model is invalid or CardOrder is empty.");
            }

            var cards = await _cardRepository.GetCardsByColumnIdAsync(model.ColumnId);
            if (cards == null || !cards.Any())
            {
                return _resultFactory.Failure<List<UpdateCardOrderResponseModel>>("No cards found for the specified column.");
            }

            var cardDictionary = cards.ToDictionary(card => card.Id);
            var orderedCards = model.CardOrder.Select(cardId => cardDictionary[cardId]).ToList();

            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _cardRepository.UpdateCardsAsync(orderedCards);
                transaction.Complete();
            }

            var updateCardOrderResponseModels = _mapper.Map<List<UpdateCardOrderResponseModel>>(orderedCards);
            return _resultFactory.Success(updateCardOrderResponseModels);
        }
    }
}