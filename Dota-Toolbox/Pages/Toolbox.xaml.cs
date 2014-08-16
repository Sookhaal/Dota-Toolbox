using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Dota_Toolbox.Settings;
using System.Xml.Serialization;
using System.IO;
using Dota_Toolbox.Pages.Settings;
using FirstFloor.ModernUI.Presentation;

namespace Dota_Toolbox.Pages
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Toolbox : UserControl
	{
		//ApplicationSettings s = new ApplicationSettings();
		public Toolbox()
		{
			Setup.LoadSettings();
			InitializeComponent();
			AppearanceManager.Current.AccentColor = ApplicationSettings.instance.accentColor;
			AppearanceManager.Current.ThemeSource = new Uri(ApplicationSettings.instance.theme, UriKind.Relative);
		}

		private void TestingMethods(object sender, RoutedEventArgs e)
		{
		}
	}
}
