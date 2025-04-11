using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Files;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Attachments.Commands
{
    public class DeleteAttachmentCommand : IDeleteCommand<CardAttachment>
    {
        private readonly ICommandRepository<CardAttachment> _commandRepository;
        private readonly IQueryRepository<CardAttachment> _queryRepository;
        private readonly IFileService _fileService;
        private readonly IResultFactory _resultFactory;

        public DeleteAttachmentCommand(ICommandRepository<CardAttachment> commandRepository, IFileService fileService, IQueryRepository<CardAttachment> queryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository;
            _fileService = fileService;
            _queryRepository = queryRepository;
            _resultFactory = resultFactory;
        }

        public async Task<IResult<bool>> Delete(int attachmentId)
        {
            var attachment = await _queryRepository.GetByIdAsync(attachmentId);
            if (attachment == null)
            {
                return _resultFactory.Failure<bool>("Attachment not found.");
            }

            _fileService.DeleteAttachment(attachment.CardId.ToString(), attachment.FileName ?? string.Empty);

            await _commandRepository.DeleteAsync(attachment);

            return _resultFactory.Success(true);
        }
    }
}