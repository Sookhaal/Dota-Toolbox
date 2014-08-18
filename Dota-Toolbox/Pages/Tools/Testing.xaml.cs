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

namespace Dota_Toolbox.Pages.Tools
{
	/// <summary>
	/// Interaction logic for Testing.xaml
	/// </summary>
	public partial class Testing : UserControl
	{
		public Testing()
		{
			InitializeComponent();
			TreeViewItem treeItem = null;
			treeItem = new TreeViewItem();
			treeItem.IsExpanded = true;
			List<StackPanel> stacks = new List<StackPanel>();
			for (int i = 0; i < 5; i++)
			{
				stacks.Add(new StackPanel());
				stacks[i].Orientation = Orientation.Horizontal;

				TextBox heroNameEditable = new TextBox();
				heroNameEditable.Text = "Item 1";
				treeItem.Header = "CustomHeroList";
				CheckBox heroEnabled = new CheckBox();
				heroEnabled.Margin = new Thickness(0, 0, 0, 0);
				stacks[i].Children.Add(heroEnabled);
				stacks[i].Children.Add(heroNameEditable);
			}

			treeItem.Items.Add(new TreeViewItem()
			{
				Header = stacks[0]
			});
			treeItem.Items.Add(new TreeViewItem()
			{
				Header = stacks[1]
			});
			treeItem.Items.Add(new TreeViewItem()
			{
				Header = stacks[2]
			});
			treeItem.Items.Add(new TreeViewItem()
			{
				Header = stacks[3]
			});
			treeItem.Items.Add(new TreeViewItem()
			{
				Header = stacks[4]
			});
			treeItem.Items.Add(new TreeView());

			//treeView.
			treeView.Items.Add(treeItem);
		}

		public class TestClass
		{
			private string _name;
			public string name
			{
				get { return _name; }
				set { _name = value; }
			}
			public TestClass(string name) { }
		}
	}
}
