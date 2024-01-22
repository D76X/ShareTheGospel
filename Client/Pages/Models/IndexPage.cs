using BlazorApp.Client.Abstractions.Models;

namespace BlazorApp.Client.Pages.Models
{
    public class IndexPage: IIndexPage
    {
        public string SelectedLanguage { get; set; }

        public void Dispose()
        {
            //
        }
    }
}
