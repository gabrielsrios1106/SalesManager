using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor;
using MudBlazor.Services;
using SalesManager.Web;
using SalesManager.Web.Authentication;
using SalesManager.Web.Interfaces;
using SalesManager.Web.Services;
using System.Security.Claims;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddHttpClient("APIRestful", client => client.BaseAddress = new Uri(builder.Configuration["APIEndpoint"]));

        builder.Services.AddAuthorizationCore();

        builder.Services.AddMudServices(config =>
        {
            config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomLeft;
            config.SnackbarConfiguration.PreventDuplicates = true;
            config.SnackbarConfiguration.NewestOnTop = false;
            config.SnackbarConfiguration.ShowCloseIcon = true;
            config.SnackbarConfiguration.VisibleStateDuration = 2500;
            config.SnackbarConfiguration.HideTransitionDuration = 500;
            config.SnackbarConfiguration.ShowTransitionDuration = 500;
            config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
        });

        builder.Services.AddScoped<ILoginService, LoginService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();
        builder.Services.AddScoped<IClientService, ClientService>();
        builder.Services.AddScoped<IAPIService, APIService>();
        builder.Services.AddScoped<IRegisterService, RegisterService>();
        builder.Services.AddScoped<IStockMovementService, StockMovementService>();
        builder.Services.AddScoped<IFinancialManagerService, FinancialManagerService>();
        builder.Services.AddScoped<ISessionDataService, SessionDataService>();

        builder.Services.AddScoped<AuthenticationProvider>();
        builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationProvider>(
            provider => provider.GetRequiredService<AuthenticationProvider>()
        );

        await builder.Build().RunAsync();
    }
}