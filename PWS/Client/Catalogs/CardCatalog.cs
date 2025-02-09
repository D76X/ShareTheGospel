﻿using Client.Pages.Models.Cards;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Catalogs
{
    public class CardCatalog: 
        ICardCatalog,
        ISearchable
    {
        public const string Azure001 = "AzureSeries001";
        public const string Security001 = "Security001";
        public const string CSharp001 = "CSharp001";
        public const string DesignPatterns001 = "DesignPatterns001";
        public const string AzureBackup001 = "AzureBackup001";
        public const string AzureStorage001 = "AzureStorage001";

        private static IEnumerable<ICardModel> CardModels 
        {
            get
            {
                var cardModels = new List<ICardModel>();
                cardModels.AddRange(AzureSeriesCard.GetCardModels());
                cardModels.AddRange(SecurityCard.GetCardModels());
                cardModels.AddRange(CSharpCard.GetCardModels());
                cardModels.AddRange(DesignPatternsCard.GetCardModels());
                cardModels.AddRange(AzureBackupCard.GetCardModels());
                return cardModels;
            }
        }

        public ICardModel GetModel(
            string cardId,
            string? language)
        {
            switch (cardId)
            {
                case Azure001: return AzureSeriesCard.GetCardModel(language);
                case Security001: return SecurityCard.Create(language);
                case CSharp001: return CSharpCard.Create(language);
                case DesignPatterns001: return DesignPatternsCard.Create(language);
                case AzureBackup001: return AzureBackupCard.Create(language);
                case AzureStorage001: return AzureStorageCard.Create(language);
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
                this.GetType());

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
