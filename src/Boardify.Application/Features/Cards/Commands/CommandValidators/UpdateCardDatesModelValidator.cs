using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class UpdateCardDatesModelValidator : AbstractValidator<UpdateCardDatesModel>
    {
        public UpdateCardDatesModelValidator()
        {
            RuleFor(x => x.Id)
               .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.StartDate)
                .Must(ValidatorsUtils.BeAValidDate)
                .WithMessage(ValidatorsUtils.InvalidDateMessage)
                .LessThanOrEqualTo(x => x.DueDate)
                .When(x => x.DueDate.HasValue)
                .WithMessage(ValidatorsUtils.DateRangeMessage);

            RuleFor(x => x.DueDate)
                 .Must(ValidatorsUtils.BeAValidDate)
                 .WithMessage(ValidatorsUtils.InvalidDateMessage)
                 .When(x => x.DueDate.HasValue)
                 .LessThanOrEqualTo(DateTime.MaxValue)
                 .WithMessage(ValidatorsUtils.DateRangeMessage);
        }
    }
}
