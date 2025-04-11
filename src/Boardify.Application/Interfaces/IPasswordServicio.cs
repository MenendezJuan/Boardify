namespace Boardify.Application.Interfaces
{
    public interface IPasswordServicio
    {
        (string Hash, string Salt) HashWithSalt(string password);

        bool Check(string hash, string password, string salt);
    }
}