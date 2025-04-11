using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Boards.Commands.CommandValidators
{
    public class UpdateBoardLabelModelValidator : AbstractValidator<UpdateBoardLabelModel>
    {
        public UpdateBoardLabelModelValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MaximumLength(100).WithMessage(ValidatorsUtils.MaximumLengthMessage);

            RuleFor(model => model.Colour)
                .MaximumLength(100).WithMessage(ValidatorsUtils.MaximumLengthMessage);
        }
    }
}