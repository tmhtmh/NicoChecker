using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace nicochecker
{
	/// <summary>
	/// Video service.
	/// </summary>
	public interface VideoService
	{
		/// <summary>
		/// Fetchs the playlist async.
		/// </summary>
		/// <returns>The playlist async.</returns>
		/// <param name="playlistID">Playlist identifier.</param>
		Task<Playlist> fetchPlaylistAsync(String playlistID);

		/// <summary>
		/// Fetchs the watch history list async.
		/// </summary>
		/// <returns>The watch history list async.</returns>
		Task<List<Movie>> fetchWatchHistoryListAsync();
	}
}
