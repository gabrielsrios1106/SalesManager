using DataTransferObjects.Departments;
using DataTransferObjects.Products;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SalesManager.Web.Pages.Dialogs
{
    public class ProductDetailsDialogBase : ComponentBase
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

        [Parameter] public ProductGetDTO ProductGetDTO { get; set; }

        protected void Cancel() => MudDialog.Cancel();
    }
}
