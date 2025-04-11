using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify.Application.Features.Comments.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Comments.Commands
{
    public class UpdateCommentCommand : IUpdateCommand<UpdateCommentModel, UpdateCommentResponseModel>
    {
        private readonly ICommandRepository<CardComment> commandRepository;
        private readonly IQueryRepository<CardComment> queryRepository;
        private readonly IMapper _mapper;
        private readonly IResultFactory _resultFactory;

        public UpdateCommentCommand(ICommandRepository<CardComment> commandRepository, IMapper mapper, IResultFactory resultFactory, IQueryRepository<CardComment> queryRepository)
        {
            this.commandRepository = commandRepository;
            _mapper = mapper;
            _resultFactory = resultFactory;
            this.queryRepository = queryRepository;
        }

        public async Task<IResult<UpdateCommentResponseModel>> Update(UpdateCommentModel model)
        {
            var comment = await queryRepository.GetByIdAsync(model.Id);
            if (comment == null)
            {
                return _resultFactory.Failure<UpdateCommentResponseModel>("Comentario no encontrado.");
            }

            comment.Comment = model.Comment;

            await commandRepository.UpdateAsync(comment);

            var response = _mapper.Map<UpdateCommentResponseModel>(comment);

            return _resultFactory.Success(response);
        }
    }
}
