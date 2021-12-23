using System.Net.Http.Json;
using Kryptos.Web.Shared.Models;

namespace Kryptos.Web.Client.Services.PasswordCracker;

public class PasswordCrackerClientService : IPasswordCrackerClientService
{
    private readonly HttpClient _http;

    public PasswordCrackerClientService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> Hash(HashAlgorithm algorithm, string input)
    {
       using var response = await _http.PostAsJsonAsync("api/PasswordCracker/Hash", 
            new HashPasswordRequest {HashAlgorithm = algorithm, Password = input});

        return await response.Content.ReadAsStringAsync(); //TODO: error handling
    }

    public async Task<CrackingResult> BruteForceAttack(BruteForceAttackRequest request)
    {
        using var response = await _http.PostAsJsonAsync("api/PasswordCracker/BruteForce", request);

        return await response.Content.ReadFromJsonAsync<CrackingResult>() ?? new CrackingResult(); //TODO: error handling
    }
    
    public async Task<CrackingResult> DictionaryAttack(DictionaryAttackRequest request)
    {
        using var response = await _http.PostAsJsonAsync("api/PasswordCracker/Dictionary", request);

        return await response.Content.ReadFromJsonAsync<CrackingResult>() ?? new CrackingResult(); //TODO: error handling
    }
}