using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Cards.Commands
{
    public class UpdateCardDatesCommand : IUpdateCommand<UpdateCardDatesModel, UpdateCardDatesResponseModel>
    {
        private readonly ICommandRepository<Card> _commandRepository;
        private readonly IQueryRepository<Card> _queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateCardDatesCommand(ICommandRepository<Card> commandRepository, IQueryRepository<Card> queryRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateCardDatesResponseModel>> Update(UpdateCardDatesModel request)
        {
            var card = await _queryRepository.GetByIdAsync(request.Id);
            if (card == null)
            {
                return _resultFactory.Failure<UpdateCardDatesResponseModel>("Card not found.");
            }

            _mapper.Map(request, card);
            await _commandRepository.UpdateAsync(card);

            var response = new UpdateCardDatesResponseModel
            {
                StartDate = card.StartDate,
                DueDate = card.DueDate
            };

            return _resultFactory.Success(response);
        }
    }
}
