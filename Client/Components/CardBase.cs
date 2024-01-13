using BlazorApp.Client.Translations;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Components
{
    public class CardBase : ComponentBase
    {
         [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public CardFace Face { get; set; }

        [Parameter]
        public string SelectedLanguage { get; set; }

        public string ImageSrc => Face.ImageSrc;
        public string PageRef => Face.PageRef(SelectedLanguage);
        public string PageTitle => Face.PageTitle(SelectedLanguage);
        public string Text => Face.PageText(SelectedLanguage);

        //public void LinkSelected(string toPage)
        //{
        //    var nextUri = $"{toPage}/{LanguageSelectorBase.SelectedLanguage}";
        //    NavigationManager.NavigateTo(nextUri);
        //}
    }
}
