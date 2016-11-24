using System;
using System.Net;
using Xamarin.Forms;

namespace nicochecker
{
	public class NicoLoginSetting : Setting
	{
		public NicoLoginSetting()
		{
		}

		public String GetUser()
		{
			return GetValue<String>(SettingKey.NicoUser);
		}

		public String GetPassword()
		{
			return GetValue<String>(SettingKey.NicoPassword);
		}

		public CookieContainer GetCookie()
		{
			return GetValue<CookieContainer>(SettingKey.NicoCookie);
		}

		public void SetUser(String value)
		{
			SetValue<String>(SettingKey.NicoUser, value);
		}

		public void SetPassword(String value)
		{
			SetValue<String>(SettingKey.NicoPassword, value);
		}

		public void SetCookie(CookieContainer value)
		{
			SetValue<CookieContainer>(SettingKey.NicoCookie, value);
		}
	}
}
