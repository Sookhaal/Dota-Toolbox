using Dota_Toolbox.Settings;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Dota_Toolbox.Pages.Tools;

namespace Dota_Toolbox.Windows
{
	/// <summary>
	/// Interaction logic for AddHeroWindow.xaml
	/// </summary>
	public partial class AddHeroWindow : ModernWindow
	{
		public static string newHero = "";
		public HerolistEditor parentWindow;
		private string[] heroesListLines;
		private ErrorDialog errorDialog;

		/// <summary>
		/// Initialization.
		/// </summary>
		public AddHeroWindow()
		{
			InitializeComponent();
			heroNameComboBox.AddHandler(TextBoxBase.TextChangedEvent, new TextChangedEventHandler(newHeroNameTextChanged));

			try { heroesListLines = File.ReadAllLines("Data\\DefaultHeroesList.txt"); }
			catch
			{
				Button b = new Button();
				b.Content = "DefaultHeroesList.txt";
				b.Click += ErrorButtonClicked;

				errorDialog = new ErrorDialog();
				errorDialog.AddColoredHeader("File Not Found");
				errorDialog.AddButton(b);
				errorDialog.AddBBCode("An empty file will be created. Feel free to edit it.");
				errorDialog.AddBBCode("[url=http://pastebin.com/raw.php?i=EU59vmAb]Default Values.[/url]");

				errorDialog.ShowDialog();
			}

			if (heroesListLines.Length > 0)
			{
				for (int i = 0; i < heroesListLines.Length; i++)
					heroNameComboBox.Items.Add(heroesListLines[i]);
				heroNameComboBox.SelectedIndex = 0;
			}
		}

		#region UI Events
		private void ErrorButtonClicked(object sender, RoutedEventArgs e)
		{
			Utils.ExplorePath(ApplicationSettings.applicationPath + "\\Data");
		}

		private void newHeroNameTextChanged(object sender, TextChangedEventArgs e)
		{
			newHero = heroNameComboBox.Text;
		}

		private void ComboboxLostFocus(object sender, RoutedEventArgs e)
		{
			heroNameComboBox.Text = heroNameComboBox.Text.Replace(" ", "_");
		}

		private void CloseClicked(object sender, RoutedEventArgs e)
		{
			this.Hide();
		}

		private void AddClicked(object sender, RoutedEventArgs e)
		{
			if (newHero.Length > 0)
			{
				parentWindow.AddHero(newHero);
				feedbackText.BBCode = "Added " + "[color=#" +
					ApplicationSettings.instance.accentColor.R.ToString("X2") +
					ApplicationSettings.instance.accentColor.G.ToString("X2") +
					ApplicationSettings.instance.accentColor.B.ToString("X2") + "]" +
					newHero +
					"[/color]";
			}
			else
			{
				feedbackText.BBCode = "[color=#" +
					ApplicationSettings.instance.accentColor.R.ToString("X2") +
					ApplicationSettings.instance.accentColor.G.ToString("X2") +
					ApplicationSettings.instance.accentColor.B.ToString("X2") + "]" +
					"You need to enter a name." +
					"[/color]";
			}
		}
		#endregion
	}
}