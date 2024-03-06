using System.Collections.Generic;
using BlazorApp.Client.Translations;
using Websites.Razor.ClassLibrary.Components;

namespace BlazorApp.Client.Models
{
    public class MainNavBar
    {
        public static string KeyAbout = PageTranslations.About.ToLower();

        public string SelectedLanguage { get; set; }

        public MenuItem About => Models[KeyAbout][SelectedLanguage];

        public Dictionary<string, Dictionary<string, MenuItem>> Models = new()
        {
            {
                KeyAbout,
                new Dictionary<string, MenuItem>()
                {
                    { LanguageSelectorBase.LanguageEn, new MenuItem(PageTranslations.About, PageTranslations.About) },
                    { LanguageSelectorBase.LanguageDe, new MenuItem(PageTranslations.About, PageTranslations.About) },
                    { LanguageSelectorBase.LanguageIt, new MenuItem(PageTranslations.About, PageTranslations.About) },
                }
            }
        };
    }
}
