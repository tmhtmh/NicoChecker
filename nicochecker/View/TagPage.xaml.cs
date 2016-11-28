using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace nicochecker
{
	public partial class TagPage : ContentPage
	{
		private ObservableCollection<Tags.Tag> TagList = new ObservableCollection<Tags.Tag>();

		private Playlist Playlist;

		private List<Playlist> Playlists;

		public TagPage(Playlist playlist, List<Playlist> playlists)
		{
			InitializeComponent();

			this.Title = "タグ";

			this.Playlist = playlist;
			this.Playlists = playlists;

			this.ListView.ItemsSource = this.TagList;

			List<String> tagNameList = playlist.TagList;
			foreach (var tagName in tagNameList)
			{
				this.TagList.Add(new Tags.Tag(tagName));
			}
		}

		public void AddTag(Tags.Tag tag)
		{
			foreach (var target in this.TagList)
			{
				if (target.Name.Equals(tag.Name))
				{
					DisplayAlert(null, "既に追加されているタグです。", "OK");
					return;
				}
			}

			this.TagList.Add(tag);
			Tags.getInstance().AddTag(tag.Name);
			this.Playlist.AddTag(tag.Name);
		}

		private void Handle_AddTagClicked(object sender, System.EventArgs e)
		{
			var tagName = this.TagInputEntry.Text;
			if (string.IsNullOrEmpty(tagName))
			{
				DisplayAlert(null, "タグを入力してください", "OK");
				return;
			}

			AddTag(new Tags.Tag(tagName));

			this.TagInputEntry.Text = string.Empty;
		}

		private void Handle_SelectTagClicked(object sender, System.EventArgs e)
		{
			Navigation.PushAsync(new TagListPage(this));
		}

		private void Handle_DeleteClicked(object sender, System.EventArgs e)
		{
			var tag = (Tags.Tag)((MenuItem)sender).CommandParameter;

			this.TagList.Remove(tag);
			this.Playlist.DeleteTag(tag.Name);

			updateTagList();
		}

		private void updateTagList()
		{
			Tags.getInstance().ClearTags();

			foreach (Playlist playlist in this.Playlists)
			{
				foreach (var tagName in playlist.TagList)
				{
					Tags.getInstance().AddTag(tagName);
				}
			}
		}
	}
}
