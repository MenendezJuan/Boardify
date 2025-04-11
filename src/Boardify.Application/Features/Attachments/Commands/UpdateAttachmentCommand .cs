﻿using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Exceptions.Custom;
using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Domain.Relationships;

namespace Boardify.Application.Features.Attachments.Commands
{
    public class UpdateAttachmentCommand : IUpdateCommand<UpdateAttachmentRequestModel, UpdateAttachmentResponseModel>
    {
        private readonly ICommandRepository<CardAttachment> _commandRepository;
        private readonly IQueryRepository<CardAttachment> _queryRepository;
        private readonly IResultFactory _resultFactory;

        public UpdateAttachmentCommand(ICommandRepository<CardAttachment> commandRepository, IQueryRepository<CardAttachment> queryRepository, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository ?? throw new ArgumentNullException(nameof(commandRepository));
            _queryRepository = queryRepository ?? throw new ArgumentNullException(nameof(queryRepository));
            _resultFactory = resultFactory ?? throw new ArgumentNullException(nameof(resultFactory));
        }

        public async Task<IResult<UpdateAttachmentResponseModel>> Update(UpdateAttachmentRequestModel model)
        {
            var attachment = await _queryRepository.GetByIdAsync(model.Id);
            if (attachment == null)
            {
                throw new NotFoundException("Invalid file or empty file.");
            }

            string originalExtension = Path.GetExtension(attachment.FileName ?? string.Empty);
            string newExtension = Path.GetExtension(model.NewFileName);

            if (string.IsNullOrEmpty(newExtension) || newExtension != originalExtension)
            {
                model.NewFileName = Path.ChangeExtension(model.NewFileName, originalExtension);
            }

            attachment.FileName = model.NewFileName;
            await _commandRepository.UpdateAsync(attachment);

            var response = new UpdateAttachmentResponseModel
            {
                FileName = attachment.FileName
            };

            return _resultFactory.Success(response);
        }
    }
}