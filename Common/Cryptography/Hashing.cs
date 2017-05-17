using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Puma.Prey.Common.Cryptography
{
    public class Hashing
    {
        private static int _MIN_SALT_LENGTH = 16;
        private static int _MIN_HASH_ITERATIONS = 1;

        /// <summary>
        /// Generates a SHA512 hash of the input value
        /// </summary>
        /// <param name="input">Input value to be hashed</param>
        /// <param name="salt">Optional salt parameter to append to the input. Salt size mus be at least 16 bytes.</param>
        /// <param name="salt">Optional number of iterations to perform on the hash value. Value must be greater than or equal to 1.</param>
        /// <returns>Hex encoded string of the hashed bytes</returns>
        public static byte[] GenerateHash(string input, byte[] salt = null, int iterations = 1)
        {
            //Iterations must be >= 1
            if (iterations < _MIN_HASH_ITERATIONS)
                throw new ArgumentException(string.Format("Iterations must be greater than or equal to {0}.", _MIN_HASH_ITERATIONS));

            //Salt must be at least 16 bytes
            if (salt != null && salt.Length < _MIN_SALT_LENGTH)
                throw new ArgumentException(string.Format("Salt must be at least {0} bytes in length.", _MIN_SALT_LENGTH));

            //Get the bytes from the cleartext input
            byte[] pwd = Encoding.ASCII.GetBytes(input);

            //Final input bytes will be the lengh of the password plus any salt bytes added to the end
            byte[] inputBytes = new byte[pwd.Length + (salt != null ? salt.Length : 0)];
            
            //Copy the password bytes to the into the input arrary
            pwd.CopyTo(inputBytes, 0);

            //Append the optional salt value if one is supplied
            if (salt != null)
                salt.CopyTo(inputBytes, pwd.Length);

            //Hash the value for the first iteration
            HashAlgorithm sha512 = new SHA512Managed();
            byte[] hashBytes = sha512.ComputeHash(inputBytes);

            //Perform multiple iterations if requested
            for (int i = 1; i < iterations; i++)
                hashBytes = sha512.ComputeHash(hashBytes);

            //Return the bytes
            return hashBytes;
        }

        public static byte[] GenerateHashMd5(string input, byte[] salt = null)
        {
            //Get the bytes from the cleartext input
            byte[] pwd = Encoding.ASCII.GetBytes(input);

            //Final input bytes will be the lengh of the password plus any salt bytes added to the end
            byte[] inputBytes = new byte[pwd.Length + (salt != null ? salt.Length : 0)];

            //Copy the password bytes to the into the input arrary
            pwd.CopyTo(inputBytes, 0);

            //Append the optional salt value if one is supplied
            if (salt != null)
                salt.CopyTo(inputBytes, pwd.Length);

            //Hash the value for the first iteration
            HashAlgorithm hash = new MD5CryptoServiceProvider();
            byte[] hashBytes = hash.ComputeHash(inputBytes);

            //Return the bytes
            return hashBytes;
        }

        public static byte[] GenerateHashSha1(string input, byte[] salt = null)
        {
            //Get the bytes from the cleartext input
            byte[] pwd = Encoding.ASCII.GetBytes(input);

            //Final input bytes will be the lengh of the password plus any salt bytes added to the end
            byte[] inputBytes = new byte[pwd.Length + (salt != null ? salt.Length : 0)];

            //Copy the password bytes to the into the input arrary
            pwd.CopyTo(inputBytes, 0);

            //Append the optional salt value if one is supplied
            if (salt != null)
                salt.CopyTo(inputBytes, pwd.Length);

            //Hash the value for the first iteration
            HashAlgorithm hash = new SHA1CryptoServiceProvider();
            byte[] hashBytes = hash.ComputeHash(inputBytes);

            HashAlgorithm hash2 = new SHA1Managed();
            byte[] hashBytes2 = hash.ComputeHash(inputBytes);

            //Return the bytes
            return hashBytes;
        }
    }
}
