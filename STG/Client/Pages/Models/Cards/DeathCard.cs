using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class DeathCard
{
    public const string Death001Image = "/images/rip1.svg";

    public static ICardModel Create(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = DeathEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = DeathDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = DeathIt;
                break;
            default:
                model = DeathEn;
                break;
        }

        return model;
    }

    private static ICardModel DeathEn => new CardModel(
        Death001Image,
        $"{PageTranslations.Death001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.Death001, LanguageSelectorBase.LanguageEn),
        @"Are you afraid of death?");

    private static ICardModel DeathDe => new CardModel(
        Death001Image,
        $"{PageTranslations.Death001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.Death001, LanguageSelectorBase.LanguageDe),
        @"Hast du angst vor dem Tod?");

    private static ICardModel DeathIt => new CardModel(
        Death001Image,
        $"{PageTranslations.Death001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.Death001, LanguageSelectorBase.LanguageIt),
        @"Temi la morte?");
}

