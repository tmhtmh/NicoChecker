using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace nicochecker
{
	public partial class AddCommentPage : ContentPage
	{
		private Playlist Playlist;

		public AddCommentPage(Playlist playlist)
		{
			InitializeComponent();

			this.Title = "コメント";

			ToolbarItems.Add(new ToolbarItem("保存", "ic_check.png", async () =>
			{
				this.Playlist.Comment = CommentEditor.Text;
				await Navigation.PopAsync();
			}));

			this.Playlist = playlist;

			this.CommentEditor.Text = playlist.Comment;
		}
	}
}
