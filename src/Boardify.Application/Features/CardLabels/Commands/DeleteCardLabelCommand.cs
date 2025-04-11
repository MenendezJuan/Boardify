using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.CardLabels;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.CardLabels.Commands
{
    public class DeleteCardLabelCommand : IDeleteCardLabelCommand
    {
        private readonly ICommandRepository<CardLabel> _commandRepository;
        private readonly ICardLabelRepository _queryRepository;
        private readonly IResultFactory _resultFactory;

        public DeleteCardLabelCommand(ICommandRepository<CardLabel> commandRepository, ICardLabelRepository queryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(int cardId, int labelId)
        {
            var cardLabel = await _queryRepository.GetByCardIdAndLabelIdAsync(cardId, labelId);
            if (cardLabel == null)
            {
                return _resultFactory.Failure<bool>("No se encontro la etiqueta");
            }

            await _commandRepository.DeleteAsync(cardLabel);
            return _resultFactory.Success(true);
        }
    }
}