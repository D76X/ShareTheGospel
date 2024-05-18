using System.Collections.Generic;
using Client.Translations;

namespace Client.Catalogs;

public static class PageCatalog
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
