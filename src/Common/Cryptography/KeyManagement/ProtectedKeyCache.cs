using System;
using System.Collections.Generic;
using System.Threading;

namespace Puma.Prey.Common.Cryptography.KeyManagement
{
    /// <summary>
	/// Represents the protected key cache.
	/// </summary>
	public class ProtectedKeyCache
    {
        private readonly Dictionary<string, ProtectedKey> cache = new Dictionary<string, ProtectedKey>();
        /// <summary>
        /// Gets the protected ke for the key file name.
        /// </summary>
        /// <param name="keyFileName">The key file name to get the key.</param>
        /// <returns>A <see cref="T:Microsoft.Practices.EnterpriseLibrary.Security.Cryptography.ProtectedKey" /> for the key file name.</returns>
        public ProtectedKey this[string keyFileName]
        {
            get
            {
                if (string.IsNullOrEmpty(keyFileName))
                {
                    throw new ArgumentException("keyFileName");
                }
                Dictionary<string, ProtectedKey> obj;
                Monitor.Enter(obj = this.cache);
                ProtectedKey result;
                try
                {
                    result = (this.cache.ContainsKey(keyFileName) ? this.cache[keyFileName] : null);
                }
                finally
                {
                    Monitor.Exit(obj);
                }
                return result;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                if (string.IsNullOrEmpty(keyFileName))
                {
                    throw new ArgumentException("keyFileName");
                }
                Dictionary<string, ProtectedKey> obj;
                Monitor.Enter(obj = this.cache);
                try
                {
                    this.cache[keyFileName] = value;
                }
                finally
                {
                    Monitor.Exit(obj);
                }
            }
        }

        /// <summary>
        ///  Clear the cache.
        /// </summary>
        public void Clear()
        {
            Dictionary<string, ProtectedKey> obj;
            Monitor.Enter(obj = this.cache);
            try
            {
                this.cache.Clear();
            }
            finally
            {
                Monitor.Exit(obj);
            }
        }
    }
}
