using BlazorApp.Client.Abstractions.Models;
using BlazorApp.Client.Pages.Models.Cards;

namespace BlazorApp.Client.Common
{
    public static class CardCatalog 
    {
        public const string Death001 = "Death001";

        public static ICardModel GetCard(
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
