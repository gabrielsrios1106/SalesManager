using DataTransferObjects.Products;
using Microsoft.AspNetCore.Components;
using SalesManager.Web.Interfaces;
using SalesManager.Web.Services;

namespace SalesManager.Web.Pages.Selects
{
    public class SelectProductComponentBase : ComponentBase
    {
        [Parameter] public string ProductSelected { get; set; } = string.Empty;
        [Parameter] public EventCallback<int> ChangeProduct { get; set; }
        [Inject] IProductService ProductService { get; set; }
        [Inject] ISessionDataService SessionDataService { get; set; }

        protected List<ProductGetDTO> productGetDTO = new List<ProductGetDTO>();

        protected bool PageIsReady { get; set; }

        protected async override Task OnInitializedAsync() => await GetProductsAsync();

        private async Task GetProductsAsync()
        {
            productGetDTO = await ProductService.GetProductsAsync($"Products?idUser={SessionDataService.GetIdUser()}&showInactive=false&orderby=ProductName");

            PageIsReady = true;
            StateHasChanged();
        }

        protected async Task OnChangeProductAsync(string selectedProduct)
        {
            if (int.TryParse(selectedProduct, out int productId) && ChangeProduct.HasDelegate)
            {
                ProductSelected = selectedProduct;
                await ChangeProduct.InvokeAsync(productId);
            }
        }
    }
}
