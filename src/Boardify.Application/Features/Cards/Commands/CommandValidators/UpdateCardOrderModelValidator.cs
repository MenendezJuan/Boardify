using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class UpdateCardOrderModelValidator : AbstractValidator<UpdateCardOrderModel>
    {
        public UpdateCardOrderModelValidator()
        {
            RuleFor(x => x.ColumnId)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.CardOrder)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .Must(cardOrder => cardOrder != null && cardOrder.All(id => id > 0))
                .WithMessage(ValidatorsUtils.InvalidCardOrderMessage);
        }
    }
}
