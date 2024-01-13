using Websites.Razor.ClassLibrary.Components;

namespace BlazorApp.Client.Translations;

static class CardCatalog
{
    private static CardFace? _death001;
    public static CardFace Death001 => _death001 ??= new CardFace(
        "/images/rip1.svg",
        new[]
        {
            $"{PageCatalog.Death001}/{LanguageSelectorBase.LanguageEn}",
            $"{PageCatalog.Death001}/{LanguageSelectorBase.LanguageDe}",
            $"{PageCatalog.Death001}/{LanguageSelectorBase.LanguageIt}",
        },
        new[]
        {
            PageCatalog.Translation(PageCatalog.Death001,LanguageSelectorBase.LanguageEn),
            PageCatalog.Translation(PageCatalog.Death001,LanguageSelectorBase.LanguageDe),
            PageCatalog.Translation(PageCatalog.Death001,LanguageSelectorBase.LanguageIt),
        },
        new[]
        {
            @"Are you afraid of death?",
            @"Hast du angst vor dem Tod?",
            @"Temi la morte?",
        });
}