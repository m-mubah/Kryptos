using Kryptos.Web.Client.Services.PasswordCracker;
using Kryptos.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.JSInterop;

namespace Kryptos.Web.Client.Components.PasswordCracker.BruteForceForm;

public partial class BruteForceForm : ComponentBase
{
    [Inject] private IPasswordCrackerClientService PasswordCrackerService { get; set; }
    [Inject] private NavigationManager NavigationManager { get; set; }
    
    private EditContext EditContext;
    private BruteForceAttackRequest FormData { get; set; } = new();
    private string? ButtonDisabled { get; set; } = "disabled";
    
    private HubConnection? HubConnection;
    public bool IsConnected =>
        HubConnection?.State == HubConnectionState.Connected;
    
    private CrackingResult CrackingResult { get; set; }
    private bool ShowResult { get; set; } = false;
    private bool RequestLoading { get; set; } = false;
    
    protected override async Task OnInitializedAsync()
    {
        EditContext = new EditContext(FormData);
        
        HubConnection = new HubConnectionBuilder().WithUrl(NavigationManager.ToAbsoluteUri("/password-cracker"))
            .Build();

        HubConnection.On<CrackingResult>("BruteForceResult", result =>
        {
            RequestLoading = false;
            ShowResult = true;
            CrackingResult = result;
            
            StateHasChanged();
        });

        await HubConnection.StartAsync();
        await base.OnInitializedAsync();
    }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);

        SetButtonDisabledStatus();
    }
    
    private void EditContext_OnFieldChanged(object? sender, FieldChangedEventArgs e)
    {
        SetButtonDisabledStatus();
    }
 
    private void SetButtonDisabledStatus()
    {
        ButtonDisabled = EditContext.Validate() ? null : "disabled";
    }

    private async Task ValidFormSubmit(EditContext editContext)
    {
        if (HubConnection is not null)
        {
            RequestLoading = true;
            if (ShowResult)
            {
                ShowResult = false;

                await Task.Delay(250);
            }
            
            await HubConnection.SendAsync("BruteForceAttack", FormData);
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