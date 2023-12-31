namespace BlazorApp.Client.Translations;

static class PageCatalog
{
    public const string Index = @"Index";
    public const string Death001 = @"Death001";
    public const string TheLaw001 = @"TheLaw001";
    public const string TheCross001 = @"TheCross001";

    public static Dictionary<string, string[]> Translations = new()
    {
        {Index, new []{@"Home", @"Home", @"Home" }},
        {Death001, new [] {@"Death", @"Der Tod", @"La Morte" }},
        {TheLaw001, new [] {@"The Law", @"Das Gesetz", @"La legge"}},
        {TheCross001, new []{ @"The Cross", @"Das Kreuz ", @"La croce"}}
    };

    public static string Translation(string id, string language) => Translator.Get(id, language, Translations);

    //public static string Translation(
    //    string pageId,
    //    string language)
    //{
    //    int languageIndex;

    //    switch (language)
    //    {
    //        case LanguageSelectorBase.LanguageEn: languageIndex = 1; break;
    //        case LanguageSelectorBase.LanguageDe: languageIndex = 2; break;
    //        case LanguageSelectorBase.LanguageIt: languageIndex = 3; break;
    //        default: languageIndex = 0; break;

    //    }

    //    int pageIndex;

    //    switch (pageId)
    //    {
    //        case Death001: pageIndex = 1; break;
    //        case TheLaw001: pageIndex = 2; break;
    //        case TheCross001: pageIndex = 3; break;
    //        default: pageIndex = 0 ; break;
    //    }

    //    return Translations[pageIndex,languageIndex];
    //}
}