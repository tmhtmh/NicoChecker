using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using nicochecker.Helpers;
using Xamarin.Forms;

namespace nicochecker
{
	/// <summary>
	/// Nico login page.
	/// </summary>
	public partial class NicoLoginPage : ContentPage
	{
		private MainDetailPage MainDetailPage;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.NicoLoginPage"/> class.
		/// </summary>
		/// <param name="mainDetailPage">Main detail page.</param>
		public NicoLoginPage(MainDetailPage mainDetailPage)
		{
			InitializeComponent();

			this.MainDetailPage = mainDetailPage;
			this.Title = AppResources.NicoLogin;

			NicoUserEntry.Text = Settings.NicoUser;
			NicoPasswordEntry.Text = Settings.NicoPassword;
		}

		private async void HandleLoginCliked(object sender, System.EventArgs e)
		{
			VideoServiceSession session = new NicoSession(NicoUserEntry.Text, NicoPasswordEntry.Text);
			try
			{
				VideoService service = await session.LoginAsync();
				await Navigation.PopModalAsync();
				await this.MainDetailPage.Start(service);
			}
			catch
			{
				// TODO: エラー詳細
				await DisplayAlert(null, "ログインに失敗しました。", "OK");
			}
		}
	}
}
