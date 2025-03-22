using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Password_Authentication
{
    public class Hashing
    {
        public static (string salt, string hash) HashPassword(string password)
        {
            // Generate a random salt
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            string salt = Convert.ToBase64String(saltBytes);

            // Hash the password with the salt
            string hash = ComputeSHA256Hash(password + salt);

            return (salt, hash);
        }

        public static bool VerifyPassword(string password, string storedSalt, string storedHash)
        {
            // Compute hash with the stored salt
            string computedHash = ComputeSHA256Hash(password + storedSalt);
            return computedHash == storedHash; // Compare hashes
        }

        private static string ComputeSHA256Hash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
