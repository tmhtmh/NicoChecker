using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using nicochecker.Helpers;
using PropertyChanged;

namespace nicochecker
{
	/// <summary>
	/// Playlist Infomation.
	/// </summary>
	[ImplementPropertyChanged]
	public class Playlist
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>The identifier.</value>
		public String Id { get; set; }

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
		/// Gets or sets the creator.
		/// </summary>
		/// <value>The creator.</value>
		public String Creator { get; set; }

		/// <summary>
		/// Gets the thumbnail URL.
		/// </summary>
		/// <value>The thumbnail URL.</value>
		public String ThumbnailUrl
		{
			get 
			{
				return MovieList[0].ThumbnailUrl;
			}
		}

		/// <summary>
		/// Gets or sets the movie list.
		/// </summary>
		/// <value>The movie list.</value>
		public List<Movie> MovieList { get; set; }

		/// <summary>
		/// Gets or sets the tag list.
		/// </summary>
		/// <value>The tag list.</value>
		public List<String> TagList { 
			get
			{
				String data = Settings.GetValue("tag_" + Id, string.Empty);
				if (string.IsNullOrEmpty(data))
				{
					return new List<String>();
				}

				return JsonConvert.DeserializeObject<List<String>>(data);
			}
			set
			{
				Settings.SetValue("tag_" + Id, JsonConvert.SerializeObject(value));
			}
		}

		/// <summary>
		/// Gets or sets the no watched number.
		/// </summary>
		/// <value>The no watched number.</value>
		public int NoWatchedNum
		{
			get
			{
				int num = 0;
				foreach (var movie in MovieList)
				{
					if (!movie.Watched)
					{
						num++;
					}
				}
				return num;
			}
			set
			{
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.Playlist"/> class.
		/// </summary>
		public Playlist()
		{
			MovieList = new List<Movie>();
		}

		/// <summary>
		/// Adds the tag.
		/// </summary>
		/// <param name="tag">Tag Name.</param>
		public void AddTag(String tag)
		{
			List<string> tags = TagList;
			tags.Add(tag);
			TagList = tags;
		}

		/// <summary>
		/// Deletes the tag.
		/// </summary>
		/// <param name="tag">Tag Name.</param>
		public void DeleteTag(String tag)
		{
			List<string> tags = TagList;
			tags.Remove(tag);
			TagList = tags;
		}

		/// <summary>
		/// Clears the tag.
		/// </summary>
		public void ClearTag()
		{
			TagList = new List<string>();
		}
	}
}
