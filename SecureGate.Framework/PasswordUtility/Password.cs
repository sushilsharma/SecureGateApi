using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SecureGate.Framework.PasswordUtility
{
    public class Password
    {
        #region PasswordStrength enum

        public enum PasswordStrength
        {
            Low = 1,
            Medium = 2,
            Strong = 3,
            SuperStrong = 4
        }

        #endregion

        #region FIELDS

        #endregion

        #region PROPERTIES

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>The password.</value>
        public string PasswordText { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        /// <value>The password hash.</value>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the password salt.
        /// </summary>
        /// <value>The password salt.</value>
        public long Salt { get; set; }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Initializes a new instance of the <see cref="Password"/> class.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt value to be added to the password hashing.</param>
        public Password(string password, int salt)
        {
            PasswordText = password;
            Salt = salt;
            ComputeSaltedHash();
        }

        #endregion

        /// <summary>
        /// Creates a new random password.
        /// </summary>
        /// <param name="passwordLength">Length of the password to be generated.</param>
        /// <returns></returns>
        public static string CreateRandomPassword(int passwordLength)
        {
            String allowedChars = "abcdefghijkmnopqrstuvwxyzABCDEFGHJKLMNOPQRSTUVWXYZ23456789";
            var randomBytes = new Byte[passwordLength];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);

            var chars = new char[passwordLength];
            int allowedCharCount = allowedChars.Length;
            for (int i = 0; i < passwordLength; i++)
            {
                chars[i] = allowedChars[randomBytes[i] % allowedCharCount];
            }
            return new string(chars);
        }

        /// <summary>
        /// Creates a random salt value.
        /// </summary>
        /// <returns></returns>
        public static int CreateRandomSalt()
        {
            var saltBytes = new Byte[4];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(saltBytes);

            return (((saltBytes[0]) << 24) + ((saltBytes[1]) << 16) + ((saltBytes[2]) << 8) + (saltBytes[3]));
        }

        /// <summary>
        /// Sets the Password Hash property with the computed salted hash.
        /// </summary>
        public void ComputeSaltedHash()
        {
            // Create Byte array of password string
            var encoder = new ASCIIEncoding();
            Byte[] secretBytes = encoder.GetBytes(PasswordText);

            // Create a new salt
            var saltBytes = new Byte[4];
            saltBytes[0] = (byte)(Salt >> 24);
            saltBytes[1] = (byte)(Salt >> 16);
            saltBytes[2] = (byte)(Salt >> 8);
            saltBytes[3] = (byte)(Salt);

            // append the two arrays
            var toHash = new Byte[secretBytes.Length + saltBytes.Length];
            Array.Copy(secretBytes, 0, toHash, 0, secretBytes.Length);
            Array.Copy(saltBytes, 0, toHash, secretBytes.Length, saltBytes.Length);

            SHA1 sha1 = SHA1.Create();

            Byte[] computedHash = sha1.ComputeHash(toHash);
            PasswordHash = encoder.GetString(computedHash);
        }

        /// <summary>
        ///   Hashes a password string.
        /// </summary>
        /// <param name = "password">The password.</param>
        /// <param name = "salt">The salt.</param>
        /// <returns></returns>
        public static string HashPassword(string password, int salt)
        {
            var sha = new SHA256Managed();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = BitConverter.GetBytes(salt);
            byte[] combinedBytes = passwordBytes.Concat(saltBytes).ToArray();
            byte[] passwordHash = sha.ComputeHash(combinedBytes);
            return Convert.ToBase64String(passwordHash);
        }

        /// <summary>
        ///   Calculate the password strength
        /// </summary>
        /// <param name = "password">The password to check</param>
        /// <returns>An integer score value 0 - 4</returns>
        public static long GetPasswordStrength(string password)
        {
            long score = 0;

            // Low
            if (password.Length > 0)
                score = 1;

            // Medium
            if (password.Length > 4 && Regex.Match(password, @"([a-zA-Z])").Success &&
                Regex.Match(password, @"([0-9])").Success)
                score = 2;

            // Strong
            if (password.Length > 7 && Regex.Match(password, @"([a-z].*[A-Z])|([A-Z].*[a-z])").Success &&
                Regex.Match(password, @"([0-9])").Success)
                score = 3;

            // Super Strong
            if (password.Length > 9 && Regex.Match(password, @"([a-z].*[A-Z])|([A-Z].*[a-z])").Success &&
                Regex.Match(password, @"([0-9])").Success && Regex.Match(password, @"([!,@,#,$,%,^,&,*,?,_,~])").Success)
                score = 4;

            return score;
        }

        /// <summary>
        /// Gets the default password.
        /// </summary>
        /// <returns></returns>
        public static string GetDefaultPassword()
        {
            //TODO: Get value from settings
            return "e_Smart";
        }

        /// <summary>
        /// Gets the is required password change value for a user.
        /// </summary>
        /// <returns></returns>
        public static bool GetIsRequiredPasswordChange()
        {
            //TODO: Get value from settings
            return true;
        }

        /// <summary>
        /// Gets the default salt.
        /// </summary>
        /// <returns></returns>
        public static long GetDefaultSalt()
        {
            return long.MaxValue;
        }
    }
}
