using AuthLibrary.Auth.EmailRegistration;
using BaseModelLibrary.Lib.Models.UserModels;
using InternetShopDBContext.Lib;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace AuthLibrary.Registration
{
    public static class PasswordController
    {
        static RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();

        public static bool CheckUserPassword(User user, string enteredPassword)
        {
            if (user == null) return false;
            string hash = user.EmailLogin.Hash;
            string password = enteredPassword + user.EmailLogin.Salt;

            string receivedHash = HashPassword(password);

            return hash == receivedHash;
        }

        public static void CreatePassword(EmailLogin emailLogin, string password)
        {
            string salt = CreateSalt();
            emailLogin.Salt = salt;
            emailLogin.Hash = HashPassword(password + salt);
        }
        public static string HashPassword(string str)
        {
            string someString;

            using (HMACSHA256 hmac = new HMACSHA256(Encoding.ASCII.GetBytes(Secrets.SuperPuperSecretKey)))
            {
                byte[] hashValue = hmac.ComputeHash(Encoding.ASCII.GetBytes(str));
                someString = Convert.ToBase64String(hashValue);
            }
            return someString;
        }

        public static string CreateSalt(int saltLength = 12)
        {
            byte[] ar = new byte[saltLength];
            rngCsp.GetBytes(ar);
            return Convert.ToBase64String(ar);
        }

        public static string HashForEmail(string str)
        {

            var token = JwtController.GenerateToken(str);

            return token;
        }
        public static bool ValidateEmail(string email, string token)
        {
            return JwtController.ValidateToken(token, email);
        }
    }
}
