using Websites.Razor.ClassLibrary.Translations;
namespace Client.Translations;

static class PageTranslations
{
    public const string Index = @"Index";
    public const string About = @"About";
    public const string AzureSeries001 = @"AzureSeries001";
    public const string Security001 = @"Security001";
    public const string CSharp001 = @"CSharp001";
    public const string DesignPatterns001 = @"DesignPatterns001";
    public const string AzureBackup001 = @"AzureBackup001";
    public const string AzureStorage001 = @"AzureStorage001";
    public const string ServiceEndpoint001 = @"ServiceEndpoint001";

    public static Dictionary<string, string[]> Translations = new()
    {
        {Index, new []{@"Home", @"Home", @"Home" }},
        {About, new []{ @"About", @"About ", @"About"}},
        {AzureSeries001, new [] {@"Azure", @"Azure", @"Azure" }},
        {Security001, new [] { @"Security", @"Sicherheit", @"Sicurezza" }},
        {CSharp001, new [] {@"C#", @"C#", @"C#" }},
        {DesignPatterns001, new [] { @"Design Patterns", @"Designmuster", @"Design Patterns" }},
        {AzureBackup001, new [] { @"Azure Backup", @"Azure Backup", @"Azure Backup" }},
        {AzureStorage001, new [] { @"Azure Storage", @"Azure Storage", @"Azure Storage" }},
        {ServiceEndpoint001, new [] { @"Service Endpoint", @"Service Endpoint", @"Service Endpoint" }},
    };

    public static string Translation(string id, string language) => Translator.Get(id, language, Translations);
}