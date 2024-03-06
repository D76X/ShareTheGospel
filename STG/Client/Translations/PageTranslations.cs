using System.Collections.Generic;
using BlazorApp.Client.Pages;

namespace BlazorApp.Client.Translations;

static class PageTranslations
{
    public const string Index = @"Index";
    public const string About = @"About";
    public const string Death001 = @"Death001";
    public const string TheLaw001 = @"TheLaw001";

    public const string TheCross001 = @"TheCross001";

    public static Dictionary<string, string[]> Translations = new()
    {
        {Index, new []{@"Home", @"Home", @"Home" }},
        {About, new []{ @"About", @"About ", @"About"}},
        {Death001, new [] {@"Death", @"Der Tod", @"La Morte" }},
        {TheLaw001, new [] {@"The Law", @"Das Gesetz", @"La legge"}},
        {TheCross001, new []{ @"The Cross", @"Das Kreuz ", @"La croce"}},
    };

    public static string Translation(string id, string language) => Translator.Get(id, language, Translations);
}