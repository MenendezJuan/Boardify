using Boardify.Application.Features.Users.Models;
using Boardify.Application.Utils;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boardify.Application.Features.Users.Commands.CommandValidators
{
    public class UpdatePasswordModelValidator : AbstractValidator<UpdatePasswordModel>
    {
        public UpdatePasswordModelValidator()
        {
            RuleFor(model => model.CurrentPassword)
                            .NotEmpty()
                            .WithMessage(ValidatorsUtils.RequiredField.Replace("{PropertyName}", "Contraseña actual"));

            RuleFor(model => model.NewPassword)
                .NotEmpty()
                .WithMessage(ValidatorsUtils.RequiredField.Replace("{PropertyName}", "Nueva contraseña"))
                .MinimumLength(8)
                .WithMessage(ValidatorsUtils.MinimumLengthMessage.Replace("{PropertyName}", "Nueva contraseña").Replace("{MinLength}", "6"))
                .Matches(@"[A-Z]").WithMessage(ValidatorsUtils.PasswordUppercaseMessage.Replace("{PropertyName}", "Nueva contraseña"))
                .Matches(@"[a-z]").WithMessage(ValidatorsUtils.PasswordLowercaseMessage.Replace("{PropertyName}", "Nueva contraseña"))
                .Matches(@"[0-9]").WithMessage(ValidatorsUtils.PasswordDigitMessage.Replace("{PropertyName}", "Nueva contraseña"))
                .Matches(@"[\W_]").WithMessage(ValidatorsUtils.PasswordSpecialCharMessage.Replace("{PropertyName}", "Nueva contraseña"));
        }
    }
}
