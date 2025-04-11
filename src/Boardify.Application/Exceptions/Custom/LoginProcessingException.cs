namespace Boardify.Application.Exceptions.Custom
{
    public class LoginProcessingException : Exception
    {
        public LoginProcessingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
