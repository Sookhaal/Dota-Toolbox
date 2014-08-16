using Dota_Toolbox.Windows;
using FirstFloor.ModernUI.Presentation;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Dota_Toolbox.Pages
{
	/// <summary>
	/// Interaction logic for AddHeroDialog.xaml
	/// </summary>
	public partial class AddHeroDialog : ModernDialog
	{
		public static string newHero = "";
		private string[] heroesListLines;
		private ErrorDialog errorDialog;

		public AddHeroDialog()
		{
			InitializeComponent();

			newHeroName.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
					  new System.Windows.Controls.TextChangedEventHandler(newHeroNameTextChanged));

			this.Buttons = new Button[] { this.OkButton, this.CancelButton };

			try
			{
				heroesListLines = File.ReadAllLines("Data\\DefaultHeroesList2.txt");
			}
			catch
			{
				Button b = new Button();
				b.Content = "DefaultHeroesList.txt";
				b.Click += ExploreFolder;

				errorDialog = new ErrorDialog();
				errorDialog.AddColoredHeader("File Not Found");
				errorDialog.AddButton(b);
				errorDialog.AddBBCode("An empty file will be created. Feel free to edit it.");
				errorDialog.AddBBCode("[url=http://pastebin.com/raw.php?i=EU59vmAb]Default Values.[/url]");

				errorDialog.ShowDialog();
			}
			/*if (heroesListLines.Length > 0)
				for (int i = 0; i < heroesListLines.Length; i++)
					newHeroName.Items.Add(heroesListLines[i]);*/
		}

		private void ExploreFolder(object sender, RoutedEventArgs e)
		{
			if (Directory.Exists(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data\\"))
				Process.Start(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data\\");
			else
				Console.WriteLine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Data\\");
		}

		private void newHeroNameTextChanged(object sender, TextChangedEventArgs e)
		{
			newHero = newHeroName.Text;
		}
	}
}