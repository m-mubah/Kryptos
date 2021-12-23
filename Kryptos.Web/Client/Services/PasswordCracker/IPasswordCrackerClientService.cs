using Kryptos.Web.Shared.Models;

namespace Kryptos.Web.Client.Services.PasswordCracker;

public interface IPasswordCrackerClientService
{
    Task<string> Hash(HashAlgorithm algorithm, string input);
    Task<CrackingResult> BruteForceAttack(BruteForceAttackRequest request);
    Task<CrackingResult> DictionaryAttack(DictionaryAttackRequest request);
}