using Xamarin.Forms;
using Plugin.Connectivity;

namespace NetStatus {
	public partial class App : Application {
		public App()
		{
			InitializeComponent();

			setNetworkStatus();
		}

		protected override void OnStart()
		{
			CrossConnectivity.Current.ConnectivityChanged += (sender, e) => {
				setNetworkStatus();
			};
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}

		void setNetworkStatus()
		{
			if (CrossConnectivity.Current != null && CrossConnectivity.Current.IsConnected) {
				MainPage = new NetworkViewPage();
			} else {
				MainPage = new NoNetworkPage();
			}

		}
	}
}
