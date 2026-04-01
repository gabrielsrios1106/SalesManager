using DataTransferObjects.Clients;
using DataTransferObjects.Products;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Pages.Dialogs
{
    public class ProductCreateDialogBase : ComponentBase
    {
        [Inject] ISnackbar SnackbarService { get; set; }
        [Inject] IProductService ProductService { get; set; }
        [Inject] ISessionDataService SessionDataService { get; set; }

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

        [Parameter] public EventCallback GetProductsAsync { get; set; }

        protected ProductPostDTO ProductPostDTO { get; set; }
        protected bool PageIsReady { get; set; }

        protected override void OnInitialized()
        {
            ProductPostDTO = new ProductPostDTO(SessionDataService.GetIdUser());
            PageIsReady = true;
        }

        protected async void CreateProductAsync()
        {
            PageIsReady = !PageIsReady;

            bool createSucessfull = await ProductService.CreateAsync("Products", ProductPostDTO);

            if (createSucessfull)
            {
                SnackbarService.Add("Produto cadastrado com sucesso", Severity.Success);

                await GetProductsAsync.InvokeAsync();
                StateHasChanged();
                MudDialog.Close();
            }

            PageIsReady = !PageIsReady;
        }

        protected void ChangeDepartmentIdCreate(int departmentId)
        {
            if (ProductPostDTO is null)
                return;

            ProductPostDTO.DepartmentId = departmentId;
        }

        protected void Cancel() => MudDialog.Cancel();
    }
}
