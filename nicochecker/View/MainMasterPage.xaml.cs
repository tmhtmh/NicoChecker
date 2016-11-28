using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace nicochecker
{
	/// <summary>
	/// Main master page.
	/// </summary>
	public partial class MainMasterPage : ContentPage
	{
		private MainPage MainPage;

		private MainDetailPage DetailPage;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.MainMasterPage"/> class.
		/// </summary>
		/// <param name="mainPage">Main page.</param>
		/// <param name="detailPage">Detail page.</param>
		public MainMasterPage(MainPage mainPage, MainDetailPage detailPage)
		{
			InitializeComponent();

			this.Title = "メニュー";
			this.Icon = "ic_menu.png";
			this.MainPage = mainPage;
			this.DetailPage = detailPage;
			this.BackgroundColor = Utils.getPrimaryColor();
			this.BackgroundColor = Color.FromHex("007aff");

			this.ListView.ItemsSource = Tags.getInstance().TagList;
			this.ListView.BackgroundColor = Utils.getPrimaryColor();
			this.ListView.BackgroundColor = Color.FromHex("007aff");
		}

		private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			DetailPage.ChangeTag(((Tags.Tag)e.Item));
			this.MainPage.IsPresented = false;
		}
	}
}
