using Kryptos.Web.Client.Models.Luhn;
using Kryptos.Web.Client.Services.Luhn;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Kryptos.Web.Client.Components.Luhn.ValidateCardForm;

public partial class ValidateCardForm : ComponentBase
{
    [Inject] private  ILuhnService LuhnService { get; set; }
    
    private EditContext EditContext;
    private ValidateCard FormData { get; set; } = new();
    private string? ButtonDisabled { get; set; } = "disabled";
    private bool ShowResult { get; set; } = false;
    
    private ValidationResult Result { get; set; }
    
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

        if (LuhnService.Validate(FormData.CardNumber))
        {
            string issuer = LuhnService.GetCreditCardManufacturer(FormData.CardNumber);
            string industry = LuhnService.GetMajorIndustryIdentifier(FormData.CardNumber);
                
            Result = new ValidationResult(true, FormData.CardNumber, issuer, industry);
        }
        else
        {
            Result = new ValidationResult(false);
        }
        
        // OddResult = HammingCodeService.Encode(FormData.Value, Parity.Odd);
        // EvenResult = HammingCodeService.Encode(FormData.Value, Parity.Even);

        ShowResult = true;
    }
}