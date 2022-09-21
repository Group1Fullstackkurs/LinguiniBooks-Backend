using DBDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using DBDataAccess.Models;
using DBDataAccess.Helpers;

namespace DBDataAccess.Helpers
{
    internal class PwdHelper
    {
        /// <summary>
        /// Checks if password is valid.
        /// </summary>
        /// <param name="user">The user whose password is to be checked.</param>
        /// <param name="pwd">The password of the user</param>
        /// <returns>A bool</returns>
        public bool IsPwdValid(UserModel user, string pwd)
        {
            return user.Hash == GetSaltedHash(pwd, user.Salt);
        }

        /// <summary>
        /// Salts the hash for extra security.
        /// </summary>
        /// <param name="pwd">The password to be salted.</param>
        /// <param name="salt">The salt for the password</param>
        /// <returns>A string with the salted password</returns>
        public string GetSaltedHash(string pwd, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pwd + salt);
            var SHA256String = SHA256.Create();
            byte[] hash = SHA256String.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Gets salt to use on hashed password.
        /// </summary>
        /// <param name="size">Size of the salt.</param>
        /// <returns>A string with the salt to use on password.</returns>
        public string GetSalt(int size = 32)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}
