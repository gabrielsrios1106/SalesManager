using Microsoft.AspNetCore.Components.Authorization;
using SalesManager.Web.Interfaces;
using System.Security.Claims;

namespace SalesManager.Web.Services;

public class SessionDataService : ISessionDataService
{
    private static AuthenticationState s_authenticationState;
    private static int s_idUser;

    public SessionDataService() { }

    public Task<AuthenticationState> GetAuthenticationState() =>
        (s_authenticationState is null) ? Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()))) : Task.FromResult(s_authenticationState);

    public void SetAuthenticationState(ClaimsPrincipal claimsPrincipal)
    {
        if (claimsPrincipal is null)
            claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity());

        s_authenticationState = new AuthenticationState(claimsPrincipal);
    }

    public void SetIdUser(int idUser) => s_idUser = idUser;

    public int GetIdUser() => s_idUser;
}