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

			//herolistPath = ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + file;

			//UpdateHeroList();
			treeRootItem = new TreeViewItem();
			treeRootItem.IsExpanded = true;
			docks = new List<DockPanel>();
			/*//while (kv_list.GetEnumerator().MoveNext())
			treeRootItem.Header = "CustomHeroList";
			treeRootItem.Items.Clear();
			parentKeys.Clear();
			valueKeys.Clear();
			docks.Clear();
			herolist_treeview.Items.Clear();
			parentKeys.Add(treeRootItem);
			herolist_treeview.Items.Add(treeRootItem);

			parentKeys.Add(treeRootItem);
			DoHierarchy(kv_list);*/
		}

		private void OpenAddHeroWindow()
		{
			try
			{
				addHeroWindow.Show();
			}
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
					valueKeysList.Add(input[x]);
					DoHierarchy(input[x].children);
				}
				else
				{
					AddValueKey(parentKeys[parentKeys.Count - 1], input[x]);
					parentKeysList.Add(input[x]);
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
					valueKeysList.Add(input[x]);
					DoHierarchy(input[x].children);
				}
				else
				{
					AddValueKey(parentKeys[parentKeys.Count - 1], input[x]);
					parentKeysList.Add(input[x]);
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
			/*treeRootItem.Header = herolist_list[0].Parent.Key;
			UpdateHeroList();
			UpdateTreeView();
			selectedHeroName = "";*/



			/*if (herolist_list.Count > 0)
				removeHeroButton.Content = "Remove " + herolist_list[herolist_list.Count - 1].key;*/
			herolistPath = ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + file;
			UpdateHeroList();
			treeRootItem.Header = "CustomHeroList";
			treeRootItem.Items.Clear();
			parentKeys.Clear();
			valueKeys.Clear();
			docks.Clear();
			herolist_treeview.Items.Clear();
			parentKeys.Add(treeRootItem);
			herolist_treeview.Items.Add(treeRootItem);

			parentKeys.Add(treeRootItem);
			DoHierarchy(kv_list);
		}

		#endregion UI Events

		/*private string ProperHeroName(string rawHeroName)
		{
			string s;
			s = rawHeroName.Substring(14).Trim();									//Remove "npc_dota_hero_"
			s = Regex.Replace(s, "_", " ", RegexOptions.None);
			s = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(s);		//from "legion commander" to "Legion Commander"
			return s;
		}*/

		private void Save(object sender, RoutedEventArgs e)
		{
			/*string root = "\"CustomHeroList\"";
			string tab = "\t";
			string start = "\r\n{\r\n" + tab;
			string end = "\r\n}";
			string children = "";
			for (int i = 0; i < 5; i++)
			{
				children += AddChild(i.ToString()) + start + end;
			}
			string text = root + start + children + end;*/

			/*TextChild sub1 = new TextChild("\"sub1\"", "\"sub1 Value\"");
			TextChild sub2 = new TextChild("\"sub2\"", "\"sub2 Value\"");
			TextChild root = new TextChild("\"NameRoot\"");
			TextChild root2 = new TextChild("\"NameRoot2\"");
			root.AddChild(sub1);
			root2.AddChild(sub2);
			root.AddChild(root2);*/

			/*string text = "";

			//text = root.GetText();

			if (File.Exists(ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\testing3.txt"))
			{
				File.WriteAllText(ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\testing3.txt", text);
			}*/


			/*string root = "\"" + tempList[0].Parent.Key + "\"";
			root += "\r\n{\r\n";
			for (int i = 0; i < herolist_list.Count; i++)
			{
				//for (int i = 0; i < tempList[i].Parent)
				root += "\t";
				root += "\"" + herolist_list[i].name + "\"" + "\"" + herolist_list[i].enable + "\"";
				root += "\r\n";
			}
			root += "}";

			string text = "";
			text = root;

			if (File.Exists(ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\testing3.txt"))
			{
				File.WriteAllText(ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\testing3.txt", text);
			}*/

			//Console.WriteLine(doc.testText.Length);
		}

		private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer scv = (ScrollViewer)sender;
			scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta * 0.25);
			e.Handled = true;
		}
	}
}