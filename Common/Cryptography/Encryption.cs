using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using Puma.Prey.Common.Cryptography.KeyManagement;

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
                byte[] inputBytes = Encoding.ASCII.GetBytes(plainText);

                //Stand up AES algorithm
                AesManaged aes = new AesManaged();

                //Set key and iv
                aes.Key = key;
                aes.IV = iv;

                //Create the crypto stream
                CryptoStream cStream = new CryptoStream(mStream
                    , aes.CreateEncryptor(), CryptoStreamMode.Write);
                cStream.Write(inputBytes, 0, inputBytes.Length);
                cStream.FlushFinalBlock();
                cStream.Close();

                //Get the output
                output = mStream.ToArray();

                //Close other resources
                mStream.Close();
                aes.Clear();
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
                //Stand up AES algorithms  
                AesManaged aes = new AesManaged();

                //Set key and iv
                aes.Key = key;
                aes.IV = iv;

                CryptoStream cStream = new CryptoStream(mStream
                    , aes.CreateDecryptor(), CryptoStreamMode.Write);
                cStream.Write(cipherText, 0, cipherText.Length);
                cStream.FlushFinalBlock();
                cStream.Close();

                output = mStream.ToArray();

                //Close resources
                mStream.Close();
                aes.Clear();
            }

            //Return base 64 encoded string
            return Encoding.ASCII.GetString(output);
        }

        public static byte[] EncryptDes(string plainText)
        {
            //Return value
            byte[] output = null;

            //Create memory steam
            using (MemoryStream mStream = new MemoryStream())
            {
                //Input bytes
                byte[] inputBytes = Encoding.ASCII.GetBytes(plainText);

                //Stand up AES algorithm
                DESCryptoServiceProvider des = new DESCryptoServiceProvider(); ;

                //Set key and iv
                des.Key = DEFAULT_KEY.GetBytesFromHexString();
                des.IV = DEFAULT_IV.GetBytesFromHexString();

                //Create the crypto stream
                CryptoStream cStream = new CryptoStream(mStream
                    , des.CreateEncryptor(), CryptoStreamMode.Write);
                cStream.Write(inputBytes, 0, inputBytes.Length);
                cStream.FlushFinalBlock();
                cStream.Close();

                //Get the output
                output = mStream.ToArray();

                //Close other resources
                mStream.Close();
                des.Clear();
            }

            //Return the array
            return output;
        }

        public static string DecryptDes(byte[] cipherText)
        {
            byte[] output;

            //Create memory stream from the input bytes
            using (MemoryStream mStream = new MemoryStream())
            {
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                
                //Set key and iv
                des.Key = DEFAULT_KEY.GetBytesFromHexString();
                des.IV =DEFAULT_IV.GetBytesFromHexString();

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
            return Encoding.ASCII.GetString(output);
        }

        public static byte[] EncryptAesEcb(string plainText)
        {
            //Return value
            byte[] output = null;

            //Create memory steam
            using (MemoryStream mStream = new MemoryStream())
            {
                //Input bytes
                byte[] inputBytes = Encoding.ASCII.GetBytes(plainText);
                
                //Stand up AES algorithm
                AesManaged aes = new AesManaged();
                aes.Mode = CipherMode.ECB;

                RijndaelManaged rijMan = new RijndaelManaged();
                rijMan.Mode = CipherMode.ECB;

                AesCryptoServiceProvider aesUnmanged = new AesCryptoServiceProvider();
                aesUnmanged.Mode = CipherMode.ECB;

                //Set key and iv
                aes.Key = DEFAULT_KEY.GetBytesFromHexString();

                //Create the crypto stream
                CryptoStream cStream = new CryptoStream(mStream
                    , aes.CreateEncryptor(), CryptoStreamMode.Write);
                cStream.Write(inputBytes, 0, inputBytes.Length);
                cStream.FlushFinalBlock();
                cStream.Close();

                //Get the output
                output = mStream.ToArray();

                //Close other resources
                mStream.Close();
                aes.Clear();
            }

            //Return the array
            return output;
        }

        public static string DecryptAesEcb(byte[] cipherText)
        {
            byte[] output;

            //Create memory stream from the input bytes
            using (MemoryStream mStream = new MemoryStream())
            {
                //Stand up AES algorithms  
                AesManaged aes = new AesManaged();
                aes.Mode = CipherMode.ECB;

                //Set key and iv
                aes.Key = DEFAULT_KEY.GetBytesFromHexString();

                CryptoStream cStream = new CryptoStream(mStream
                    , aes.CreateDecryptor(), CryptoStreamMode.Write);
                cStream.Write(cipherText, 0, cipherText.Length);
                cStream.FlushFinalBlock();
                cStream.Close();

                output = mStream.ToArray();

                //Close resources
                mStream.Close();
                aes.Clear();
            }

            //Return base 64 encoded string
            return Encoding.ASCII.GetString(output);
        }

    }
}