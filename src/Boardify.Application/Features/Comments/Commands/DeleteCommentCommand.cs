using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Comments.Commands
{
    public class DeleteCommentCommand : IDeleteCommand<CardComment>
    {
        private readonly ICommandRepository<CardComment> _commandRepository;
        private readonly IQueryRepository<CardComment> _queryRepository;
        private readonly IResultFactory _resultFactory;
        public DeleteCommentCommand(ICommandRepository<CardComment> commandRepository, IQueryRepository<CardComment> queryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository;
            _queryRepository = queryRepository;
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(int id)
        {
            var comment = await _queryRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return _resultFactory.Failure<bool>("No se encontro el comentario");
            }

            await _commandRepository.DeleteAsync(comment);
            return _resultFactory.Success(true);
        }
    }
}
