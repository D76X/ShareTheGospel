using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Pages.Models.Pages
{
    public class IndexModel : IIndexPage
    {
        private readonly ICardService _cardService;
        private readonly ILanguageService _languageService;
        private readonly ISearchService _searchService;

        public IndexModel(
            ICardService cardService,
            ILanguageService languageService,
            ISearchService searchService)
        {
            _cardService = cardService;
            _languageService = languageService;
            _searchService = searchService;
        }

        public ICardModel GetCard(string cardId) => _cardService.GetCard(cardId);

        public void Dispose()
        {
            //
        }
    }
}
