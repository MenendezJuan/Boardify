using Boardify.Application.Features.Cards.Models;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class UpdateCardModelValidator : AbstractValidator<UpdateCardModel>
    {
        public UpdateCardModelValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().MaximumLength(90);
            RuleFor(x => x.ColumnId).NotEmpty();
        }
    }
}