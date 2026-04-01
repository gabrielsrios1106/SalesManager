using DataTransferObjects.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Authentication;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Pages
{
    public class LogoutBase : ComponentBase
    {
        [Inject] ILoginService LoginService { get; set; }
        [Inject] ISnackbar SnackbarService { get; set; }
        [Inject] AuthenticationProvider AuthenticationProvider { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected void LogoutAsync()
        {
            AuthenticationProvider.UserLogoutAsync();

            NavigationManager.NavigateTo("/");
            SnackbarService.Add("Usuário desconectado", Severity.Success);
        }
    }
}
