using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Pages.Models.Pages;

public class SeriesModel : ISeriesModel
{
    private readonly ICardService _cardService;
    
    public SeriesModel(ICardService cardService)
    {
        _cardService = cardService;
    }

    public void Dispose()
    {
        //?
    }
    

    public IEnumerable<ICardModel> GetCards(IEnumerable<ITag> tags) =>
        _cardService.GetCards(tags);
}