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
	/// Interaction logic for AbilitySpecialDialog.xaml
	/// </summary>
	public partial class AbilitySpecialDialog : ModernDialog
	{
		public List<KeyValue> abilitySpecials_list = new List<KeyValue>();
		public AbilitySpecialDialog()
		{
			InitializeComponent();
			this.Buttons = new Button[] { this.OkButton, this.CancelButton };
		}

		public void SetSpecials()
		{
			for (int i = 0; i < abilitySpecials_list.Count; i++)
				if (abilitySpecials_list[i].HasChildren)
					//for (int a = 0; a < abilitySpecials_list[i].children.Count; a++)
					AddSpecial(abilitySpecials_list[i]);
		}

		public KeyValue GetNewSpecials()
		{
			KeyValue newSpecials = new KeyValue("AbilitySpecial");
			for (int i = 0; i < s.Children.Count - 1; i++)			//-1 Because "Add Behavior" button is a child
			{
				Grid g = (Grid)s.Children[i];
				ComboBox t = (ComboBox)g.Children[0];
				TextBox n = (TextBox)g.Children[1];
				TextBox v = (TextBox)g.Children[2];

				if (t.SelectedItem.ToString() == "") t.SelectedItem = t.Items[0];
				if (n.Text == "") n.Text = "var_" + (i + 1);
				if (v.Text == "") v.Text = "0";

				KeyValue abilitySpecial = new KeyValue((i + 1).ToString("D2"));
				KeyValue var_type = new KeyValue("var_type", t.SelectedItem.ToString());
				KeyValue var = new KeyValue(n.Text, v.Text);

				abilitySpecial.AddChild(var_type);
				abilitySpecial.AddChild(var);

				newSpecials.AddChild(abilitySpecial);
			}
			return newSpecials;
		}

		private void AddSpecial(KeyValue special)
		{
			Thickness margin = new Thickness(0, 0, 3, 0);
			Grid grid = new Grid();
			ComboBox type = new ComboBox();
			TextBox name = new TextBox();
			TextBox value = new TextBox();
			Button delete = new Button();

			type.Margin = margin;
			name.Margin = margin;
			value.Margin = margin;

			type.ToolTip = "Type";
			name.ToolTip = "Name";
			value.ToolTip = "Value";

			type.VerticalAlignment = VerticalAlignment.Center;
			name.VerticalAlignment = VerticalAlignment.Center;
			value.VerticalAlignment = VerticalAlignment.Center;
			delete.VerticalAlignment = VerticalAlignment.Center;

			for (int i = 0; i < specialType_combobox_base.Items.Count; i++)
				type.Items.Add(specialType_combobox_base.Items[i]);
			try
			{
				for (int i = 0; i < special.children.Count; i++)
					if (String.Equals("var_type", special.children[i].Key))
						type.SelectedItem = special.children[i].Value;
					else
					{
						name.Text = special.children[i].Key;
						value.Text = special.children[i].Value;
					}
			}
			catch
			{
				type.SelectedItem = type.Items[0];
			}

			//name.Text = if(String.Equals()

			delete.Content = "X";
			delete.Background = x_button_base.Background;
			delete.Click += RemoveSpecial_Click;

			Grid.SetColumn(type, 0);
			Grid.SetColumn(name, 1);
			Grid.SetColumn(value, 2);
			Grid.SetColumn(delete, 3);

			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.ColumnDefinitions.Add(new ColumnDefinition());
			grid.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
			grid.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
			grid.ColumnDefinitions[2].Width = new GridLength(1, GridUnitType.Star);
			grid.ColumnDefinitions[3].Width = GridLength.Auto;

			grid.Children.Add(type);
			grid.Children.Add(name);
			grid.Children.Add(value);
			grid.Children.Add(delete);

			s.Children.Insert(s.Children.Count - 1, grid);
		}

		private void AddSpecial_Click(object sender, RoutedEventArgs e)
		{
			AddSpecial(new KeyValue(""));
		}

		private void RemoveSpecial_Click(object sender, RoutedEventArgs e)
		{
			var tempButton = (Button)sender;
			var parent = (Grid)tempButton.Parent;
			s.Children.Remove(parent);
		}
	}
}
