using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Utils;
using Boardify.Domain.Enums;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class UpdateCardPriorityModelValidator : AbstractValidator<UpdateCardPriorityModel>
    {
        public UpdateCardPriorityModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.Priority)
                .Must(priorityValue => Enum.IsDefined(typeof(PriorityEnum), priorityValue))
                .WithMessage(ValidatorsUtils.InvalidPriorityMessage);
        }
    }
}
