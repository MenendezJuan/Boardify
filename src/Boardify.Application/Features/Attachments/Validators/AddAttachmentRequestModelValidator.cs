using Boardify.Application.Utils;
using Boardify.Domain.Relationships;
using FluentValidation;

namespace Boardify.Application.Features.Attachments.Validators
{
    public class AddAttachmentRequestModelValidator : AbstractValidator<CardAttachment>
    {
        public AddAttachmentRequestModelValidator()
        {
            RuleFor(x => x.FileName)
                  .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                  .Matches(@"^[\w\s\-.]+(\.[A-Za-z]{2,})?$").WithMessage("El nombre del archivo debe tener una extensión válida.");

            RuleFor(x => x.CardId)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);
        }
    }
}
