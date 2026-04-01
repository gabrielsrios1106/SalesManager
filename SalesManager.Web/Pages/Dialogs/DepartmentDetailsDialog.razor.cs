using DataTransferObjects.Departments;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace SalesManager.Web.Pages.Dialogs
{
    public class DepartmentDetailsDialogBase : ComponentBase
    {
        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

        [Parameter] public DepartmentGetDTO DepartmentGetDTO { get; set; }

        protected void Cancel() => MudDialog.Cancel();
    }
}
