using FluentAssertions;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;
using Websites.Razor.ClassLibrary.Models;
// ReSharper disable InconsistentNaming

namespace Websites.Razor.ClassLibrary.Test
{
    public class SearchResultTest
    {
        [Fact]
        public void CardBase_Is_Searchable()
        {
            // arrange
            string typeStrTestCard = nameof(TestCardFactory.TestCard);
            string typeStrCardModel = nameof(CardModel);

            var testCard = TestCardFactory.GetTestCard();
            var testCardSearchables = testCard.Searchables;
            var searchableTestCard = testCard as ISearchable;

            // CASE-0: no match for search term
            string keyword0 = @"keyword0";
            string searchTerm0 = $"{keyword0}";

            // act 
            var results0 = searchableTestCard.GetResult(searchTerm0);

            // assert
            testCardSearchables.Should().NotBeNull();
            testCardSearchables.Should().BeOfType<ISearchable[]>();

            results0.Type.Should().Be<SearchableBase>();
            results0.TypeStr.Should().Be(typeStrTestCard);              
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
            result0Searchables.Searchables.Should().HaveCount(testCard.Searchables!.Length);
            
            var cardModels = testCard.GetModels().OrderBy(m => m.PageRef).ToArray();
            

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
            string keyword1 = @"keyword1";
            string searchTerm1 = $"{keyword1}";
            string keyword2 = @"keyword2";
            string searchTerm2 = $"{keyword2}";
            string keyword3 = @"keyword3";
            string searchTerm3 = $"{keyword3}";

            var cardModelWithKeyword1 = cardModels.Single(i => i.PageText.Contains(searchTerm1));
            var cardModelWithKeyword2 = cardModels.Single(i => i.PageText.Contains(searchTerm2));
            var cardModelWithKeyword3 = cardModels.Single(i => i.PageText.Contains(searchTerm3));

            // act 
            var results1 = searchableTestCard.GetResult(searchTerm1);

            // assert
            results1.Should().NotBeNull();
            // there is a match for the card
            results1.Should().BeOfType<MatchSearchResult>();
            results1.IsMatch.Should().BeTrue();
            results1.SearchTerm.Should().Be(searchTerm1);
            results1.Type.Should().Be<SearchableBase>();
            results1.TypeStr.Should().Be(typeStrTestCard);
            results1.SearchResults.Should().NotBeEmpty();
            results1.Value.Should().NotBeNull();
            results1.Value.Should().BeOfType<SearchableBase>();

            // there is a match for the card which has 3 searchables in it 
            // these searchables are its 3 contained card models
            results1.SearchResults.Should().HaveCount(3);
            var typeStrSearchResults1 = results1.SearchResults.Select(i=>i.TypeStr).ToArray();
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
            var results2 = searchableTestCard.GetResult(searchTerm2);

            // assert
            results2.Should().NotBeNull();
            // there is a match for the card
            results2.Should().BeOfType<MatchSearchResult>();
            results2.IsMatch.Should().BeTrue();
            results2.SearchTerm.Should().Be(searchTerm2);
            results2.Type.Should().Be<SearchableBase>();
            results2.TypeStr.Should().Be(typeStrTestCard);
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

            // -----------------------------------------------------------------------------------------------
            // CASE-3: there is a match for search term 3 in the page text of one of the models of the card

            // act 
            var results3 = searchableTestCard.GetResult(searchTerm3);

            // assert
            results3.Should().NotBeNull();
            // there is a match for the card
            results3.Should().BeOfType<MatchSearchResult>();
            results3.IsMatch.Should().BeTrue();
            results3.SearchTerm.Should().Be(searchTerm3);
            results3.Type.Should().Be<SearchableBase>();
            results3.TypeStr.Should().Be(typeStrTestCard);
            results3.SearchResults.Should().NotBeEmpty();
            results3.Value.Should().NotBeNull();
            results3.Value.Should().BeOfType<SearchableBase>();

            // there is a match for the card which has 3 searchables in it 
            // these searchables are its 3 contained card models
            results3.SearchResults.Should().HaveCount(3);
            var typeStrSearchResults3 = results3.SearchResults.Select(i => i.TypeStr).ToArray();
            typeStrSearchResults3.Should().AllBe(typeStrCardModel);

            // there is only one matching card 
            var match3 = results3.SearchResults.Single(i => i.IsMatch);
            match3.Should().BeOfType<MatchSearchResult>();
            match3.SearchTerm.Should().Be(searchTerm3);
            match3.Type.Should().Be<CardModel>();
            match3.TypeStr.Should().Be(typeStrCardModel);
            var match3Value = match3.Value as CardModel;
            match3Value.Should().NotBeNull();
            match3Value!.PageText.Should().Contain(searchTerm3);
            match3Value.Should().BeEquivalentTo(cardModelWithKeyword3);
            match3Value.Should().NotBe(cardModelWithKeyword3);

            // the remaining 2 card models are not matches for this keyword
            var noMatches3 = results3.SearchResults.Where(i => !i.IsMatch).ToArray();
            noMatches3.Should().AllBeOfType<NullSearchResult>();
            var noMatch3_0 = noMatches3[0];
            var noMatch3_1 = noMatches3[1];
            noMatch3_0.SearchTerm.Should().Be(searchTerm3);
            noMatch3_1.SearchTerm.Should().Be(searchTerm3);

            var noMatchesValues3 = noMatches3
                .Select(i => i.Value as CardModel)
                .OrderBy(i => i!.PageRef)
                .ToArray();
            noMatchesValues3.Should().HaveCount(2);
            var noMatchesValues3Keyword1 = noMatchesValues3.Single(i => i!.PageText.Contains(searchTerm1));
            var noMatchesValues3Keyword2 = noMatchesValues3.Single(i => i!.PageText.Contains(searchTerm2));

            noMatchesValues3Keyword1.Should().BeEquivalentTo(cardModelWithKeyword1);
            noMatchesValues3Keyword1.Should().NotBe(cardModelWithKeyword1);

            noMatchesValues3Keyword2.Should().BeEquivalentTo(cardModelWithKeyword2);
            noMatchesValues3Keyword2.Should().NotBe(cardModelWithKeyword2);
        }

