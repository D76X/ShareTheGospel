using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Components;
using Websites.Razor.ClassLibrary.Models;

namespace Websites.Razor.ClassLibrary.Test
{
    internal class TestCardFactory
    {
        public class TestCard : CardBase
        {
            public const string TestCard001Image = "/images/testcard1.svg";

            public override IEnumerable<ICardModel> GetModels() =>
            new[] { TestCardEn, TestCardDe, TestCardIt };

        public TestCard(string typeStr) : base(typeStr) { }

        private static ICardModel TestCardEn => new CardModel(
            TestCard001Image,
            $"{PageTranslations.TestPage01}/{LanguageSelectorBase.LanguageEn}",
            PageTranslations.Translation(PageTranslations.TestPage01, LanguageSelectorBase.LanguageEn),
            @"TestCard Series keyword1");

        private static ICardModel TestCardDe => new CardModel(
            TestCard001Image,
            $"{PageTranslations.TestPage01}/{LanguageSelectorBase.LanguageDe}",
            PageTranslations.Translation(PageTranslations.TestPage01, LanguageSelectorBase.LanguageDe),
            @"TestCard Reihe keyword2");

        private static ICardModel TestCardIt => new CardModel(
            TestCard001Image,
            $"{PageTranslations.TestPage01}/{LanguageSelectorBase.LanguageIt}",
            PageTranslations.Translation(PageTranslations.TestPage01, LanguageSelectorBase.LanguageIt),
            @"TestCard Serie keyword3");
        }

        public static CardBase GetTestCard() => new TestCard("TestCard");

    } }
