using DataTransferObjects.Clients;
using DataTransferObjects.Departments;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Pages.Dialogs
{
    public class ClientDetailsDialogBase : ComponentBase
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

        [Parameter] public ClientGetDTO ClientGetDTO { get; set; }

        protected void Cancel() => MudDialog.Cancel();
    }
}
