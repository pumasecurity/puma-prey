using System;
using System.IO;
using System.Security.Cryptography;

namespace Puma.Prey.Common.Cryptography.KeyManagement
{
    /// <summary>
	/// Reads and writes cryptographic keys to and from streams.
	/// </summary>
	public class KeyReaderWriter : IKeyReader, IKeyWriter
    {
        internal const int versionNumber = 4321;
        internal const int versionNumberLength = 4;

        /// <summary>
        /// Reads a DPAPI-protected key from the given <see cref="T:System.IO.Stream" />.
        /// </summary>
        /// <param name="protectedKeyStream"><see cref="T:System.IO.Stream" /> containing the DPAPI-protected key.</param>
        /// <param name="protectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope"></see> used to decrypt the key read from the stream.</param>
        /// <returns>Key read from stream, encapsulated in a <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see>.</returns>
        public ProtectedKey Read(Stream protectedKeyStream, DataProtectionScope protectionScope)
        {
            this.ValidateKeyVersion(protectedKeyStream);
            return ProtectedKey.CreateFromEncryptedKey(this.ReadEncryptedKey(protectedKeyStream), protectionScope);
        }

        /// <overloads>
        /// Restores a cryptographic key from a <see cref="T:System.IO.Stream" />. This method is intended for use in
        /// transferring a key between machines.
        /// </overloads>
        /// <summary>
        /// Restores a cryptogrpahic key from a <see cref="T:System.IO.Stream" />. This method is intended for use in
        /// transferring a key between machines.
        /// </summary>
        /// <param name="protectedKeyStream"><see cref="T:System.IO.Stream" /> from which key is to be restored.</param>
        /// <param name="passphrase">User-provided passphrase used to encrypt the key in the arhive.</param>
        /// <param name="protectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope" /> used to protect the key on disk. </param>
        /// <returns>Key restored from stream, encapsulated in a <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see>.</returns>
        public ProtectedKey Restore(Stream protectedKeyStream, string passphrase, DataProtectionScope protectionScope)
        {
            return this.ProtectKey(this.Restore(protectedKeyStream, passphrase), protectionScope);
        }

        /// <summary>
        /// Restores a cryptographic key from a <see cref="T:System.IO.Stream" />. This method is intended for use in
        /// transferring a key between machines.
        /// </summary>
        /// <param name="protectedKeyStream"><see cref="T:System.IO.Stream" /> from which key is to be restored.</param>
        /// <param name="passphrase">User-provided passphrase used to encrypt the key in the arhive.</param>
        /// <returns>Unencrypted key restored from stream. It is the responsibility of the calling code to
        /// clear the returned byte array.</returns>
        public byte[] Restore(Stream protectedKeyStream, string passphrase)
        {
            this.ValidateKeyVersion(protectedKeyStream);
            byte[] array = new byte[16];
            byte[] array2 = new byte[protectedKeyStream.Length - 4L - (long)array.Length];
            protectedKeyStream.Read(array, 0, array.Length);
            protectedKeyStream.Read(array2, 0, array2.Length);
            return this.DecryptKeyForRestore(passphrase, array2, array);
        }

        /// <summary>
        /// Writes an encrypted key to an output stream. This method is not intended to allow the keys to be 
        /// moved from machine to machine.
        /// </summary>
        /// <param name="outputStream"><see cref="T:System.IO.Stream" /> to which DPAPI-protected key is to be written.</param>
        /// <param name="key">Encrypted key to be written to stream.</param>
        public void Write(Stream outputStream, ProtectedKey key)
        {
            this.WriteVersionNumber(outputStream, 4321);
            this.WriteEncryptedKey(outputStream, key);
        }

