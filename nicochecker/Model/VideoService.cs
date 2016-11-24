using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Model.VideoService
{
	public interface VideoService
	{
	  	Task<List<Playlist>> fetchPlaylistAsync(String playlistID);

		Task<Playlist> fetchMovieListAsync(Playlist playlist);

		Task<List<Movie>> fetchWatchHistoryListAsync();
	}
}
