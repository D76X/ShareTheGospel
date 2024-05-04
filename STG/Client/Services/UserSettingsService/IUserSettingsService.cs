using System;
using Client.Shared;

namespace Client.Services.UserSettingsService
{
    public interface IUserSettingsService
	{
		event EventHandler<UserSettingsServicesChangedEventHandlerArgs> UserSettingsChanged;
		UserSettings UserSettings { get; set; }
		void OnSettingsChange();
	}
}
