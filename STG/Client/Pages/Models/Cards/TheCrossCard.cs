using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class TheCrossCard
{
    public const string Cross001Image = "/images/thecross1.svg";

    public static ICardModel Create(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = TheLawEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = TheLawDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = TheLawIt;
                break;
            default:
                model = TheLawEn;
                break;
        }

        return model;
    }

    private static ICardModel TheLawEn => new CardModel(
        Cross001Image,
        $"{PageTranslations.TheCross001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.TheCross001, LanguageSelectorBase.LanguageEn),
        @"What does the cross mean for you?");

    private static ICardModel TheLawDe => new CardModel(
        Cross001Image,
        $"{PageTranslations.TheCross001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.TheCross001, LanguageSelectorBase.LanguageDe),
        @"Was tut das Kreuz für Sie?");

    private static ICardModel TheLawIt => new CardModel(
        Cross001Image,
        $"{PageTranslations.TheCross001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.TheCross001, LanguageSelectorBase.LanguageIt),
        @"Cosa fa la croce per te?");
}

