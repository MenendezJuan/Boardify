using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Boards.Commands.CommandValidators
{
    public class CreateBoardModelValidator : AbstractValidator<CreateBoardModel>
    {
        public CreateBoardModelValidator()
        {
            RuleFor(model => model.WorkspaceId)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);

            RuleFor(model => model.Name)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MaximumLength(100).WithMessage(ValidatorsUtils.MaximumLengthMessage);

            RuleFor(model => model.Description)
                .MaximumLength(500).WithMessage(ValidatorsUtils.MaximumLengthMessage);

            RuleFor(model => model.Visibility)
                .IsInEnum().WithMessage(ValidatorsUtils.InvalidVisibilityMessage);
        }
    }
}