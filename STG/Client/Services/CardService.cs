using BlazorApp.Client.Abstractions.Models;
using BlazorApp.Client.Abstractions.Services;
using BlazorApp.Client.Common;

namespace BlazorApp.Client.Services;

class CardService : ICardService
{
    private readonly ILanguageService _languageService;

    public CardService(ILanguageService languageService)
    {
        _languageService = languageService;
    }

    public ICardModel GetCard(string cardId) =>
        CardCatalog.GetCard(
            cardId,
            _languageService.SelectedLanguage);

}