using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class DesignPatternsCard :
    ISearchable
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

    public ISearchResult GetResult(string searchTerm)
    {
        var searchResult = new SearchResult(
            searchTerm,
            this,
            nameof(DesignPatternsCard),
            this.GetType());

        foreach (var cardModel in GetCardModels())
        {
            var searchableCard = cardModel as ISearchable;
            var result = searchableCard?.GetResult(searchTerm);
            searchResult.Add(result);
        }

        return searchResult;
    }

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

