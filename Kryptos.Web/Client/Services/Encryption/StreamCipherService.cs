using System.Security.Cryptography;
using System.Text;
using Kryptos.Web.Client.Extensions;

namespace Kryptos.Web.Client.Services.Encryption;

public class StreamCipherService : IStreamCipherService
{
    private string GetKeyStream(string key, int length = 512)
    {
        using (SHA512 sha512 = SHA512.Create())
        {
            var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(key));

            Random random = new Random(BitConverter.ToInt32(bytes));

            string binary = "";

            while (binary.Length < length)
            {
                binary += Convert.ToString(random.Next(), 2);
            }

            return binary;
        }
    }

    private string XOR(string keyStream, string binary)
    {
        List<int> result = new List<int>();

        for (int i = 0; i < binary.Length; i++)
        {
            result.Add(binary[i] ^ keyStream[i]);
        }

        return string.Join("", result);
    }
    
    public string Encrypt(string key, string secret)
    {
        string keyStream = GetKeyStream(key);
        string binaryMessage = secret.ToBinary();

        return XOR(keyStream, binaryMessage);
    }

    public string Decrypt(string key, string encryptedMessage)
    {
        string keyStream = GetKeyStream(key);
        string decodedBinary = XOR(keyStream, encryptedMessage);

        return decodedBinary.BinaryToString();
    }
}