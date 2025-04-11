using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class MoveCardRequestModelValidator : AbstractValidator<MoveCardRequestModel>
    {
        public MoveCardRequestModelValidator()
        {
            RuleFor(x => x.CardId)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.SourceColumnId)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.DestinationColumnId)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.DestinationCardOrder)
                .NotEmpty().WithMessage(ValidatorsUtils.InvalidCardOrderMessage);
        }
    }
}
