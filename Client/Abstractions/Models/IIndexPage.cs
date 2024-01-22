namespace BlazorApp.Client.Abstractions.Models;

public interface IIndexPage: IDisposable
{
    string SelectedLanguage { get; set; }
}