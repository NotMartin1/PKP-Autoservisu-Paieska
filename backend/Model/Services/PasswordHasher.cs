
using System.Security.Cryptography;
using System.Security.Policy;
using Model.Services.Interfaces;


namespace Model.Services;

    public class PasswordHasher : IPasswordHasher
    {

private const int SaltSize = 128 / 8;
        private const int KeySize = 256 / 8;
        private const int Iterations = 10000;
        private static readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;
        private static char delimiter = ';';

        public string HashPassword(string password)
        {
            var salt = RandomNumberGenerator.GetBytes(SaltSize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);
            return string.Join(delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            var elements = passwordHash.Split(delimiter);
            var salt = Convert.FromBase64String(elements[0]);
            var hash = Convert.FromBase64String(elements[1]);

            var hashInput = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, _hashAlgorithm, KeySize);

            return CryptographicOperations.FixedTimeEquals(hash, hashInput);    
            
        }
    
    }
