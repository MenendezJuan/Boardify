using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Specific.Cards;

namespace Boardify.Application.Features.Cards.Queries
{
    public class GetCardDetailsQuery : IGetCardDetailsQuery
    {
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetCardDetailsQuery(ICardRepository cardRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _cardRepository = cardRepository;
            _mapper = mapper;
            _resultFactory = resultFactory;
        }

        public async Task<IResult<BoardCardDetailResponse>> GetBoardCardAsync(int cardId)
        {
            var card = await _cardRepository.GetCardWithDetailsAsync(cardId);

            if (card == null)
            {
                return _resultFactory.Failure<BoardCardDetailResponse>("Card not found");
            }

            return _resultFactory.Success(_mapper.Map<BoardCardDetailResponse>(card));
        }
    }
}