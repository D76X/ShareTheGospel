using Client.Abstractions.Models;

namespace Client.Abstractions.Services
{
    public interface ICardService
    {
        ICardModel GetCard(string cardId);
    }
}
