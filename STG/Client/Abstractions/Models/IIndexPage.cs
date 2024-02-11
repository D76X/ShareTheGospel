namespace BlazorApp.Client.Abstractions.Models;

public interface IIndexPage: IDisposable
{
    public ICardModel GetCard(string cardId);
}