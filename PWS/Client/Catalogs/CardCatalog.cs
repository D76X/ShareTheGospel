using Client.Pages.Models.Cards;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Catalogs
{
    public class CardCatalog: 
        ICardCatalog,
        ISearchable
    {
        public const string Azure001 = "Azure001";
        public const string CSharp001 = "CSharp001";
        public const string DesignPatterns001 = "DesignPatterns001";

        private static IEnumerable<ICardModel> CardModels 
        {
            get
            {
                var cardModels = new List<ICardModel>();
                cardModels.AddRange(AzureCard.GetCardModels());
                cardModels.AddRange(CSharpCard.GetCardModels());
                cardModels.AddRange(DesignPatternsCard.GetCardModels());
                return cardModels;
            }
        }

        public ICardModel GetModel(
            string cardId,
            string? language)
        {
            switch (cardId)
            {
                case Azure001: return AzureCard.GetCardModel(language);
                case CSharp001: return CSharpCard.Create(language);
                case DesignPatterns001: return DesignPatternsCard.Create(language);
                default: return null;
            }
        }

        public IEnumerable<ICardModel> GetModels() => CardModels;

        private ISearchable[]? _searchables;

        public ISearchable[]? Searchables
        {
            get
            {
                if (_searchables != null) return _searchables;
                _searchables = GetModels().OfType<ISearchable>().ToArray();
                return _searchables;
            }
        }

        public ISearchResult GetResult(string searchTerm)
        {
            var searchResult = new SearchResult(
                searchTerm,
                this,
                nameof(CardCatalog),
                this.GetType(),
                false);

            foreach (var cardModel in CardModels)
            {
                var searchableCard = cardModel as ISearchable;
                var result = searchableCard?.GetResult(searchTerm);
                searchResult.Add(result);
            }

            return searchResult;
        }
    }
}
