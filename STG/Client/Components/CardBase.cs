using Client.Abstractions.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Components
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
