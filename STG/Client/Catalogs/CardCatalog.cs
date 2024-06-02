using Client.Pages.Models.Cards;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Catalogs
{
    public class CardCatalog: ICardCatalog 
    {
        public const string Death001 = "Death001";
        public const string TheLaw001 = "TheLaw001";
        public const string TheCross001 = "TheCross001";
        public const string TheBible001 = "TheBible001";
        public const string Hell001 = "Hell001";

        public ICardModel GetCard(
            string cardId,
            string? language)
        {
            switch (cardId)
            {
                case Death001: return DeathCard.Create(language);
                case TheLaw001: return TheLawCard.Create(language);
                case TheCross001: return TheCrossCard.Create(language);
                case TheBible001: return TheBibleCard.Create(language);
                case Hell001: return HellCard.Create(language);
                default: return null;
            }
        }
    }
}
