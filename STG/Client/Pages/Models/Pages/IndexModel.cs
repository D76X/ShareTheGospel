using System;
using System.Collections.Generic;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Pages.Models.Pages
{
    public class IndexModel : IIndexModel
    {
        private readonly ICardService _cardService;
        private readonly ILanguageService _languageService;
        private readonly ISearchService _searchService;

        public string TestString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public event Action? OnStateHasChanged;

        public IndexModel(
            ILanguageService languageService,
            ICardService cardService,
            ISearchService searchService)
        {
            _languageService = languageService;
            _cardService = cardService;
            _searchService = searchService;
        }

        public ICardModel GetCard(string cardId) => 
            _cardService.GetCard(cardId);
        
        public IEnumerable<ICardModel> GetCards(IEnumerable<string> cardIds) => 
            _cardService.GetCards(cardIds);

        public void Dispose()
        {
            //
        }
    }
}
