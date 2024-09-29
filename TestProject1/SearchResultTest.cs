using FluentAssertions;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;
using Websites.Razor.ClassLibrary.Models;

namespace TestProject1
{
    public class SearchResultTest
    {
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
            
            var cardModel0 = new CardModel(image,pageRef,pageTitle,pageText);
            
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