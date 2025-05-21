using System;
using System.IO;
using System.Security.Cryptography;

namespace Puma.Prey.Common.Cryptography
{
    public class Utility
    {
        /// <summary>
        /// <para>Determine if two byte arrays are equal.</para>
        /// </summary>
        /// <param name="byte1">
        /// <para>The first byte array to compare.</para>
        /// </param>
        /// <param name="byte2">
        /// <para>The byte array to compare to the first.</para>
        /// </param>
        /// <returns>
        /// <para><see langword="true" /> if the two byte arrays are equal; otherwise <see langword="false" />.</para>
        /// </returns>
        public static bool CompareBytes(byte[] byte1, byte[] byte2)
        {
            if (byte1 == null || byte2 == null)
            {
                return false;
            }
            if (byte1.Length != byte2.Length)
            {
                return false;
            }
            bool result = true;
            for (int i = 0; i < byte1.Length; i++)
            {
                if (byte1[i] != byte2[i])
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// <para>Returns a byte array from a string representing a hexidecimal number.</para>
        /// </summary>
        /// <param name="hexidecimalNumber">
        /// <para>The string containing a valid hexidecimal number.</para>
        /// </param>
        /// <returns><para>The byte array representing the hexidecimal.</para></returns>

        /// <summary>
        /// <para>Combines two byte arrays into one.</para>
        /// </summary>
        /// <param name="buffer1"><para>The prefixed bytes.</para></param>
        /// <param name="buffer2"><para>The suffixed bytes.</para></param>
        /// <returns><para>The combined byte arrays.</para></returns>
        public static byte[] CombineBytes(byte[] buffer1, byte[] buffer2)
        {
            if (buffer1 == null)
            {
                throw new ArgumentNullException("buffer1");
            }
            if (buffer2 == null)
            {
                throw new ArgumentNullException("buffer2");
            }
            byte[] array = new byte[buffer1.Length + buffer2.Length];
            Buffer.BlockCopy(buffer1, 0, array, 0, buffer1.Length);
            Buffer.BlockCopy(buffer2, 0, array, buffer1.Length, buffer2.Length);
            return array;
        }

        /// <summary>
        /// <para>Fills <paramref name="bytes" /> zeros.</para>
        /// </summary>
        /// <param name="bytes">
        /// <para>The byte array to fill.</para>
        /// </param>
        public static void ZeroOutBytes(byte[] bytes)
        {
            if (bytes == null)
            {
                return;
            }
            Array.Clear(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Transforms an array of bytes according to the given cryptographic transform.
        /// </summary>
        /// <param name="transform"><see cref="T:System.Security.Cryptography.ICryptoTransform" /> used to transform the given <paramref name="buffer" />.</param>
        /// <param name="buffer">Buffer to transform. It is the responsibility of the caller to clear this array when finished.</param>
        /// <returns>Transformed array of bytes. It is the responsibility of the caller to clear this byte array
        /// if necessary.</returns>
        public static byte[] Transform(ICryptoTransform transform, byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }
            byte[] result = null;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                CryptoStream cryptoStream = null;
                try
                {
                    cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
                    cryptoStream.Write(buffer, 0, buffer.Length);
                    cryptoStream.FlushFinalBlock();
                    result = memoryStream.ToArray();
                }
                finally
                {
                    if (cryptoStream != null)
                    {
                        cryptoStream.Close();
                        ((IDisposable)cryptoStream).Dispose();
                    }
                }
            }
            return result;
        }
    }
}
