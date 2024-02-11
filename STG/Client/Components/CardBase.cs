using BlazorApp.Client.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Components
{
    public class CardBase : ComponentBase
    {
        [Parameter]
        public ICardModel? Model { get; set; }

        public string ImageSrc => Model!.ImageSrc;
        public string PageRef => Model!.PageRef;
        public string PageTitle => Model!.PageTitle;
        public string Text => Model!.PageText;
    }
}
