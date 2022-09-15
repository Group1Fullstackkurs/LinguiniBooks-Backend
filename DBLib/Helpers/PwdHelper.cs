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
        public bool IsPwdValid(UserModel user, string pwd)
        {
            return user.Hash == GetSaltedHash(pwd, user.Salt);
        }

        public string GetSaltedHash(string pwd, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(pwd + salt);
            var SHA256String = SHA256.Create();
            byte[] hash = SHA256String.ComputeHash(bytes);

            return Convert.ToBase64String(hash);
        }

        public string GetSalt(int size = 32)
        {
            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}
