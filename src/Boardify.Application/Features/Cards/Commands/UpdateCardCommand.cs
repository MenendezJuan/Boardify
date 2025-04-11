using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Cards.Commands
{
    public class UpdateCardCommand : IUpdateCommand<UpdateCardModel, UpdateCardResponseModel>
    {
        private readonly ICommandRepository<Card> _commandRepository;
        private readonly IQueryRepository<Card> _queryRepository;
        private readonly IQueryRepository<Column> _columnQueryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateCardCommand(ICommandRepository<Card> commandRepository, IQueryRepository<Card> queryRepository, IMapper mapper, IQueryRepository<Column> columnQueryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _columnQueryRepository = columnQueryRepository ?? throw new ArgumentNullException(nameof(columnQueryRepository));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<UpdateCardResponseModel>> Update(UpdateCardModel request)
        {
            var card = await _queryRepository.GetByIdAsync(request.Id);
            if (card == null)
            {
                return _resultFactory.Failure<UpdateCardResponseModel>(new Dictionary<string, string> { { "Id", "Card not found." } });
            }

            var column = await _columnQueryRepository.GetByIdAsync(request.ColumnId);
            if (column == null)
            {
                return _resultFactory.Failure<UpdateCardResponseModel>(new Dictionary<string, string> { { "ColumnId", "Column not found." } });
            }

            _mapper.Map(request, card);

            await _commandRepository.UpdateAsync(card);

            var response = _mapper.Map<UpdateCardResponseModel>(card);

            return _resultFactory.Success(response);
        }
    }
}