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
    
    /// <summary>
    /// Get the hashed input based on the selected hashing algorithm.
    /// </summary>
    /// <param name="algorithm">Hashing algorithm</param>
    /// <param name="input">string to hash</param>
    /// <returns>hash as a string</returns>
    public async Task<string> Hash(HashAlgorithm algorithm, string input)
    {
        // send request to server
       using var response = await _http.PostAsJsonAsync("api/PasswordCracker/Hash", 
            new HashPasswordRequest {HashAlgorithm = algorithm, Password = input});

        return await response.Content.ReadAsStringAsync(); //TODO: error handling
    }
    
    /// <summary>
    /// Performs a brute force attack on a hashed string with the given hash algorithm.
    /// </summary>
    /// <param name="request">Hash to brute force and hashing algorithm</param>
    /// <returns>actual value or failure message</returns>
    public async Task<CrackingResult> BruteForceAttack(BruteForceAttackRequest request)
    {
        // send request to server
        using var response = await _http.PostAsJsonAsync("api/PasswordCracker/BruteForce", request);

        return await response.Content.ReadFromJsonAsync<CrackingResult>() ?? new CrackingResult(); //TODO: error handling
    }
    
    /// <summary>
    /// Performs a dictionary attack on a hashed string with the given hash algorithm.
    /// </summary>
    /// <param name="request">Hash and hashing algorithm</param>
    /// <returns>actual value or failure message</returns>
    public async Task<CrackingResult> DictionaryAttack(DictionaryAttackRequest request)
    {
        // send request to server
        using var response = await _http.PostAsJsonAsync("api/PasswordCracker/Dictionary", request);

        return await response.Content.ReadFromJsonAsync<CrackingResult>() ?? new CrackingResult(); //TODO: error handling
    }
}