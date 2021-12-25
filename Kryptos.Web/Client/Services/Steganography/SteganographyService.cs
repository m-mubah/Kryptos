using Kryptos.Web.Client.Extensions;

namespace Kryptos.Web.Client.Services.Steganography;

public class SteganographyService : ISteganographyService
{
    // mappings of unicode invisible characters to binary
    private Dictionary<string, string> Codes = new()
    {
        {"00", "\u200C"},
        {"01", "\u200D"},
        {"10", "\u2060"},
        {"11", "\u2062"}
    };

    /// <summary>
    /// Hides a given secret inside the message
    /// </summary>
    /// <param name="message">cover text</param>
    /// <param name="secret">secret text</param>
    /// <returns>cover text with secret hidden</returns>
    public string Conceal(string message, string secret)
    {
        List<string> output = new List<string>();
        // map the binary to the invisible characters
        for (int i = 0; i < secret.Length; i += 2)
        {
            output.Add(Codes[secret.Substring(i, i + 2 - i)]);
        }
        
        // combines the list into a single string
        string hidden = string.Join("", output);
        
        // if the message has white-space append to the first white space
        if (message.Contains(" "))
        {
            return message.ReplaceFirst(" ", " " + hidden);
        }
        
        // otherwise place it before the message
        return message.Substring(0, 1) + hidden + message.Substring(1);
    }

    /// <summary>
    /// Reveals a secret text from cover text
    /// </summary>
    /// <param name="message">cover text with hidden secret</param>
    /// <returns>secret message</returns>
    public string Reveal(string message)
    {   
        // select the parts of the message with invisible characters
        string parts = message.Contains(" ") ? message.Split(" ")[1] : message.Substring(1);

        List<string> output = new List<string>();

        for (int i = 0; i < parts.Length; i++)
        {
            string code = parts[i].ToString();
            
            // get the binary that corresponds to a given code
            string binary = Codes.FirstOrDefault(c => c.Value == code).Key;

            output.Add(binary);
        }
        
        // convert to a single string
        return string.Join("", output);
    }
}