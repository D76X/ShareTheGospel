using System.Collections.Generic;
using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class TheBibleCard: CardBase
{
    public const string Bible001Image = "/images/thebible1.svg";

    public static ICardModel GetCardModel(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = TheBibleEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = TheBibleDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = TheBibleIt;
                break;
            default:
                model = TheBibleEn;
                break;
        }

        return model;
    }

    public static IEnumerable<ICardModel> GetCardModels() =>
        new[] { TheBibleEn, TheBibleDe, TheBibleIt };

    public TheBibleCard() : base(nameof(TheBibleCard)) { }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel TheBibleEn => new CardModel(
        Bible001Image,
        $"{PageTranslations.TheBible001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.TheBible001, LanguageSelectorBase.LanguageEn),
        @"What are the Holy Scriptures?");

    private static ICardModel TheBibleDe => new CardModel(
        Bible001Image,
        $"{PageTranslations.TheBible001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.TheBible001, LanguageSelectorBase.LanguageDe),
        @"Was ist die Bible?");

    private static ICardModel TheBibleIt => new CardModel(
        Bible001Image,
        $"{PageTranslations.TheBible001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.TheBible001, LanguageSelectorBase.LanguageIt),
        @"Cosa sono le Sacre Scritture?");
}

