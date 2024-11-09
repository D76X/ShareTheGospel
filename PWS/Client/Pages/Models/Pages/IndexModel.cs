using Websites.Razor.ClassLibrary.Abstractions.Extensions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Pages.Models.Pages
{
    public class IndexModel : IIndexModel
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
            _searchService.SearchResultChanged += SearchServiceOnSearchResultChanged;
        }

        private void SearchServiceOnSearchResultChanged(
            object? sender,
            ISearchResult e)
        {
            var searchTerm = e.SearchTerm;

            var flatResults = e.Flatten();

            var count1 = flatResults.Count();
            var count2 = flatResults.Count(i=>i is NullSearchResult);
            var count3 = flatResults.Count(i => !(i is NullSearchResult));
            var count4 = flatResults.Count(i => (i is MatchSearchResult));

            TestString = $"{searchTerm}:{count1}:{count2}:{count3}:{count4}";
            OnStateHasChanged?.Invoke();
        }

        public ICardModel GetCard(string cardId) => 
            _cardService.GetCard(cardId);

        public void Dispose()
        {
            _searchService.SearchResultChanged -= SearchServiceOnSearchResultChanged;
        }
    }
}
