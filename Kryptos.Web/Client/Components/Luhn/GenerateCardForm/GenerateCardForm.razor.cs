using Kryptos.Web.Client.Models.Luhn;
using Kryptos.Web.Client.Services.Luhn;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Kryptos.Web.Client.Components.Luhn.GenerateCardForm;

public partial class GenerateCardForm : ComponentBase
{
    [Inject] public ILuhnService LuhnService { get; set; }

    private EditContext EditContext;
    private GenerateCard FormData { get; set; } = new();
    private string? ButtonDisabled { get; set; } = "disabled";

    private CreditCard Result { get; set; }
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

        Result = LuhnService.Generate(FormData.CardManufacturer);
        ShowResult = true;
    }
}