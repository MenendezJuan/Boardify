using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Boards.Commands.CommandValidators
{
    public class RemoveMemberFromBoardCommandValidator : AbstractValidator<RemoveMemberFromBoardModel>
    {
        public RemoveMemberFromBoardCommandValidator()
        {
            RuleFor(model => model.BoardId).GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);

            RuleFor(model => model.UserId).GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);
        }
    }
}