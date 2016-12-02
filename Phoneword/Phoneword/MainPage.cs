using System;
using Xamarin.Forms;

namespace Phoneword
{
	public class MainPage : ContentPage
	{
		protected string translatedNumber { get; set; }

		public MainPage()
		{
			var entry = new Entry
			{
				Text = "1-855-XAMARIN"
			};

			var translateButton = new Button
			{
				Text = "Translate"
			};

			var callButton = new Button
			{
				Text = "Call",
				IsEnabled = false
			};

			translateButton.Clicked += (sender, e) =>
			{
				translatedNumber = PhonewordTranslator.ToNumber(entry.Text);
				if (translatedNumber != null)
				{
					callButton.Text = "Call " + translatedNumber;
					callButton.IsEnabled = true;
				}
				else {
					callButton.Text = "Call";
					callButton.IsEnabled = false;
				}
					
			};

			callButton.Clicked += async (object sender, EventArgs e) =>
			{
				if (await DisplayAlert(
					"Dial a Number",
					"Would you like to call " + translatedNumber + "?",
					"Yes",
					"No"))
				{
					var dialer = DependencyService.Get<IDialer>();
					if (dialer != null)
					{
						await dialer.DialAsync(translatedNumber);
					}
				}
			};

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Orientation = StackOrientation.Vertical,
				Spacing = 15,
				Children = {
					new Label {
						HorizontalTextAlignment = TextAlignment.Center,
						Text = "Enter a Phoneword"
					},
					entry,
					translateButton,
					callButton
				}
			};

			Padding = Device.OnPlatform<double>(40, 20, 20);
		}
	}
}
