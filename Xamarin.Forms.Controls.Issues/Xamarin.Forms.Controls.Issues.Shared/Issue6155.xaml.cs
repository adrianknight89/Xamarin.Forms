using System.Collections.ObjectModel;
using Xamarin.Forms.CustomAttributes;
using Xamarin.Forms.Internals;
using System;
using System.Security.Cryptography;
using Xamarin.Forms.Xaml;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;

#if UITEST
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using NUnit.Framework;
using Xamarin.Forms.Core.UITests;
using System.Linq;
#endif

namespace Xamarin.Forms.Controls.Issues
{
#if UITEST
	[Category(UITestCategories.CollectionView)]
#endif
#if APP
	[XamlCompilation(XamlCompilationOptions.Compile)]
#endif
	[Preserve(AllMembers = true)]
	[Issue(IssueTracker.Github, 6155, "[Bug] ScrollBarVisibility (ListView/CollectionView) on ASUS", PlatformAffected.All)]
	public partial class Issue6155 : TestContentPage
	{
#if APP
		public Issue6155()
		{
			Device.SetFlags(new List<string> { CollectionView.CollectionViewExperimental });

			InitializeComponent();

			BindingContext = new ViewModel6155();
		}
#endif

		protected override void Init()
		{

		}

		private void OnFlashClicked(object sender, EventArgs e)
		{
			var button = sender as Button;
			var grid = button.Parent.Parent as Grid;

			(grid.Children[1] as CollectionView).HorizontalScrollBarVisibility = ScrollBarVisibility.Always;
			(grid.Children[2] as CollectionView).VerticalScrollBarVisibility = ScrollBarVisibility.Always;
		}
	}

	[Preserve(AllMembers = true)]
	public class ViewModel6155
	{
		public ObservableCollection<Model6155> FakeCollection { get; set; }

		public ViewModel6155()
		{
			FakeCollection = new ObservableCollection<Model6155>
			{
				new Model6155("eat"),
				new Model6155("drink"),
				new Model6155("sleep"),
				new Model6155("eat"),
				new Model6155("drink"),
				new Model6155("sleep"),
				new Model6155("eat"),
				new Model6155("drink"),
				new Model6155("sleep"),
				new Model6155("eat"),
				new Model6155("drink"),
				new Model6155("sleep")
			};
		}
	}

	[Preserve(AllMembers = true)]
	public class Model6155
	{
		public string TheText { get; set; }

		public Model6155(string text)
		{
			TheText = text;
		}
	}
}