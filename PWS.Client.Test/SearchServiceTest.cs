using Client.Catalogs;
using FluentAssertions;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Extensions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;
using Websites.Razor.ClassLibrary.Models;
using Websites.Razor.ClassLibrary.Services;

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
            var searchTerm = @"azure";

            // act
            var result0 = _searchService.GetResult(searchTerm);
            var flattenedResult = result0.Flatten().ToArray();

            // assert the flatten search result
            flattenedResult.Should().NotBeNull();

            // 11 searchable items have been searched.
            flattenedResult.Should().HaveCount(11);
            
            // 3 matches have been found for this keyword
            var matches = flattenedResult.Where(i => i.IsMatch).ToArray();
            matches.Should().HaveCount(3);

            // 8 search items are not matches 
            var notMatches = flattenedResult.Where(i => !i.IsMatch).ToArray();
            notMatches.Should().HaveCount(8);

            // 6 search items are null searches, thus they
            // do not hold the keyword anywhere in their searchable attributes
            var nullSearches = flattenedResult.Where(i => i is NullSearchResult).ToArray();
            nullSearches.Should().HaveCount(6);
            // all null searches are also not matches
            notMatches.Should().Contain(nullSearches);

            // 2 search items are not matches but are not null searches
            // these are search item containers such as the catalog
            var searchContainers = flattenedResult
                .Where(i => !(i is NullSearchResult) && !i.IsMatch)
                .OrderBy(i=>i.TypeStr)
                .ToArray();
            searchContainers.Should().HaveCount(2);
            searchContainers[0].Type.Should().Be(typeof(CardCatalog));
            searchContainers[1].Type.Should().Be(typeof(SearchService));
            // all search containers are also not matches
            notMatches.Should().Contain(searchContainers);
            
            // assert the packed search result
            result0.Should().NotBeNull();
            result0.IsMatch.Should().BeFalse();
            result0.SearchTerm.Should().Be(searchTerm);
            result0.Type.Should().Be(typeof(SearchService));

            // assert that the search service exposes the output of the
            // call GetResult on its property SearchResult
            var searchServiceSearchResult = _searchService.SearchResult;
            result0.Should().Be(searchServiceSearchResult);

            // assert that the search service has searched only the CardCatalog
            result0.SearchResults.Should().ContainSingle();
            var result1 = result0.SearchResults.Single();
            result1.Should().BeOfType<SearchResult>();
            result1.IsMatch.Should().BeFalse();
            result1.Type.Should().Be<CardCatalog>();
            result1.Type.Should().Be(typeof(CardCatalog));
            result1.TypeStr.Should().Be(nameof(CardCatalog));
            result1.Value.Should().BeOfType<CardCatalog>();
            result1.Value.Should().BeAssignableTo<ISearchable>();

            // assert all the search items found in the catalog: all card models.
            var result2= result1.SearchResults.ToArray();
            result2.Should().AllSatisfy(i => i.Type.Should().Be(typeof(CardModel)));
            result2.Should().NotBeNull();
            result2.Should().BeAssignableTo<IEnumerable<ISearchResult>>();
            result2.Should().HaveCount(9);
            result2.Should().AllBeAssignableTo<ISearchResult>();
            
            var result2Matches = result2.Where(i => i.IsMatch).ToArray();
            var result2NoMatches = result2.Where(i => !i.IsMatch).ToArray();
            result2Matches.Should().HaveCount(3);
            result2NoMatches.Should().HaveCount(6);
        }
    }
}
