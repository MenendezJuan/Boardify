using Boardify.Application.Features.Workspaces.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Workspaces.Commands.CommandValidators
{
    public class RemoveMemberFromWorkspaceModelValidator : AbstractValidator<RemoveMemberFromWorkspaceModel>
    {
        public RemoveMemberFromWorkspaceModelValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);

            RuleFor(x => x.WorkspaceId)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);
        }
    }
}