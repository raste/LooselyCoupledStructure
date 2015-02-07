using System;
using System.Linq;
using System.Security.Cryptography;

using BL.Services.Common;

namespace BL.Services.Security.Cryptography
{
    /// <summary>
    /// Modified variant of:
    /// <para>
    /// Password Hashing With PBKDF2 (http://crackstation.net/hashing-security.htm).
    /// Copyright (c) 2013, Taylor Hornby
    /// All rights reserved.
    /// </para>
    /// <para>
    /// Creates Salted Hashes, constructed of Salt+Hashed(plainText+Salt) 
    /// </para>
    /// </summary>
    internal class HashExecutor
    {
        private const int HASH_AND_SALT_MINIMUM_SIZE = 24;

        private const int PBKDF2_ITERATIONS = 1200;

        private const int ITERATION_INDEX = 0;
        private const int SALT_INDEX = 1;
        private const int PBKDF2_INDEX = 2;

        private const char DELIMETER = ':';

        private IConfigurationService configService = null;

        public HashExecutor(IConfigurationService configService)
        {
            this.configService = configService;
        }

        public string Hash(string plainTextInput)
        {
            byte[] salt = this.GenerateSalt();

            string hashResult = this.Hash(plainTextInput, salt, PBKDF2_ITERATIONS);

            return hashResult;
        }

        private byte[] GenerateSalt()
        {
            int saltSize = this.GetHashSaltSizeSettingValue();

            byte[] salt = new byte[saltSize];
            using (RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider())
            {
                csprng.GetBytes(salt);
            }

            return salt;
        }

        private string Hash(string plainTextInput, byte[] salt, int iterations)
        {
            int hashSize = this.GetHashSizeWithoutSaltSettingValue();

            // Hash the text with salt
            byte[] hash;
            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(plainTextInput, salt))
            {
                pbkdf2.IterationCount = iterations;

                hash = pbkdf2.GetBytes(hashSize);
            }

            string strHash = Convert.ToBase64String(hash);
            string strSalt = Convert.ToBase64String(salt);

            ///Adding the salt and the iterations to the hashed data. 
            ///This way the salt can be extracted later and used (to hash) to check if the user data is equal to the hashed one.
            string resultHash = string.Format("{0}{1}{2}{1}{3}"
                , iterations
                , DELIMETER
                , strSalt
                , strHash);

            return resultHash;
        }

        public bool Compare(string hashedText, string plainTextInput)
        {
            if (ValidateCompare(hashedText, plainTextInput) == false)
            {
                return false;
            }

            int iterations = default(int);
            byte[] salt = default(byte[]);

            ExtractIterationsAndSaltFromHash(hashedText, out iterations, out salt);

            string plainTextHash = this.Hash(plainTextInput, salt, iterations);

            bool equal = SlowEquals(plainTextHash, hashedText);
            return equal;
        }

        private bool ValidateCompare(string hashedText, string plainTextInput)
        {
            if (string.IsNullOrEmpty(hashedText) == true || string.IsNullOrEmpty(plainTextInput) == true)
            {
                return false;
            }

            int saltSize = this.GetHashSaltSizeSettingValue();
            if (saltSize > hashedText.Length)
            {
                return false;
            }

            string[] split = hashedText.Split(DELIMETER);
            if (split.Count() < 3)
            {
                return false;
            }

            return true;
        }

        private void ExtractIterationsAndSaltFromHash(string hash, out int iterations, out byte[] salt)
        {
            string[] split = hash.Split(DELIMETER);

            iterations = Int32.Parse(split[ITERATION_INDEX]);
            salt = Convert.FromBase64String(split[SALT_INDEX]);
        }

        private int GetHashSaltSizeSettingValue()
        {
            int saltSize = configService.SecurityHashSaltSize();

            if ((saltSize % 4) != 0)
            {
                throw new ArgumentOutOfRangeException("Hash salt size should be modulus of 4 (20,24,28...160..200..)");
            }

            if (saltSize < HASH_AND_SALT_MINIMUM_SIZE)
            {
                throw new ArgumentOutOfRangeException(string.Format("Hash salt size should not be less than {0}"
                    , HASH_AND_SALT_MINIMUM_SIZE));
            }

            return saltSize;
        }

        private int GetHashSizeWithoutSaltSettingValue()
        {
            int hashSize = configService.SecurityHashLengthWOSalt();

            if (hashSize < HASH_AND_SALT_MINIMUM_SIZE)
            {
                throw new ArgumentOutOfRangeException(string.Format("Hash size without salt should not be less than {0}"
                    , HASH_AND_SALT_MINIMUM_SIZE));
            }

            return hashSize;
        }

        /// <summary>
        /// Compares two strings in length-constant time. This comparison
        /// method is used so that password hashes cannot be extracted from
        /// on-line systems using a timing attack and then attacked off-line.
        /// </summary>
        /// <returns>True if both strings are equal. False otherwise.</returns>
        private bool SlowEquals(string first, string second)
        {
            bool diff = (first.Length == second.Length);
            for (int i = 0; i < first.Length && i < second.Length; i++)
            {
                if (first[i] != second[i])
                {
                    diff = false;
                }
            }

            return diff;
        }
    }
}
