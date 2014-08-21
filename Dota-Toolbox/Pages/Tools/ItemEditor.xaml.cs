using Dota_Toolbox.Parser;
using Dota_Toolbox.Settings;
using Dota_Toolbox.Windows;
using KVLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
	/// Interaction logic for ItemEditor.xaml
	/// </summary>
	public partial class ItemEditor : UserControl
	{
		public static ObservableCollection<KeyValue> kv_list = new ObservableCollection<KeyValue>();
		private string filePath, selectedKey, file = "npc_items_custom.txt";
		private KVDocument doc = new KVDocument();
		private List<DockPanel> docks;
		private TreeViewItem treeRootItem;
		private Binding keyBinding, valueBinding;
		private AddItemWindow addItemWindow;
		private KeyValue newKV, root;
		private Item tempItem;

		private List<KeyValue> parentKeysList = new List<KeyValue>();
		private List<KeyValue> valueKeysList = new List<KeyValue>();
		private List<TreeViewItem> parentKeys = new List<TreeViewItem>();
		private List<TreeViewItem> valueKeys = new List<TreeViewItem>();

		public ItemEditor()
		{
			this.DataContext = this;
			InitializeComponent();
			treeRootItem = new TreeViewItem();
			treeRootItem.IsExpanded = true;
			docks = new List<DockPanel>();
		}
		#region Methods
		private void OpenAddItemWindow()
		{
			OpenAddItemWindow(null);
		}

		private void OpenAddItemWindow(string itemName)
		{
			try
			{
				for (int i = 0; i < parentKeysList.Count; i++)
					if (String.Equals(itemName, root.children[i].Key))
						tempItem = new Item(root.children[i]);
				addItemWindow.a = tempItem;
				addItemWindow.Show();
			}
			catch
			{
				addItemWindow = new AddItemWindow();
				OpenAddItemWindow(itemName);
			}
		}

		private void ToggleRemoveButton()
		{
			if (kv_list.Count <= 0)
			{
				removeKV_Button.Content = "Nothing To Remove";
				removeKV_Button.IsEnabled = false;
			}
			else if (kv_list.Count > 0)
			{
				removeKV_Button.Content = "Remove " + kv_list[kv_list.Count - 1].Key;
				removeKV_Button.IsEnabled = true;
			}
		}

		private void SaveToFile()
		{
			Utils.SaveToFile(kv_list, (ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + "TestingItem.txt"));
		}
		#endregion

		#region Updates
		private void UpdateKVList()
		{
			kv_list.Clear();
			itemlist_treeview.Items.Clear();
			kv_list = doc.ReadKeyValuesFromFile(filePath);
			ToggleRemoveButton();
		}

		/*private void DoHierarchy(List<KeyValue> input)
		{
			for (int x = 0; x < input.Count; x++)
			{
				if (input[x].Value == "" && input[x].HasChildren)
				{
					AddParentKey(parentKeys[parentKeys.Count - 1], input[x]);
					parentKeysList.Add(input[x]);
					DoHierarchy(input[x].children);
				}
				else
				{
					AddValue(parentKeys[parentKeys.Count - 1], input[x]);
					valueKeysList.Add(input[x]);
				}
			}
		}*/

		private void DoRootHierarchy(ObservableCollection<KeyValue> input)
		{
			for (int x = 0; x < input.Count; x++)
			{
				if (input[x].Value == "" && input[x].HasChildren)
				{
					AddParentKey(parentKeys[0], input[x]);
					parentKeysList.Add(input[x]);
					/*if (input[x].children != null)
						DoHierarchy(input[x].children);
					else
						input[x].children = new List<KeyValue>();*/
				}
				else
				{
					AddValue(parentKeys[parentKeys.Count - 1], input[x]);
					valueKeysList.Add(input[x]);
				}
			}
		}

		private void AddValue(TreeViewItem parent, KeyValue keyValue)
		{
			TreeViewItem t = new TreeViewItem();
			DockPanel dockPanel = new DockPanel();
			keyBinding = new Binding();
			keyBinding.Path = new PropertyPath("Key");
			keyBinding.Source = keyValue;
			keyBinding.Mode = BindingMode.TwoWay;

			valueBinding = new Binding();
			valueBinding.Path = new PropertyPath("Value");
			valueBinding.Source = keyValue;
			valueBinding.Mode = BindingMode.TwoWay;

			TextBox keyText = new TextBox();
			keyText.SetBinding(TextBox.TextProperty, keyBinding);
			keyText.Width = 200;

			TextBox valueTextbox = new TextBox();
			valueTextbox.SetBinding(CheckBox.IsCheckedProperty, valueBinding);
			//valueTextbox.TextChanged += new TextChangedEventHandler(Value_Changed);

			dockPanel.Children.Add(valueTextbox);
			dockPanel.Children.Add(keyText);
			dockPanel.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(Item_Click);
			t.Header = dockPanel;

			valueKeys.Add(t);
			parent.Items.Add(valueKeys[valueKeys.Count - 1]);
		}

		private void AddParentKey(TreeViewItem parent, KeyValue keyValue)
		{
			TreeViewItem t = new TreeViewItem();

			keyBinding = new Binding();
			keyBinding.Path = new PropertyPath("Key");
			keyBinding.Source = keyValue;
			keyBinding.Mode = BindingMode.TwoWay;

			TextBox keyText = new TextBox();
			keyText.SetBinding(TextBox.TextProperty, keyBinding);
			Button keyButton = new Button();
			keyButton.SetBinding(Button.ContentProperty, keyBinding);

			DockPanel dockPanel = new DockPanel();
			dockPanel.Children.Add(keyButton);
			dockPanel.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(Item_Click);
			t.Header = dockPanel;

			parentKeys.Add(t);
			parent.Items.Add(parentKeys[parentKeys.Count - 1]);
		}
		#endregion

		#region UI Events
		private void AddItem_Click(object sender, RoutedEventArgs e)
		{
		}

		private void RemoveItem_Click(object sender, RoutedEventArgs e)
		{
			RemoveItem();
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			if (Directory.Exists(ApplicationSettings.instance.currentModPath))
				filePath = ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + file;
			UpdateKVList();
			treeRootItem.Header = "DOTAAbilities";
			treeRootItem.Items.Clear();
			parentKeys.Clear();
			valueKeys.Clear();
			docks.Clear();
			parentKeysList.Clear();
			itemlist_treeview.Items.Clear();
			parentKeys.Add(treeRootItem);
			itemlist_treeview.Items.Add(treeRootItem);

			DoRootHierarchy(kv_list);
			if (kv_list != null)
				root = kv_list[0].Parent;
		}

		private void Value_Changed(object sender, TextChangedEventArgs e)
		{
		}

		private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer scv = (ScrollViewer)sender;
			scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta * 0.25);
			e.Handled = true;
		}

		private DockPanel tempPanel;
		private Button tempButton;
		private void Item_Click(object sender, MouseButtonEventArgs e)
		{
			tempPanel = sender as DockPanel;
			tempButton = tempPanel.Children[0] as Button;
			OpenAddItemWindow(tempButton.Content.ToString());
			addItemWindow.Title = "Edit Item";
			/*tempStackpanel = sender as DockPanel;
			tempTextbox = tempStackpanel.Children[0] as TextBox;
			selectedKey = tempTextbox.Text;
			removeKV_Button.Content = "Remove " + selectedKey;*/
			if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
				RemoveItem();
		}

		private void RemoveItem()
		{
			for (int i = 0; i < kv_list.Count; i++)
			{

			}
			kv_list.RemoveAt(kv_list.Count - 1);
			root.RemoveChild(kv_list[kv_list.Count - 1]);
			UpdateTreeView();
			//DoRootHierarchy(kv_list);
			SaveToFile();
		}

		private void UpdateTreeView()
		{
			treeRootItem.Items.Clear();
			parentKeys.Clear();
			valueKeys.Clear();
			parentKeys.Add(treeRootItem);
			itemlist_treeview.Items.Clear();
			itemlist_treeview.Items.Add(treeRootItem);
			parentKeysList.Clear();
			DoRootHierarchy(kv_list);
		}
		#endregion
	}
}
