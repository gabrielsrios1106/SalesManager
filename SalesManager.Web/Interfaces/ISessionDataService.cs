using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace SalesManager.Web.Interfaces;

public interface ISessionDataService
{
    Task<AuthenticationState> GetAuthenticationState();

    void SetAuthenticationState(ClaimsPrincipal claimsPrincipal);

    void SetIdUser(int idUser);

    int GetIdUser();
}