using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Pages.Models.Pages
{
    public class IndexModel : IIndexPage
    {
        private readonly ICardService _cardService;
        private readonly ILanguageService _languageService;
        private readonly ISearchService _searchService;

        public string TestString { get; set; } = "TestString";

        /// <summary>
        /// the event that this model raises to notify the corresponding
        /// component StateHasChanged that it is time to redraw the UI
        /// as the model has changed.
        /// </summary>
        public event Action? OnStateHasChanged;

        public IndexModel(
            ICardService cardService,
            ILanguageService languageService,
            ISearchService searchService)
        {
            _cardService = cardService;
            _languageService = languageService;
            _searchService = searchService;
            _searchService.SearchResultsChanged += SearchServiceOnSearchResultsChanged;
        }

        private void SearchServiceOnSearchResultsChanged(
            object? sender, 
            IEnumerable<SearchResult> e)
        {
            var test = e.FirstOrDefault()!.Values.FirstOrDefault()!;
            TestString = test;
            OnStateHasChanged?.Invoke();
        }

        public ICardModel GetCard(string cardId) => 
            _cardService.GetCard(cardId);

        public void Dispose()
        {
            _searchService.SearchResultsChanged -= SearchServiceOnSearchResultsChanged;
        }
    }
}
