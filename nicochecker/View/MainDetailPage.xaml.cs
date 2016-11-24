using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using nicochecker.Helpers;
using Xamarin.Forms;

namespace nicochecker
{
	/// <summary>
	/// Main detail page.
	/// </summary>
	public partial class MainDetailPage : ContentPage
	{
		private ObservableCollection<Playlist> DisplayPlaylists = new ObservableCollection<Playlist>();

		private List<Playlist> AllPlaylists = new List<Playlist>();

		private VideoService Service;

		private Tags.Tag CurrentTag = Tags.DefaultTag;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.MainDetailPage"/> class.
		/// </summary>
		public MainDetailPage()
		{
			InitializeComponent();

			this.Title = CurrentTag.Name;

			ToolbarItems.Add(new ToolbarItem("追加", "ic_add.png", async () =>
			{
				await Navigation.PushAsync(new AddPlayListManualPage(this, this.Service));
			}));

			this.ListView.ItemsSource = DisplayPlaylists;

			this.ListView.Refreshing += async (sender, e) =>
			{
				this.AllPlaylists.Clear();
				await Start();
			};

			if (string.IsNullOrEmpty(Settings.NicoLoginCookies))
			{
				Navigation.PushModalAsync(new NicoLoginPage(this));
				return;
			}

			Start();
		}

		/// <summary>
		/// Start this instance.
		/// </summary>
		public async Task Start()
		{
			this.DisplayPlaylists.Clear();
			VideoService service = await new NicoSession(Settings.NicoUser, Settings.NicoPassword).LoginAsync();
			await Start(service);
		}

		/// <summary>
		/// Start the specified service.
		/// </summary>
		/// <param name="service">Service.</param>
		public async Task Start(VideoService service)
		{
			this.ListView.IsRefreshing = true;

			this.Service = service;
			// 履歴取得
			try
			{
				await this.Service.fetchWatchHistoryListAsync();
			}
			catch
			{
				// 履歴取得に失敗したらクッキー情報をクリアし、再ログイン
				Settings.NicoLoginCookies = string.Empty;
				try
				{
					this.Service = await new NicoSession(Settings.NicoUser, Settings.NicoPassword).LoginAsync();
					await this.Service.fetchWatchHistoryListAsync();
				}
				catch
				{
					// ログイン失敗したらログイン要求
					this.ListView.IsRefreshing = false;
					await Navigation.PushModalAsync(new NicoLoginPage(this));
				}
			}

			var playlists = Settings.Playlists;
			foreach (Playlist value in playlists)
			{
				try
				{
					var playlist = await this.Service.fetchPlaylistAsync(value.Id);
					this.AllPlaylists.Add(playlist);
				}
				catch
				{
					// 取得失敗したものは飛ばす（削除されている可能性あり）
				}
			}

			updateDisplayItems();
			updateTagList();

			this.ListView.IsRefreshing = false;
		}

		/// <summary>
		/// Adds the playlist.
		/// </summary>
		/// <param name="playlist">Playlist.</param>
		public void AddPlaylist(Playlist playlist)
		{
			this.AllPlaylists.Add(playlist);
			savePlaylists();

			// 追加時は作成者をタグに自動追加
			playlist.AddTag(playlist.Creator);

			ChangeTag(Tags.DefaultTag);
			updateTagList();
		}

		/// <summary>
		/// Changes the tag.
		/// </summary>
		/// <param name="tag">Tag.</param>
		public void ChangeTag(Tags.Tag tag)
		{
			this.CurrentTag = tag;
			this.Title = tag.Name;

			updateDisplayItems();
		}

		private void updateDisplayItems()
		{
			this.DisplayPlaylists.Clear();

			foreach (Playlist playlist in this.AllPlaylists)
			{
				if (this.CurrentTag.Name.Equals("全て"))
				{
					this.DisplayPlaylists.Add(playlist);
					continue;
				}

				foreach (var tagName in playlist.TagList)
				{
					if (tagName.Equals(CurrentTag.Name))
					{
						this.DisplayPlaylists.Add(playlist);
					}
				}
			}
		}

		private void updateTagList()
		{
			Tags.getInstance().ClearTags();

			foreach (Playlist playlist in this.AllPlaylists)
			{
				foreach (var tagName in playlist.TagList)
				{
					Tags.getInstance().AddTag(tagName);
				}
			}
		}

		private void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			Navigation.PushAsync(new PlaylistMoviesPage((Playlist) e.Item, this.AllPlaylists));
		}

		private void Handle_DeleteClicked(object sender, System.EventArgs e)
		{
			Playlist playlist = (Playlist)((MenuItem)sender).CommandParameter;
			this.DisplayPlaylists.Remove(playlist);
			this.AllPlaylists.Remove(playlist);
			playlist.ClearTag();
			savePlaylists();

			updateTagList();
		}

		private void savePlaylists()
		{
			Settings.Playlists = this.AllPlaylists;
		}
	}
}
