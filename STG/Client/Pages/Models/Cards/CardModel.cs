using Client.Abstractions.Models;

namespace Client.Pages.Models.Cards;

public class CardModel : ICardModel
{
    public CardModel(
        string imageSrc,
        string pageRef,
        string pageTitle,
        string pageText)
    {
        ImageSrc = imageSrc;
        PageRef = pageRef;
        PageTitle = pageTitle;
        PageText = pageText;
    }

    public string ImageSrc { get; }
    public string PageRef { get; }
    public string PageTitle { get; }
    public string PageText { get; }
}