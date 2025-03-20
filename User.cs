using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Password_Authentication
{
    public class User
    {
        public string Salt { get; }
        public string HashedPassword { get; private set; }
        public int FailedAttempts { get; set; }

        public User(string password)
        {
            Salt = GenerateSalt();
            HashedPassword = HashPassword(password, Salt);
            FailedAttempts = 0;
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] saltedPassword = Encoding.UTF8.GetBytes(salt + password);
                byte[] hash = sha256.ComputeHash(saltedPassword);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password)
        {
            return HashedPassword == HashPassword(password, Salt);
        }
    }
}