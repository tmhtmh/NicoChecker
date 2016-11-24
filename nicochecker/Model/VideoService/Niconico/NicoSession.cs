using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using nicochecker.Helpers;

namespace nicochecker
{
	/// <summary>
	/// Nico session.
	/// </summary>
	public class NicoSession : VideoServiceSession
	{
		private String UserName;

		private String Password;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.NicoSession"/> class.
		/// </summary>
		/// <param name="userName">User name.</param>
		/// <param name="password">Password.</param>
		public NicoSession(String userName, String password)
		{
			this.UserName = userName;
			this.Password = password;
		}

		public async Task<VideoService> LoginAsync()
		{
			var cookieCache = Settings.NicoLoginCookies;
			if (!string.IsNullOrEmpty(cookieCache))
			{
				CookieContainer cookieContainer = new CookieContainer();

				List<Cookie> cookies = JsonConvert.DeserializeObject<List<Cookie>>(Settings.NicoLoginCookies);
				foreach (var cookie in cookies)
				{
					cookieContainer.Add(new Uri("http://www.nicovideo.jp"), cookie);
				}

				return new NicoService(cookieContainer);
			}

			var handler = new HttpClientHandler() { UseCookies = true };
			HttpClient client = new HttpClient(handler);

			var content = new StringContent("mail=" + this.UserName + "&password=" + this.Password);
			content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded");
			var result = await client.PostAsync("https://secure.nicovideo.jp/secure/login?site=niconico", content);
			if (!result.IsSuccessStatusCode)
			{
				// TODO:
				throw new Exception();
			}

			foreach (var value in result.Headers.GetValues("x-niconico-authflag"))
			{
				if (value.Equals("0"))
				{
					// TODO:
					throw new Exception();
				}
			}

			Settings.NicoUser = this.UserName;
			Settings.NicoPassword = this.Password;

			JArray jArray = new JArray();

			CookieCollection cookieCollection = handler.CookieContainer.GetCookies(new Uri("https://secure.nicovideo.jp"));
			foreach (Cookie cookie in cookieCollection)
			{
				jArray.Add(JObject.Parse(JsonConvert.SerializeObject(cookie)));
			}

			Settings.NicoLoginCookies = jArray.ToString();
                       
			return new NicoService(handler.CookieContainer);
		}
	}
}
