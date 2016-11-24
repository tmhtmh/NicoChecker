
using System.Collections.Generic;
using nicochecker.Helpers;
using Xamarin.Forms;

namespace nicochecker
{
	/// <summary>
	/// Main page.
	/// </summary>
	public partial class MainPage : MasterDetailPage
	{
		/// <summary>
		/// Gets the detail page.
		/// </summary>
		/// <value>The detail page.</value>
		public MainDetailPage DetailPage { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:nicochecker.MainPage"/> class.
		/// </summary>
		public MainPage()
		{
			InitializeComponent();
			DetailPage = new MainDetailPage();
			this.Detail = Utils.createNavigation(DetailPage);
			this.Master = new MainMasterPage(this, DetailPage);
		}
	}
}
