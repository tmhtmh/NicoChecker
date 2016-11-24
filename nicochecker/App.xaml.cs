using System.Diagnostics;
using nicochecker.Helpers;
using Xamarin.Forms;

namespace nicochecker
{
	public partial class App : Application
	{
		private MainDetailPage DetailPage;
		public App()
		{
			InitializeComponent();

			MainPage mainPage = new MainPage();
			MainPage = mainPage;

			DetailPage = mainPage.DetailPage;
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected async override void OnResume()
		{
			// Handle when your app resumes
			// 履歴取得

			if (string.IsNullOrEmpty(Settings.NicoLoginCookies))
			{
				return;
			}

			await DetailPage.Start();
		}
	}
}
