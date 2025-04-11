using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Workspaces.Commands.CommandValidators
{
    public class UpdateWorkspaceCommandValidator : AbstractValidator<UpdateWorkspaceModel>
    {
        public UpdateWorkspaceCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                .MaximumLength(72).WithMessage(ValidatorsUtils.MaximumLengthMessage);
        }
    }
}