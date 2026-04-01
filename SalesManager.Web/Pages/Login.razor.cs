using DataTransferObjects.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Authentication;
using SalesManager.Web.Interfaces;


namespace SalesManager.Web.Pages
{
    public class LoginBase : ComponentBase
    {
        [Inject] ILoginService LoginService { get; set; }
        [Inject] IRegisterService RegisterService { get; set; }
        [Inject] ISnackbar SnackbarService { get; set; }
        [Inject] AuthenticationProvider AuthenticationProvider { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }

        protected LoginFormPostDTO LoginFormPostDTO = new LoginFormPostDTO();
        protected UserPostDTO RegisterPostDTO = new UserPostDTO();
        protected string Option { get; set; } = "login";    

        protected async Task LoginAsync()
        {
            string user = await LoginService.LoginAsync(LoginFormPostDTO);

            if (!string.IsNullOrEmpty(user))
            {
                await AuthenticationProvider.UserLoginAsync(user);
                NavigationManager.NavigateTo("/");

                SnackbarService.Add("Acesso validado", Severity.Success);
                StateHasChanged();
            }
        }

        protected async void CreateRegisterAsync()
        {
            bool createSucessfull = await RegisterService.CreateAsync("Register", RegisterPostDTO);

            if (createSucessfull)
            {
                SnackbarService.Add("Usuário cadastrado com sucesso", Severity.Success);

                LoginFormPostDTO.Email = RegisterPostDTO.Email;
                LoginFormPostDTO.Password = RegisterPostDTO.Password;

                await LoginAsync();
            }
        }

        protected void ChangeOption(string option) => Option = option;
    }
}
