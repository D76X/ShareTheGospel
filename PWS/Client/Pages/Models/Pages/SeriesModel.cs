using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Pages.Models.Pages;

public class SeriesModel : ISeriesModel
{
    private readonly ICardService _cardService;

    public event Action? OnStateHasChanged;

    public SeriesModel(ICardService cardService)
    {
        _cardService = cardService;
    }

    public void Dispose()
    {
        //?
    }

    public ICardModel GetCard(string cardId) =>
        _cardService.GetCard(cardId);
}