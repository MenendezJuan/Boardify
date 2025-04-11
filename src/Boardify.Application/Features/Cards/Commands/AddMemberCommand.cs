using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Cards.Commands
{
    public class AddMemberCommand : ICreateCommand<AddMemberRequestModel, CardMemberResponseModel>
    {
        private readonly ICommandRepository<CardMember> _commandRepository;
        private readonly IQueryRepository<Card> _cardQueryRepository;
        private readonly IQueryRepository<User> _userQueryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public AddMemberCommand(
            ICommandRepository<CardMember> commandRepository,
            IQueryRepository<Card> cardQueryRepository,
            IQueryRepository<User> userQueryRepository,
            IMapper mapper,
            IResultFactory resultFactory)
        {
            _commandRepository = commandRepository;
            _cardQueryRepository = cardQueryRepository;
            _userQueryRepository = userQueryRepository;
            _mapper = mapper;
            _resultFactory = resultFactory;
        }

        public async Task<IResult<CardMemberResponseModel>> Create(AddMemberRequestModel model)
        {
            var card = await _cardQueryRepository.GetByIdAsync(model.CardId);
            var user = await _userQueryRepository.GetByIdAsync(model.UserId);

            if (card == null)
            {
                return _resultFactory.Failure<CardMemberResponseModel>("Card not found.");
            }

            if (user == null)
            {
                return _resultFactory.Failure<CardMemberResponseModel>("User not found.");
            }

            var cardMember = new CardMember { CardId = model.CardId, UserId = model.UserId };
            await _commandRepository.InsertAsync(cardMember);

            var response = _mapper.Map<CardMemberResponseModel>(cardMember);
            return _resultFactory.Success(response);
        }
    }
}