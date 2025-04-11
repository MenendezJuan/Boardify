using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Cards.Queries
{
    public class GetAllCardsQuery : IGetAllQuery<CardResponseModel>
    {
        private readonly IQueryRepository<Card> _queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public GetAllCardsQuery(
            IQueryRepository<Card> queryRepository,
            IMapper mapper,
            IResultFactory resultFactory)
        {
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<IEnumerable<CardResponseModel>>> GetAllAsync()
        {
            var cards = await _queryRepository.GetAllAsync();
            var cardModels = _mapper.Map<IEnumerable<CardResponseModel>>(cards);
            return _resultFactory.Success(cardModels);
        }
    }
}