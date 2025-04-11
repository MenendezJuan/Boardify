namespace Boardify.Application.Exceptions.Custom
{
    public class TokenCreationException : Exception
    {
        public TokenCreationException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
