using System;
using Xamarin.Forms;

namespace nicochecker
{
	public class Setting
	{
		public void SetValue<T>(SettingKey key, T value) where T : class
		{
			Application.Current.Properties[key.ToString()] = value;
			Application.Current.SavePropertiesAsync();
		}

		public T GetValue<T>(SettingKey key) where T : class
		{
			return Application.Current.Properties.ContainsKey(key.ToString()) ? Application.Current.Properties[key.ToString()] as T : null;
		}
	}
}
