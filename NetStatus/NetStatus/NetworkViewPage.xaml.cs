using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Plugin.Connectivity;
using System.Linq;
using Plugin.Connectivity.Abstractions;
using System.Text;

namespace NetStatus {
	public partial class NetworkViewPage : ContentPage {
		public NetworkViewPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			setNetworkStatus();

			if (CrossConnectivity.Current != null) {
				CrossConnectivity.Current.ConnectivityChanged += OnConnectivityChanged;
			}

			base.OnAppearing();
		}

		protected override void OnDisappearing()
		{
			if (CrossConnectivity.Current != null) {
				CrossConnectivity.Current.ConnectivityChanged -= OnConnectivityChanged;
			}

			base.OnDisappearing();
		}

		private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
		{
			setNetworkStatus();
		}

		private void setNetworkStatus()
		{
			if (CrossConnectivity.Current != null
				&& CrossConnectivity.Current.ConnectionTypes != null) 
			{
				var sb = new StringBuilder();

				foreach (var type in CrossConnectivity.Current.ConnectionTypes) {
					sb.Append(type.ToString());
				}

				this.ConnectionDetails.Text = sb.ToString();
			} else {
				this.ConnectionDetails.Text = "???";
			}
		}
	}
}
