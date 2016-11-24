using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace nicochecker
{
	/// <summary>
	/// Playlist movies page.
	/// </summary>
	public partial class PlaylistMoviesPage : ContentPage
	{
		private Playlist Playlist;

		private List<Playlist> Playlists;

		private ObservableCollection<Movie> Movies;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.PlaylistMoviesPage"/> class.
		/// </summary>
		/// <param name="playlist">Playlist.</param>
		/// <param name="playlists">Playlists.</param>
		public PlaylistMoviesPage(Playlist playlist, List<Playlist> playlists)
		{
			InitializeComponent();

			this.Title = playlist.Title;

			this.Playlist = playlist;
			this.Playlists = playlists;

			Movies = new ObservableCollection<Movie>(playlist.MovieList);
			this.ListView.ItemsSource = Movies;

			ToolbarItems.Add(new ToolbarItem("タグ", "ic_local_offer.png", async () =>
			{
				// TODO:
			}));

			ToolbarItems.Add(new ToolbarItem("コメント", "ic_mode_comment.png", async () =>
			{
				// TODO:
			}));
		}

		private async void Handle_ItemTapped(object sender, Xamarin.Forms.ItemTappedEventArgs e)
		{
			Movie movie = (Movie) e.Item;

			var result = await DisplayActionSheet("メニュー", "キャンセル", null, "動画を見る", "既読にする", "ここまで既読にする", "未読にする", "これ以降を未読にする");
			switch (result)
			{
				case "動画を見る":
					movie.Watched = true;
					Device.OpenUri(new Uri(movie.Url));
					break;
				case "既読にする":
					movie.Watched = true;
					break;
				case "未読にする":
					movie.Watched = false;
					break;
				case "ここまで既読にする":
					changeWatched(movie, true);
					break;
				case "これ以降を未読にする":
					changeWatched(movie, false);
					break;
				case "キャンセル":
					return;
			}

			// 既読数通知
			this.Playlist.NoWatchedNum = -1;
		}

		private void changeWatched(Movie movie, bool watched)
		{
			bool isTarget = !watched;
			foreach (Movie target in this.Movies)
			{
				if (target.Equals(movie))
				{
					target.Watched = watched;
					isTarget = watched;
				}

				if (isTarget)
				{
					target.Watched = watched;
				}
			}
		}
	}
}
