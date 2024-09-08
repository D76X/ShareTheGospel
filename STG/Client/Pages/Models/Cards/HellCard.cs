using System.Collections.Generic;
using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class HellCard : CardBase
{
    public const string Hell001Image = "/images/hell1.svg";

    public static ICardModel GetCardModel(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = HellEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = HellDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = HellIt;
                break;
            default:
                model = HellEn;
                break;
        }

        return model;
    }

    public static IEnumerable<ICardModel> GetCardModels() =>
        new[] { HellEn, HellDe, HellIt };

    public HellCard() : base(nameof(HellCard)) { }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel HellEn => new CardModel(
        Hell001Image,
        $"{PageTranslations.Hell001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.Hell001, LanguageSelectorBase.LanguageEn),
        @"What is Hell?");

    private static ICardModel HellDe => new CardModel(
        Hell001Image,
        $"{PageTranslations.Hell001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.Hell001, LanguageSelectorBase.LanguageDe),
        @"Was ist die Hölle?");

    private static ICardModel HellIt => new CardModel(
        Hell001Image,
        $"{PageTranslations.Hell001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.Hell001, LanguageSelectorBase.LanguageIt),
        @"Cosa sono gli Inferi?");
}

