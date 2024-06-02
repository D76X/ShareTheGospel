﻿using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class TheLawCard
{
    public const string Death001Image = "/images/commandments1.svg";

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
        Death001Image,
        $"{PageTranslations.TheLaw001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.TheLaw001, LanguageSelectorBase.LanguageEn),
        @"Are you a good person?");

    private static ICardModel TheLawDe => new CardModel(
        Death001Image,
        $"{PageTranslations.TheLaw001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.TheLaw001, LanguageSelectorBase.LanguageDe),
        @"Sind Sie gut?");

    private static ICardModel TheLawIt => new CardModel(
        Death001Image,
        $"{PageTranslations.TheLaw001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.TheLaw001, LanguageSelectorBase.LanguageIt),
        @"Sei una brava persona?");
}

