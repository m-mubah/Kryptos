﻿using Kryptos.Web.Client.Models.HammingCode;
using Kryptos.Web.Client.Services.HammingCode;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace Kryptos.Web.Client.Components.HammingCode.EncodeForm;

public partial class EncodeForm : ComponentBase
{
    [Inject] private IJSRuntime JsRuntime { get; set; }
    [Inject] private  IHammingCodeService HammingCodeService { get; set; }
    
    private EditContext EditContext;
    private EncodeSequence FormData { get; set; } = new();
    private string? ButtonDisabled { get; set; } = "disabled";

    private bool ShowResult { get; set; } = false;
    
    private EncodingResult OddResult { get; set; }
    private EncodingResult EvenResult { get; set; }
    
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
        
        OddResult = HammingCodeService.Encode(FormData.Value, Parity.Odd);
        EvenResult = HammingCodeService.Encode(FormData.Value, Parity.Even);

        ShowResult = true;
    }
}