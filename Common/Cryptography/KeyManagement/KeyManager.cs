using System.IO;
using System.Security.Cryptography;

namespace Puma.Prey.Common.Cryptography.KeyManagement
{
    public class KeyManager
    {
        private static readonly ProtectedKeyCache cache = new ProtectedKeyCache();

        /// <summary>
        /// Archives a cryptographic key to a <see cref="T:System.IO.Stream" />. This method is intended for use in 
        /// transferring a key between machines.
        /// </summary>
        /// <param name="outputStream"><see cref="T:System.IO.Stream" /> to which key is to be archived.</param>
        /// <param name="keyToArchive">Key to be archived.</param>
        /// <param name="passphrase">User-provided passphrase used to encrypt the key in the arhive.</param>
        public static void ArchiveKey(Stream outputStream, ProtectedKey keyToArchive, string passphrase)
        {
            IKeyWriter keyWriter = new KeyReaderWriter();
            keyWriter.Archive(outputStream, keyToArchive, passphrase);
        }

        /// <summary>
        /// Clear the key manager cache.
        /// </summary>
        public static void ClearCache()
        {
            KeyManager.cache.Clear();
        }

        /// <overloads>
        /// Reads an encrypted key from an input stream. This method is not intended to allow keys to be transferred
        /// from another machine.
        /// </overloads>
        /// <summary>
        /// Reads an encrypted key from an input stream. This method is not intended to allow keys to be transferred
        /// from another machine.
        /// </summary>
        /// <param name="inputStream"><see cref="T:System.IO.Stream" /> from which DPAPI-protected key is to be read.</param>
        /// <param name="dpapiProtectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope" /> used to protect the key on disk. </param>
        /// <returns>Key read from stream, encapsulated in a <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see>.</returns>
        public static ProtectedKey Read(Stream inputStream, DataProtectionScope dpapiProtectionScope)
        {
            IKeyReader keyReader = new KeyReaderWriter();
            return keyReader.Read(inputStream, dpapiProtectionScope);
        }

        /// <summary>
        /// Reads an encrypted key from an input file. This method is not intended to allow keys to be transferred
        /// from another machine.
        /// </summary>
        /// <param name="protectedKeyFileName">Input file from which DPAPI-protected key is to be read.</param>
        /// <param name="dpapiProtectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope" /> used to protect the key on disk. </param>
        /// <returns>Key read from stream, encapsulated in a <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see>.</returns>
        public static ProtectedKey Read(string protectedKeyFileName, DataProtectionScope dpapiProtectionScope)
        {
            string fullPath = Path.GetFullPath(protectedKeyFileName);
            if (KeyManager.cache[fullPath] != null)
            {
                return KeyManager.cache[fullPath];
            }
            ProtectedKey result;
            using (FileStream fileStream = new FileStream(protectedKeyFileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                ProtectedKey protectedKey = KeyManager.Read(fileStream, dpapiProtectionScope);
                KeyManager.cache[fullPath] = protectedKey;
                result = protectedKey;
            }
            return result;
        }

        /// <summary>
        /// Restores a cryptogrpahic key from a <see cref="T:System.IO.Stream" />. This method is intended for use in
        /// transferring a key between machines.
        /// </summary>
        /// <param name="inputStream"><see cref="T:System.IO.Stream" /> from which key is to be restored.</param>
        /// <param name="passphrase">User-provided passphrase used to encrypt the key in the arhive.</param>
        /// <param name="protectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope" /> used to protect the key on disk. </param>
        /// <returns>Key restored from stream, encapsulated in a <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see>.</returns>
        public static ProtectedKey RestoreKey(Stream inputStream, string passphrase, DataProtectionScope protectionScope)
        {
            IKeyReader keyReader = new KeyReaderWriter();
            return keyReader.Restore(inputStream, passphrase, protectionScope);
        }

        /// <overloads>
        /// Writes an encrypted key to an output stream. This method is not intended to allow the keys to be 
        /// moved from machine to machine.
        /// </overloads>
        /// <summary>
        /// Writes an encrypted key to an output stream. This method is not intended to allow the keys to be 
        /// moved from machine to machine.
        /// </summary>
        /// <param name="outputStream"><see cref="T:System.IO.Stream" /> to which DPAPI-protected key is to be written.</param>
        /// <param name="encryptedKey">Encrypted key to be written to stream.</param>
        /// <param name="dpapiProtectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope" /> used to protect the key on disk. </param>
        public static void Write(Stream outputStream, byte[] encryptedKey, DataProtectionScope dpapiProtectionScope)
        {
            ProtectedKey key = ProtectedKey.CreateFromEncryptedKey(encryptedKey, dpapiProtectionScope);
            KeyManager.Write(outputStream, key);
        }

        /// <summary>
        /// Writes an encrypted key to an output stream. This method is not intended to allow the keys to be 
        /// moved from machine to machine.
        /// </summary>
        /// <param name="outputStream"><see cref="T:System.IO.Stream" /> to which DPAPI-protected key is to be written.</param>
        /// <param name="key">Encrypted key to be written to stream.</param>
        public static void Write(Stream outputStream, ProtectedKey key)
        {
            IKeyWriter keyWriter = new KeyReaderWriter();
            keyWriter.Write(outputStream, key);
        }

        public static byte[] ReadKey(string keyFile, DataProtectionScope dpapiProtectionScope)
        {
            //Read the key from the DPAPI protected key file
            ProtectedKey key = KeyManager.Read(keyFile, dpapiProtectionScope);
            return key.DecryptedKey;
        }

        public static void WriteKey(byte[] key, string keyFile, DataProtectionScope dpapiProtectionScope)
        {
            //Write the key to the DPAPI protected key file
            using (FileStream fs = new FileStream(keyFile, FileMode.Create))
            {
                ProtectedKey protectedKey = ProtectedKey.CreateFromPlaintextKey(
                    key, dpapiProtectionScope);
                KeyManager.Write(fs, protectedKey);
                fs.Flush();
                fs.Close();
            }
        }
    }
}
