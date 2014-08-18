using Dota_Toolbox.Settings;
using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Dota_Toolbox.Pages.Settings
{
	/// <summary>
	/// A simple view model for configuring theme, font and accent colors.
	/// </summary>
	public class AppearanceViewModel
		: NotifyPropertyChanged
	{
		private const string FontSmall = "small";
		private const string FontLarge = "large";

		// 9 accent colors from metro design principles
		/*private Color[] accentColors = new Color[]{
			Color.FromRgb(0x33, 0x99, 0xff),   // blue
			Color.FromRgb(0x00, 0xab, 0xa9),   // teal
			Color.FromRgb(0x33, 0x99, 0x33),   // green
			Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
			Color.FromRgb(0xf0, 0x96, 0x09),   // orange
			Color.FromRgb(0xff, 0x45, 0x00),   // orange red
			Color.FromRgb(0xe5, 0x14, 0x00),   // red
			Color.FromRgb(0xff, 0x00, 0x97),   // magenta
			Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
		};*/

		// 20 accent colors from Windows Phone 8
		private Color[] accentColors = new Color[]{
			Color.FromRgb(29,226,231),
			Color.FromRgb(29,178,231),
			Color.FromRgb(29,128,231),
			Color.FromRgb(29,79,231),
			Color.FromRgb(29,31,231),
			Color.FromRgb(69,31,231),
			Color.FromRgb(125,31,231),
			Color.FromRgb(175,31,231),
			Color.FromRgb(227,31,231),
			Color.FromRgb(227,31,189),
			Color.FromRgb(227,31,137),
			Color.FromRgb(227,31,90),
			Color.FromRgb(227,31,29),
			Color.FromRgb(227,69,29),
			Color.FromRgb(227,114,29),
			Color.FromRgb(227,159,29),
			Color.FromRgb(227,225,29),
			Color.FromRgb(201,225,29),
			Color.FromRgb(156,225,29),
			Color.FromRgb(116,225,29),
			Color.FromRgb(74,225,29),
			Color.FromRgb(27,225,29),
			Color.FromRgb(27,225,69),
			Color.FromRgb(27,225,126),
			Color.FromRgb(27,225,173),
			Color.FromRgb(27,225,223),
			Color.FromRgb(0,0,0),
			Color.FromRgb(25,25,25),
			Color.FromRgb(50,50,50),
			Color.FromRgb(80,80,80),
			Color.FromRgb(110,110,110),
			Color.FromRgb(150,150,150),
			Color.FromRgb(200,200,200),
			Color.FromRgb(255,255,255),
		};

		private Color selectedAccentColor;
		private LinkCollection themes = new LinkCollection();
		private Link selectedTheme;
		private string selectedFontSize;

		public AppearanceViewModel()
		{
			// add the default themes
			this.themes.Add(new Link { DisplayName = "dark", Source = AppearanceManager.DarkThemeSource });
			this.themes.Add(new Link { DisplayName = "light", Source = AppearanceManager.LightThemeSource });
			//this.selectedTheme = this.themes[1];
			//AppearanceManager.Current.ThemeSource = this.selectedTheme.Source;

			this.SelectedFontSize = AppearanceManager.Current.FontSize == FontSize.Large ? FontLarge : FontSmall;
			SyncThemeAndColor();

			AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
		}

		public void SetTheme(int index)
		{
			AppearanceManager.Current.ThemeSource = Themes[index].Source;
		}

		private void SyncThemeAndColor()
		{
			// synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
			this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

			// and make sure accent color is up-to-date
			this.SelectedAccentColor = AppearanceManager.Current.AccentColor;
		}

		private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
			{
				SyncThemeAndColor();
			}
		}

		public LinkCollection Themes
		{
			get { return this.themes; }
		}

		public string[] FontSizes
		{
			get { return new string[] { FontSmall, FontLarge }; }
		}

		public Color[] AccentColors
		{
			get { return this.accentColors; }
		}

		public Link SelectedTheme
		{
			get { return this.selectedTheme; }
			set
			{
				if (this.selectedTheme != value)
				{
					this.selectedTheme = value;
					OnPropertyChanged("SelectedTheme");

					// and update the actual theme
					AppearanceManager.Current.ThemeSource = value.Source;
					ApplicationSettings.instance.theme = value.Source.ToString();
					Setup.SaveSettings();
				}
			}
		}

		public string SelectedFontSize
		{
			get { return this.selectedFontSize; }
			set
			{
				if (this.selectedFontSize != value)
				{
					this.selectedFontSize = value;
					OnPropertyChanged("SelectedFontSize");

					AppearanceManager.Current.FontSize = value == FontLarge ? FontSize.Large : FontSize.Small;
				}
			}
		}

		public Color SelectedAccentColor
		{
			get { return this.selectedAccentColor; }
			set
			{
				if (this.selectedAccentColor != value)
				{
					this.selectedAccentColor = value;
					OnPropertyChanged("SelectedAccentColor");

					AppearanceManager.Current.AccentColor = value;
					ApplicationSettings.instance.accentColor = value;
					Console.WriteLine(value);
					Setup.SaveSettings();
				}
			}
		}
	}
}
