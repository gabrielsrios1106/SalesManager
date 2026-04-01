using Microsoft.AspNetCore.Components;

namespace SalesManager.Web.Shared
{
    public class FormModalBase<TItem> : ComponentBase
    {
        [Parameter] public RenderFragment ChildContent { get; set; }
        [Parameter] public EventCallback CloseModal { get; set; }
        [Parameter] public EventCallback Save { get; set; }
        [Parameter] public TItem Model { get; set; }
        [Parameter] public string Title { get; set; }
        [Parameter] public string NameOfButton{ get; set; }

        protected async Task OnValidSubmitFormModal() => await Save.InvokeAsync();
    }
}