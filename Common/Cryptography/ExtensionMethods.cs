using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Puma.Prey.Common.Cryptography
{
    public static class ExtensionMethods
    {
        public static string ToHexString(this byte[] bytes)
        {
            SoapHexBinary shb = new SoapHexBinary(bytes);
            return shb.ToString();
        }

        public static byte[] GetBytesFromHexString(this string hexString)
        {
            SoapHexBinary shb = SoapHexBinary.Parse(hexString);
            return shb.Value;
        }
    }
}
