using Client.Pages.Models.Cards;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Catalogs
{
    public class CardCatalog: ICardCatalog 
    {
        public const string Azure001 = "Azure001";
        public const string CSharp001 = "CSharp001";
        public const string DesignPatterns001 = "DesignPatterns001";
        
        public ICardModel GetCard(
            string cardId,
            string? language)
        {
            switch (cardId)
            {
                case Azure001: return AzureCard.Create(language);
                case CSharp001: return CSharpCard.Create(language);
                case DesignPatterns001: return DesignPatternsCard.Create(language);
                default: return null;
            }
        }
    }
}
