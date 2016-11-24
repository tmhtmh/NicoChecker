using System;
using nicochecker.Helpers;
using PropertyChanged;

namespace nicochecker
{
	/// <summary>
	/// Movie Infomation.
	/// </summary>
	[ImplementPropertyChanged]
	public class Movie
	{
		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		/// <value>The URL.</value>
		public String Url { get; set; }

		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		/// <value>The title.</value>
		public String Title { get; set; }

		/// <summary>
		/// Gets or sets the thumbnail URL.
		/// </summary>
		/// <value>The thumbnail URL.</value>
		public String ThumbnailUrl { get; set; }

		/// <summary>
		/// Gets or sets the post date.
		/// </summary>
		/// <value>The post date.</value>
		public DateTime PostDate { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:nicochecker.Movie"/> is watched.
		/// </summary>
		/// <value><c>true</c> if watched; otherwise, <c>false</c>.</value>
		public Boolean Watched 
		{ 
			get
			{
				return System.Convert.ToBoolean(Settings.GetValue<String>(Url, false.ToString()));
			}
			set
			{
				Settings.SetValue<string>(Url, value.ToString());
				Unwatched = !value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="T:nicochecker.Movie"/> is unwatched.
		/// </summary>
		/// <value><c>true</c> if unwatched; otherwise, <c>false</c>.</value>
		public Boolean Unwatched
		{
			get
			{
				return !Watched;
			}
			set
			{
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.Movie"/> class.
		/// </summary>
		public Movie()
		{
		}
	}
}
