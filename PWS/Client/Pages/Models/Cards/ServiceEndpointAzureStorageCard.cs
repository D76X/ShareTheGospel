using Client.Catalogs;
using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class ServiceEndpointAzureStorageCard() :
    CardBase(nameof(ServiceEndpointAzureStorageCard), Tags)
{
    public const string ServiceEndpointAzureStorage001Image = "/images/storage1.padlock1.svg";

    public new static IEnumerable<ITag> Tags { get; } =
    [
        TagCatalog.TagAzure,
        TagCatalog.TagAzureStorage,
        TagCatalog.TagSecurity,
    ];

    public static IEnumerable<ICardModel> GetCardModels() =>
    [
        AzureStorageEn,
        AzureStorageDe,
        AzureStorageIt
    ];

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

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel AzureStorageEn => new CardModel(
        ServiceEndpointAzureStorage001Image,
        $"{PageTranslations.ServiceEndpoint001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.ServiceEndpoint001, LanguageSelectorBase.LanguageEn),
        @"Storage Account Access",
        Tags);

    private static ICardModel AzureStorageDe => new CardModel(
        ServiceEndpointAzureStorage001Image,
        $"{PageTranslations.ServiceEndpoint001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.ServiceEndpoint001, LanguageSelectorBase.LanguageDe),
        @"Storage Account Access",
        Tags);

    private static ICardModel AzureStorageIt => new CardModel(
        ServiceEndpointAzureStorage001Image,
        $"{PageTranslations.ServiceEndpoint001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.ServiceEndpoint001, LanguageSelectorBase.LanguageIt),
        @"Storage Account Access",
        Tags);
}
