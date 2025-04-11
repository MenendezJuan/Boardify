using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class CreateCardValidator : AbstractValidator<CreateCardModel>
    {
        public CreateCardValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                                 .MaximumLength(150).WithMessage(ValidatorsUtils.MaximumLengthMessage);
            RuleFor(x => x.ColumnId).GreaterThan(0).WithMessage(ValidatorsUtils.MaximumLengthMessage);
        }
    }
}