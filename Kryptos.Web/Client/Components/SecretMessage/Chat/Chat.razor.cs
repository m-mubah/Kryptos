using Kryptos.Web.Client.Models.SecretMessage;
using Kryptos.Web.Client.Services.Encryption;
using Kryptos.Web.Client.Services.Steganography;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;

namespace Kryptos.Web.Client.Components.SecretMessage.Chat;

public partial class Chat : ComponentBase
{
    [Inject] private IStreamCipherService StreamCipherService { get; set; }
    [Inject] private ISteganographyService SteganographyService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    
    private EditContext ChatConcealMessageEditContext;
    private ChatConcealMessage ConcealMessageFormData { get; set; } = new();
    private bool ShowResult { get; set; } = false;
    private EditContext ChatMessageEditContext;
    private ReadyToSendMessage ReadyToSendMessage { get; set; } = new();

    private HubConnection? HubConnection;
    public bool IsConnected =>
        HubConnection?.State == HubConnectionState.Connected;
    
    private List<ChatMessage> Messages = new();

    protected override async Task OnInitializedAsync()
    {
        ChatConcealMessageEditContext = new EditContext(ConcealMessageFormData);
        ChatMessageEditContext = new EditContext(SendMessage);

        HubConnection = new HubConnectionBuilder().WithUrl(NavigationManager.ToAbsoluteUri("/chat"))
            .Build();

        HubConnection.On<string, string, DateTime>("ReceiveMessage", (message, username, time) =>
        {
            Messages.Add(new()
            {
                Message = message,
                Username = username,
                SentTime = time,
                SentMessage = false
            });

            StateHasChanged();
        });

        await HubConnection.StartAsync();
        
        await base.OnInitializedAsync();
    }

    private void GenerateSecretMessage(EditContext editContext)
    {
        string encryptedBinary = StreamCipherService.Encrypt(ConcealMessageFormData.Key, ConcealMessageFormData.Secret);
        ReadyToSendMessage.Message = SteganographyService.Conceal(ConcealMessageFormData.Message, encryptedBinary);
    }

    private async Task SendMessage()
    {
        if (HubConnection is not null)
        {
            await HubConnection
                .SendAsync("SendMessage", ReadyToSendMessage.Message, ConcealMessageFormData.Name);
            
            Messages.Add(new()
            {
                Message = ReadyToSendMessage.Message,
                Username = ConcealMessageFormData.Name,
                SentTime = DateTime.Now,
                SentMessage = true
            });

            StateHasChanged();
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (HubConnection is not null)
        {
            await HubConnection.DisposeAsync();
        }
    }
}