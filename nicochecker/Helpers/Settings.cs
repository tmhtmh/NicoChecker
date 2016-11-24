// Helpers/Settings.cs
using System.Collections.Generic;
using Newtonsoft.Json;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace nicochecker.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters.
	/// </summary>
	public static class Settings
	{
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		#region Setting Constants

		private static readonly string SettingsDefault = string.Empty;
		private enum Keys
		{
			NicoUser,
			NicoPassword,
			NicoLoginCookies,
			Playlist,
		}

		#endregion


		public static string NicoUser
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(Keys.NicoUser.ToString(), SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(Keys.NicoUser.ToString(), value);
			}
		}

		public static string NicoPassword
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(Keys.NicoPassword.ToString(), SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(Keys.NicoPassword.ToString(), value);
			}
		}

		public static string NicoLoginCookies
		{
			get
			{
				return AppSettings.GetValueOrDefault<string>(Keys.NicoLoginCookies.ToString(), SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue<string>(Keys.NicoLoginCookies.ToString(), value);
			}
		}

		public static List<Playlist> Playlists
		{
			get
			{
				var data = AppSettings.GetValueOrDefault<string>(Keys.Playlist.ToString(), SettingsDefault);
				if (data.Equals(SettingsDefault))
				{
					return new List<Playlist>();
				}

				List<SavePlaylist> savePlaylists = JsonConvert.DeserializeObject<List<SavePlaylist>>(data);
				List<Playlist> playlists = new List<Playlist>();
				foreach (SavePlaylist savePlaylist in savePlaylists)
				{
					playlists.Add(new Playlist() { Id = savePlaylist.Id });
				}

				return playlists;
			}
			set
			{
				List<SavePlaylist> savePlaylists = new List<SavePlaylist>();
				foreach (Playlist playlist in value)
				{
					savePlaylists.Add(new SavePlaylist() { Id = playlist.Id, TagList = playlist.TagList });
				}
				AppSettings.AddOrUpdateValue<string>(Keys.Playlist.ToString(), JsonConvert.SerializeObject(savePlaylists));
			}
		}

		private class SavePlaylist
		{
			public string Id { get; set; }

			public List<string> TagList { get; set; }
		}


		public static void SetValue<T>(string key, T value) where T : class
		{
			AppSettings.AddOrUpdateValue<T>(key, value);
		}

		public static T GetValue<T>(string key, T defaultValue) where T : class
		{
			return AppSettings.GetValueOrDefault<T>(key, defaultValue);
		}
	}
}