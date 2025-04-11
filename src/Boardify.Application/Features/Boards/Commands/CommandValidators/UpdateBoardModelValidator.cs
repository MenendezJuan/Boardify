using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Boards.Commands.CommandValidators
{
    public class UpdateBoardModelValidator : AbstractValidator<UpdateBoardModel>
    {
        public UpdateBoardModelValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MaximumLength(100).WithMessage(ValidatorsUtils.MaximumLengthMessage);

            RuleFor(model => model.Description)
                .MaximumLength(255).WithMessage(ValidatorsUtils.MaximumLengthMessage);

            RuleFor(model => model.Visibility)
                .IsInEnum().WithMessage(ValidatorsUtils.InvalidVisibilityMessage);
        }
    }
}