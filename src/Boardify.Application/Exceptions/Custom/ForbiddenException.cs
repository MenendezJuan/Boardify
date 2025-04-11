namespace Boardify.Application.Exceptions.Custom
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException() : base("You do not have permission to perform the requested action.")
        {
        }

        public ForbiddenException(string mensaje) : base(mensaje)
        {
        }
    }
}