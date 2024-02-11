using BlazorApp.Client.Abstractions.Models;

namespace BlazorApp.Client.Abstractions.Services
{
    public interface ICardService
    {
        ICardModel GetCard(string cardId);
    }
}
