namespace Boardify.Application.Exceptions.Custom
{
    public class AuthorizationExcepction : Exception
    {
        public AuthorizationExcepction() : base("Not authorized.")
        {
        }

        public AuthorizationExcepction(string message) : base(message)
        {
        }
    }
}