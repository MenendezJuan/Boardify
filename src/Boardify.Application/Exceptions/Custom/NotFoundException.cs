namespace Boardify.Application.Exceptions.Custom
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name, object key) : base($"{name} ({key}) not found.")
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }
    }
}