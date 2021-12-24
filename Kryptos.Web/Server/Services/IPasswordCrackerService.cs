using Kryptos.Web.Shared.Models;

namespace Kryptos.Web.Server.Services;

public interface IPasswordCrackerService
{
    string Hash(HashAlgorithm algorithm, string input);
    Task<CrackingResult> BruteForceAttack(BruteForceAttackRequest request);
    Task<CrackingResult> DictionaryAttack(HashAlgorithm algorithm, string hash, IEnumerable<string> lines);
}