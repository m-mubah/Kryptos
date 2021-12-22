using Kryptos.Web.Client.Extensions;

namespace Kryptos.Web.Client.Services.Steganography;

public class SteganographyService : ISteganographyService
{
    private Dictionary<string, string> Codes = new()
    {
        {"00", "\u200C"},
        {"01", "\u200D"},
        {"10", "\u2060"},
        {"11", "\u2062"}
    };

    public string Conceal(string message, string secret)
    {
        List<string> output = new List<string>();
        for (int i = 0; i < secret.Length; i += 2)
        {
            output.Add(Codes[secret.Substring(i, i + 2 - i)]);
        }

        string hidden = string.Join("", output);

        if (message.Contains(" "))
        {
            return message.ReplaceFirst(" ", " " + hidden);
        }

        return message.Substring(0, 1) + hidden + message.Substring(1);
    }

    public string Reveal(string message)
    {
        string parts = message.Contains(" ") ? message.Split(" ")[1] : message.Substring(1);

        List<string> output = new List<string>();

        for (int i = 0; i < parts.Length; i++)
        {
            string code = parts[i].ToString();
            string binary = Codes.FirstOrDefault(i => i.Value == code).Key;

            output.Add(binary);
        }

        return string.Join("", output);
    }
}