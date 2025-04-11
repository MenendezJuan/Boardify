using Boardify.Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;

namespace Boardify.External.Servicios.HashPassword
{
    public class PasswordService : IPasswordServicio
    {
        private readonly PasswordOptions _options;

        public PasswordService(IOptions<PasswordOptions> options)
        {
            _options = options.Value;
        }

        public bool Check(string hash, string password, string salt)
        {
            string hashedPassword = Hash(password, salt);
            return hash == hashedPassword;
        }

        public (string Hash, string Salt) HashWithSalt(string password)
        {
            string salt = GenerateSalt();
            string hash = Hash(password, salt);
            return (hash, salt);
        }

        private string Hash(string password, string salt)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(password, Encoding.UTF8.GetBytes(salt), _options.Iterations, HashAlgorithmName.SHA256))
            {
                byte[] hash = rfc2898.GetBytes(_options.KeySize);
                return Convert.ToBase64String(hash);
            }
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[_options.SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
    }
}