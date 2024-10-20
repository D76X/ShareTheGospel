﻿using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;
using CardBase = Websites.Razor.ClassLibrary.Abstractions.Models.CardBase;

namespace Client.Pages.Models.Cards;

public class AzureCard: CardBase
{
    public const string Azure001Image = "/images/azure1.svg";

    public static IEnumerable<ICardModel> GetCardModels() => 
        new[] { AzureEn, AzureDe, AzureIt };

    public static ICardModel GetCardModel(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = AzureEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = AzureDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = AzureIt;
                break;
            default:
                model = AzureEn;
                break;
        }

        return model;
    }

    public AzureCard():base(nameof(AzureCard)){ }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel AzureEn => new CardModel(
        Azure001Image,
        $"{PageTranslations.Azure001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.Azure001, LanguageSelectorBase.LanguageEn),
        @"In this Azure Series");

    private static ICardModel AzureDe => new CardModel(
        Azure001Image,
        $"{PageTranslations.Azure001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.Azure001, LanguageSelectorBase.LanguageDe),
        @"In diese Azure Reihe");

    private static ICardModel AzureIt => new CardModel(
        Azure001Image,
        $"{PageTranslations.Azure001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.Azure001, LanguageSelectorBase.LanguageIt),
        @"In questa Azure Serie");
}

