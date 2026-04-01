using DataTransferObjects.Clients;
using DataTransferObjects.Departments;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Pages.Dialogs
{
    public class ClientUpdateDialogBase : ComponentBase
    {
        [Inject] ISnackbar SnackbarService { get; set; }
        [Inject] IClientService ClientService { get; set; }

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

        [Parameter] public ClientPutDTO ClientPutDTO { get; set; }
        [Parameter] public EventCallback GetClientsAsync { get; set; }

        protected bool PageIsReady { get; set; }

        protected override void OnInitialized() => PageIsReady = true;

        protected async void UpdateClientAsync()
        {
            PageIsReady = !PageIsReady;

            bool createSucessfull = await ClientService.UpdateAsync("Clients", ClientPutDTO);

            if (createSucessfull)
            {
                SnackbarService.Add("Cliente atualizado com sucesso", Severity.Success);

                await GetClientsAsync.InvokeAsync();
                StateHasChanged();
                MudDialog.Close();
            }

            PageIsReady = !PageIsReady;
        }

        protected void Cancel() => MudDialog.Cancel();
    }
}
