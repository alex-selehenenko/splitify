﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Splitify.Identity.Domain.Utils
{
    public static class PasswordUtils
    {
        public static byte[] HashPassword(string password, byte[] salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);

            using var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, salt, 10000, HashAlgorithmName.SHA256);
            return pbkdf2.GetBytes(32);
        }

        public static byte[] GenerateSalt()
        {
            var salt = new byte[16];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);
            return salt;
        }
    }
}
