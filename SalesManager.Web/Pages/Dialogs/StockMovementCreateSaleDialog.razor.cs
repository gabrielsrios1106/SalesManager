using DataTransferObjects.Products;
using DataTransferObjects.StockMovement;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Pages.Dialogs
{
    public class StockMovementCreateSaleDialogBase : ComponentBase
    {
        [Inject] ISnackbar SnackbarService { get; set; }
        [Inject] IStockMovementService StockMovementService { get; set; }
        [Inject] IProductService ProductService { get; set; }
        [Inject] ISessionDataService SessionDataService { get; set; }

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

        [Parameter] public EventCallback GetStockMovementsAsync { get; set; }

        protected StockMovementSalePostDTO StockMovementSalePostDTO { get; set; }

        protected bool PageIsReady { get; set; }

        protected override void OnInitialized()
        {
            StockMovementSalePostDTO = new StockMovementSalePostDTO(SessionDataService.GetIdUser());
            PageIsReady = true;
        }

        protected async void CreateStockMovementSaleAsync()
        {
            PageIsReady = !PageIsReady;

            bool createSucessfull = await StockMovementService.CreateSaleAsync("StockMovement", StockMovementSalePostDTO);

            if (createSucessfull)
            {
                SnackbarService.Add("Venda cadastrado com sucesso", Severity.Success);

                await GetStockMovementsAsync.InvokeAsync();
                MudDialog.Close();
            }

            StateHasChanged();
            PageIsReady = !PageIsReady;
        }

        protected void ChangeClientIdCreateSale(int clientId)
        {
            if (StockMovementSalePostDTO is null) return;

            StockMovementSalePostDTO.ClientId = clientId;
        }

        protected async void ChangeProductIdCreateSale(int productId)
        {
            if (StockMovementSalePostDTO is null) return;

            StockMovementSalePostDTO.ProductId = productId;

            ProductGetDTO productGetDTO = await ProductService.GetProductByIdAsync($"Products/GetProductById/{productId}");

            if (productGetDTO != null)
            {
                StockMovementSalePostDTO.SalePrice = productGetDTO.Price;
                StateHasChanged();
                Console.WriteLine("passei aqui");
            }
        }

        protected void Cancel() => MudDialog.Cancel();
    }
}
