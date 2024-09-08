using Client.Pages.Models.Cards;
using System.Collections.Generic;
using System.Linq;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Catalogs
{
    public class CardCatalog: 
        ICardCatalog,
        ISearchable
    {
        public const string Death001 = "Death001";
        public const string TheLaw001 = "TheLaw001";
        public const string TheCross001 = "TheCross001";
        public const string TheBible001 = "TheBible001";
        public const string Hell001 = "Hell001";

        private static IEnumerable<ICardModel> CardModels
        {
            get
            {
                var cardModels = new List<ICardModel>();
                cardModels.AddRange(DeathCard.GetCardModels());
                cardModels.AddRange(TheLawCard.GetCardModels());
                cardModels.AddRange(TheCrossCard.GetCardModels());
                cardModels.AddRange(TheBibleCard.GetCardModels());
                cardModels.AddRange(HellCard.GetCardModels());
                return cardModels;
            }
        }

        public ICardModel GetModel(
            string cardId,
            string? language)
        {
            switch (cardId)
            {
                case Death001: return DeathCard.GetCardModel(language);
                case TheLaw001: return TheLawCard.GetCardModel(language);
                case TheCross001: return TheCrossCard.GetCardModel(language);
                case TheBible001: return TheBibleCard.GetCardModel(language);
                case Hell001: return HellCard.GetCardModel(language);
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
