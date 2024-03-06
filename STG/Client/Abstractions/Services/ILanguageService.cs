using System;

namespace BlazorApp.Client.Abstractions.Services;

public interface ILanguageService
{ 
    string? SelectedLanguage { get; set; }
    event EventHandler<string>? SelectedLanguageChanged;
}