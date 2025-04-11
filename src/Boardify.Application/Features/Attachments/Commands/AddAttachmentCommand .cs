using AutoMapper;
using Boardify_Essentials.Extensions.System.ResultPattern;
using Boardify_Essentials.Extensions.System.ResultPattern.Factories;
using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Interfaces.Generics;
using Boardify.Application.Interfaces.Specific.Attachment;
using Boardify.Application.Interfaces.Specific.Files;
using Boardify.Domain.Relationships;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Boardify.Application.Features.Attachments.Commands
{
    public class AddAttachmentCommand : IAddAttachmentCommand
    {
        private readonly ICommandRepository<CardAttachment> _commandRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
        private readonly IValidator<CardAttachment> _validator;
        private readonly IResultFactory _resultFactory;

        public AddAttachmentCommand(ICommandRepository<CardAttachment> commandRepository, IMapper mapper, IFileService fileService, IValidator<CardAttachment> validator, IResultFactory resultFactory)
        {
            _commandRepository = commandRepository;
            _mapper = mapper;
            _fileService = fileService;
            _validator = validator;
            _resultFactory = resultFactory;
        }

        public async Task<IResult<AttachmentResponseModel>> AddAttachmentAsync(IFormFile file, int cardId)
        {
            if (file == null || file.Length == 0)
            {
                return _resultFactory.Failure<AttachmentResponseModel>(new Dictionary<string, string> { { "File", "Invalid file or empty file." } });
            }

            var savedFilePath = await _fileService.SaveAttachmentAsync(file, cardId.ToString());

            var attachment = new CardAttachment
            {
                FileName = file.FileName,
                FilePath = savedFilePath,
                CardId = cardId,
                FileSize = file.Length,
                ContentType = file.ContentType,
                CreatedDate = DateTime.UtcNow
            };

            var validationResult = await _validator.ValidateAsync(attachment);
            if (!validationResult.IsValid)
            {
                return _resultFactory.Failure<AttachmentResponseModel>(
                    validationResult.Errors.ToDictionary(e => e.PropertyName, e => e.ErrorMessage));
            }

            await _commandRepository.InsertAsync(attachment);

            var responseModel = _mapper.Map<AttachmentResponseModel>(attachment);
            return _resultFactory.Success(responseModel);
        }
    }
}