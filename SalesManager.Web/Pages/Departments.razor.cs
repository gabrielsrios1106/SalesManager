using DataTransferObjects.Departments;
using Microsoft.AspNetCore.Components;
using Models;
using MudBlazor;
using SalesManager.Web.Interfaces;
using SalesManager.Web.Pages.Dialogs;
using SalesManager.Web.Services;
using SalesManager.Web.Shared;

namespace SalesManager.Web.Pages
{
    public class DepartmentsBase : ComponentBase
    {
        [Inject] IDepartmentService DepartmentService { get; set; }
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
        protected bool statusDepartment = false;

        public bool IsLoading { get; set; } = false;

        protected List<DepartmentGetDTO> departmentsGetDTO = new List<DepartmentGetDTO>();
        protected List<DepartmentGetDTO> departmentsGetDTOToShow = new List<DepartmentGetDTO>();

        protected async override Task OnInitializedAsync()
        {
            await GetDepartmentsAsync();
        }

        protected void ChangeStatusOption()
        {
            statusDepartment = !statusDepartment;
            FilterDepartmentsToShow();
            StateHasChanged();
        }

        protected async Task GetDepartmentsAsync()
        {
            IsLoading = true;
            StateHasChanged();

            departmentsGetDTO = await DepartmentService.GetDepartmentsAsync($"Departments?idUser={SessionDataService.GetIdUser()}");
            IsLoading = false;
            FilterDepartmentsToShow();
            StateHasChanged();
        }

        private void FilterDepartmentsToShow() => departmentsGetDTOToShow = departmentsGetDTO.Where(d => d.Status == (statusDepartment ? 0 : 1)).ToList();

        protected void OnButtonClicked(int departmentId)
        {
            idDelete = departmentId;

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
                { "ConfirmAsync", EventCallback.Factory.Create(this, DeleteDepartmentAsync) },
                { "ConfirmMessage", "Você tem certeza que quer deletar esse departamento?" },
                { "ConfirmButtonText", "Deletar!" }
            };

            DialogService.ShowAsync<ConfirmationDialog>("Cuidado", dialogParameters, dialogOptions);
        }

        protected async void DeleteDepartmentAsync()
        {
            bool deleteDepartment = await DepartmentService.DeleteAsync($"Departments/{idDelete}");

            if (deleteDepartment)
            {
                await GetDepartmentsAsync();

                string message = (departmentsGetDTO.Any(d => d.Status == 0 && d.Id == idDelete)) ? "desativado" : "excluído";
                SnackbarService.Add($"Departamento {message} com sucesso", Severity.Success);
            }

            StateHasChanged();
        }

        protected bool FilterFunc(DepartmentGetDTO departmentGetDTO)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (departmentGetDTO.DepartmentName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            if (departmentGetDTO.CreatedAt.ToString().Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }

        protected async void ActiveDepartmentsAsync(DepartmentGetDTO departmentGetDTO)
        {
            DepartmentPutDTO departmentPutDTO = new DepartmentPutDTO(departmentGetDTO);
            departmentPutDTO.Status = 1;

            bool updateSucessfull = await DepartmentService.UpdateAsync("Departments", departmentPutDTO);

            if (updateSucessfull)
            {
                await GetDepartmentsAsync();
                SnackbarService.Add("Departamento ativado com sucesso", Severity.Success);
            }

            StateHasChanged();
        }

        protected void ShowCreateModal()
        {
            Console.WriteLine("Oi");

            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            DialogParameters dialogParameters = new DialogParameters<DepartmentCreateDialog>();
            dialogParameters.Add("GetDepartmentsAsync", EventCallback.Factory.Create(this, GetDepartmentsAsync));

            DialogService.ShowAsync<DepartmentCreateDialog>("Cadastrar Departamento", dialogParameters, dialogOptions);
        }

        protected void ShowUpdateModal(DepartmentGetDTO departmentGetDTO)
        {
            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            DialogParameters dialogParameters = new DialogParameters<DepartmentUpdateDialog>();
            dialogParameters.Add("DepartmentPutDTO", new DepartmentPutDTO(departmentGetDTO));
            dialogParameters.Add("GetDepartmentsAsync", EventCallback.Factory.Create(this, GetDepartmentsAsync));

            DialogService.ShowAsync<DepartmentUpdateDialog>("Atualizar Departamento", dialogParameters, dialogOptions);
        }

        protected void ShowDetailsModal(DepartmentGetDTO departmentGetDTO)
        {
            DialogOptions dialogOptions = new DialogOptions
            {
                BackgroundClass = "blurry-dialog-class",
                CloseOnEscapeKey = true,
                CloseButton = true,
                MaxWidth = MaxWidth.Small,
                FullWidth = true
            };

            DialogParameters dialogParameters = new DialogParameters<DepartmentDetailsDialog>();
            dialogParameters.Add("DepartmentGetDTO", departmentGetDTO);

            DialogService.ShowAsync<DepartmentDetailsDialog>(departmentGetDTO.DepartmentName, dialogParameters, dialogOptions);
        }
    }
}
