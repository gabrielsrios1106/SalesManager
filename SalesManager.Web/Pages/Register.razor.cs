using DataTransferObjects.Utils;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Interfaces;

namespace SalesManager.Web.Pages
{
    public class RegisterBase : ComponentBase
    {
        [Inject] IRegisterService RegisterService { get; set; }
        [Inject] ISnackbar SnackbarService { get; set; }

        protected UserPostDTO registerPostDTO = new UserPostDTO();

        protected async void CreateRegisterAsync()
        {
            bool createSucessfull = await RegisterService.CreateAsync("Register", registerPostDTO);

            if (createSucessfull)
            {
                SnackbarService.Add("Usuário cadastrado com sucesso", Severity.Success);
            }
        }
    }
}
