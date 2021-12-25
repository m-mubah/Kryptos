using Kryptos.Web.Shared.Models;

namespace Kryptos.Web.Server.Services;

public interface IPasswordCrackerService
{
    /// <summary>
    /// Get the hashed input based on the selected hashing algorithm.
    /// </summary>
    /// <param name="algorithm">Hashing algorithm</param>
    /// <param name="input">string to hash</param>
    /// <returns>hash as a string</returns>
    string Hash(HashAlgorithm algorithm, string input);
    
    /// <summary>
    /// Performs a brute force attack on a hashed string with the given hash algorithm.
    /// </summary>
    /// <param name="request">Hash to brute force and hashing algorithm</param>
    /// <returns>actual value or failure message</returns>
    CrackingResult BruteForceAttack(BruteForceAttackRequest request);
    
    /// <summary>
    /// Performs a dictionary attack on a hashed string with the given hash algorithm.
    /// </summary>
    /// <param name="algorithm">Hash algorithm</param>
    /// <param name="hash">Hash string</param>
    /// <param name="lines">An enumerable of strings from the dictionary</param>
    /// <returns>actual value or failure message</returns>
    CrackingResult DictionaryAttack(HashAlgorithm algorithm, string hash, IEnumerable<string> lines);
}