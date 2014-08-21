using Dota_Toolbox.Parser;
using Dota_Toolbox.Settings;
using Dota_Toolbox.Windows;
using KVLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Dota_Toolbox.Pages.Tools
{
	/// <summary>
	/// Interaction logic for HerolistEditor.xaml
	/// </summary>
	public partial class HerolistEditor : UserControl
	{
		public static ObservableCollection<KeyValue> kv_list = new ObservableCollection<KeyValue>();
		private string filePath, selectedKey, file = "herolist.txt";
		private KVDocument doc = new KVDocument();
		private List<DockPanel> docks;
		private TreeViewItem treeRootItem;
		private Binding keyBinding, valueBinding;
		private AddHeroWindow addKVWindow;
		private KeyValue newKV, root;

		private List<KeyValue> parentKeysList = new List<KeyValue>();
		private List<KeyValue> valueKeysList = new List<KeyValue>();
		private List<TreeViewItem> parentKeys = new List<TreeViewItem>();
		private List<TreeViewItem> valueKeys = new List<TreeViewItem>();

		public HerolistEditor()
		{
			this.DataContext = this;
			InitializeComponent();
			treeRootItem = new TreeViewItem();
			treeRootItem.IsExpanded = true;
			docks = new List<DockPanel>();
		}

		#region Methods
		private void OpenAddHeroWindow()
		{
			try { addKVWindow.Show(); }
			catch
			{
				addKVWindow = new AddHeroWindow();
				addKVWindow.parentWindow = this;
				OpenAddHeroWindow();
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

		public void AddHero(string name)
		{
			newKV = new KeyValue(name, "1");
			kv_list.Add(newKV);
			root.AddChild(newKV);
			AddBoolKey(treeRootItem, newKV);
			ToggleRemoveButton();
			SaveToFile();
		}

		private void SaveToFile()
		{
			Utils.SaveToFile(kv_list, ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + file);
		}
		#endregion

		#region Updates
		private void UpdateKVList()
		{
			kv_list.Clear();
			herolist_treeview.Items.Clear();
			kv_list = doc.ReadKeyValuesFromFile(filePath);
			ToggleRemoveButton();
		}

		private void UpdateTreeView()
		{
			treeRootItem.Items.Clear();
			parentKeys.Clear();
			valueKeys.Clear();
			parentKeys.Add(treeRootItem);
			docks.Clear();
			herolist_treeview.Items.Clear();
			herolist_treeview.Items.Add(treeRootItem);
			DoRootHierarchy(kv_list);
		}

		private void DoHierarchy(List<KeyValue> input)
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
					AddBoolKey(parentKeys[parentKeys.Count - 1], input[x]);
					valueKeysList.Add(input[x]);
				}
			}
		}

		private void DoRootHierarchy(ObservableCollection<KeyValue> input)
		{
			for (int x = 0; x < input.Count; x++)
			{
				if (input[x].Value == "" && input[x].HasChildren)
				{
					AddParentKey(parentKeys[0], input[x]);
					parentKeysList.Add(input[x]);
					if (input[x].children != null)
						DoHierarchy(input[x].children);
					else
						input[x].children = new List<KeyValue>();
				}
				else
				{
					AddBoolKey(parentKeys[parentKeys.Count - 1], input[x]);
					valueKeysList.Add(input[x]);
				}
			}
		}

		private void AddBoolKey(TreeViewItem parent, KeyValue keyValue)
		{
			TreeViewItem t = new TreeViewItem();
			DockPanel dockPanel = new DockPanel();
			keyBinding = new Binding();
			keyBinding.Path = new PropertyPath("Key");
			keyBinding.Source = keyValue;
			keyBinding.Mode = BindingMode.TwoWay;

			valueBinding = new Binding();
			valueBinding.Path = new PropertyPath("Bool");
			valueBinding.Source = keyValue;
			valueBinding.Mode = BindingMode.TwoWay;

			TextBox keyText = new TextBox();
			keyText.SetBinding(TextBox.TextProperty, keyBinding);
			keyText.Width = 200;

			CheckBox valueCheckbox = new CheckBox();
			valueCheckbox.SetBinding(CheckBox.IsCheckedProperty, valueBinding);
			valueCheckbox.Unchecked += new RoutedEventHandler(Checkbox_Unchecked);
			valueCheckbox.Checked += new RoutedEventHandler(Checkbox_Checked);

			dockPanel.Children.Add(valueCheckbox);
			dockPanel.Children.Add(keyText);
			dockPanel.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(KV_Click);
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
			dockPanel.Children.Add(keyText);
			dockPanel.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(KV_Click);
			t.Header = dockPanel;

			parentKeys.Add(t);
			parent.Items.Add(parentKeys[parentKeys.Count - 1]);
		}

		private void RemoveHero()
		{
			for (int i = 0; i < kv_list.Count; i++)
			{
				if (String.Equals(selectedKey, root.children[i].Key))			//If strings match -> remove hero under mouse
				{																//Else remove last entry
					root.RemoveChild(kv_list[i]);
					kv_list.RemoveAt(i);
					UpdateTreeView();
					ToggleRemoveButton();
					SaveToFile();
					return;
				}
			}
			kv_list.RemoveAt(kv_list.Count - 1);
			root.RemoveChild(kv_list[kv_list.Count - 1]);
			ToggleRemoveButton();
			UpdateTreeView();
			SaveToFile();
		}
		#endregion

		#region UI Events

		private void GridSelectionChange(object sender, SelectionChangedEventArgs e)
		{
			ToggleRemoveButton();
		}

		private void GridSizeChanged(object sender, SizeChangedEventArgs e)
		{
			ToggleRemoveButton();
		}

		private void AddHero_Click(object sender, RoutedEventArgs e)
		{
			OpenAddHeroWindow();
		}

		private void RemoveHero_Click(object sender, RoutedEventArgs e)
		{
			RemoveHero();
		}

		private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
		{
			SaveToFile();
		}

		private void Checkbox_Checked(object sender, RoutedEventArgs e)
		{
			SaveToFile();
		}

		private void SaveButton_Click(object sender, RoutedEventArgs e)
		{
			SaveToFile();
		}

		private TextBox tempTextbox;
		private DockPanel tempPanel;

		private void KV_Click(object sender, MouseButtonEventArgs e)
		{
			try
			{
				tempPanel = sender as DockPanel;
				tempTextbox = tempPanel.Children[1] as TextBox;
				selectedKey = tempTextbox.Text;
				removeKV_Button.Content = "Remove " + selectedKey;
				if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
					RemoveHero();
			}
			catch
			{
				Console.WriteLine("File is empty.");
			}
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			if (Directory.Exists(ApplicationSettings.instance.currentModPath))
				filePath = ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + file;
			UpdateKVList();
			treeRootItem.Header = "CustomHeroList";
			treeRootItem.Items.Clear();
			parentKeys.Clear();
			valueKeys.Clear();
			docks.Clear();
			herolist_treeview.Items.Clear();
			parentKeys.Add(treeRootItem);
			herolist_treeview.Items.Add(treeRootItem);

			//parentKeysList.Add(kv_list[0].Parent);
			DoRootHierarchy(kv_list);
			if (kv_list != null)
				root = kv_list[0].Parent;
		}

		private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer scv = (ScrollViewer)sender;
			scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta * 0.25);
			e.Handled = true;
		}
		#endregion UI Events
	}
}