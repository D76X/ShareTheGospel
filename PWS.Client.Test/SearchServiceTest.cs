using Client.Catalogs;
using Client.Pages.Models.Cards;
using FluentAssertions;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;
using Websites.Razor.ClassLibrary.Models;
// ReSharper disable InconsistentNaming

namespace PWS.Client.Test
{
    public class SearchServiceTest
    {
        private readonly ISearchService _searchService;

        public SearchServiceTest(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [Fact]
        public void SearchService_Finds_AzureCard_In_CardCatalog()
        {
            // arrange

            // Moq Dependencies
            // invoke UUT constructor
            // resolve dependencies
            var searchTerm = "azure";

            // act
            var result0 = _searchService.GetResult(searchTerm);
            
            // assert
            result0.Should().NotBeNull();
            result0.IsMatch.Should().BeFalse();
            result0.SearchTerm.Should().Be(searchTerm);
            
            // assert that the search service exposes the output of the
            // call GetResult on its property  SearchResult
            var searchServiceSearchResult = _searchService.SearchResult;
            result0.Should().Be(searchServiceSearchResult);

            // assert that the search service has searched only the CardCatalog
            result0.SearchResults.Should().ContainSingle();
            var result1 = result0.SearchResults.Single();
            result1.Should().BeOfType<SearchResult>();
            result1.IsMatch.Should().BeFalse();
            result1.Type.Should().Be<CardCatalog>();
            result1.TypeStr.Should().Be(nameof(CardCatalog));
            result1.Value.Should().BeOfType<CardCatalog>();
            result1.Value.Should().BeAssignableTo<ISearchable>();

            var result2= result1.SearchResults;
            result2.Should().NotBeNull();
            result2.Should().BeAssignableTo<IEnumerable<ISearchResult>>();
            result2.Should().BeOfType<List<ISearchResult>>();
            result2.Should().HaveCount(9);
            result2.Should().AllBeAssignableTo<ISearchResult>();
            
            // this is not finished yet ...
            throw new NotImplementedException("must implement this test");
        }

        
    }
}
