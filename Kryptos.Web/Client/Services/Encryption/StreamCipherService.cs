using System.Security.Cryptography;
using System.Text;
using Kryptos.Web.Client.Extensions;

namespace Kryptos.Web.Client.Services.Encryption;

public class StreamCipherService : IStreamCipherService
{
    /// <summary>
    /// Converts the key to a key stream. Key stream is binary value of 512 bits.
    /// </summary>
    /// <param name="key">Key to convert</param>
    /// <returns>A binary string</returns>
    private string GetKeyStream(string key)
    {
        using (SHA512 sha512 = SHA512.Create())
        {
            var bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(key));

            Random random = new Random(BitConverter.ToInt32(bytes));

            string binary = "";

            while (binary.Length < 512)
            {
                binary += Convert.ToString(random.Next(), 2);
            }

            return binary;
        }
    }

    /// <summary>
    /// Performs XOR operation on the key stream and binary string
    /// </summary>
    /// <param name="keyStream">Key stream - a string of 1s and 0s</param>
    /// <param name="binary">Binary string - a string of 1s and 0s</param>
    /// <returns>XOR result</returns>
    private string XOR(string keyStream, string binary)
    {
        List<int> result = new List<int>();
        
        // take each bit and apply XOR individually
        for (int i = 0; i < binary.Length; i++)
        {
            result.Add(binary[i] ^ keyStream[i]);
        }
        
        // convert the result to a string
        return string.Join("", result);
    }
    
    /// <summary>
    /// Encrypts the secret string using the key.
    /// </summary>
    /// <param name="key">Key to encrypt with</param>
    /// <param name="secret">Secret message</param>
    /// <returns>Encrypted binary string (a string of 1s and 0s)</returns>
    public string Encrypt(string key, string secret)
    {
        string keyStream = GetKeyStream(key);
        string binaryMessage = secret.ToBinary();

        return XOR(keyStream, binaryMessage);
    }

    /// <summary>
    /// Decrypts an encrypted message using the given key
    /// </summary>
    /// <param name="key">Key to decrypt with</param>
    /// <param name="encryptedMessage">A binary encrypted string.</param>
    /// <returns>Secret message</returns>
    public string Decrypt(string key, string encryptedMessage)
    {
        string keyStream = GetKeyStream(key);
        string decodedBinary = XOR(keyStream, encryptedMessage);

        return decodedBinary.BinaryToString();
    }
}