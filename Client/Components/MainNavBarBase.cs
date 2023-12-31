﻿using BlazorApp.Client.Translations;
using Microsoft.AspNetCore.Components;
using Websites.Razor.ClassLibrary.Components;

namespace BlazorApp.Client.Components;

public class MainNavBarBase : ComponentBase
{
    [Inject]
    public NavigationManager NavigationManager { get; set; }
		
    public void LinkSelected(string toPage)
    {
        var nextUri = $"{toPage}/{LanguageSelectorBase.SelectedLanguage}";
        NavigationManager.NavigateTo(nextUri);
    }

    public void LanguageSelected(string selectedLanguage)
    {
        var baseUri = NavigationManager.BaseUri;

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

    public string miTour => MenuCatalog.Translation(MenuCatalog.Tour, LanguageSelectorBase.SelectedLanguage);
    protected string miIndex => PageCatalog.Translation(PageCatalog.Index, LanguageSelectorBase.SelectedLanguage);
    public string miDeath => PageCatalog.Translation(PageCatalog.Death001, LanguageSelectorBase.SelectedLanguage);
    public string miTheLaw => PageCatalog.Translation(PageCatalog.TheLaw001, LanguageSelectorBase.SelectedLanguage);
    public string miTheCross => PageCatalog.Translation(PageCatalog.TheCross001, LanguageSelectorBase.SelectedLanguage);
    public string miAbout => PageCatalog.Translation(PageCatalog.Index, LanguageSelectorBase.SelectedLanguage);
}