using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class AddMemberRequestModelValidator : AbstractValidator<AddMemberRequestModel>
    {
        public AddMemberRequestModelValidator()
        {
            RuleFor(x => x.CardId)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);
        }
    }
}
