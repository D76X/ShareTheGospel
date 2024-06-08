//using Client.Abstractions.Services;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Pages.Models.Pages
{
    public class IndexModel : IIndexPage
    {
        private readonly ILanguageService _languageService;
        private readonly ICardService _cardService;
        private readonly ISearchService _searchService;

        public IndexModel(
            ILanguageService languageService,
            ICardService cardService,
            ISearchService searchService)
        {
            _languageService = languageService;
            _cardService = cardService;
            _searchService = searchService;
        }

        public ICardModel GetCard(string cardId) => _cardService.GetCard(cardId);

        public void Dispose()
        {
            //
        }
    }
}
