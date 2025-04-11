using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Workspaces.Commands.CommandValidators
{
    public class AddMemberToWorkspaceModelValidator : AbstractValidator<AddMemberToWorkspaceModel>
    {
        public AddMemberToWorkspaceModelValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);

            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);
        }
    }
}