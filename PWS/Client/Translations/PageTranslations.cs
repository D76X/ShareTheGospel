namespace Client.Translations;

static class PageTranslations
{
    public const string Index = @"Index";
    public const string About = @"About";
    public const string Azure001 = @"Azure001";
    public const string CSharp001 = @"CSharp001";
    public const string DesignPatterns001 = @"DesignPatterns001";

    public static Dictionary<string, string[]> Translations = new()
    {
        {Index, new []{@"Home", @"Home", @"Home" }},
        {About, new []{ @"About", @"About ", @"About"}},
        {Azure001, new [] {@"Azure", @"Azure", @"Azure" }},
        {CSharp001, new [] {@"C#", @"C#", @"C#" }},
        {DesignPatterns001, new [] { @"Design Patterns", @"Designmuster", @"Design Patterns" }},
	};

    public static string Translation(string id, string language) => Translator.Get(id, language, Translations);
}