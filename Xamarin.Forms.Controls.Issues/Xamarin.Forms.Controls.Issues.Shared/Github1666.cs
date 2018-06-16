using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;

#if UITEST
using Xamarin.UITest;
using NUnit.Framework;
#endif

namespace Xamarin.Forms.Controls.Issues
{
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 1666, "[Enhancement] Use WKWebView on iOS", PlatformAffected.iOS)]
	public class Github1666 : TestContentPage // or TestMasterDetailPage, etc ...
	{
		protected override void Init()
		{
			var stackLayout = new StackLayout
			{
				Orientation = StackOrientation.Vertical,
				Spacing = 10
			};

			var webView = new WebView
			{
				BackgroundColor = Color.Magenta,
				Source = "https://blog.xamarin.com",
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			stackLayout.Children.Add(webView);

			var status = new Label
			{
				Text = "Status",
				HorizontalTextAlignment = TextAlignment.Center
			};
			stackLayout.Children.Add(status);

			webView.Navigating += (sender, e) => { status.Text = "Navigating"; };
			webView.Navigated += (sender, e) => { status.Text = "Navigated"; };

			var stackLayoutChild1 = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Spacing = 10,
				HorizontalOptions = LayoutOptions.Center
			};

			var button = new Button
			{
				Text = "Back",
				Command = new Command(async () =>
				{
					if (webView.CanGoBack)
						webView.GoBack();
					else
						await DisplayAlert("Alert", "Can't go back", "Dismiss");
				})
			};
			stackLayoutChild1.Children.Add(button);

			var button2 = new Button
			{
				Text = "Forward",
				Command = new Command(async () => 
				{
					if (webView.CanGoForward)
						webView.GoForward();
					else
						await DisplayAlert("Alert", "Can't go forward", "Dismiss");
				})
			};
			stackLayoutChild1.Children.Add(button2);

			stackLayout.Children.Add(stackLayoutChild1);

			var stackLayoutChild2 = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Spacing = 10,
				HorizontalOptions = LayoutOptions.Center
			};

			var button3 = new Button
			{
				Text = "EvaluateJS",
				Command = new Command(async () => { DisplayAlert("Alert", await webView.EvaluateJavaScriptAsync("document.title"), "Dismiss"); })
			};
			stackLayoutChild2.Children.Add(button3);

			var button4 = new Button
			{
				Text = "HTMLWebViewSource",
				Command = new Command(() =>
				{
					var htmlSource = new HtmlWebViewSource
					{
						Html = @"<html><body><h1>Xamarin.Forms</h1><p>Welcome to WebView.</p></body></html>"
					};
					webView.Source = htmlSource;
				})
			};
			stackLayoutChild2.Children.Add(button4);

			stackLayout.Children.Add(stackLayoutChild2);

			Content = stackLayout;
		}
	}
}