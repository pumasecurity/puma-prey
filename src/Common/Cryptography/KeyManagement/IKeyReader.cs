using System.IO;
using System.Security.Cryptography;

namespace Puma.Prey.Common.Cryptography.KeyManagement
{
    public interface IKeyReader
    {
        /// <summary>
        /// Reads a DPAPI protected key from the given <see cref="T:System.IO.Stream"></see>.
        /// </summary>
        /// <param name="protectedKeyContents"><see cref="T:System.IO.Stream"></see> containing the key to be read.</param>
        /// <param name="protectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope"></see> used to encrypt the generated key.</param>
        /// <returns>Key read from stream, encapsulated in a <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see>.</returns>
        ProtectedKey Read(Stream protectedKeyContents, DataProtectionScope protectionScope);

        /// <summary>
        /// Restores a key from an encrypted key archive <see cref="T:System.IO.Stream"></see>.
        /// </summary>
        /// <param name="protectedKeyContents"><see cref="T:System.IO.Stream"></see> containing the key to be read.</param>
        /// <param name="passphrase">User-provided passphrase used to encrypt the key in the archive for safekeeping. </param>
        /// <param name="protectionScope"><see cref="T:System.Security.Cryptography.DataProtectionScope"></see> used to encrypt the generated key.</param>
        /// <returns>Key read from stream, encapsulated in a <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey"></see>.</returns>
        ProtectedKey Restore(Stream protectedKeyContents, string passphrase, DataProtectionScope protectionScope);

        /// <summary>
        /// Restores a key from an encrypted key archive <see cref="T:System.IO.Stream"></see>.
        /// </summary>
        /// <param name="protectedKeyContents"><see cref="T:System.IO.Stream"></see> containing the key to be read.</param>
        /// <param name="passphrase">User-provided passphrase used to encrypt the key in the archive for safekeeping. </param>
        /// <returns>Unencrypted key read from stream.
        /// The caller of this method is responsible for clearing the contents of this byte array.</returns>
        byte[] Restore(Stream protectedKeyContents, string passphrase);
    }
}
