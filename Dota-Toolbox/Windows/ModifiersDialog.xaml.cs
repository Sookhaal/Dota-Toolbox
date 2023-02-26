using FirstFloor.ModernUI.Windows.Controls;
using KVLib;
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

namespace Dota_Toolbox.Windows
{
	/// <summary>
	/// Interaction logic for ModifiersDialog.xaml
	/// </summary>
	public partial class ModifiersDialog : ModernDialog
	{
		public List<KeyValue> modifiers_list = new List<KeyValue>();
		public ModifiersDialog()
		{
			InitializeComponent();
			this.Buttons = new Button[] { this.OkButton, this.CancelButton };
		}
	}
}
