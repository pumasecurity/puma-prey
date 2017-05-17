using System;
using System.Security.Cryptography;

namespace Puma.Prey.Common.Cryptography.KeyManagement
{
    public class ProtectedKey
    {
        private byte[] protectedKey;
        private DataProtectionScope protectionScope;
       
        /// <summary>
        /// Gets the encrypted key contained by this object.
        /// </summary>
        public byte[] EncryptedKey
        {
            get
            {
                return (byte[])this.protectedKey.Clone();
            }
        }
        
        /// <summary>
        /// Gets the decrypted key protected by this object. It is the responsibility of the caller of this method 
        /// to clear the returned byte array.
        /// </summary>
        public byte[] DecryptedKey
        {
            get
            {
                return this.Unprotect();
            }
        }
        
        /// <summary>
        /// Constructor method use to create a new <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see> from a plaintext symmetric key. The caller of this method
        /// is responsible for clearing the byte array containing the plaintext key after calling this method.
        /// </summary>
        /// <param name="plaintextKey">Plaintext key to protect. The caller of this method is responsible for 
        /// clearing the byte array containing the plaintext key after calling this method.</param>
        /// <param name="dataProtectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope"></see></param> used to protect this key.
        /// <returns>Initialized <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see> containing the plaintext key protected with DPAPI.</returns>
        public static ProtectedKey CreateFromPlaintextKey(byte[] plaintextKey, DataProtectionScope dataProtectionScope)
        {
            byte[] array = ProtectedData.Protect(plaintextKey, null, dataProtectionScope);
            return new ProtectedKey(array, dataProtectionScope);
        }
        
        /// <summary>
        /// Constructor method used to create a new <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey" /> from an already encrypted symmetric key.
        /// </summary>
        /// <param name="encryptedKey">Encrypted key to protect.</param>
        /// <param name="dataProtectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope"></see></param> used to protect this key.
        /// <returns>Initialized <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see> containing the plaintext key protected with DPAPI.</returns>
        public static ProtectedKey CreateFromEncryptedKey(byte[] encryptedKey, DataProtectionScope dataProtectionScope)
        {
            return new ProtectedKey(encryptedKey, dataProtectionScope);
        }
        
        /// <summary>
        /// Returns the decrypted symmetric key.
        /// </summary>
        /// <returns>Unencrypted symmetric key. It is the responsibility of the caller of this method to
        /// clear the returned byte array.</returns>
        public virtual byte[] Unprotect()
        {
            return ProtectedData.Unprotect(this.protectedKey, null, this.protectionScope);
        }

        private ProtectedKey(byte[] protectedKey, DataProtectionScope protectionScope)
        {
            this.protectionScope = protectionScope;
            this.protectedKey = protectedKey;
        }
    }
}
