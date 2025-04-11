using AutoMapper;
﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Cards.Commands
{
    public class UpdateCardDescriptionCommand : IUpdateCommand<UpdateCardDescriptionModel, UpdateCardDescriptionResponseModel>
    {
        private readonly ICommandRepository<Card> _commandRepository;
        private readonly IQueryRepository<Card> _queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateCardDescriptionCommand(ICommandRepository<Card> commandRepository, IQueryRepository<Card> queryRepository, IMapper mapper, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<UpdateCardDescriptionResponseModel>> Update(UpdateCardDescriptionModel request)
        {
            var card = await _queryRepository.GetByIdAsync(request.Id);
            if (card == null)
            {
                return _resultFactory.Failure<UpdateCardDescriptionResponseModel>(new Dictionary<string, string> { { "Id", "Card not found." } });
            }

            _mapper.Map(request, card);

            await _commandRepository.UpdateAsync(card);

            var response = new UpdateCardDescriptionResponseModel
            {
                Description = request.Description ?? string.Empty
            };
            return _resultFactory.Success(response);
        }
    }
}
