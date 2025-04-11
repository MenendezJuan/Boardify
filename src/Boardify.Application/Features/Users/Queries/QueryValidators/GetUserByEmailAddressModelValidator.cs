using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Users.Queries.QueryValidators
{
    public class GetUserByEmailAddressModelValidator : AbstractValidator<GetUserByEmailAddressModel>
    {
        public GetUserByEmailAddressModelValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                                  .EmailAddress().WithMessage(ValidatorsUtils.EmailMessage);
        }
    }
}