using Client.Catalogs;
using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class AzureBackupCard() : 
    CardBase(nameof(AzureBackupCard), Tags)
{
    public const string AzureBackup001Image = "/images/azure-backup-vault.svg";

    public new static IEnumerable<ITag> Tags { get; } =
    [
        TagCatalog.TagAzure,
        TagCatalog.TagAzureBackup
    ];

    public static IEnumerable<ICardModel> GetCardModels() =>
    [
        AzureBackupEn,
        AzureBackupDe,
        AzureBackupIt
    ];

    public static ICardModel Create(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = AzureBackupEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = AzureBackupDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = AzureBackupIt;
                break;
            default:
                model = AzureBackupEn;
                break;
        }

        return model;
    }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel AzureBackupEn { get; set; } = new CardModel(
        AzureBackup001Image,
        $"{PageTranslations.AzureBackup001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.AzureBackup001, LanguageSelectorBase.LanguageEn),
        @"Protect and Recover Workloads",
        Tags);

    private static ICardModel AzureBackupDe { get; set; } = new CardModel(
        AzureBackup001Image,
        $"{PageTranslations.AzureBackup001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.AzureBackup001, LanguageSelectorBase.LanguageDe),
        @"Schützen und Wiederherstellen Workloads",
        Tags);

    private static ICardModel AzureBackupIt { get; set; } = new CardModel(
        AzureBackup001Image,
        $"{PageTranslations.AzureBackup001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.AzureBackup001, LanguageSelectorBase.LanguageIt),
        @"Proteggi e Restora Workloads",
        Tags);

}
