using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class UpdateCardDescriptionModelValidator : AbstractValidator<UpdateCardDescriptionModel>
    {
        public UpdateCardDescriptionModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);
        }
    }
}
