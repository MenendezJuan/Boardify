﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Cards.Commands
{
    public class UpdateCardReporterCommand : IUpdateCommand<UpdateCardReporterModel, UpdateCardReporterResponseModel>
    {
        private readonly ICommandRepository<Card> _commandRepository;
        private readonly IQueryRepository<Card> _queryRepository;
        private readonly IQueryRepository<User> _userQueryRepository;
        private readonly IResultFactory _resultFactory;

        public UpdateCardReporterCommand(ICommandRepository<Card> commandRepository, IQueryRepository<Card> queryRepository, IQueryRepository<User> userQueryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _userQueryRepository = userQueryRepository ?? throw new ArgumentNullException(nameof(userQueryRepository));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<UpdateCardReporterResponseModel>> Update(UpdateCardReporterModel request)
        {
            var card = await _queryRepository.GetByIdAsync(request.Id);
            if (card == null)
            {
                return _resultFactory.Failure<UpdateCardReporterResponseModel>(new Dictionary<string, string> { { "Id", "Card not found." } });
            }

            if (request.ReporterId.HasValue)
            {
                var user = await _userQueryRepository.GetByIdAsync(request.ReporterId.Value);
                if (user == null)
                {
                    return _resultFactory.Failure<UpdateCardReporterResponseModel>(new Dictionary<string, string> { { "ReporterId", "User not found." } });
                }
                card.ReporterId = request.ReporterId.Value;
            }
            else
            {
                card.ReporterId = null;
            }

            await _commandRepository.UpdateAsync(card);

            var response = new UpdateCardReporterResponseModel { ReporterId = request.ReporterId };
            return _resultFactory.Success(response);
        }
    }
}