        /// <summary>
        /// Archives a cryptographic key to a <see cref="T:System.IO.Stream" />. This method is intended for use in 
        /// transferring a key between machines.
        /// </summary>
        /// <param name="outputStream"><see cref="T:System.IO.Stream" /> to which key is to be archived.</param>
        /// <param name="keyToBeArchived">Key to be archived.</param>
        /// <param name="passphrase">User-provided passphrase used to encrypt the key in the arhive.</param>
        public void Archive(Stream outputStream, ProtectedKey keyToBeArchived, string passphrase)
        {
            if (outputStream == null)
            {
                throw new ArgumentNullException("outputStream");
            }
            byte[] bytes = BitConverter.GetBytes(4321);
            byte[] array = this.GenerateSalt();
            byte[] encryptedKey = this.GetEncryptedKey(keyToBeArchived, passphrase, array);
            outputStream.Write(bytes, 0, bytes.Length);
            outputStream.Write(array, 0, array.Length);
            outputStream.Write(encryptedKey, 0, encryptedKey.Length);
        }

        private byte[] GetEncryptedKey(ProtectedKey keyToBeArchived, string passphrase, byte[] salt)
        {
            byte[] decryptedKey = keyToBeArchived.DecryptedKey;
            byte[] result;
            try
            {
                result = this.EncryptKeyForArchival(decryptedKey, passphrase, salt);
            }
            finally
            {
                Utility.ZeroOutBytes(decryptedKey);
            }
            return result;
        }

        private byte[] GenerateSalt()
        {
            return Random.GenerateCrytpoRandomBytes(16);
        }

        private byte[] EncryptKeyForArchival(byte[] keyToExport, string passphrase, byte[] salt)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            byte[] rgbKey = this.GenerateArchivalKey(rijndaelManaged, passphrase, salt);
            byte[] rgbIV = new byte[rijndaelManaged.BlockSize / 8];
            return Utility.Transform(rijndaelManaged.CreateEncryptor(rgbKey, rgbIV), keyToExport);
        }

        private void WriteEncryptedKey(Stream outputStream, ProtectedKey key)
        {
            outputStream.Write(key.EncryptedKey, 0, key.EncryptedKey.Length);
        }

        private byte[] ReadEncryptedKey(Stream protectedKeyStream)
        {
            byte[] array = new byte[protectedKeyStream.Length - 4L];
            protectedKeyStream.Read(array, 0, array.Length);
            return array;
        }

        private void WriteVersionNumber(Stream outputStream, int versionNumber)
        {
            byte[] bytes = BitConverter.GetBytes(versionNumber);
            outputStream.Write(bytes, 0, bytes.Length);
        }

        private uint ReadVersionNumber(Stream protectedKeyStream)
        {
            byte[] array = new byte[4];
            protectedKeyStream.Read(array, 0, array.Length);
            return BitConverter.ToUInt32(array, 0);
        }

        private void ValidateKeyVersion(Stream protectedKeyStream)
        {
            uint num = this.ReadVersionNumber(protectedKeyStream);
            if (num != 4321u)
            {
                throw new InvalidOperationException("Key version is invalid");
            }
        }

        private byte[] GenerateArchivalKey(SymmetricAlgorithm archivalEncryptionAlgorithm, string passphrase, byte[] salt)
        {
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(passphrase, salt);
            return rfc2898DeriveBytes.GetBytes(archivalEncryptionAlgorithm.KeySize / 8);
        }

        private byte[] DecryptKeyForRestore(string passphrase, byte[] encryptedKey, byte[] salt)
        {
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            byte[] array = this.GenerateArchivalKey(rijndaelManaged, passphrase, salt);
            byte[] rgbIV = new byte[rijndaelManaged.BlockSize / 8];
            byte[] result = Utility.Transform(rijndaelManaged.CreateDecryptor(array, rgbIV), encryptedKey);
            Utility.ZeroOutBytes(array);
            return result;
        }

        private ProtectedKey ProtectKey(byte[] decryptedKey, DataProtectionScope protectionScope)
        {
            ProtectedKey result = ProtectedKey.CreateFromPlaintextKey(decryptedKey, protectionScope);
            Utility.ZeroOutBytes(decryptedKey);
            return result;
        }
    }
}
