using Kryptos.Web.Server.Constants;
using Kryptos.Web.Server.Services;
using Kryptos.Web.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kryptos.Web.Server.Controllers;

[Route("api/[controller]")]
public class PasswordCrackerController : ControllerBase
{
    private readonly IPasswordCrackerService _passwordCrackerService;
    private readonly IWebHostEnvironment _environment;

    public PasswordCrackerController(IPasswordCrackerService passwordCrackerService, IWebHostEnvironment environment)
    {
        _passwordCrackerService = passwordCrackerService;
        _environment = environment;
    }

    [HttpPost("Hash")]
    public async Task<string> Hash([FromBody] HashPasswordRequest hashPasswordRequest)
    {
        await Task.Delay(0);
        return _passwordCrackerService.Hash(hashPasswordRequest.HashAlgorithm, hashPasswordRequest.Password);
    }

    [HttpPost("BruteForce")]
    public async Task<CrackingResult> BruteForce([FromBody] BruteForceAttackRequest request)
    {
        var result = await Task.Run(() =>
            _passwordCrackerService.BruteForceAttack(request));

        return result;
    }

    [HttpPost("Dictionary")]
    public async Task<CrackingResult> Dictionary([FromBody] DictionaryAttackRequest request)
    {
        var path = Directory.GetCurrentDirectory();
        var lines = System.IO.File.ReadLines(Directory.GetCurrentDirectory() + FilePathConstants.DefaultDictionary);
        
        var result = await Task.Run(() => 
            _passwordCrackerService.DictionaryAttack(request.HashAlgorithm, request.Hash, lines));

        return result;
    }
}