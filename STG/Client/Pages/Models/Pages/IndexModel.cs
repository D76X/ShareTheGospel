using BlazorApp.Client.Abstractions.Models;
using BlazorApp.Client.Abstractions.Services;

namespace BlazorApp.Client.Pages.Models.Pages
{
    public class IndexModel : IIndexPage
    {
        private readonly ILanguageService _languageService;
        private readonly ICardService _cardService;

        public IndexModel(
            ILanguageService languageService,
            ICardService cardService)
        {
            _languageService = languageService;
            _cardService = cardService;
        }

        public ICardModel GetCard(string cardId) => _cardService.GetCard(cardId);
        

        public void Dispose()
        {
            //
        }
    }
}
