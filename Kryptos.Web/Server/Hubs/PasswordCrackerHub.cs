using Kryptos.Web.Server.Constants;
using Kryptos.Web.Server.Services;
using Kryptos.Web.Shared.Models;
using Microsoft.AspNetCore.SignalR;

namespace Kryptos.Web.Server.Hubs;

public class PasswordCrackerHub : Hub
{
    private readonly IPasswordCrackerService _passwordCrackerService;

    public PasswordCrackerHub(IPasswordCrackerService passwordCrackerService)
    {
        _passwordCrackerService = passwordCrackerService;
    }


    public async Task BruteForceAttack(BruteForceAttackRequest request)
    {
        var result = await Task.Run(() =>
            _passwordCrackerService.BruteForceAttack(request));
        
        await Clients.Client(Context.ConnectionId)
            .SendAsync("BruteForceResult", result);
    }

    public async Task DictionaryAttack(DictionaryAttackRequest request)
    {
        var lines = File.ReadLines(Directory.GetCurrentDirectory() + FilePathConstants.DefaultDictionary);
        
        var result = await Task.Run(() => 
            _passwordCrackerService.DictionaryAttack(request.HashAlgorithm, request.Hash, lines));
        
        await Clients.Client(Context.ConnectionId)
            .SendAsync("DictionaryResult", result);
    }
}