using Boardify.Application.Features.Users.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Users.Commands.CommandValidators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserModel>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                  .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                  .MaximumLength(30).WithMessage(ValidatorsUtils.MaximumLengthMessage)
                  .Matches("^[a-zA-Z]+$").WithMessage("El nombre solo puede contener letras.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MaximumLength(45).WithMessage(ValidatorsUtils.MaximumLengthMessage)
                .Matches("^[a-zA-Z]+$").WithMessage("El apellido solo puede contener letras.");

            RuleFor(x => x.Email)
             .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
             .EmailAddress().WithMessage(ValidatorsUtils.EmailMessage)
             .MaximumLength(120).WithMessage(ValidatorsUtils.MaximumLengthMessage)
             .Must(ValidatorsUtils.BeAllowedEmailDomain).WithMessage("Only Gmail and Hotmail domains are allowed.");

            RuleFor(x => x.Password)
                          .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                          .MinimumLength(8).WithMessage(ValidatorsUtils.MinimumLengthMessage)
                          .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                          .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.");
        }
    }
}