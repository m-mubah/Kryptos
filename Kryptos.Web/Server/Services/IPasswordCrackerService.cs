using Kryptos.Web.Shared.Models;

namespace Kryptos.Web.Shared.Services;

public interface IPasswordCrackerService
{
    Task<string> Hash(HashAlgorithm algorithm, string input);
    Task<CrackingResult> BruteForceAttack(BruteForceAttackRequest request);
    Task<CrackingResult> DictionaryAttack(HashAlgorithm algorithm, string hash, IEnumerable<string> lines);
}