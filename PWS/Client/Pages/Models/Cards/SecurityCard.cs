using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class SecurityCard : CardBase
{
    public const string Security001Image = "/images/security.svg";

    public static IEnumerable<ICardModel> GetCardModels() =>
        new[]
        {
            SecurityEn,
            SecurityDe, 
            SecurityIt
        };

    public static ICardModel Create(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = SecurityEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = SecurityDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = SecurityIt;
                break;
            default:
                model = SecurityEn;
                break;
        }

        return model;
    }

    public SecurityCard() : base(nameof(SecurityCard)) { }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel SecurityEn => new CardModel(
        Security001Image,
        $"{PageTranslations.Security001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.Security001, LanguageSelectorBase.LanguageEn),
        @"Security Series");

    private static ICardModel SecurityDe => new CardModel(
        Security001Image,
        $"{PageTranslations.Security001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.Security001, LanguageSelectorBase.LanguageDe),
        @"Sicherheit Reihe");

    private static ICardModel SecurityIt => new CardModel(
        Security001Image,
        $"{PageTranslations.Security001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.Security001, LanguageSelectorBase.LanguageIt),
        @"Security serie");
}