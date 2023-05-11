using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;

namespace BlazorApp.Client.Components
{
	public class MainNavBarBase : ComponentBase
	{
		protected const string CountryCodeUk = "gb";
		protected const string CountryCodeGermany = "de";
		protected const string CountryCodeItaly = "it";

		[Inject]
		public NavigationManager NavigationManager { get; set; }

		public void LanguageSelected(string selectedLanguage)
		{
			var baseUri = NavigationManager.BaseUri;
			var currentUri = NavigationManager.Uri;
			if(currentUri==baseUri) return;

			if (currentUri == $"{baseUri}#")
			{
				NavigationManager.NavigateTo($"{baseUri}{selectedLanguage}", forceLoad: true);
			};
			

			if (currentUri.EndsWith($"/{selectedLanguage}")) return;

			if (currentUri.EndsWith("/en") ||
			    currentUri.EndsWith("/de") ||
			    currentUri.EndsWith("/it"))
			{
				currentUri = currentUri.Substring(0, currentUri.Length - 3);
			}
			
			var nextUri = $"{currentUri}/{selectedLanguage}";
			NavigationManager.NavigateTo(nextUri);
			
		}
	}
}
