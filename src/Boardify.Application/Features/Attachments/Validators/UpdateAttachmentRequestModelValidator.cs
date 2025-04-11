using Boardify.Application.Features.Attachments.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Attachments.Validators
{
    public class UpdateAttachmentRequestModelValidator : AbstractValidator<UpdateAttachmentRequestModel>
    {
        public UpdateAttachmentRequestModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.NewFileName)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .Matches(@"^[\w\s\-.]+(\.[A-Za-z]{2,})?$").WithMessage("El campo {PropertyName} debe tener un nombre de archivo válido.");
        }
    }
}
