using Kryptos.Web.Shared.Models;

namespace Kryptos.Web.Server.Services;

public interface IPasswordCrackerService
{
    string Hash(HashAlgorithm algorithm, string input);
    CrackingResult BruteForceAttack(BruteForceAttackRequest request);
    CrackingResult DictionaryAttack(HashAlgorithm algorithm, string hash, IEnumerable<string> lines);
}