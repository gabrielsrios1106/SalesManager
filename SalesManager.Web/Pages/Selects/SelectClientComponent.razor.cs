using DataTransferObjects.Clients;
using Microsoft.AspNetCore.Components;
using SalesManager.Web.Interfaces;
using SalesManager.Web.Services;

namespace SalesManager.Web.Pages.Selects
{
    public class SelectClientComponentBase : ComponentBase
    {
        [Parameter] public string ClientSelected { get; set; } = string.Empty;
        [Parameter] public EventCallback<int> ChangeClient { get; set; }
        [Inject] IClientService ClientService { get; set; }
        [Inject] ISessionDataService SessionDataService { get; set; }

        protected List<ClientGetDTO> clientsGetDTO = new List<ClientGetDTO>();

        protected bool PageIsReady { get; set; }

        protected async override Task OnInitializedAsync() => await GetClientsAsync();

        private async Task GetClientsAsync()
        {
            clientsGetDTO = await ClientService.GetClientsAsync($"Clients?idUser={SessionDataService.GetIdUser()}&showInactive=false");

            PageIsReady = true;
            StateHasChanged();
        }

        protected async Task OnChangeClientAsync(string selectedClient)
        {
            if (int.TryParse(selectedClient, out int clientId) && ChangeClient.HasDelegate)
            {
                ClientSelected = selectedClient;
                await ChangeClient.InvokeAsync(clientId);
            }
        }
    }
}
