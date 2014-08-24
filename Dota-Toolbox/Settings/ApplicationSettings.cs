using FirstFloor.ModernUI.Presentation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Xml.Serialization;

namespace Dota_Toolbox.Settings
{
	public class ApplicationSettings
	{
		#region Hidden Settings/Informations
		private static ApplicationSettings _instance;
		private static string _applicationPath;

		[XmlIgnore]
		public static ApplicationSettings instance
		{
			get
			{
				if (_instance == null)
					_instance = new ApplicationSettings();
				return _instance;
			}
			set { _instance = value; }
		}

		[XmlIgnore]
		public static string applicationPath
		{
			get { return Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location); }
		}
		#endregion

		#region Exposed Settings
		private string _dotaPath, _currentMod, _currentModePath, _theme;
		private Color _accentColor;
		private bool _autoSave;

		[XmlElement("DotaPath")]
		public string dotaPath
		{
			get { return _dotaPath; }
			set { _dotaPath = value; }
		}

		[XmlElement("CurrentModPath")]
		public string currentModPath
		{
			get { return _currentModePath; }
			set { _currentModePath = value; }
		}

		[XmlElement("CurentMod")]
		public string currentMod
		{
			get { return _currentMod; }
			set { _currentMod = value; }
		}

		[XmlElement("Theme")]
		public string theme
		{
			get { return _theme; }
			set { _theme = value; }
		}

		[XmlElement("AccentColor")]
		public Color accentColor
		{
			get { return _accentColor; }
			set { _accentColor = value; }
		}

		[XmlElement("AutoSave")]
		public bool autoSave
		{
			get { return _autoSave; }
			set { _autoSave = value; }
		}
		#endregion
	}
}
