using System.Text;

namespace Kryptos.Web.Client.Extensions;

public static class StringExtensions
{
    /// <summary>
    /// Converts a string to it's binary form and returns it as a string.
    /// </summary>
    /// <param name="data">string to be converted</param>
    /// <returns></returns>
    public static string ToBinary(this string data)
    {
        char[] buffer = new char[data.Length * 8];
        int index = 0;
        for (int i = 0; i < data.Length; i++)
        {
            string binary = Convert.ToString(data[i], 2).PadLeft(8, '0');
            for (int j = 0; j < 8; j++)
            {
                buffer[index] = binary[j];
                index++;
            }
        }
        return new string(buffer);
    }

    /// <summary>
    /// Converts a binary string (a string of 1s and 0s) to a string of words or characters.
    /// E.g: 0110100001100101011011000110110001101111 -> hello
    /// There must be no spaces between the 1s and 0s.
    /// </summary>
    /// <param name="binaryString">A string of 1s and 0s</param>
    /// <returns>Original string value</returns>
    public static string BinaryToString(this string binaryString)
    {
        Encoding  enc = Encoding.UTF8;

        var bytes = new byte[binaryString.Length / 8];

        var ilen = binaryString.Length / 8;			                

        for (var i = 0; i < ilen; i++)
        {                                       
            bytes[i] = Convert.ToByte(binaryString.Substring(i*8, 8), 2);
        }

        string str = enc.GetString(bytes);

        return str;
    }
    
    /// <summary>
    /// Returns a new string in which the first occurence of a specified string in the current instance is replaced with
    /// another specified string.
    /// </summary>
    /// <param name="text">Current text.</param>
    /// <param name="oldValue">The string to be replaced.</param>
    /// <param name="newValue">The string to replace the first occurence with.</param>
    /// <returns>A string that is equivalent to the current string except the first occurence of oldValue is
    /// replaced with newValue. If oldValue is not found in the current instance, the method returns current
    /// instance unchanged.</returns>
    public static string ReplaceFirst(this string text, string oldValue, string newValue)
    {
        int pos = text.IndexOf(oldValue, StringComparison.Ordinal);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + newValue + text.Substring(pos + oldValue.Length);
    }
}