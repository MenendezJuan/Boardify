using Boardify.Application.Features.Boards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Boards.Commands.CommandValidators
{
    public class AddLabelToBoardCommandValidator : AbstractValidator<AddLabelToBoardModel>
    {
        public AddLabelToBoardCommandValidator()
        {
            RuleFor(model => model.BoardId)
                .GreaterThan(0).WithMessage(ValidatorsUtils.GreaterThanMessage);
        }
    }
}