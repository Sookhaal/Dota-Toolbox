using Dota_Toolbox.Parser;
using Dota_Toolbox.Settings;
using Dota_Toolbox.Windows;
using KVLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
		private string herolistPath, selectedHeroName, file = "testing.txt";
		private KVDocument doc = new KVDocument();
		private List<DockPanel> docks;
		private TreeViewItem treeRootItem;
		private Binding keyBinding, valueBinding;
		private AddHeroWindow addHeroWindow;
		private KeyValue newHero;

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

		private void OpenAddHeroWindow()
		{
			try { addHeroWindow.Show(); }
			catch
			{
				addHeroWindow = new AddHeroWindow();
				addHeroWindow.parentWindow = this;
				addHeroWindow.Show();
			}

		}

		private void UpdateHeroList()
		{
			kv_list.Clear();
			herolist_treeview.Items.Clear();
			kv_list = doc.ReadKeyValuesFromFile(herolistPath);
			/*for (int i = 0; i < tempList.Count; i++)
			{
				var d = new KeyValue(tempList[i].Key, tempList[i].Value);
				//d.AddChild()
				herolist_list.Add(d);
			}*/
			//doc.GetChildren()
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
			DoHierarchy(kv_list);
		}

		private void DoHierarchy(List<KeyValue> input)
		{
			for (int x = 0; x < input.Count; x++)
			{
				if (input[x].Value == "")
				{
					AddParentKey(parentKeys[parentKeys.Count - 1], input[x]);
					parentKeysList.Add(input[x]);
					DoHierarchy(input[x].children);
				}
				else
				{
					AddValueKey(parentKeys[parentKeys.Count - 1], input[x]);
					valueKeysList.Add(input[x]);
				}
			}
		}

		private void DoHierarchy(ObservableCollection<KeyValue> input)
		{
			for (int x = 0; x < input.Count; x++)
			{
				if (input[x].Value == "")
				{
					AddParentKey(parentKeys[parentKeys.Count - 1], input[x]);
					parentKeysList.Add(input[x]);
					DoHierarchy(input[x].children);
				}
				else
				{
					AddValueKey(parentKeys[parentKeys.Count - 1], input[x]);
					valueKeysList.Add(input[x]);
				}
			}
		}

		private void AddValueKey(TreeViewItem parent, KeyValue keyValue)
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
			TextBox valueText = new TextBox();
			valueText.SetBinding(TextBox.TextProperty, valueBinding);

			dockPanel.Children.Add(keyText);
			dockPanel.Children.Add(valueText);
			dockPanel.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(HeroClick);
			t.Header = dockPanel;

			valueKeys.Add(t);
			parent.Items.Add(valueKeys[valueKeys.Count - 1]);									//Does it work?
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
			dockPanel.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(HeroClick);
			t.Header = dockPanel;

			parentKeys.Add(t);
			parent.Items.Add(parentKeys[parentKeys.Count - 1]);								//Does it work?
		}

		public void AddHero(string name)
		{
			newHero = new KeyValue(name, "1");
			kv_list.Add(newHero);
			AddValueKey(treeRootItem, newHero);
			ToggleRemoveButton();
		}

		private void RemoveHero()
		{
			for (int i = 0; i < kv_list.Count; i++)
			{
				if (String.Equals(selectedHeroName, kv_list[i].Key))
				{
					kv_list.RemoveAt(i);
					UpdateTreeView();
					ToggleRemoveButton();
					return;
				}
			}
			kv_list.RemoveAt(kv_list.Count - 1);
			ToggleRemoveButton();
			UpdateTreeView();
		}

		private void ToggleRemoveButton()
		{
			if (kv_list.Count <= 0)
			{
				removeHeroButton.Content = "Nothing To Remove";
				removeHeroButton.IsEnabled = false;
			}
			else if (kv_list.Count > 0)
			{
				removeHeroButton.Content = "Remove " + kv_list[kv_list.Count - 1].Key;
				removeHeroButton.IsEnabled = true;
			}
		}

		#region UI Events

		private void GridSelectionChange(object sender, SelectionChangedEventArgs e)
		{
			ToggleRemoveButton();
		}

		private void GridSizeChanged(object sender, SizeChangedEventArgs e)
		{
			ToggleRemoveButton();
		}

		private void AddHeroClick(object sender, RoutedEventArgs e)
		{
			OpenAddHeroWindow();
		}

		private void RemoveHeroClick(object sender, RoutedEventArgs e)
		{
			RemoveHero();
		}

		private TextBox tempTextbox;
		private DockPanel tempStackpanel;

		private void HeroClick(object sender, MouseButtonEventArgs e)
		{
			tempStackpanel = sender as DockPanel;
			tempTextbox = tempStackpanel.Children[0] as TextBox;
			selectedHeroName = tempTextbox.Text;
			removeHeroButton.Content = "Remove " + selectedHeroName;
			if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
				RemoveHero();
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			if (Directory.Exists(ApplicationSettings.instance.currentModPath))
				herolistPath = ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + file;
			else
			{
				Console.WriteLine("");
			}
			UpdateHeroList();
			treeRootItem.Header = "CustomHeroList";
			treeRootItem.Items.Clear();
			parentKeys.Clear();
			valueKeys.Clear();
			docks.Clear();
			herolist_treeview.Items.Clear();
			parentKeys.Add(treeRootItem);
			herolist_treeview.Items.Add(treeRootItem);

			//parentKeysList.Add(kv_list[0].Parent);
			DoHierarchy(kv_list);
		}

		#endregion UI Events

		private string Write(List<KeyValue> parent)
		{
			string o = "";
			for (int i = 0; i < parent.Count; i++)
			{
				parent[i].TabIndex = parent[i].Parent.TabIndex + 1;

				for (int a = 0; a < parent[i].TabIndex; a++)
					o += "\t";
				o += "\"" + parent[i].Key + "\"";
				if (parent[i].Value != "")
				{
					o += "\t\"" + parent[i].Value + "\"\r\n";
				}
				else
				{
					o += "\r\n";
					for (int a = 0; a < parent[i].TabIndex; a++)
						o += "\t";
					o += "{";
					o += "\r\n";
					o += Write(parent[i].children);
					for (int a = 0; a < parent[i].TabIndex; a++)
						o += "\t";
					o += "}\r\n";
				}
			}
			return o;
		}

		private void Save(object sender, RoutedEventArgs e)
		{
			string root = "";
			root = "\"" + parentKeysList[0].Parent.Key + "\"";
			root += "\r\n{\r\n";
			root += Write(parentKeysList[0].Parent.children);
			root += "}";

			if (File.Exists(ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\testing3.txt"))
			{
				File.WriteAllText(ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\testing3.txt", root);
			}
		}

		private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer scv = (ScrollViewer)sender;
			scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta * 0.25);
			e.Handled = true;
		}
	}
}