        [Fact]
        public void CardModel_Is_Searchable()
        {
            // arrange

            string typeStr = nameof(CardModel);

            // searchable properties of ICardModel
            string image = @"image";
            string pageRef = @"pageRef";
            string pageTitle = @"pageTitle";
            string pageText = @"pageText";

            string keyword1 = @"keyword1";
            string keyword2 = @"keyword2";
            string keyword3 = @"keyword3";

            string searchTerm1 = $"{keyword1}";

            // -------------------------------------------------------------------------
            // CASE-0
            // the search produces no results when the search term does not have a match

            var cardModel0 = new CardModel(image, pageRef, pageTitle, pageText);

            // act
            var cardModelInterface = cardModel0 as ICardModel;
            var searchable0 = cardModel0 as ISearchable;

            // assert
            cardModelInterface.Should().NotBeNull();
            searchable0.Should().NotBeNull();
            cardModel0.Searchables.Should().BeNull();

            // act
            var results0 = searchable0.GetResult(searchTerm1);

            // assert
            results0.Should().BeOfType<NullSearchResult>();
            (results0 as ISearchResult).Should().NotBeNull();
            results0.SearchTerm.Should().Be(searchTerm1);
            results0.Value.Should().Be(cardModel0);
            results0.Type.Should().Be<CardModel>();
            results0.TypeStr.Should().Be(typeStr);
            results0.IsMatch.Should().BeFalse();
            results0.SearchResults.Should().BeEmpty();

            // ---------------------------------------------------------------------------------
            // CASE-1
            // the search finds a result of type CardModel when any of the searchable properties 
            // of ICardModel contains the search term

            // arrange 
            var cardModel1A = new CardModel($"{image} {keyword1}", pageRef, pageTitle, pageText);
            var searchable1A = cardModel1A as ISearchable;

            // act
            var results1A = searchable1A.GetResult(searchTerm1);

            // assert
            results1A.Should().BeOfType<MatchSearchResult>();
            (results1A as ISearchResult).Should().NotBeNull();
            results1A.SearchTerm.Should().Be(searchTerm1);
            results1A.Value.Should().Be(cardModel1A);
            results1A.Type.Should().Be<CardModel>();
            results1A.TypeStr.Should().Be(typeStr);
            results1A.IsMatch.Should().BeTrue();
            results1A.SearchResults.Should().BeEmpty();

            // arrange
            var cardModel1B = new CardModel("image", $"{pageRef} {keyword1}", pageTitle, pageText);
            var searchable1B = cardModel1B as ISearchable;

            // act 
            var results1B = searchable1B.GetResult(searchTerm1);

            // assert
            results1B.Should().BeOfType<MatchSearchResult>();
            (results1B as ISearchResult).Should().NotBeNull();
            results1B.SearchTerm.Should().Be(searchTerm1);
            results1B.Value.Should().Be(cardModel1B);
            results1B.Type.Should().Be<CardModel>();
            results1B.TypeStr.Should().Be(typeStr);
            results1B.IsMatch.Should().BeTrue();
            results1B.SearchResults.Should().BeEmpty();

            // arrange
            var cardModel1C = new CardModel($"{image}", $"{pageRef}", $"{pageTitle} {keyword1}", $"{pageText}");
            var searchable1C = cardModel1C as ISearchable;

            // act 
            var results1C = searchable1C.GetResult(searchTerm1);

            // assert
            results1C.Should().BeOfType<MatchSearchResult>();
            (results1C as ISearchResult).Should().NotBeNull();
            results1C.SearchTerm.Should().Be(searchTerm1);
            results1C.Value.Should().Be(cardModel1C);
            results1C.Type.Should().Be<CardModel>();
            results1C.TypeStr.Should().Be(typeStr);
            results1C.IsMatch.Should().BeTrue();
            results1C.SearchResults.Should().BeEmpty();

            // arrange
            var cardModel1D = new CardModel($"{image}", $"{pageRef}", $"{pageTitle}", $"{pageText} {keyword1}");
            var searchable1D = cardModel1D as ISearchable;

            // act 
            var results1D = searchable1D.GetResult(searchTerm1);

            // assert
            results1D.Should().BeOfType<MatchSearchResult>();
            (results1D as ISearchResult).Should().NotBeNull();
            results1D.SearchTerm.Should().Be(searchTerm1);
            results1D.Value.Should().Be(cardModel1D);
            results1D.Type.Should().Be<CardModel>();
            results1D.TypeStr.Should().Be(typeStr);
            results1D.IsMatch.Should().BeTrue();
            results1D.SearchResults.Should().BeEmpty();

            // -------------------------------------------------------------------------
            // CASE-2
            // the search is selective by the search term 

            // arrange
            string searchTerm2 = $"{keyword2}";

            var cardModel2A = new CardModel($"{image} {keyword1}", pageRef, pageTitle, pageText);
            var searchable2A = cardModel2A as ISearchable;

            var cardModel2B = new CardModel($"{image} {keyword2}", pageRef, pageTitle, pageText);
            var searchable2B = cardModel2B as ISearchable;

            // act 
            var results2A1 = searchable2A.GetResult(searchTerm1);
            var results2A2 = searchable2A.GetResult(searchTerm2);
            var results2B1 = searchable2B.GetResult(searchTerm1);
            var results2B2 = searchable2B.GetResult(searchTerm2);

            // assert

            // search finds a match for ST1 on modelA
            results2A1.IsMatch.Should().BeTrue();
            results2A1.Value.Should().Be(cardModel2A);
            results2A1.SearchTerm.Should().Be(searchTerm1);

            // search does not find a match for ST2 on modelA
            results2A2.IsMatch.Should().BeFalse();
            results2A2.Should().NotBeNull();
            results2A2.Should().BeOfType<NullSearchResult>();
            results2A2.Type.Should().Be<CardModel>();
            results2A2.SearchResults.Should().BeEmpty();
            results2A2.SearchTerm.Should().Be(searchTerm2);

            // search does not find a match for ST1 on modelB
            results2B1.IsMatch.Should().BeFalse();
            results2B1.Should().NotBeNull();
            results2B1.Should().BeOfType<NullSearchResult>();
            results2B1.Type.Should().Be<CardModel>();
            results2B1.SearchResults.Should().BeEmpty();
            results2B1.SearchTerm.Should().Be(searchTerm1);

            // search finds a match for ST2 on modelB
            results2B2.IsMatch.Should().BeTrue();
            results2B2.Value.Should().Be(cardModel2B);
            results2B2.SearchTerm.Should().Be(searchTerm2);

            // -------------------------------------------------------------------------
        }
    }
}