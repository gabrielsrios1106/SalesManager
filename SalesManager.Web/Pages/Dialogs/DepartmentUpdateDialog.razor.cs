using DataTransferObjects.Departments;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Pages.Dialogs
{
    public class DepartmentUpdateDialogBase : ComponentBase
    {
        [Inject] ISnackbar SnackbarService { get; set; }
        [Inject] IDepartmentService DepartmentService { get; set; }

        [CascadingParameter] IMudDialogInstance MudDialog { get; set; }

        [Parameter] public DepartmentPutDTO DepartmentPutDTO { get; set; }
        [Parameter] public EventCallback GetDepartmentsAsync { get; set; }

        protected bool PageIsReady { get; set; }

        protected override void OnInitialized() => PageIsReady = true;

        protected async void UpdateDepartmentAsync()
        {
            PageIsReady = !PageIsReady;

            bool createSucessfull = await DepartmentService.UpdateAsync("Departments", DepartmentPutDTO);

            if (createSucessfull)
            {
                SnackbarService.Add("Departamento atualizado com sucesso", Severity.Success);

                await GetDepartmentsAsync.InvokeAsync();
                StateHasChanged();
                MudDialog.Close();
            }

            PageIsReady = !PageIsReady;
        }

        protected void Cancel() => MudDialog.Cancel();
    }
}
