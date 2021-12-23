﻿using Kryptos.Web.Client.Services.PasswordCracker;
using Kryptos.Web.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Kryptos.Web.Client.Components.PasswordCracker.BruteForceForm;

public partial class BruteForceForm : ComponentBase
{
    [Inject] private IPasswordCrackerClientService PasswordCrackerService { get; set; }
    private EditContext EditContext;
    private BruteForceAttackRequest FormData { get; set; } = new();
    private string? ButtonDisabled { get; set; } = "disabled";

    private CrackingResult CrackingResult { get; set; }
    private bool ShowResult { get; set; } = false;
    private bool RequestLoading { get; set; } = false;
    
    protected override void OnInitialized()
    {
        EditContext = new EditContext(FormData);
        
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
        RequestLoading = true;
        if (ShowResult)
        {
            ShowResult = false;

            await Task.Delay(250);
        }

        CrackingResult = await PasswordCrackerService.BruteForceAttack(FormData);
        
        ShowResult = true;
        RequestLoading = false;
    }
}