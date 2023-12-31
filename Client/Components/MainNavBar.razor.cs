using Microsoft.AspNetCore.Components;
using Websites.Razor.ClassLibrary.Components;
// ReSharper disable InconsistentNaming

namespace BlazorApp.Client.Components
{
    static class Translator
    {
        public static string Get(
            string id,
            string language,
            Dictionary<string, string[]> translations)
        {
            int languageIndex;

            switch (language)
            {
                case LanguageSelectorBase.LanguageEn: languageIndex = 0; break;
                case LanguageSelectorBase.LanguageDe: languageIndex = 1; break;
                case LanguageSelectorBase.LanguageIt: languageIndex = 2; break;
                default: languageIndex = 0; break;

            }

            if(translations.TryGetValue(id, out var values)) return values[languageIndex];
            return @"translation error";
        }
    }

    static class MenuCatalog
    {
        public const string Tour = @"Tour";
        public const string About = @"About";

        public static Dictionary<string, string[]> Translations = new()
        {
            { Tour, new []{@"Tour", @"Tour", @"Visita" }},
            { About, new []{@"About", @"Infos", @"Informazioni" }},
        };

        public static string Translation(string id, string language) => Translator.Get(id, language, Translations);
    }

    static class PageCatalog
    {
        public const string Index = @"Index";
        public const string Death001 = @"Death001";
        public const string TheLaw001 = @"TheLaw001";
        public const string TheCross001 = @"TheCross001";

        public static Dictionary<string,string[]> Translations = new()
        {
            {Index, new []{@"Home", @"Home", @"Home" }},
            {Death001, new [] {@"Death", @"Der Tod", @"La Morte" }},
            {TheLaw001, new [] {@"The Law", @"Das Gesetz", @"La legge"}},
            {TheCross001, new []{ @"The Cross", @"Das Kreuz ", @"La croce"}}
        };

        public static string Translation(string id, string language) => Translator.Get(id, language, Translations);

        //public static string Translation(
        //    string pageId,
        //    string language)
        //{
        //    int languageIndex;

        //    switch (language)
        //    {
        //        case LanguageSelectorBase.LanguageEn: languageIndex = 1; break;
        //        case LanguageSelectorBase.LanguageDe: languageIndex = 2; break;
        //        case LanguageSelectorBase.LanguageIt: languageIndex = 3; break;
        //        default: languageIndex = 0; break;

        //    }

        //    int pageIndex;

        //    switch (pageId)
        //    {
        //        case Death001: pageIndex = 1; break;
        //        case TheLaw001: pageIndex = 2; break;
        //        case TheCross001: pageIndex = 3; break;
        //        default: pageIndex = 0 ; break;
        //    }

        //    return Translations[pageIndex,languageIndex];
        //}
    }

	public class MainNavBarBase : ComponentBase
	{
        [Inject]
		public NavigationManager NavigationManager { get; set; }
		
        public void LinkSelected(string toPage)
        {
            var nextUri = $"{toPage}/{LanguageSelectorBase.SelectedLanguage}";
            NavigationManager.NavigateTo(nextUri);
        }

        public void LanguageSelected(string selectedLanguage)
		{
			var baseUri = NavigationManager.BaseUri;

			var currentUri = NavigationManager.Uri;
            if (currentUri.EndsWith($"/{selectedLanguage}")) return;

			if (currentUri.EndsWith($"/{LanguageSelectorBase.LanguageEn}") ||
			    currentUri.EndsWith($"/{LanguageSelectorBase.LanguageDe}") ||
			    currentUri.EndsWith($"/{LanguageSelectorBase.LanguageIt}"))
			{
				currentUri = currentUri.Substring(0, currentUri.Length - 3);
			}

            if (currentUri.EndsWith("/")) currentUri = currentUri.Substring(0, currentUri.Length - 1); 

            var nextUri = $"{currentUri}/{selectedLanguage}";
			NavigationManager.NavigateTo(nextUri);
			
		}

        public string miTour => MenuCatalog.Translation(MenuCatalog.Tour, LanguageSelectorBase.SelectedLanguage);
        protected string miIndex => PageCatalog.Translation(PageCatalog.Index, LanguageSelectorBase.SelectedLanguage);
        public string miDeath => PageCatalog.Translation(PageCatalog.Death001, LanguageSelectorBase.SelectedLanguage);
        public string miTheLaw => PageCatalog.Translation(PageCatalog.TheLaw001, LanguageSelectorBase.SelectedLanguage);
        public string miTheCross => PageCatalog.Translation(PageCatalog.TheCross001, LanguageSelectorBase.SelectedLanguage);
        public string miAbout => PageCatalog.Translation(PageCatalog.Index, LanguageSelectorBase.SelectedLanguage);
    }
}
