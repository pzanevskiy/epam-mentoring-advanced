using System.Security.Cryptography;

namespace PasswordGenerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Span<byte> salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            const string password = "password";
            var passwordHash = GeneratePasswordHashUsingSalt(password, salt.ToArray());

            Console.WriteLine("Password hash: " + passwordHash);
        }

        public static string GeneratePasswordHashUsingSalt(string passwordText, byte[] salt)
        {
            const int iterate = 10000;
            using var pbkdf2 = new Rfc2898DeriveBytes(passwordText, salt, iterate);
            Span<byte> hash = pbkdf2.GetBytes(20);
            Span<byte> hashBytes = new byte[36];

            salt.CopyTo(hashBytes);
            hash.CopyTo(hashBytes.Slice(16));

            return Convert.ToBase64String(hashBytes);
        }
    }
}