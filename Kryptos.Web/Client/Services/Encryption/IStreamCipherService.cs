namespace Kryptos.Web.Client.Services.Encryption;

public interface IStreamCipherService
{
    string Encrypt(string key, string secret);
    string Decrypt(string key, string encryptedMessage);
}