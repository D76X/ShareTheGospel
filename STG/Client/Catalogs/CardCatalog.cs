using Client.Pages.Models.Cards;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Catalogs
{
    public class CardCatalog: ICardCatalog 
    {
        public const string Death001 = "Death001";

        public ICardModel GetCard(
            string cardId,
            string? language)
        {
            switch (cardId)
            {
                case Death001: return DeathCard.Create(language);
                default: return null;
            }
        }
    }
}
