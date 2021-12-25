using Kryptos.Web.Shared.Models;

namespace Kryptos.Web.Client.Services.PasswordCracker;

public interface IPasswordCrackerClientService
{
    /// <summary>
    /// Get the hashed input based on the selected hashing algorithm.
    /// </summary>
    /// <param name="algorithm">Hashing algorithm</param>
    /// <param name="input">string to hash</param>
    /// <returns>hash as a string</returns>
    Task<string> Hash(HashAlgorithm algorithm, string input);
    
    /// <summary>
    /// Performs a brute force attack on a hashed string with the given hash algorithm.
    /// </summary>
    /// <param name="request">Hash to brute force and hashing algorithm</param>
    /// <returns>actual value or failure message</returns>
    Task<CrackingResult> BruteForceAttack(BruteForceAttackRequest request);
    
    /// <summary>
    /// Performs a dictionary attack on a hashed string with the given hash algorithm.
    /// </summary>
    /// <param name="request">Hash and hashing algorithm</param>
    /// <returns>actual value or failure message</returns>
    Task<CrackingResult> DictionaryAttack(DictionaryAttackRequest request);
}