namespace Puma.Prey.Common.Cryptography
{
    public class Random
    {
        public static byte[] GenerateCrytpoRandomBytes(int bytes)
        {
            byte[] numbers = new byte[bytes];
            System.Random random = new System.Random();
            random.NextBytes(numbers);
            return numbers;
        }
    }
}
