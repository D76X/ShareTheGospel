using System.Collections.Generic;
using BlazorApp.Client.Translations;

namespace BlazorApp.Client.Pages.Models;

public static class Catalog
{
    public const string Tour = @"Tour";
    public const string KeyAbout = @"About";

    public static Dictionary<string, string[]> Translations = new()
    {
        { Tour, new []{@"Tour", @"Tour", @"Visita" }},
        { KeyAbout, new []{@"About", @"Infos", @"Informazioni" }},
    };

    public static string Translation(string id, string language) => Translator.Get(id, language, Translations);
}
