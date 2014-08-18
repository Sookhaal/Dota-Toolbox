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
using Dota_Toolbox.Windows;
using System.Diagnostics;

namespace Dota_Toolbox.Pages
{
	/// <summary>
	/// Interaction logic for Home.xaml
	/// </summary>
	public partial class Toolbox : UserControl
	{
		public Toolbox()
		{
			Setup.LoadSettings();
			InitializeComponent();
			if (ApplicationSettings.instance.accentColor.A == 0)
			{
				ApplicationSettings.instance.accentColor = Color.FromArgb(255, ApplicationSettings.instance.accentColor.R, ApplicationSettings.instance.accentColor.G, ApplicationSettings.instance.accentColor.B);
				Setup.SaveSettings();
			}
			AppearanceManager.Current.AccentColor = ApplicationSettings.instance.accentColor;

			try { AppearanceManager.Current.ThemeSource = new Uri(ApplicationSettings.instance.theme, UriKind.Relative); }
			catch { Trace.TraceWarning("Theme setting not found. Using default Theme:" + AppearanceManager.Current.ThemeSource); }
		}
	}
}
