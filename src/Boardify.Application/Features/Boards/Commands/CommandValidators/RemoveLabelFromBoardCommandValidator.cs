using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Boards.Commands.CommandValidators
{
    public class RemoveLabelFromBoardCommandValidator : AbstractValidator<RemoveLabelFromBoardModel>
    {
        public RemoveLabelFromBoardCommandValidator()
        {
            RuleFor(model => model.BoardId).GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);

            RuleFor(model => model.BoardLabelId).GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);
        }
    }
}