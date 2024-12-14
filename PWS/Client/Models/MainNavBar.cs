using Client.Translations;
using Websites.Razor.ClassLibrary.Components;

namespace Client.Models
{
    public class MainNavBar
    {
        public static string KeyIndex = PageTranslations.Index.ToLower();
        public static string KeyAbout = PageTranslations.About.ToLower();

        public string SelectedLanguage { get; set; }

        public MenuItem Index => Models[KeyIndex][SelectedLanguage];
        public MenuItem About => Models[KeyAbout][SelectedLanguage];

        public Dictionary<string, Dictionary<string, MenuItem>> Models = new()
        {
            {
                KeyIndex,
                new Dictionary<string, MenuItem>()
                {
                    { LanguageSelectorBase.LanguageEn, new MenuItem(PageTranslations.Index, PageTranslations.Index) },
                    { LanguageSelectorBase.LanguageDe, new MenuItem(PageTranslations.Index, PageTranslations.Index) },
                    { LanguageSelectorBase.LanguageIt, new MenuItem(PageTranslations.Index, PageTranslations.Index) },
                }
            },
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
