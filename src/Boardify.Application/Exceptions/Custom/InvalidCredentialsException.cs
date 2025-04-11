namespace Boardify.Application.Exceptions.Custom
{
    public class InvalidCredentialsException : Exception
    {
        public InvalidCredentialsException(string message)
            : base(message)
        {
        }
    }

}
