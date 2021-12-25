namespace Kryptos.Web.Client.Services.Encryption;

public interface IStreamCipherService
{
    /// <summary>
    /// Encrypts the secret string using the key.
    /// </summary>
    /// <param name="key">Key to encrypt with</param>
    /// <param name="secret">Secret message</param>
    /// <returns>Encrypted binary string (a string of 1s and 0s)</returns>
    string Encrypt(string key, string secret);
    
    /// <summary>
    /// Decrypts an encrypted message using the given key
    /// </summary>
    /// <param name="key">Key to decrypt with</param>
    /// <param name="encryptedMessage">A binary encrypted string.</param>
    /// <returns>Secret message</returns>
    string Decrypt(string key, string encryptedMessage);
}