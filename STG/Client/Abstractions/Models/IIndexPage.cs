using System;

namespace Client.Abstractions.Models;

public interface IIndexPage: IDisposable
{
    public ICardModel GetCard(string cardId);
}