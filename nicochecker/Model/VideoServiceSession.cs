using System;
using System.Threading.Tasks;

namespace Model.VideoService
{
	public interface VideoServiceSession
	{
		Task<VideoService> LoginAsync();
	}
}
