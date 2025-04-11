using FluentValidation.Results;

namespace Boardify.Application.Exceptions.Custom
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public BadRequestException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = validationResult.ToDictionary();
        }

        public IDictionary<string, string[]> ValidationErrors { get; private set; }
    }
}