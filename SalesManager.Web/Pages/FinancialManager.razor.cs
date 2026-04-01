using DataTransferObjects.FinancialManager;
using DataTransferObjects.StockMovement;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SalesManager.Web.Interfaces;
using System.Globalization;

namespace SalesManager.Web.Pages
{
    public class FinancialManagerBase : ComponentBase
    {
        [Inject] IFinancialManagerService FinancialManagerService { get; set; }
        [Inject] ISessionDataService SessionDataService { get; set; }

        protected string searchString = "";
        protected bool dense = false;
        protected bool hover = true;
        protected bool ronly = false;
        protected bool canCancelEdit = false;
        protected bool blockSwitch = false;
        public bool IsLoading { get; set; } = false;

        protected FinancialManagerGetDTO financialManagerGetDTO = new FinancialManagerGetDTO();
        protected BalanceGetDTO balanceGetDTO = new BalanceGetDTO();
        protected CultureInfo CultureInfoPtBR { get; set; } = new CultureInfo("pt-BR");
        protected List<FinancialManagerGetDTO> financialManagersGetDTO = new List<FinancialManagerGetDTO>();

        public string[] XAxisLabels = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", };
        protected int Index = -1;
        protected List<ChartSeries> Series = new List<ChartSeries>()
        {
        new ChartSeries() { Name = "Receita", Data = new double[] { 90, 79, 72, 69, 62, 62, 55, 65, 70 } },
        new ChartSeries() { Name = "Despesa", Data = new double[] { 10, 41, 35, 51, 49, 62, 69, 91, 148 } },
        };

        protected async override Task OnInitializedAsync()
        {
            await GetFinancialManagerAsync();
        }

        protected async Task GetFinancialManagerAsync()
        {
            IsLoading = true;
            StateHasChanged();

            balanceGetDTO = await FinancialManagerService.GetBalanceAsync($"FinancialManager/Balance?idUser={SessionDataService.GetIdUser()}");
            financialManagersGetDTO = await FinancialManagerService.GetFinancialManagersAsync($"FinancialManager?idUser={SessionDataService.GetIdUser()}");

            IsLoading = false;
            StateHasChanged();
        }

        protected bool FilterFunc(FinancialManagerGetDTO financialManagerGetDTO)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return true;
            if (financialManagerGetDTO.ProductName.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                return true;
            return false;
        }
    }
}
