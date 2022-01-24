using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace Puma.Prey.Common.Cryptography
{
    public static class ExtensionMethods
    {
        public static string ToHexString(this byte[] bytes)
        {
            var shb = new SoapHexBinary(bytes);
            return shb.ToString();
        }

        public static byte[] GetBytesFromHexString(this string hexString)
        {
            var shb = SoapHexBinary.Parse(hexString);
            return shb.Value;
        }
    }
}
