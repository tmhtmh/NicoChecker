using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace nicochecker
{
	/// <summary>
	/// Add play list manual page.
	/// </summary>
	public partial class AddPlayListManualPage : ContentPage
	{
		private MainDetailPage MainPage;

		private VideoService Service;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.AddPlayListManualPage"/> class.
		/// </summary>
		/// <param name="mainPage">Main page.</param>
		/// <param name="service">Service.</param>
		public AddPlayListManualPage(MainDetailPage mainPage, VideoService service)
		{
			InitializeComponent();

			this.Title = "マイリストを追加";

			this.MainPage = mainPage;
			this.Service = service;
		}

		private async void Handle_AddPlaylistClicked(object sender, System.EventArgs e)
		{
			try
			{
				var playlist = await this.Service.fetchPlaylistAsync(this.PlaylistIdEntry.Text);
				this.MainPage.AddPlaylist(playlist);
				await Navigation.PopAsync();
			}
			catch
			{
				await DisplayAlert(null, "マイリストが見つかりませんでした。", "OK");
			}
		}
	}
}
