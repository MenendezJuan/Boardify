using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Users.Queries.QueryValidators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(x => x.Email)
                 .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                 .EmailAddress().WithMessage(ValidatorsUtils.EmailMessage)
                 .MaximumLength(120).WithMessage(ValidatorsUtils.MaximumLengthMessage);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MinimumLength(8).WithMessage(ValidatorsUtils.MinimumLengthMessage);
        }
    }
}