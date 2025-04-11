using FluentValidation.Results;

namespace Boardify.Application.Exceptions.Custom
{
    public class BussinessException : Exception
    {
        public BussinessException() : base("Problems with the integrity of the information.")
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public BussinessException(string message, object key) : base($"Problems with the integrity of the information in Entity {key}.{message}")
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public BussinessException(string message, ValidationResult validationResult) : base(message)
        {
            ValidationErrors = new Dictionary<string, string[]>();
        }

        public IDictionary<string, string[]> ValidationErrors { get; private set; }
    }
}