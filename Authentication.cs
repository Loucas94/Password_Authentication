using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Password_Authentication
{
    public class Authentication
    {
        private Dictionary<string, User> users = new Dictionary<string, User>();
        private const int MaxAttempts = 3;

        public bool RegisterUser(string username, string password)
        {
            if (users.ContainsKey(username))
            {
                return false; // User already exists
            }
            users[username] = new User(password);
            return true;
        }

        public string AuthenticateUser(string username, string password)
        {
            if (!users.ContainsKey(username))
            {
                return "User does not exist.";
            }

            User user = users[username];

            if (user.FailedAttempts >= MaxAttempts)
            {
                return "Account locked due to too many failed attempts.";
            }

            if (user.VerifyPassword(password))
            {
                user.FailedAttempts = 0;
                return "Authentication successful!";
            }
            else
            {
                user.FailedAttempts++;
                return $"Authentication failed! {MaxAttempts - user.FailedAttempts} attempts left.";
            }
        }
    }
}