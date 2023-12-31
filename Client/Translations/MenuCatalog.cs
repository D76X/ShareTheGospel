namespace BlazorApp.Client.Translations;

static class MenuCatalog
{
    public const string Tour = @"Tour";
    public const string About = @"About";

    public static Dictionary<string, string[]> Translations = new()
    {
        { Tour, new []{@"Tour", @"Tour", @"Visita" }},
        { About, new []{@"About", @"Infos", @"Informazioni" }},
    };

    public static string Translation(string id, string language) => Translator.Get(id, language, Translations);
}