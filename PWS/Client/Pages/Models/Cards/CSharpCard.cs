using Client.Catalogs;
using Client.Translations;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Client.Pages.Models.Cards;

public class CSharpCard: CardBase
{
    public const string CSharp001Image = "/images/csharp1.svg";
    
    public static IEnumerable<ICardModel> GetCardModels() =>
        new[] { CSharpEn, CSharpDe, CSharpIt };

    public static ICardModel Create(string? language)
    {
        ICardModel? model;

        switch (language)
        {
            case LanguageSelectorBase.LanguageEn:
                model = CSharpEn;
                break;
            case LanguageSelectorBase.LanguageDe:
                model = CSharpDe;
                break;
            case LanguageSelectorBase.LanguageIt:
                model = CSharpIt;
                break;
            default:
                model = CSharpEn;
                break;
        }

        return model;
    }

    public CSharpCard():base(nameof(CSharpCard)
        ,
        new ITag[]
        {
            TagCatalog.TagCSharp,
        }) { }

    public override IEnumerable<ICardModel> GetModels() => GetCardModels();

    private static ICardModel CSharpEn => new CardModel(
	    CSharp001Image,
        $"{PageTranslations.CSharp001}/{LanguageSelectorBase.LanguageEn}",
        PageTranslations.Translation(PageTranslations.CSharp001, LanguageSelectorBase.LanguageEn),
        @"C# Series");

    private static ICardModel CSharpDe => new CardModel(
	    CSharp001Image,
        $"{PageTranslations.CSharp001}/{LanguageSelectorBase.LanguageDe}",
        PageTranslations.Translation(PageTranslations.CSharp001, LanguageSelectorBase.LanguageDe),
		@"C# Reihe");

    private static ICardModel CSharpIt => new CardModel(
        CSharp001Image,
        $"{PageTranslations.CSharp001}/{LanguageSelectorBase.LanguageIt}",
        PageTranslations.Translation(PageTranslations.CSharp001, LanguageSelectorBase.LanguageIt),
		@"C# Serie");
}

