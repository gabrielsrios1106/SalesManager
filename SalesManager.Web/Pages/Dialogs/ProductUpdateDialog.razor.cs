using DataTransferObjects.Products;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Pages.Dialogs
{
    public class ProductUpdateDialogBase : ComponentBase
    {
        [Inject] ISnackbar SnackbarService { get; set; }
        [Inject] IProductService ProductService { get; set; }

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

        [Parameter] public ProductPutDTO ProductPutDTO { get; set; }
        [Parameter] public EventCallback GetProductsAsync { get; set; }

        protected bool PageIsReady { get; set; }

        protected override void OnInitialized() => PageIsReady = true;

        protected async void UpdateProductAsync()
        {
            PageIsReady = !PageIsReady;

            bool createSucessfull = await ProductService.UpdateAsync("Products", ProductPutDTO);

            if (createSucessfull)
            {
                SnackbarService.Add("Produto atualizado com sucesso", Severity.Success);

                await GetProductsAsync.InvokeAsync();
                StateHasChanged();
                MudDialog.Close();
            }

            PageIsReady = !PageIsReady;
        }

        protected void ChangeDepartmentIdUpdate(int departmentId)
        {
            if (ProductPutDTO is null)
                return;

            ProductPutDTO.DepartmentId = departmentId;
        }

        protected void Cancel() => MudDialog.Cancel();
    }
}
