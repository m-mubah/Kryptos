using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Kryptos.Web.Shared.Models;
using HashAlgorithm = Kryptos.Web.Shared.Models.HashAlgorithm;

namespace Kryptos.Web.Shared.Services;

public class PasswordCrackerService : IPasswordCrackerService
{
    public async Task<string> Hash(HashAlgorithm algorithm, string input)
    {
        await Task.Delay(0);

        input = input.ToLower();

        switch (algorithm)
        {
            case HashAlgorithm.Sha1:
                using (SHA1 sha1 = SHA1.Create())
                {
                    var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                    return string.Concat(Array.ConvertAll(hash, h => h.ToString("x2")));
                }
            case HashAlgorithm.Sha256:
                using (SHA256 sha256 = SHA256.Create())
                {
                    var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
                    return string.Concat(Array.ConvertAll(hash, h => h.ToString("x2")));
                }
            case HashAlgorithm.Sha384:
                using (SHA384 sha384 = SHA384.Create())
                {
                    var hash = sha384.ComputeHash(Encoding.UTF8.GetBytes(input));
                    return string.Concat(Array.ConvertAll(hash, h => h.ToString("x2")));
                }
            case HashAlgorithm.Sha512:
                using (SHA512 sha512 = SHA512.Create())
                {
                    var hash = sha512.ComputeHash(Encoding.UTF8.GetBytes(input));
                    return string.Concat(Array.ConvertAll(hash, h => h.ToString("x2")));
                }
            case HashAlgorithm.Md5:
                using (MD5 md5 = MD5.Create())
                {
                    var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                    return string.Concat(Array.ConvertAll(hash, h => h.ToString("x2")));
                }
            default:
                throw new ArgumentException("Invalid hash algorithm.");
        }
    }

    public async Task<CrackingResult> BruteForceAttack(BruteForceAttackRequest request)
    {
        string? generatedPassword = null;
        int wordCount = 0;

        Stopwatch timer = Stopwatch.StartNew();

        foreach (var word in GenerateWord())
        {
            wordCount++;
            string computedHash = await Hash(request.HashAlgorithm, word);

            if (computedHash == request.Hash)
            {
                generatedPassword = word;
                break;
            }
        }

        timer.Stop();
        TimeSpan elapsedTime = timer.Elapsed;

        return new CrackingResult
        {
            Password = generatedPassword ?? "",
            TotalTime =
                $"{elapsedTime.Hours:00}:{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}.{elapsedTime.Milliseconds / 10:00}",
            WordCount = wordCount,
            ErrorMessage = generatedPassword == null ? "Word not found." : null
        };
    }

    public async Task<CrackingResult> DictionaryAttack(HashAlgorithm algorithm, string hash, IEnumerable<string> lines)
    {
        string? generatedPassword = null;
        int wordCount = 0;

        Stopwatch timer = Stopwatch.StartNew();

        foreach (var line in lines)
        {
            var computedHash = await Hash(algorithm, line);
            wordCount++;

            if (computedHash == hash)
            {
                generatedPassword = line;
                break;
            }
        }
        
        timer.Stop();
        TimeSpan elapsedTime = timer.Elapsed;

        return new CrackingResult
        {
            Password = generatedPassword ?? "",
            TotalTime =
                $"{elapsedTime.Hours:00}:{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}.{elapsedTime.Milliseconds / 10:00}",
            WordCount = wordCount,
            ErrorMessage = generatedPassword == null ? "Word not found." : null
        };
    }

    private IEnumerable<string> GenerateWord(int maxLength = 6)
    {
        for (int length = 1; length <= maxLength; ++length)
        {
            StringBuilder stringBuilder = new StringBuilder(new String('0', length));
            while (true)
            {
                string word = stringBuilder.ToString();
                yield return word;

                if (word.All(item => item == 'z'))
                {
                    break;
                }

                for (int i = length - 1; i >= 0; --i)
                    if (stringBuilder[i] != 'z')
                    {
                        stringBuilder[i] = (Char) (stringBuilder[i] + 1);
                        break;
                    }
                    else
                    {
                        stringBuilder[i] = '0';
                    }
            }
        }
    }
}