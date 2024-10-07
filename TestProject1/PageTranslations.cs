using Websites.Razor.ClassLibrary.Translations;

namespace Websites.Razor.ClassLibrary.Test;

static class PageTranslations
{
    public const string TestPage01 = @"TestPage01";
        
    public static Dictionary<string, string[]> Translations = new()
    {
        {TestPage01, new []{@"English", @"Deutsch", @"Italiano" }}, };

    public static string Translation(string id, string language) => 
        Translator.Get(id, language, Translations);
}