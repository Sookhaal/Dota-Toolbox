using Dota_Toolbox.Settings;
using Ookii.Dialogs.Wpf;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;

namespace Dota_Toolbox.Pages.Settings
{
	/// <summary>
	/// Interaction logic for Setup.xaml
	/// </summary>
	public partial class Setup : UserControl
	{
		private string dotaPath;
		private VistaFolderBrowserDialog dotaFolderBrowser;
		private string[] modsList;

		//public ApplicationSettings s = new ApplicationSettings();
		public Setup()
		{
			LoadSettings();
			InitializeComponent();

			modslist_combobox.AddHandler(System.Windows.Controls.Primitives.TextBoxBase.TextChangedEvent,
					  new System.Windows.Controls.TextChangedEventHandler(modsListTextChanged));

			dotaPath = ApplicationSettings.instance.dotaPath;
			dotaPathText.Text = ApplicationSettings.instance.dotaPath;
			dotaFolderBrowser = new VistaFolderBrowserDialog();
			dotaFolderBrowser.Description = "Please select DotA 2's main folder.";
			dotaFolderBrowser.UseDescriptionForTitle = true;

			modsList = Directory.GetDirectories(ApplicationSettings.instance.dotaPath + "\\dota_ugc\\game\\dota_addons");
			for (int i = 0; i < modsList.Length; i++)
			{
				modsList[i] = modsList[i].Remove(0, modsList[i].LastIndexOf("\\") + 1);		//Removing path. We keep folder's name.
				modslist_combobox.Items.Add(modsList[i]);
			}
			modslist_combobox.Text = ApplicationSettings.instance.currentMod;
			ApplicationSettings.instance.currentModPath = ApplicationSettings.instance.dotaPath + "\\dota_ugc\\game\\dota_addons" + modslist_combobox.Text;
			//modslist_combobox.Items.Add
		}

		public static void SaveSettings()
		{
			if (!File.Exists("Settings\\Settings.xml"))
				File.Create("Settings\\Settings.xml");
			XmlSerializer xs = new XmlSerializer(typeof(ApplicationSettings));
			using (StreamWriter wr = new StreamWriter("Settings\\Settings.xml"))
			{
				xs.Serialize(wr, ApplicationSettings.instance);
			}
		}

		public static void LoadSettings()
		{
			if (!File.Exists("Settings\\Settings.xml"))
				File.Create("Settings\\Settings.xml");
			XmlSerializer xs = new XmlSerializer(typeof(ApplicationSettings));
			try
			{
				using (StreamReader rd = new StreamReader("Settings\\Settings.xml"))
				{
					ApplicationSettings.instance = xs.Deserialize(rd) as ApplicationSettings;
				}
			}
			catch
			{
				SaveSettings();
			}
		}

		private void BrowseFolder(object sender, RoutedEventArgs e)
		{
			if ((bool)dotaFolderBrowser.ShowDialog(MainWindow.GetWindow(this)))
			{
				UpdateDotaPath(dotaFolderBrowser.SelectedPath);
			}
		}

		private void DotaPathTextUpdate(object sender, TextChangedEventArgs e)
		{
			UpdateDotaPath(dotaPathText.Text);
			dotaPath = dotaPathText.Text;
		}

		private void UpdateDotaPath(string newPath)
		{
			if (Directory.Exists(newPath))
			{
				ApplicationSettings.instance.dotaPath = newPath;
				dotaPathText.Text = ApplicationSettings.instance.dotaPath;
				SaveSettings();
			}
			else if (dotaPath == null || dotaPathText.Text == "")
				dotaPathText.Text = "DotA 2 Path";
		}

		private bool ModExists(string nameToTest)
		{
			for (int i = 0; i < modsList.Length; i++)
				if (nameToTest == modsList[i])
					return true;
				else continue;
			return false;
		}

		private void SaveCurrentMod()
		{
			if (ModExists(modslist_combobox.Text))
			{
				ApplicationSettings.instance.currentMod = modslist_combobox.Text;
				ApplicationSettings.instance.currentModPath = ApplicationSettings.instance.dotaPath + "\\dota_ugc\\game\\dota_addons\\" + modslist_combobox.Text;
			}
			SaveSettings();
		}

		private void modsListTextChanged(object sender, TextChangedEventArgs e)
		{
			SaveCurrentMod();
		}
	}
}