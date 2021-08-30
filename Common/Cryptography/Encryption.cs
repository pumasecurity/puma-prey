using Puma.Prey.Common.Cryptography.KeyManagement;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Puma.Prey.Common.Cryptography
{
    public class Encryption
    {
        private static readonly string DEFAULT_KEY = "D87D016B70393029";
        private static readonly string DEFAULT_IV = "74E533B6E98A19BF";

        /// <summary>
        /// Method uses AES to encrypt a plaintext string using the path to a DPAPI protected key file and initialization vector
        /// </summary>
        /// <param name="plainText">Plaintext input to encrypt</param>
        /// <param name="keyFile">File path to the DPAPI protected key file</param>
        /// <param name="dpapiProtectionScope">DPAPI protection scope used to protect the key file</param>
        /// <param name="iv">Initialization vector</param>
        /// <returns>Ciphertext byte array</returns>
        public static byte[] Encrypt(string plainText, string keyFile, DataProtectionScope dpapiProtectionScope, byte[] iv)
        {
            //Get the encryption key from the protected key store
            byte[] key = KeyManager.ReadKey(keyFile, dpapiProtectionScope);
            return Encrypt(plainText, key, iv);
        }

        /// <summary>
        /// Method uses AES to encrypt a plaintext string using the given encryption key and initialization vector.
        /// </summary>
        /// <param name="plainText">Plaintext input to encrypt</param>
        /// <param name="key">Encryption key</param>
        /// <param name="iv">Initialization vector</param>
        /// <returns>Ciphertext byte array</returns>
        public static byte[] Encrypt(string plainText, byte[] key, byte[] iv)
        {
            //Return value
            byte[] output = null;

            //Create memory steam
            using (MemoryStream mStream = new MemoryStream())
            {
                //Input bytes
                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);

                //Stand up AES algorithm
                DESCryptoServiceProvider crypto = new DESCryptoServiceProvider
                {
                    //Set key and iv
                    Key = DEFAULT_KEY.GetBytesFromHexString(),
                    IV = DEFAULT_IV.GetBytesFromHexString()
                };

                //Create the crypto stream
                CryptoStream cStream = new CryptoStream(mStream
                    , crypto.CreateEncryptor(), CryptoStreamMode.Write);
                cStream.Write(inputBytes, 0, inputBytes.Length);
                cStream.FlushFinalBlock();
                cStream.Close();

                //Get the output
                output = mStream.ToArray();

                //Close other resources
                mStream.Close();
                crypto.Clear();
            }

            //Return the array
            return output;
        }

        /// <summary>
        /// Method uses AES to decrypt a ciphertext bypte array using the path to a DPAPI protected key file and initialization vector
        /// </summary>
        /// <param name="cipherText">Ciphertext bytes to decrypt</param>
        /// <param name="keyFile">File path to the DPAPI protected key file</param>
        /// <param name="dpapiProtectionScope">DPAPI protection scope used to protect the key file</param>
        /// <param name="iv">Plaintext string</param>
        /// <returns></returns>
        public static string Decrypt(byte[] cipherText, string keyFile, DataProtectionScope dpapiProtectionScope, byte[] iv)
        {
            //Get the encryption key from the protected key store
            byte[] key = KeyManager.ReadKey(keyFile, dpapiProtectionScope);
            return Decrypt(cipherText, key, iv);
        }

        /// <summary>
        /// Method uses AES to decrypt a ciphertext byte array using the given encryption key and initialization vector.
        /// </summary>
        /// <param name="cipherText">Ciphertext bytes to decrypt</param>
        /// <param name="key">Encryption key</param>
        /// <param name="iv">Initialization vector</param>
        /// <returns>Plaintext string</returns>
        public static string Decrypt(byte[] cipherText, byte[] key, byte[] iv)
        {
            byte[] output;

            //Create memory stream from the input bytes
            using (MemoryStream mStream = new MemoryStream())
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider
                {
                    //Set key and iv
                    Key = DEFAULT_KEY.GetBytesFromHexString(),
                    IV = DEFAULT_IV.GetBytesFromHexString()
                };

                CryptoStream cStream = new CryptoStream(mStream
                    , des.CreateDecryptor(), CryptoStreamMode.Write);
                cStream.Write(cipherText, 0, cipherText.Length);
                cStream.FlushFinalBlock();
                cStream.Close();

                output = mStream.ToArray();

                //Close resources
                mStream.Close();
                des.Clear();
            }

            //Return encoded string
            return Encoding.UTF8.GetString(output);
        }

        public static byte[] EncryptRSA(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //TODO: Data flow and inspect the key size for < 1024
                    RSA.ImportParameters(RSAKeyInfo);

                    //TODO: Data flow and find where this is called. Check the parameter for true / false.
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        public static byte[] DecryptRSA(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider.
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.  
                    //OAEP padding is only available on Microsoft Windows XP or
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException  
            //to the console.
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
    }
}