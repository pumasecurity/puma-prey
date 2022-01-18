using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Puma.Prey.Common.Deserialize
{
    public class BinaryDeserialize
    {
        public static T GetObject<T>(byte[] bytes) where T : new()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                stream.Write(bytes, 0, bytes.Length);

                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}
