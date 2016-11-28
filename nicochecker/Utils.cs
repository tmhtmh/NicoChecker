using System;
using Xamarin.Forms;

namespace nicochecker
{
	public class Utils
	{
		public static NavigationPage createNavigation(ContentPage page)
		{
			NavigationPage nav = new NavigationPage(page);
			nav.BarBackgroundColor = getPrimaryColor();
			nav.BarTextColor = Color.FromHex(Device.OnPlatform("007aff", "ffffff", "ffffff"));
			return nav;
		}

		public static Color getPrimaryColor()
		{
			return Color.FromHex(Device.OnPlatform("f5f5f5", "007aff", "9e9e9e"));
		}

		private Utils()
		{
		}
	}
}
