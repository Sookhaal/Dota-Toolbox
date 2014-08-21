﻿using FirstFloor.ModernUI.Windows.Controls;
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
	/// Interaction logic for AbilityBehaviorsDialog.xaml
	/// </summary>
	public partial class AbilityBehaviorsDialog : ModernDialog
	{
		public List<string> abilityBehavior_list = new List<string>();
		public AbilityBehaviorsDialog()
		{
			InitializeComponent();
			this.Buttons = new Button[] { this.OkButton, this.CancelButton };
		}

		public void SetBehaviors()
		{
			for (int i = 0; i < abilityBehavior_list.Count; i++)
				if (abilityBehavior_list[i] != "")
					AddComboBox(abilityBehavior_list[i]);
		}

		public string[] GetNewBehaviors()
		{
			List<string> str = new List<string>();
			for (int i = 0; i < s.Children.Count - 1; i++)			//-1 Because "Add Behavior" button is a child
			{
				DockPanel t = (DockPanel)s.Children[i];
				ComboBox cbox = (ComboBox)t.Children[1];
				str.Add(cbox.SelectedItem.ToString());
			}
			return str.ToArray();
		}

		private void AddComboBox()
		{
			AddComboBox("");
		}

		private void AddComboBox(string item)
		{
			DockPanel dockPanel = new DockPanel();
			ComboBox c = new ComboBox();
			for (int i = 0; i < abilityBehavior_combobox_base.Items.Count; i++)
				c.Items.Add(abilityBehavior_combobox_base.Items[i]);
			c.SelectedItem = item;

			Button b = new Button();
			b.Background = x_button_base.Background;
			b.Content = "X";
			b.Margin = new Thickness(4, 0, 0, 0);
			b.Click += RemoveBehavior_Click;

			DockPanel.SetDock(b, Dock.Right);
			dockPanel.Children.Add(b);
			dockPanel.Children.Add(c);
			dockPanel.Margin = new Thickness(0, 0, 0, 4);
			s.Children.Insert(s.Children.Count - 1, dockPanel);		//Insert the DockPanel on top of the button
		}

		private void AddBehavior_Click(object sender, RoutedEventArgs e)
		{
			AddComboBox(abilityBehavior_combobox_base.Items[0].ToString());
		}

		private void RemoveBehavior_Click(object sender, RoutedEventArgs e)
		{
			var tempButton = (Button)sender;
			var parent = (UIElement)tempButton.Parent;
			s.Children.Remove(parent);
		}
	}
}
