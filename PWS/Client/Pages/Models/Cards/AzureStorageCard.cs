using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class AzureStorageCard : CardBase
{
    public const string AzureStorage001Image = "/images/storage1.svg";

    public static IEnumerable<ICardModel> GetCardModels() =>
        new[]
        {
            AzureStorageEn,
            AzureStorageDe,
            AzureStorageIt
        };

    public static ICardModel Create(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = AzureStorageEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = AzureStorageDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = AzureStorageIt;
                break;
            default:
                model = AzureStorageEn;
                break;
        }

        return model;
    }

    public AzureStorageCard() : base(nameof(AzureStorageCard)) { }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel AzureStorageEn => new CardModel(
        AzureStorage001Image,
        $"{PageTranslations.AzureStorage001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.AzureStorage001, LanguageSelectorBase.LanguageEn),
        @"Azure Storage");

    private static ICardModel AzureStorageDe => new CardModel(
        AzureStorage001Image,
        $"{PageTranslations.AzureStorage001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.AzureStorage001, LanguageSelectorBase.LanguageDe),
        @"Azure Storage");

    private static ICardModel AzureStorageIt => new CardModel(
        AzureStorage001Image,
        $"{PageTranslations.AzureStorage001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.AzureStorage001, LanguageSelectorBase.LanguageIt),
        @"Azure Storage");
}