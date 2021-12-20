using Kryptos.Web.Client.Models.SecretMessage;
using Kryptos.Web.Client.Services.Encryption;
using Kryptos.Web.Client.Services.Steganography;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Kryptos.Web.Client.Components.SecretMessage.ConcealForm;

public partial class ConcealForm : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; }
    [Inject] private IStreamCipherService StreamCipherService { get; set; }
    [Inject] private ISteganographyService SteganographyService { get; set; }
    private EditContext EditContext;
    private ConcealMessage FormData { get; set; } = new();
    private string? ButtonDisabled { get; set; } = "disabled";

    private bool ShowResult { get; set; } = false;

    protected override void OnInitialized()
    {
        EditContext = new EditContext(FormData);
        EditContext.OnFieldChanged += EditContext_OnFieldChanged;

        base.OnInitialized();
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
        if (ShowResult)
        {
            ShowResult = false;

            await Task.Delay(250);
        }

        string encryptedBinary = StreamCipherService.Encrypt(FormData.Key, FormData.Secret);
        string messageWithSecret = SteganographyService.Conceal(FormData.Message, encryptedBinary);
        string extractedSecret = SteganographyService.Reveal(messageWithSecret);
        string decryptedSecret = StreamCipherService.Decrypt(FormData.Key, extractedSecret);
        await JsRuntime.InvokeVoidAsync("alert", decryptedSecret);

        ShowResult = true;
    }
}