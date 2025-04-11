using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.ChecklistItems.Validators
{
    public class CreateChecklistItemRequestValidator : AbstractValidator<CreateChecklistItemRequest>
    {
        public CreateChecklistItemRequestValidator()
        {
            RuleFor(x => x.CardId)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MaximumLength(100).WithMessage(ValidatorsUtils.MaximumLengthMessage);
        }
    }
}
