using System;
using System.Threading.Tasks;

namespace nicochecker
{
	/// <summary>
	/// Video service session.
	/// </summary>
	public interface VideoServiceSession
	{
		/// <summary>
		/// Logins the async.
		/// </summary>
		/// <returns>The async.</returns>
		Task<VideoService> LoginAsync();
	}
}
