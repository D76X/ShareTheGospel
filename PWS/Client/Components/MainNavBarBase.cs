﻿using Client.Translations;
using Microsoft.AspNetCore.Components;
using Websites.Razor.ClassLibrary.Abstractions.Services;
using Websites.Razor.ClassLibrary.Components;

namespace Client.Components;

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

    protected string miIndex => PageTranslations.Translation(PageTranslations.Index, LanguageSelectorBase.SelectedLanguage);
    
    public string miAbout => PageTranslations.Translation(PageTranslations.About, LanguageSelectorBase.SelectedLanguage);
}