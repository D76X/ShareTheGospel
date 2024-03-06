using System;
using BlazorApp.Client.Shared;

namespace BlazorApp.Client.Services.UserSettingsService
{
    public interface IUserSettingsService
	{
		event EventHandler<UserSettingsServicesChangedEventHandlerArgs> UserSettingsChanged;
		UserSettings UserSettings { get; set; }
		void OnSettingsChange();
	}
}
