using System.Collections;
using System.Diagnostics;
using System.Text;

namespace Kryptos.Web.Server.Services;

public class WordGeneratorService : IWordGeneratorService
{
    private StringBuilder _stringBuilder = new();
    public string charset = "abcdefghijklmnopqrstuvwxyz1234567890";
    public int Max { get; set; } = 6;
    public int Min { get; set; } = 1;
    
    private ulong _length;

    public IEnumerator<string> GetEnumerator()
    {
        _length = (ulong) charset.Length;
        for (double x = Min; x <= Max; x++)
        {
            ulong total = (ulong) Math.Pow(charset.Length, x);
            ulong counter = 0;
            while (counter < total)
            {
                string word = Factoradic(counter, x - 1);
                Debug.WriteLine(word);
                yield return word;
                counter++;
            }
        }
    }

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