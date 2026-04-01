using Microsoft.AspNetCore.Components.Authorization;
using SalesManager.Web.Interfaces;
using System.Security.Claims;

namespace SalesManager.Web.Authentication
{
    public class AuthenticationProvider : AuthenticationStateProvider
    {
        private readonly ISessionDataService _sessionDataService;

        public AuthenticationProvider(ISessionDataService sessionDataService)
        {
            _sessionDataService = sessionDataService;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync() => await _sessionDataService.GetAuthenticationState();

        public async Task UserLoginAsync(string authUser)
        {
            #region Update
            string[] userData = authUser.Split('|');
            string nameUser = userData[0];
            int idUser = int.Parse(userData[1]);

            List<Claim> claimsIdentity = new List<Claim>()
            {
                new Claim("IdUser", idUser.ToString()),
                new Claim(ClaimTypes.Name, nameUser)
            };

            ClaimsIdentity user = new ClaimsIdentity(claimsIdentity, "Authenticated");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(user);

            _sessionDataService.SetAuthenticationState(claimsPrincipal);
            _sessionDataService.SetIdUser(idUser);

            AuthenticationState authenticationState = await _sessionDataService.GetAuthenticationState();
            #endregion Update

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public void UserLogoutAsync()
        {
            _sessionDataService.SetAuthenticationState(null);
            NotifyAuthenticationStateChanged(_sessionDataService.GetAuthenticationState());
        }
    }
}
