using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Exceptions.Custom;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Cards.Commands
{
    public class DeleteCardCommand : IDeleteCommand<Card>
    {
        private readonly ICommandRepository<Card> _commandRepository;
        private readonly IQueryRepository<Card> _queryRepository;
        private readonly IResultFactory _resultFactory;

        public DeleteCardCommand(ICommandRepository<Card> commandRepository, IQueryRepository<Card> queryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(int id)
        {
            var card = await _queryRepository.GetByIdAsync(id);
            if (card == null)
            {
                return _resultFactory.Failure<bool>("No se encontro la tarjeta");
            }

            await _commandRepository.DeleteAsync(card);
            return _resultFactory.Success(true);
        }
    }
}