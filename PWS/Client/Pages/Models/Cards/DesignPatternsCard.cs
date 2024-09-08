using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class DesignPatternsCard : CardBase
{
    public const string DesignPatterns001Image = "/images/designpatterns1.svg";

    public static IEnumerable<ICardModel> GetCardModels() =>
        new[] { DesignPatternEn, DesignPatternDe, DesignPatternIt };

    public static ICardModel Create(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = DesignPatternEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = DesignPatternDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = DesignPatternIt;
                break;
            default:
                model = DesignPatternEn;
                break;
        }

        return model;
    }

    public DesignPatternsCard() : base(nameof(DesignPatternsCard)) { }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();
    
    private static ICardModel DesignPatternEn => new CardModel(
        DesignPatterns001Image,
        $"{PageTranslations.DesignPatterns001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.DesignPatterns001, LanguageSelectorBase.LanguageEn),
        @"Design Patterns Series");

    private static ICardModel DesignPatternDe => new CardModel(
        DesignPatterns001Image,
        $"{PageTranslations.DesignPatterns001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.DesignPatterns001, LanguageSelectorBase.LanguageDe),
		@"Design Patterns Reihe");

    private static ICardModel DesignPatternIt => new CardModel(
        DesignPatterns001Image,
        $"{PageTranslations.DesignPatterns001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.DesignPatterns001, LanguageSelectorBase.LanguageIt),
		@"Design Patterns Serie");
}

