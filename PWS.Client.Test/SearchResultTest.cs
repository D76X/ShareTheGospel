using Client.Pages.Models.Cards;
using FluentAssertions;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Models;
// ReSharper disable InconsistentNaming

namespace PWS.Client.Test
{

    public class SearchResultTest
    {
        [Fact]
        public void AzureCard_Is_Searchable()
        {
            // arrange 
            string typeStrAzureCard = nameof(AzureCard);
            string typeStrCardModel = nameof(CardModel);
            var card = new AzureCard();

            // act 
            var cardSearchables = card.Searchables;
            var searchableCard = card as ISearchable;

            // CASE-0: no match for search term
            string keyword0 = @"keyword0";
            string searchTerm0 = $"{keyword0}";

            // act 
            var results0 = searchableCard.GetResult(searchTerm0);

            // assert
            cardSearchables.Should().NotBeNull();
            cardSearchables.Should().BeOfType<ISearchable[]>();

            results0.Type.Should().Be<SearchableBase>();
            results0.TypeStr.Should().Be(typeStrAzureCard);
            results0.IsMatch.Should().BeFalse();
            results0.SearchResults.Should().BeEmpty();

            results0.Should().NotBeNull();
            results0.Should().BeOfType<NullSearchResult>();
            results0.SearchTerm.Should().Be(searchTerm0);
            results0.Value.Should().NotBeNull();
            results0.Value.Should().BeOfType<SearchableBase>();

            var result0Searchables = results0.Value as SearchableBase;
            result0Searchables!.Searchables.Should().BeOfType<ISearchable[]>();
            result0Searchables.Searchables.Should().NotBeNullOrEmpty();
            result0Searchables.Searchables.Should().AllBeOfType<CardModel>();
            result0Searchables.Searchables.Should().HaveCount(3);
            result0Searchables.Searchables.Should().HaveCount(card.Searchables!.Length);

            var cardModels = card.GetModels().OrderBy(m => m.PageRef).ToArray();


            var searchableModels = result0Searchables
                .Searchables!
                .Select(i => i as CardModel)
                .OrderBy(m => m?.PageRef)
                .ToArray();


            searchableModels[0].Should().BeEquivalentTo(cardModels[0]);
            searchableModels[0].Should().NotBe(cardModels[0]);

            searchableModels[1].Should().BeEquivalentTo(cardModels[1]);
            searchableModels[1].Should().NotBe(cardModels[1]);

            searchableModels[2].Should().BeEquivalentTo(cardModels[2]);
            searchableModels[2].Should().NotBe(cardModels[2]);

            // -----------------------------------------------------------------------------------------------
            // CASE-1: there is a match for search term 1 in the page text of one of the models of the card

            // arrange
            string keyword1 = @"In this";
            string searchTerm1 = $"{keyword1}";
            string keyword2 = @"In diese";
            string searchTerm2 = $"{keyword2}";
            string keyword3 = @"In questa";
            string searchTerm3 = $"{keyword3}";

            var cardModelWithKeyword1 = cardModels.Single(i => i.PageText.Contains(searchTerm1));
            var cardModelWithKeyword2 = cardModels.Single(i => i.PageText.Contains(searchTerm2));
            var cardModelWithKeyword3 = cardModels.Single(i => i.PageText.Contains(searchTerm3));

            // act 
            var results1 = searchableCard.GetResult(searchTerm1);

            // assert
            results1.Should().NotBeNull();
            // there is a match for the card
            results1.Should().BeOfType<MatchSearchResult>();
            results1.IsMatch.Should().BeTrue();
            results1.SearchTerm.Should().Be(searchTerm1);
            results1.Type.Should().Be<SearchableBase>();
            results1.TypeStr.Should().Be(typeStrAzureCard);
            results1.SearchResults.Should().NotBeEmpty();
            results1.Value.Should().NotBeNull();
            results1.Value.Should().BeOfType<SearchableBase>();

            // there is a match for the card which has 3 searchables in it 
            // these searchables are its 3 contained card models
            results1.SearchResults.Should().HaveCount(3);
            var typeStrSearchResults1 = results1.SearchResults.Select(i => i.TypeStr).ToArray();
            typeStrSearchResults1.Should().AllBe(typeStrCardModel);

            // there is only one matching card 
            var match1 = results1.SearchResults.Single(i => i.IsMatch);
            match1.Should().BeOfType<MatchSearchResult>();
            match1.SearchTerm.Should().Be(searchTerm1);
            match1.Type.Should().Be<CardModel>();
            match1.TypeStr.Should().Be(typeStrCardModel);
            var match1Value = match1.Value as CardModel;
            match1Value.Should().NotBeNull();
            match1Value!.PageText.Should().Contain(searchTerm1);
            match1Value.Should().BeEquivalentTo(cardModelWithKeyword1);
            match1Value.Should().NotBe(cardModelWithKeyword1);

            // the remaining 2 card models are not matches for this keyword
            var noMatches1 = results1.SearchResults.Where(i => !i.IsMatch).ToArray();
            noMatches1.Should().AllBeOfType<NullSearchResult>();
            var noMatch1_0 = noMatches1[0];
            var noMatch1_1 = noMatches1[1];
            noMatch1_0.SearchTerm.Should().Be(searchTerm1);
            noMatch1_1.SearchTerm.Should().Be(searchTerm1);

            var noMatchesValues1 = noMatches1
                .Select(i => i.Value as CardModel)
                .OrderBy(i => i!.PageRef)
                .ToArray();
            noMatchesValues1.Should().HaveCount(2);
            var noMatchesValues1Keyword2 = noMatchesValues1.Single(i => i!.PageText.Contains(searchTerm2));
            var noMatchesValues1Keyword3 = noMatchesValues1.Single(i => i!.PageText.Contains(searchTerm3));

            noMatchesValues1Keyword2.Should().BeEquivalentTo(cardModelWithKeyword2);
            noMatchesValues1Keyword2.Should().NotBe(cardModelWithKeyword2);

            noMatchesValues1Keyword3.Should().BeEquivalentTo(cardModelWithKeyword3);
            noMatchesValues1Keyword3.Should().NotBe(cardModelWithKeyword3);

            // -----------------------------------------------------------------------------------------------
            // CASE-2: there is a match for search term 2 in the page text of one of the models of the card

            // act 
            var results2 = searchableCard.GetResult(searchTerm2);

            // assert
            results2.Should().NotBeNull();
            // there is a match for the card
            results2.Should().BeOfType<MatchSearchResult>();
            results2.IsMatch.Should().BeTrue();
            results2.SearchTerm.Should().Be(searchTerm2);
            results2.Type.Should().Be<SearchableBase>();
            results2.TypeStr.Should().Be(typeStrAzureCard);
            results2.SearchResults.Should().NotBeEmpty();
            results2.Value.Should().NotBeNull();
            results2.Value.Should().BeOfType<SearchableBase>();

            // there is a match for the card which has 3 searchables in it 
            // these searchables are its 3 contained card models
            results2.SearchResults.Should().HaveCount(3);
            var typeStrSearchResults2 = results2.SearchResults.Select(i => i.TypeStr).ToArray();
            typeStrSearchResults2.Should().AllBe(typeStrCardModel);

            // there is only one matching card 
            var match2 = results2.SearchResults.Single(i => i.IsMatch);
            match2.Should().BeOfType<MatchSearchResult>();
            match2.SearchTerm.Should().Be(searchTerm2);
            match2.Type.Should().Be<CardModel>();
            match2.TypeStr.Should().Be(typeStrCardModel);
            var match2Value = match2.Value as CardModel;
            match2Value.Should().NotBeNull();
            match2Value!.PageText.Should().Contain(searchTerm2);
            match2Value.Should().BeEquivalentTo(cardModelWithKeyword2);
            match2Value.Should().NotBe(cardModelWithKeyword2);

            // the remaining 2 card models are not matches for this keyword
            var noMatches2 = results2.SearchResults.Where(i => !i.IsMatch).ToArray();
            noMatches2.Should().AllBeOfType<NullSearchResult>();
            var noMatch2_0 = noMatches2[0];
            var noMatch2_1 = noMatches2[1];
            noMatch2_0.SearchTerm.Should().Be(searchTerm2);
            noMatch2_1.SearchTerm.Should().Be(searchTerm2);

            var noMatchesValues2 = noMatches2
                .Select(i => i.Value as CardModel)
                .OrderBy(i => i!.PageRef)
                .ToArray();
            noMatchesValues2.Should().HaveCount(2);
            var noMatchesValues2Keyword1 = noMatchesValues2.Single(i => i!.PageText.Contains(searchTerm1));
            var noMatchesValues2Keyword3 = noMatchesValues2.Single(i => i!.PageText.Contains(searchTerm3));

            noMatchesValues2Keyword1.Should().BeEquivalentTo(cardModelWithKeyword1);
            noMatchesValues2Keyword1.Should().NotBe(cardModelWithKeyword1);

            noMatchesValues2Keyword3.Should().BeEquivalentTo(cardModelWithKeyword3);
            noMatchesValues2Keyword3.Should().NotBe(cardModelWithKeyword3);
        }
    }
}
