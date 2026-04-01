using DataTransferObjects.StockMovement;
using Microsoft.AspNetCore.Components;
using Models;
using MudBlazor;
using SalesManager.Web.Interfaces;
using SalesManager.Web.Pages.Dialogs;
using SalesManager.Web.Services;
using SalesManager.Web.Shared;
using System.Globalization;

namespace SalesManager.Web.Pages
{
    public class StockMovementBase : ComponentBase
    {
        [Inject] IStockMovementService StockMovementService { get; set; }
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
        protected StockMovementGetDTO stockMovementGetDTO = new StockMovementGetDTO();
        protected StockMovementPurchasePostDTO stockMovementPurchasePostDTO;
        protected StockMovementSalePostDTO stockMovementSalePostDTO;

        protected CultureInfo CultureInfoPtBR { get; set; } = new CultureInfo("pt-BR");

        public bool IsVisiblePurchase { get; set; } = false;
        public bool IsVisibleSale { get; set; } = false;
        public bool IsLoading { get; set; } = false;

        protected List<StockMovementGetDTO> stockMovementsGetDTO = new List<StockMovementGetDTO>();

        protected async override Task OnInitializedAsync()
        {
            await GetStockMovementAsync();
        }

        protected async Task GetStockMovementAsync()
        {
            IsLoading = true;
            StateHasChanged();

            stockMovementsGetDTO = await StockMovementService.GetStockMovementAsync($"StockMovement?idUser={SessionDataService.GetIdUser()}");
            IsLoading = false;
            StateHasChanged();
        }

        protected void OnButtonClicked(int stockMovementId)
        {
            idDelete = stockMovementId;

            var dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.ExtraSmall,
                FullWidth = true
            };

            var dialogParameters = new DialogParameters<ConfirmationDialog>
            {
                { "ConfirmAsync", EventCallback.Factory.Create(this, DeleteProductAsync) },
                { "ConfirmMessage", "Você tem certeza que quer deletar esse produto?" },
                { "ConfirmButtonText", "Deletar!" }
            };

            DialogService.ShowAsync<ConfirmationDialog>("Cuidado", dialogParameters, dialogOptions);
        }

        protected async void DeleteProductAsync()
        {
            bool deleteProduct = await StockMovementService.DeleteAsync($"StockMovement/{idDelete}");

            if (deleteProduct)
            {
                SnackbarService.Add("Venda excluída com sucesso", Severity.Success);
            }
            else
            {
                SnackbarService.Add("Nao foi possivel excluir essa ação", Severity.Error);
            }

            await GetStockMovementAsync();
            StateHasChanged();
        }

        protected void OpenClosePurchaseModal() => IsVisiblePurchase = !IsVisiblePurchase;
        protected void OpenCloseSaleModal() => IsVisibleSale = !IsVisibleSale;

        protected bool FilterFunc(StockMovementGetDTO stockMovementGetDTO)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (stockMovementGetDTO.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (stockMovementGetDTO.CreatedAt.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (stockMovementGetDTO.MovementType.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        protected async void CreateStockMovementPurchaseAsync()
        {
            bool createSucessfull = await StockMovementService.CreatePurchaseAsync("StockMovement", stockMovementPurchasePostDTO);

            if (createSucessfull)
            {
                OpenClosePurchaseModal();
                SnackbarService.Add("Compra cadastrada com sucesso", Severity.Success);
            }

            await GetStockMovementAsync();
        }

        protected async void CreateStockMovementSaleAsync()
        {
            bool createSucessfull = await StockMovementService.CreateSaleAsync("StockMovement", stockMovementSalePostDTO);

            if (createSucessfull)
            {
                OpenCloseSaleModal();
                SnackbarService.Add("Venda cadastrada com sucesso", Severity.Success);
            }

            await GetStockMovementAsync();
        }

        protected void ShowCreatePurchaseModal()
        {
            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };

            DialogParameters dialogParameters = new DialogParameters<StockMovementCreatePurchaseDialog>();
            dialogParameters.Add("GetStockMovementsAsync", EventCallback.Factory.Create(this, GetStockMovementAsync));

            DialogService.ShowAsync<StockMovementCreatePurchaseDialog>("Reposição de produto", dialogParameters, dialogOptions);
        }

        protected void ShowCreateSaleModal()
        {
            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };

            DialogParameters dialogParameters = new DialogParameters<StockMovementCreateSaleDialog>();
            dialogParameters.Add("GetStockMovementsAsync", EventCallback.Factory.Create(this, GetStockMovementAsync));

            DialogService.ShowAsync<StockMovementCreateSaleDialog>("Cadastro de venda", dialogParameters, dialogOptions);
        }
    }
}
