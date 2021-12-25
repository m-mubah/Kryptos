using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using Kryptos.Web.Shared.Models;
using HashAlgorithm = Kryptos.Web.Shared.Models.HashAlgorithm;

namespace Kryptos.Web.Server.Services;

public class PasswordCrackerService : IPasswordCrackerService
{
    private IWordGeneratorService _wordGenerator;

    public PasswordCrackerService(IWordGeneratorService wordGenerator)
    {
        _wordGenerator = wordGenerator;
    }

    /// <summary>
    /// Get the hashed input based on the selected hashing algorithm.
    /// </summary>
    /// <param name="algorithm">Hashing algorithm</param>
    /// <param name="input">string to hash</param>
    /// <returns>hash as a string</returns>
    public string Hash(HashAlgorithm algorithm, string input)
    {
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

    /// <summary>
    /// Performs a brute force attack on a hashed string with the given hash algorithm.
    /// </summary>
    /// <param name="request">Hash to brute force and hashing algorithm</param>
    /// <returns>actual value or failure message</returns>
    public CrackingResult BruteForceAttack(BruteForceAttackRequest request)
    {
        string? generatedPassword = null;
        int wordCount = 0;

        Stopwatch timer = Stopwatch.StartNew(); // start a timer
        
        // the task is parallelized to improve throughput
        Parallel.ForEach(_wordGenerator, (word, state) =>
        {
            wordCount++;
            string computedHash = Hash(request.HashAlgorithm, word);

            if (computedHash == request.Hash)
            {
                generatedPassword = word;
                state.Break();
            }
        });

        timer.Stop(); // stop the timer
        TimeSpan elapsedTime = timer.Elapsed; // get passed time

        return new CrackingResult
        {
            Password = generatedPassword ?? "",
            TotalTime = // convert to format hh:mm:ss.ms
                $"{elapsedTime.Hours:00}:{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}.{elapsedTime.Milliseconds / 10:00}",
            WordCount = wordCount,
            ErrorMessage = generatedPassword == null ? "Word not found." : null,
        };
    }

    /// <summary>
    /// Performs a dictionary attack on a hashed string with the given hash algorithm.
    /// </summary>
    /// <param name="algorithm">Hash algorithm</param>
    /// <param name="hash">Hash string</param>
    /// <param name="lines">An enumerable of strings from the dictionary</param>
    /// <returns>actual value or failure message</returns>
    public CrackingResult DictionaryAttack(HashAlgorithm algorithm, string hash, IEnumerable<string> lines)
    {
        string? generatedPassword = null;
        int wordCount = 0;

        Stopwatch timer = Stopwatch.StartNew(); // start a timer
        
        // the task is parallelized to improve throughput
        Parallel.ForEach(lines, (word, state) =>
        {
            wordCount++;
            string computedHash = Hash(algorithm, word);

            if (computedHash == hash)
            {
                generatedPassword = word;
                state.Break();
            }
        });

        timer.Stop(); // stop the timer
        TimeSpan elapsedTime = timer.Elapsed; // get passed time

        return new CrackingResult
        {
            Password = generatedPassword ?? "",
            TotalTime = // convert to format hh:mm:ss.ms
                $"{elapsedTime.Hours:00}:{elapsedTime.Minutes:00}:{elapsedTime.Seconds:00}.{elapsedTime.Milliseconds / 10:00}",
            WordCount = wordCount,
            ErrorMessage = generatedPassword == null ? "Word not found." : null
        };
    }
}