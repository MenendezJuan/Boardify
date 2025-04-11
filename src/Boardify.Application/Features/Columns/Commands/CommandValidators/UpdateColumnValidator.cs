using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Columns.Commands.CommandValidators
{
    public class UpdateColumnValidator : AbstractValidator<UpdateColumnModel>
    {
        public UpdateColumnValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MaximumLength(90).WithMessage(ValidatorsUtils.MaximumLengthMessage);
        }
    }
}