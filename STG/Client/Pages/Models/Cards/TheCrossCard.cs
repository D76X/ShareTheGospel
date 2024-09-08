using System.Collections.Generic;
using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class TheCrossCard: CardBase
{
    public const string Cross001Image = "/images/thecross1.svg";

    public static ICardModel GetCardModel(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = TheCrossEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = TheCrossDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = TheCrossIt;
                break;
            default:
                model = TheCrossEn;
                break;
        }

        return model;
    }

    public static IEnumerable<ICardModel> GetCardModels() =>
        new[] { TheCrossEn, TheCrossDe, TheCrossIt };


    public TheCrossCard() : base(nameof(TheCrossCard)) { }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel TheCrossEn => new CardModel(
        Cross001Image,
        $"{PageTranslations.TheCross001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.TheCross001, LanguageSelectorBase.LanguageEn),
        @"What does the cross mean for you?");

    private static ICardModel TheCrossDe => new CardModel(
        Cross001Image,
        $"{PageTranslations.TheCross001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.TheCross001, LanguageSelectorBase.LanguageDe),
        @"Was tut das Kreuz für Sie?");

    private static ICardModel TheCrossIt => new CardModel(
        Cross001Image,
        $"{PageTranslations.TheCross001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.TheCross001, LanguageSelectorBase.LanguageIt),
        @"Cosa fa la croce per te?");
}

