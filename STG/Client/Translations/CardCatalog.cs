using Websites.Razor.ClassLibrary.Components;

namespace BlazorApp.Client.Translations;

static class CardCatalog
{
    private static CardFace? _death001;
    public static CardFace Death001 => _death001 ??= new CardFace(
        "/images/rip1.svg",
        new[]
        {
            $"{PageTranslations.Death001}/{LanguageSelectorBase.LanguageEn}",
            $"{PageTranslations.Death001}/{LanguageSelectorBase.LanguageDe}",
            $"{PageTranslations.Death001}/{LanguageSelectorBase.LanguageIt}",
        },
        new[]
        {
            PageTranslations.Translation(PageTranslations.Death001,LanguageSelectorBase.LanguageEn),
            PageTranslations.Translation(PageTranslations.Death001,LanguageSelectorBase.LanguageDe),
            PageTranslations.Translation(PageTranslations.Death001,LanguageSelectorBase.LanguageIt),
        },
        new[]
        {
            @"Are you afraid of death?",
            @"Hast du angst vor dem Tod?",
            @"Temi la morte?",
        });
}