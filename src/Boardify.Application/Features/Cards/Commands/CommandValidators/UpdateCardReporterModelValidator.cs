using Boardify.Application.Features.Cards.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Cards.Commands.CommandValidators
{
    public class UpdateCardReporterModelValidator : AbstractValidator<UpdateCardReporterModel>
    {
        public UpdateCardReporterModelValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(ValidatorsUtils.RequiredField);

            RuleFor(x => x.ReporterId)
                .Must(id => id == null || id > 0)
                .WithMessage(ValidatorsUtils.RequiredField);
        }
    }
}
