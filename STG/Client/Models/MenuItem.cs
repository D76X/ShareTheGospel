using Websites.Razor.ClassLibrary.Abstractions.Models;

namespace Client.Models;

public class MenuItem : IMenuItem
{
    public string Href { get; }
    public string Text { get; }

    public MenuItem(
        string href,
        string text)
    {
        Href = href;
        Text = text;
    }
}