using Dota_Toolbox.Pages.Settings;
using Dota_Toolbox.Settings;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dota_Toolbox
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : ModernWindow
	{
		public MainWindow()
		{
			if (!File.Exists("Settings\\Settings.xml"))
				File.Create("Settings\\Settings.xml");
			Setup.LoadSettings();
			InitializeComponent();
			Console.WriteLine(ApplicationSettings.instance.dotaPath);
			if (!Directory.Exists(ApplicationSettings.instance.dotaPath + "\\dota_ugc\\game\\dota_addons\\"))
				this.ContentSource = settings_button.Source;
		}

		private void ApplicationClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			Application.Current.Shutdown();
		}
	}
}
