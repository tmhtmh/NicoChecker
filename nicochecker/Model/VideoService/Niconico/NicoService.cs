using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nicochecker.Helpers;
using System.Xml;

namespace nicochecker
{
	/// <summary>
	/// Nico service.
	/// </summary>
	class NicoService : VideoService
	{
		private const string Domain = "http://www.nicovideo.jp";
		private HttpClient Client;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.NicoService"/> class.
		/// </summary>
		/// <param name="coockieContainer">Coockie container.</param>
		public NicoService(CookieContainer coockieContainer)
		{
			var handler = new HttpClientHandler() { CookieContainer = coockieContainer };
			this.Client = new HttpClient(handler);
		}

		public async Task<Playlist> fetchPlaylistAsync(string playlistID)
		{
			var result = await this.Client.GetStreamAsync(Domain + "/mylist/" + playlistID + "?rss=2.0&sort=1&nodescription=1&noinfo=1");

			XmlReader reader = XmlReader.Create(result);

			bool isReadMovie = false;
			Playlist playlist = new Playlist() { Id = playlistID };
			Movie movie = null;
			string tag = null;
			while (reader.Read())
			{
				switch (reader.NodeType)
				{
					case XmlNodeType.Element:
						tag = reader.Name;
						if (reader.Name.Equals("item"))
						{
							isReadMovie = true;
							movie = new Movie();
						}
						break;
					case XmlNodeType.EndElement:
						if (reader.Name.Equals("item"))
						{
							playlist.MovieList.Add(movie);
						}
						break;
					case XmlNodeType.Text:
					case XmlNodeType.CDATA:
						if (isReadMovie)
						{
							switch (tag)
							{
								case "title":
									movie.Title = reader.Value;
									break;
								case "link":
									movie.Url = reader.Value;
									break;
								case "description":
									int start = reader.Value.IndexOf("src=\"", StringComparison.Ordinal);
									start += "src=\"".Length;
									int end = reader.Value.IndexOf("\" width", StringComparison.Ordinal);
									movie.ThumbnailUrl = reader.Value.Substring(start, end - start);
									break;
								case "pubDate":
									movie.PostDate = DateTime.Parse(reader.Value);
									break;
							}
						}
						else
						{
							switch (tag)
							{
								case "title":
									playlist.Title = reader.Value.Replace("マイリスト ", "").Replace("‐ニコニコ動画", "");
									break;
								case "link":
									playlist.Url = reader.Value;
									break;
								case "dc:creator":
									playlist.Creator = reader.Value;
									break;
							}
						}
						break;
				}
			}

			return playlist;
		}

		public async Task<List<Movie>> fetchWatchHistoryListAsync()
		{
			var result = await this.Client.GetStringAsync(Domain + "/api/videoviewhistory/list");
			if (!result.Contains("history"))
			{
				throw new Exception();
			}

			var rootJson = JObject.Parse(result);
			JArray history = (JArray)rootJson.GetValue("history");

			List<Movie> movies = new List<Movie>();
			foreach (JObject item in history)
			{
				var movie = new Movie()
				{
					Url = "http://www.nicovideo.jp/watch/" + item.GetValue("video_id").Value<string>(),
					Title = item.GetValue("title").Value<string>(),
					ThumbnailUrl = item.GetValue("thumbnail_url").Value<string>(),
				};
				movie.Watched = true;
				movies.Add(movie);
			}

			return movies;
		}
	}
}
