using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SalesManager.Web.Shared;

public class ConfirmationDialogBase : ComponentBase
{
    [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

    [Parameter] public string ConfirmMessage { get; set; } = "Tem certeza que deseja excluir?";
    [Parameter] public string AlertMessage { get; set; } = "Essa ação poderá ser irreversível!";
    [Parameter] public string CancelButtonText { get; set; } = "Cancelar";
    [Parameter] public Color CancelButtonColor { get; set; } = Color.Dark;
    [Parameter] public string ConfirmButtonText { get; set; } = "Confirmar";
    [Parameter] public Color ConfirmButtonColor { get; set; } = Color.Error;
    [Parameter] public EventCallback ConfirmAsync { get; set; }
    [Parameter] public EventCallback CancelAsync { get; set; }

    private bool _isBusy;

    protected async Task OnClickConfirmAsync()
    {
        if (_isBusy)
            return;

        _isBusy = true;

        await ConfirmAsync.InvokeAsync();
        MudDialog.Close();

        _isBusy = false;
    }

    protected async Task OnClickCancelAsync()
    {
        if (_isBusy)
            return;

        _isBusy = true;

        if (CancelAsync.HasDelegate)
            await CancelAsync.InvokeAsync();

        MudDialog.Cancel();

        _isBusy = false;
    }
}