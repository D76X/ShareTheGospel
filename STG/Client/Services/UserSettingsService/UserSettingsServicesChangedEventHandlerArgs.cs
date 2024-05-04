using Client.Shared;

namespace Client.Services.UserSettingsService
{
	public class UserSettingsServicesChangedEventHandlerArgs
    {
        private UserSettings _userSettings;

        public UserSettingsServicesChangedEventHandlerArgs(UserSettings userSettings)
        {
            _userSettings = userSettings;
        }
    }
}