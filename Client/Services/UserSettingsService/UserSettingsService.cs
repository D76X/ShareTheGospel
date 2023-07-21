using BlazorApp.Client.Services.LocalStorage;
using BlazorApp.Client.Shared;

namespace BlazorApp.Client.Services.UserSettingsService;

class UserSettingsService : IUserSettingsService
{
	public UserSettingsService(ILocalStorageService localStorageService)
	{
		// No!
		// https://learn.microsoft.com/en-us/answers/questions/1023717/async-calls-inside-constructor
		//_settings = await localStorageService.GetItemAsync<UserSettings>($"{nameof(UserSettings)}");
	}

	private UserSettings _defaultSettings = new UserSettings()
	{
		UserName = "defaultName",
		UserValue = "defaultValue",
		UserActive = true,
	};

	private UserSettings _settings;
	
	public event EventHandler<UserSettingsServicesChangedEventHandlerArgs> UserSettingsChanged;

	public UserSettings UserSettings
	{
		get => _settings ?? _defaultSettings;
		set { _settings = value; }
	}

	public void OnSettingsChange()
	{
		UserSettingsChanged?.Invoke(this, new UserSettingsServicesChangedEventHandlerArgs(UserSettings));
	}
}

