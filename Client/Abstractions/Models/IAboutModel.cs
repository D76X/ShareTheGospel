namespace BlazorApp.Client.Abstractions.Models;

public interface IAboutModel: IDisposable
{
    public event Action OnLanguageSelection;
    string SelectedLanguage { get; }
    bool IsEnglish { get; }
    bool IsDeutsch { get; }
    bool IsItalian { get; }
}