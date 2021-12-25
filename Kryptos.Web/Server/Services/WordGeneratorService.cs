using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Kryptos.Web.Server.Services;

public class WordGeneratorService : IWordGeneratorService
{
    private StringBuilder _stringBuilder = new();
    public string charset = "abcdefghijklmnopqrstuvwxyz1234567890"; // character set
    public int Max { get; set; } = 6; // maximum string length
    public int Min { get; set; } = 1; // minimum string length
    
    private ulong _length;
    
    // Gets each string
    public IEnumerator<string> GetEnumerator()
    {
        _length = (ulong) charset.Length;
        for (double x = Min; x <= Max; x++)
        {
            ulong total = (ulong) Math.Pow(charset.Length, x); // total possible combinations
            ulong counter = 0;
            while (counter < total) // loop until total number of combinations is reached
            {
                string word = Factoradic(counter, x - 1); // get a new word
                yield return word;
                counter++; // increment
            }
        }
    }

    /// <summary>
    /// Any combination of a subset of characters can be translated to a unique number representing that combination.
    /// Since the number is unique, it is possible to get the characters that result in the combination.
    /// Factoradic (Factorial number system) is used to calculate the unique number.
    /// This method then finds the unique combination that results in the combination.
    /// </summary>
    /// <param name="l"></param>
    /// <param name="power">length of the string</param>
    /// <returns>index of the Factoradic of length n-1</returns>
    private string Factoradic(ulong l, double power)
    {
        _stringBuilder.Length = 0;
        while (power >= 0)
        {
            _stringBuilder = _stringBuilder.Append(charset[(int) (l % _length)]);
            l /= _length;
            power--;
        }

        return _stringBuilder.ToString();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}