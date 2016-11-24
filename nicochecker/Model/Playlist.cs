using System;
using System.Collections.Generic;

namespace Model.VideoService
{
	public class Playlist
	{
		public String ID { get; }

		public String Title { get; }

		public String Creator { get; }

		public List<Movie> MovieList { get; set; }

		public List<String> TagList { get; }

		public Playlist()
		{
		}

		public void AddTag(String tag)
		{
		}

		public void DeleteTag(String tag)
		{
		}
	}
}
