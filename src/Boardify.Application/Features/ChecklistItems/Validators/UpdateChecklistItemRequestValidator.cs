using Boardify.Application.Features.ChecklistItems.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.ChecklistItems.Validators
{
    public class UpdateChecklistItemRequestValidator : AbstractValidator<UpdateChecklistItemRequest>
    {
        public UpdateChecklistItemRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MaximumLength(100).WithMessage(ValidatorsUtils.MaximumLengthMessage);

            RuleFor(x => x.IsChecked)
                .NotNull().WithMessage(ValidatorsUtils.RequiredField);
        }
    }
}
