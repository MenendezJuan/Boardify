using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Entities;

namespace Boardify.Application.Features.Columns.Commands
{
    public class DeleteColumnCommand : IDeleteCommand<Column>
    {
        private readonly ICommandRepository<Column> _commandRepository;
        private readonly IQueryRepository<Column> _queryRepository;
        private readonly IResultFactory _resultFactory;

        public DeleteColumnCommand(ICommandRepository<Column> commandRepository, IQueryRepository<Column> queryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(int id)
        {
            var column = await _queryRepository.GetByIdAsync(id);
            if (column == null)
            {
                return _resultFactory.Failure<bool>("Column not found.");
            }

            await _commandRepository.DeleteAsync(column);
            return _resultFactory.Success(true);
        }
    }
}