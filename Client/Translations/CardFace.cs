namespace BlazorApp.Client.Translations;

interface ICardFace
{
    
}

public class CardFace: ICardFace
{
    private Dictionary<string, string[]> Translations { get; }

    public CardFace(
        string imageSrc,
        string[] pageRefs,
        string[] pageTitles,
        string[] pageTexts)
    {
        ImageSrc = imageSrc;

        Translations = new()
        {
            { nameof(PageRef), pageRefs},
            { nameof(PageTitle), pageTitles},
            { nameof(PageText), pageTexts},
        };
    }

    public string ImageSrc { get; }
    public string PageRef(string language) => Translation(nameof(PageRef), language);
    public string PageTitle(string language) => Translation(nameof(PageTitle), language);
    public string PageText(string language) => Translation(nameof(PageText), language);

    private string Translation(string id, string language) => Translator.Get(id, language, Translations);
}