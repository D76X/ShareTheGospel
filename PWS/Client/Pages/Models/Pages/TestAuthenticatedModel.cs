using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;
using Websites.Razor.ClassLibrary.Components;

namespace Client.Pages.Models.Pages;

public class TestAuthenticatedModel : ITestAuthenticatedModel
{
    /// <summary>
    /// the service that notifies this model about the selections of languages made by the user on the UI
    /// </summary>
    private readonly ILanguageService _languageService;

    /// <summary>
    /// the event that this model raises to notify the corresponding
    /// component StateHasChanged that it is time to redraw the UI as
    /// the model has changed.
    /// </summary>
    public event Action? OnStateHasChanged;

    public TestAuthenticatedModel(ILanguageService languageService)
    {
        _languageService = languageService;
        SelectedLanguage = LanguageSelectorBase.SelectedLanguage;
        _languageService.SelectedLanguageChanged += OnSelectedLanguageChanged;
    }

    private void OnSelectedLanguageChanged(
        object? sender,
        string e)
    {
        SelectedLanguage = e;
        OnStateHasChanged?.Invoke();
    }

    public string SelectedLanguage { get; set; }
    public bool IsEnglish => SelectedLanguage == LanguageSelectorBase.LanguageEn;
    public bool IsDeutsch => SelectedLanguage == LanguageSelectorBase.LanguageDe;
    public bool IsItalian => SelectedLanguage == LanguageSelectorBase.LanguageIt;

    public void Dispose()
    {
        _languageService.SelectedLanguageChanged -= OnSelectedLanguageChanged;
    }
}