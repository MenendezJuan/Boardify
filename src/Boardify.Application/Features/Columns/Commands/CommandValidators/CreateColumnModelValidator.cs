using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Columns.Commands.CommandValidators
{
    public class CreateColumnModelValidator : AbstractValidator<CreateColumnModel>
    {
        public CreateColumnModelValidator()
        {
            RuleFor(x => x.BoardId)
                .GreaterThan(0)
                .WithMessage(ValidatorsUtils.GreaterThanMessage);
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(90)
                .WithMessage(ValidatorsUtils.MaximumLengthMessage);
        }
    }
}