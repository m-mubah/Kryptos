using Microsoft.AspNetCore.SignalR;

namespace Kryptos.Web.Server.Hubs;

public class ChatHub : Hub
{
    public async Task SendMessage(string message, string username)
    {
        await Clients.Others.SendAsync("ReceiveMessage", message, username, DateTime.Now);
    }
}