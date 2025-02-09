using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class AzureBackupCard : CardBase
{
    public const string AzureBackup001Image = "/images/azure-backup-vault.svg";

    public static IEnumerable<ICardModel> GetCardModels() =>
        new[]
        {
            AzureBackupEn, 
            AzureBackupDe, 
            AzureBackupIt
        };

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

    public AzureBackupCard() : base(nameof(AzureBackupCard)) { }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();
    
    private static ICardModel AzureBackupEn => new CardModel(
        AzureBackup001Image,
        $"{PageTranslations.AzureBackup001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.AzureBackup001, LanguageSelectorBase.LanguageEn),
        @"VMs, SQL Servers, files and folders and more");

    private static ICardModel AzureBackupDe => new CardModel(
        AzureBackup001Image,
        $"{PageTranslations.AzureBackup001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.AzureBackup001, LanguageSelectorBase.LanguageDe),
        @"VMs, SQL Servers, Dateien und Ordner und weiter");

    private static ICardModel AzureBackupIt => new CardModel(
        AzureBackup001Image,
        $"{PageTranslations.AzureBackup001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.AzureBackup001, LanguageSelectorBase.LanguageIt),
        @"VMs, SQL Servers, files and folders e altro");
}
