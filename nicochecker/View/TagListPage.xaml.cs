using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace nicochecker
{
	public partial class TagListPage : ContentPage
	{
		private TagPage TagPage;

		public TagListPage(TagPage tagPage)
		{
			InitializeComponent();

			this.Title = "タグ一覧";

			this.TagPage = tagPage;

			this.ListView.ItemsSource = Tags.getInstance().TagList;
		}

		private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			await Navigation.PopAsync();
			this.TagPage.AddTag(((Tags.Tag)e.Item));
		}
	}
}
