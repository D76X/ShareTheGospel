using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class DesignPatternsCard
{
    public const string DesignPatterns001Image = "/images/designpatterns1.svg";

    public static ICardModel Create(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = CSharpEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = CSharpDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = CSharpIt;
                break;
            default:
                model = CSharpEn;
                break;
        }

        return model;
    }

    private static ICardModel CSharpEn => new CardModel(
        DesignPatterns001Image,
        $"{PageTranslations.DesignPatterns001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.DesignPatterns001, LanguageSelectorBase.LanguageEn),
        @"Design Patterns Series");

    private static ICardModel CSharpDe => new CardModel(
        DesignPatterns001Image,
        $"{PageTranslations.DesignPatterns001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.DesignPatterns001, LanguageSelectorBase.LanguageDe),
		@"Design Patterns Reihe");

    private static ICardModel CSharpIt => new CardModel(
        DesignPatterns001Image,
        $"{PageTranslations.DesignPatterns001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.DesignPatterns001, LanguageSelectorBase.LanguageIt),
		@"Design Patterns Serie");
}

