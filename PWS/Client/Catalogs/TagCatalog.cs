using Client.Shared;
using Websites.Razor.ClassLibrary.Abstractions;
using Websites.Razor.ClassLibrary.Abstractions.Models;
using Websites.Razor.ClassLibrary.Abstractions.Services;

namespace Client.Catalogs;

public class TagCatalog : 
    ITagCatalog,
    ISearchable
{
    public static readonly ITag TagNone = new Tag(@"None");
    public static readonly ITag TagAzure = new Tag(@"Azure");
    public static readonly ITag TagSecurity = new Tag(@"Security");
    public static readonly ITag TagCSharp = new Tag(@"C#");
    public static readonly ITag TagDesignPattern = new Tag(@"DesignPattern");
    public static readonly ITag TagAzureStorage = new Tag(@"AzureStorage");
    public static readonly ITag TagAzureBackup = new Tag(@"AzureBackup");

    public IEnumerable<ITag> Tags =>
    [
        TagNone, 
        TagAzure, 
        TagSecurity, 
        TagCSharp,
        TagDesignPattern,
        TagAzureStorage,
        TagAzureBackup
    ];
    
    public ISearchable[]? Searchables => throw new NotImplementedException();

    public ISearchResult GetResult(string searchTerm)
    {
        throw new NotImplementedException();
    }
}