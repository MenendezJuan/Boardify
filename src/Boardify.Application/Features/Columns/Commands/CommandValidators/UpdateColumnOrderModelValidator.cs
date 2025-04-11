using Boardify.Application.Features.Columns.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Columns.Commands.CommandValidators
{
    public class UpdateColumnOrderModelValidator : AbstractValidator<UpdateColumnOrderModel>
    {
        public UpdateColumnOrderModelValidator()
        {
            RuleFor(model => model.BoardId)
                 .GreaterThan(0)
                 .WithMessage(ValidatorsUtils.GreaterThanMessage.Replace("{PropertyName}", "ID del tablero").Replace("{GreaterThan}", "0"));

            RuleFor(model => model.ColumnOrder)
                .NotEmpty()
                .WithMessage(ValidatorsUtils.RequiredField.Replace("{PropertyName}", "Orden de las columnas"))
                .Must(order => order.Distinct().Count() == order.Count())
                .WithMessage(ValidatorsUtils.InvalidColumnOrderMessage);
        }
    }
}
