using Boardify.Application.Features.Users.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Users.Commands.CommandValidators
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserModel>
    {
        public UpdateUserCommandValidator()
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
                .Must(ValidatorsUtils.BeAllowedEmailDomain).WithMessage("Only Gmail, Cedeira, and Hotmail domains are allowed.");
        }
    }
}