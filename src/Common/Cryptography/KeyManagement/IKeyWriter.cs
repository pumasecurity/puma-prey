﻿using System.IO;


namespace Puma.Prey.Common.Cryptography.KeyManagement
{
    /// <summary>
	/// Allows caller to a write a cryptographic key to a DPAPI-protected key file
	/// or to a password-protected key archive file.
	/// </summary>
	public interface IKeyWriter
    {
        /// <summary>
        /// Writes a cryptographic key to a DPAPI-protected <see cref="T:System.IO.Stream" />.
        /// </summary>
        /// <param name="outputStream"><see cref="T:System.IO.Stream" /> to which key is to be written.</param>
        /// <param name="key">Key to be written.</param>
        void Write(Stream outputStream, ProtectedKey key);

        /// <summary>
        /// Creates an archived version of the given <paramref name="key" />	written to the <paramref name="outputStream" />.
        /// </summary>
        /// <param name="outputStream"><see cref="T:System.IO.Stream" /> to which key is to be written.</param>
        /// <param name="key">Key to be written.</param>
        /// <param name="passphrase">User-provided passphrase used to encrypt the archive in the <see cref="T:System.IO.Stream" />.</param>
        void Archive(Stream outputStream, ProtectedKey key, string passphrase);
    }
}
