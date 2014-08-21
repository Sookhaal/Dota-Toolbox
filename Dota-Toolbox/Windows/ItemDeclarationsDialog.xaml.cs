using FirstFloor.ModernUI.Windows.Controls;
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
	/// Interaction logic for ItemDeclarationsDialog.xaml
	/// </summary>
	public partial class ItemDeclarationsDialog : ModernDialog
	{
		public List<string> itemDeclarations_list = new List<string>();
		public ItemDeclarationsDialog()
		{
			InitializeComponent();
			this.Buttons = new Button[] { this.OkButton, this.CancelButton };
		}

		public void SetDeclarations()
		{
			for (int i = 0; i < itemDeclarations_list.Count; i++)
				if (itemDeclarations_list[i] != "")
					AddComboBox(itemDeclarations_list[i]);
		}

		public string[] GetNewDeclarations()
		{
			List<string> str = new List<string>();
			for (int i = 0; i < s.Children.Count - 1; i++)			//-1 Because "Add Behavior" button is a child
			{
				DockPanel t = (DockPanel)s.Children[i];
				ComboBox cbox = (ComboBox)t.Children[1];
				str.Add(cbox.SelectedItem.ToString());
				Console.WriteLine(str[i]);
			}
			return str.ToArray();
		}

		private void AddComboBox(string item)
		{
			DockPanel dockPanel = new DockPanel();
			ComboBox c = new ComboBox();
			for (int i = 0; i < itemDeclarations_combobox_base.Items.Count; i++)
				c.Items.Add(itemDeclarations_combobox_base.Items[i]);
			Button b = new Button();
			b.Background = x_button_base.Background;
			b.Content = "X";
			b.Margin = new Thickness(4, 0, 0, 0);
			b.Click += RemoveDeclaration_Click;
			DockPanel.SetDock(b, Dock.Right);
			c.SelectedItem = item;
			dockPanel.Children.Add(b);
			dockPanel.Children.Add(c);
			dockPanel.Margin = new Thickness(0, 0, 0, 4);
			s.Children.Insert(s.Children.Count - 1, dockPanel);		//Insert the DockPanel on top of the button
		}

		private void AddDeclaration_Click(object sender, RoutedEventArgs e)
		{
			AddComboBox(itemDeclarations_combobox_base.Items[0].ToString());
		}

		private void RemoveDeclaration_Click(object sender, RoutedEventArgs e)
		{
			var tempButton = (Button)sender;
			var parent = (UIElement)tempButton.Parent;
			s.Children.Remove(parent);
		}
	}
}
