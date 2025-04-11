using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;
using Boardify.Domain.Enums;

namespace Boardify.Application.Features.Cards.Commands
{
    public class UpdateCardPriorityCommand : IUpdateCommand<UpdateCardPriorityModel, UpdateCardPriorityResponseModel>
    {
        private readonly ICommandRepository<Card> _commandRepository;
        private readonly IQueryRepository<Card> _queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateCardPriorityCommand(ICommandRepository<Card> commandRepository, IQueryRepository<Card> queryRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateCardPriorityResponseModel>> Update(UpdateCardPriorityModel request)
        {
            var card = await _queryRepository.GetByIdAsync(request.Id);
            if (card == null)
            {
                return _resultFactory.Failure<UpdateCardPriorityResponseModel>("Card not found.");
            }

            if (!Enum.IsDefined(typeof(PriorityEnum), request.Priority))
            {
                return _resultFactory.Failure<UpdateCardPriorityResponseModel>("Invalid priority value.");
            }

            _mapper.Map(request, card);
            await _commandRepository.UpdateAsync(card);

            var response = new UpdateCardPriorityResponseModel
            {
                Priority = request.Priority
            };

            return _resultFactory.Success(response);
        }
    }
}
