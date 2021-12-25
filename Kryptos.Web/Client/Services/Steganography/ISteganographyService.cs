namespace Kryptos.Web.Client.Services.Steganography;

public interface ISteganographyService
{
    /// <summary>
    /// Hides a given secret inside the message
    /// </summary>
    /// <param name="message">cover text</param>
    /// <param name="secret">secret text</param>
    /// <returns>cover text with secret hidden</returns>
    string Conceal(string message, string secret);
    
    /// <summary>
    /// Reveals a secret text from cover text
    /// </summary>
    /// <param name="message">cover text with hidden secret</param>
    /// <returns>secret message</returns>
    string Reveal(string message);
}