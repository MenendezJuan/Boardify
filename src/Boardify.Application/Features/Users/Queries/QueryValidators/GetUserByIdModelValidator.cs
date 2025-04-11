using Boardify.Application.Features.Users.Queries.Models;
using Boardify.Application.Utils;
using FluentValidation;

namespace Boardify.Application.Features.Users.Queries.QueryValidators
{
    public class GetUserByIdModelValidator : AbstractValidator<GetUserByIdModel>
    {
        public GetUserByIdModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(ValidatorsUtils.RequiredField);
            RuleFor(x => x.LastName).NotEmpty().WithMessage(ValidatorsUtils.RequiredField);
            RuleFor(x => x.Email).NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                                   .EmailAddress().WithMessage(ValidatorsUtils.EmailMessage);
            RuleFor(x => x.Password).NotEmpty().WithMessage(ValidatorsUtils.RequiredField)
                                     .MinimumLength(8).WithMessage(ValidatorsUtils.MinimumLengthMessage);
        }
    }
}