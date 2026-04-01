using DataTransferObjects.Clients;
using Microsoft.AspNetCore.Components;
using Models;
using MudBlazor;
using SalesManager.Web.Interfaces;
using SalesManager.Web.Pages.Dialogs;
using SalesManager.Web.Services;
using SalesManager.Web.Shared;

namespace SalesManager.Web.Pages
{
    public class ClientsBase : ComponentBase
    {
        [Inject] IClientService ClientService { get; set; }
        [Inject] IDialogService DialogService { get; set; }
        [Inject] ISnackbar SnackbarService { get; set; }
        [Inject] ISessionDataService SessionDataService { get; set; }

        protected bool confirmDelete = false;
        protected int idDelete;

        protected string searchString = "";
        protected bool dense = false;
        protected bool hover = true;
        protected bool ronly = false;
        protected bool canCancelEdit = false;
        protected bool blockSwitch = false;
        protected bool statusClient = false;

        protected ClientPutDTO clientPutDTO = new ClientPutDTO();

        public IMask emailMask = RegexMask.Email();

        public bool IsLoading { get; set; } = false;

        protected List<ClientGetDTO> clientsGetDTO = new List<ClientGetDTO>();
        protected List<ClientGetDTO> clientsGetDTOToShow = new List<ClientGetDTO>();

        protected async override Task OnInitializedAsync()
        {
            await GetClientsAsync();
        }

        protected void ChangeStatusOption()
        {
            statusClient = !statusClient;
            FilterClientsToShow();
            StateHasChanged();
        }

        protected async Task GetClientsAsync()
        {
            IsLoading = true;
            StateHasChanged();

            clientsGetDTO = await ClientService.GetClientsAsync($"Clients?idUser={SessionDataService.GetIdUser()}");
            IsLoading = false;
            FilterClientsToShow();
            StateHasChanged();
        }

        private void FilterClientsToShow() => clientsGetDTOToShow = clientsGetDTO.Where(c => c.Status == (statusClient ? 0 : 1)).ToList();

        protected void OnButtonClicked(int clientId)
        {
            idDelete = clientId;

            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.ExtraSmall,
                FullWidth = true
            };

            var dialogParameters = new DialogParameters<ConfirmationDialog>
            {
                { "ConfirmAsync", EventCallback.Factory.Create(this, DeleteClientAsync) },
                { "ConfirmMessage", "Você tem certeza que quer deletar esse cliente?" },
                { "ConfirmButtonText", "Deletar!" }
            };

            DialogService.ShowAsync<ConfirmationDialog>("Cuidado", dialogParameters, dialogOptions);
        }

        protected async void DeleteClientAsync()
        {
            bool deleteClient = await ClientService.DeleteAsync($"Clients/{idDelete}");

            if (deleteClient)
            {
                await GetClientsAsync();

                string message = (clientsGetDTO.Any(c => c.Status == 0 && c.Id == idDelete)) ? "desativado" : "excluído";
                SnackbarService.Add($"Cliente {message} com sucesso", Severity.Success);
            }

            StateHasChanged();
        }

        protected bool FilterFunc(ClientGetDTO clientGetDTO)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (clientGetDTO.ClientName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (clientGetDTO.CreatedAt.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        protected async void ActiveClientsAsync(ClientGetDTO clientGetDTO)
        {
            clientPutDTO = new ClientPutDTO(clientGetDTO);
            clientPutDTO.Status = 1;

            bool updateSucessfull = await ClientService.UpdateAsync("Clients", clientPutDTO);

            if (updateSucessfull)
            {
                await GetClientsAsync();
                SnackbarService.Add("Cliente ativado com sucesso", Severity.Success);
            }

            StateHasChanged();
        }

        protected void ShowCreateModal()
        {
            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            DialogParameters dialogParameters = new DialogParameters<ClientCreateDialog>();
            dialogParameters.Add("GetClientsAsync", EventCallback.Factory.Create(this, GetClientsAsync));

            DialogService.ShowAsync<ClientCreateDialog>("Cadastrar cliente", dialogParameters, dialogOptions);
        }

        protected void ShowUpdateModal(ClientGetDTO clientGetDTO)
        {
            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            DialogParameters dialogParameters = new DialogParameters<ClientUpdateDialog>();
            dialogParameters.Add("ClientPutDTO", new ClientPutDTO(clientGetDTO));
            dialogParameters.Add("GetClientsAsync", EventCallback.Factory.Create(this, GetClientsAsync));

            DialogService.ShowAsync<ClientUpdateDialog>("Atualizar Cliente", dialogParameters, dialogOptions);
        }
        protected void ShowDetailsModal(ClientGetDTO clientGetDTO)
        {
            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            DialogParameters dialogParameters = new DialogParameters<ClientDetailsDialog>();
            dialogParameters.Add("ClientGetDTO", clientGetDTO);

            DialogService.ShowAsync<ClientDetailsDialog>(clientGetDTO.ClientName, dialogParameters, dialogOptions);
        }
    }
}
