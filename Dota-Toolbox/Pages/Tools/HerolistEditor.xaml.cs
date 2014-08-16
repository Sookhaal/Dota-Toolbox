using Dota_Toolbox.Parser;
using Dota_Toolbox.Settings;
using FirstFloor.ModernUI.Windows.Controls;
using KVLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Dota_Toolbox.Pages.Tools
{
	/// <summary>
	/// Interaction logic for HerolistEditor.xaml
	/// </summary>
	public partial class HerolistEditor : UserControl
	{
		public static ObservableCollection<DotaData.Hero> herolist_list = new ObservableCollection<DotaData.Hero>();
		private List<DotaData.Hero> selection = new List<DotaData.Hero>();
		private List<KeyValue> tempList = new List<KeyValue>();
		private string herolistPath, selectedHeroName, file = "testing.txt";
		private KVDocument doc = new KVDocument();
		List<DockPanel> docks;
		TreeViewItem treeItem;
		Binding nameBinding, enableBinding;
		AddHeroDialog addHeroDialog;

		public HerolistEditor()
		{
			this.DataContext = this;
			InitializeComponent();

			UpdateHeroList();
			treeItem = new TreeViewItem();
			treeItem.IsExpanded = true;
			docks = new List<DockPanel>();
		}

		private void UpdateHeroList()
		{
			herolist_list.Clear();
			tempList.Clear();
			herolist_treeview.Items.Clear();
			herolistPath = ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + file;
			tempList = doc.ReadKeyValuesFromFile(herolistPath);
			for (int i = 0; i < tempList.Count; i++)
			{
				var d = new DotaData.Hero { name = tempList[i].Key, enable = tempList[i].GetBool() };
				herolist_list.Add(d);
			}
			ToggleRemoveButton();
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			treeItem.Header = tempList[0].Parent.Key;
			UpdateHeroList();
			UpdateTreeView();
			selectedHeroName = "";
			if (herolist_list.Count > 0)
				removeHeroButton.Content = "Remove " + herolist_list[herolist_list.Count - 1].name;
		}

		void UpdateTreeView()
		{
			treeItem.Items.Clear();
			docks.Clear();
			herolist_treeview.Items.Clear();
			for (int i = 0; i < herolist_list.Count; i++)
			{
				docks.Add(new DockPanel());

				nameBinding = new Binding();
				nameBinding.Path = new PropertyPath("name");
				nameBinding.Source = herolist_list[i];
				nameBinding.Mode = BindingMode.TwoWay;

				enableBinding = new Binding();
				enableBinding.Path = new PropertyPath("enable");
				enableBinding.Source = herolist_list[i];
				enableBinding.Mode = BindingMode.TwoWay;

				TextBox heroNameEditable = new TextBox();
				heroNameEditable.SetBinding(TextBox.TextProperty, nameBinding);
				CheckBox heroEnabled = new CheckBox();
				heroEnabled.SetBinding(CheckBox.IsCheckedProperty, enableBinding);
				heroEnabled.Margin = new Thickness(0, 0, 0, 0);
				docks[i].Children.Add(heroEnabled);
				docks[i].Children.Add(heroNameEditable);
				docks[i].PreviewMouseLeftButtonDown += new MouseButtonEventHandler(HeroClick);
				docks[i].MinWidth = 175;

				treeItem.Items.Add(new TreeViewItem() { Header = docks[i] });
			}
			herolist_treeview.Items.Add(treeItem);
		}

		private void AddHero()
		{
			/*MessageBoxButton btn = MessageBoxButton.OK;
			ModernDialog.ShowMessage("", "", btn);*/
			addHeroDialog = new AddHeroDialog();
			addHeroDialog.ShowDialog();
			string t = addHeroDialog.MessageBoxResult.ToString();
			Console.WriteLine(t);
			if (addHeroDialog.MessageBoxResult.ToString() == "OK")
			{
				var d = new DotaData.Hero { name = "Added 1", enable = true };
				herolist_list.Add(d);
			}
			UpdateTreeView();
			ToggleRemoveButton();
		}

		private void RemoveHero()
		{
			int index;
			for (index = 0; index < herolist_list.Count; index++)
			{
				if (String.Equals(selectedHeroName, herolist_list[index].name))
				{
					herolist_list.RemoveAt(index);
					UpdateTreeView();
					ToggleRemoveButton();
					return;
				}
			}
			if (index == herolist_list.Count)
			{
				herolist_list.RemoveAt(index - 1);
			}
			ToggleRemoveButton();
			UpdateTreeView();
		}

		private void ToggleRemoveButton()
		{
			if (herolist_list.Count <= 0)
			{
				removeHeroButton.Content = "Nothing To Remove";
				removeHeroButton.IsEnabled = false;
			}
			else if (herolist_list.Count > 0)
			{
				removeHeroButton.Content = "Remove " + herolist_list[herolist_list.Count - 1].name;
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
			AddHero();
		}

		private void RemoveHeroClick(object sender, RoutedEventArgs e)
		{
			RemoveHero();
		}

		TextBox tempTextbox;
		DockPanel tempStackpanel;
		private void HeroClick(object sender, MouseButtonEventArgs e)
		{
			tempStackpanel = sender as DockPanel;
			tempTextbox = tempStackpanel.Children[1] as TextBox;
			selectedHeroName = tempTextbox.Text;
			removeHeroButton.Content = "Remove " + selectedHeroName;
			if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
				RemoveHero();
		}

		#endregion

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

			Console.WriteLine(herolist_list[0].name);
			Console.WriteLine(herolist_list[0].enable);
		}

		private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
		{
			ScrollViewer scv = (ScrollViewer)sender;
			scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta * 0.25);
			e.Handled = true;
		}
	}
}