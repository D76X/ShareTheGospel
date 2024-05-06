using Client.Abstractions.Services;
using Client.Common;
using Websites.Razor.ClassLibrary.Abstractions.Models;

namespace Client.Services;

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