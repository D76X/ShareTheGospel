using BlazorApp.Client.Abstractions.Services;
using BlazorApp.Client.Pages.Models;
using BlazorApp.Client.Translations;
using Microsoft.AspNetCore.Components;
using Websites.Razor.ClassLibrary.Components;

namespace BlazorApp.Client.Components;

public class MainNavBarBase : ComponentBase
{
    [Inject]
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public ILanguageService? LanguageService { get; set; }
    
    protected override void OnInitialized()
    {
        LanguageService!.SelectedLanguage = LanguageSelectorBase.LanguageEn;
        base.OnInitialized();
    }

    public void LinkSelected(string toPage)
    {
        var nextUri = $"{toPage}/{LanguageSelectorBase.SelectedLanguage}";
        NavigationManager!.NavigateTo(nextUri);
    }

    public void LanguageSelected(string? selectedLanguage)
    {
        LanguageService!.SelectedLanguage = selectedLanguage;
        
        var baseUri = NavigationManager!.BaseUri;

        var currentUri = NavigationManager.Uri;
        if (currentUri.EndsWith($"/{selectedLanguage}")) return;

        if (currentUri.EndsWith($"/{LanguageSelectorBase.LanguageEn}") ||
            currentUri.EndsWith($"/{LanguageSelectorBase.LanguageDe}") ||
            currentUri.EndsWith($"/{LanguageSelectorBase.LanguageIt}"))
        {
            currentUri = currentUri.Substring(0, currentUri.Length - 3);
        }

        if (currentUri.EndsWith("/")) currentUri = currentUri.Substring(0, currentUri.Length - 1); 

        var nextUri = $"{currentUri}/{selectedLanguage}";
        NavigationManager.NavigateTo(nextUri);
			
    }

    public string miTour => Catalog.Translation(Catalog.Tour, LanguageSelectorBase.SelectedLanguage);
    protected string miIndex => PageTranslations.Translation(PageTranslations.Index, LanguageSelectorBase.SelectedLanguage);
    public string miDeath => PageTranslations.Translation(PageTranslations.Death001, LanguageSelectorBase.SelectedLanguage);
    public string miTheLaw => PageTranslations.Translation(PageTranslations.TheLaw001, LanguageSelectorBase.SelectedLanguage);
    public string miTheCross => PageTranslations.Translation(PageTranslations.TheCross001, LanguageSelectorBase.SelectedLanguage);
    public string miAbout => PageTranslations.Translation(PageTranslations.About, LanguageSelectorBase.SelectedLanguage);
}