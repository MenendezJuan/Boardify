using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Comments.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Comments.Commands
{
    public class AddCommentCommand : ICreateCommand<AddCommentModel, AddCommentResponseModel>
    {
        private readonly ICommandRepository<CardComment> commandRepository;
        private readonly IQueryRepository<User> _userQueryRepository;
        private readonly IQueryRepository<Card> _cardQueryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public AddCommentCommand(ICommandRepository<CardComment> commandRepository, IMapper mapper, IResultFactory resultFactory, IQueryRepository<Card> cardQueryRepository, IQueryRepository<User> userQueryRepository)
        {
            this.commandRepository = commandRepository;
            _mapper = mapper;
            _resultFactory = resultFactory;
            _cardQueryRepository = cardQueryRepository;
            _userQueryRepository = userQueryRepository;
        }

        public async Task<IResult<AddCommentResponseModel>> Create(AddCommentModel model)
        {
            var card = await _cardQueryRepository.GetByIdAsync(model.CardId);
            if (card == null)
            {
                return _resultFactory.Failure<AddCommentResponseModel>("Invalid CardId.");
            }

            var user = await _userQueryRepository.GetByIdAsync(model.UserId);
            if (user == null)
            {
                return _resultFactory.Failure<AddCommentResponseModel>("Invalid UserId.");
            }

            var commentEntity = _mapper.Map<CardComment>(model);
            commentEntity.CreatedTime = DateTime.UtcNow;

            await commandRepository.InsertAsync(commentEntity);

            var responseModel = _mapper.Map<AddCommentResponseModel>(commentEntity);
            responseModel.CreatedTime = commentEntity.CreatedTime; 

            return _resultFactory.Success(responseModel);
        }
    }
}
