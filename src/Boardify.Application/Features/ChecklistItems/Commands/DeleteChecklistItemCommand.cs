using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Cards;
using Boardify.Domain.Enums;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.ChecklistItems.Commands
{
    public class DeleteChecklistItemCommand : IDeleteCommand<ChecklistItem>
    {
        private readonly ICommandRepository<ChecklistItem> _commandRepository;
        private readonly IQueryRepository<ChecklistItem> _queryRepository;
        private readonly IResultFactory _resultFactory;

        public DeleteChecklistItemCommand(ICommandRepository<ChecklistItem> commandRepository, 
            IQueryRepository<ChecklistItem> queryRepository, 
            IResultFactory resultFactory)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(int id)
        {
            var item = await _queryRepository.GetByIdAsync(id);
            if (item == null)
            {
                return _resultFactory.Failure<bool>("Checklist Item not found.");
            }

            await _commandRepository.DeleteAsync(item);

            return _resultFactory.Success(true);
        }
    }
}