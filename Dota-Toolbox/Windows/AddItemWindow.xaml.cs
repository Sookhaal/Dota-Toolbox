using Dota_Toolbox.Parser;
using Dota_Toolbox.Settings;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
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

namespace Dota_Toolbox.Windows
{
	/// <summary>
	/// Interaction logic for AddItemWindow.xaml
	/// </summary>
	public partial class AddItemWindow : ModernWindow
	{
		public Item a;
		private PromptDialog errorDialog;
		private AbilityBehaviorsDialog abilityBehaviorsDialog;
		private string[] currentLines;
		private string tempText;

		public AddItemWindow()
		{
			InitializeComponent();
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			if (a.name != "" && a.name != null)
				name.Text = a.name;
			if (a.id >= 0)
				id.Text = a.id.ToString();
			/*if (a.abilityBehavior != "" && a.abilityBehavior != null)
				abilityBehaviorsDialog.abilityBehavior_list.Add();*/
			/*Console.WriteLine(a.abilityBehavior.Length);
			for (int i = 0; i < a.abilityBehavior.Length; i++)
				Console.WriteLine(a.abilityBehavior[i]);*/

			/*if (a.abilityBehavior != "" && a.abilityBehavior != null)
				abilityBehavior.Text = a.abilityBehavior;
			if (a.abilityUnitTargetType != "" && a.abilityUnitTargetType != null)
				abilityUnitTargetType.Text = a.abilityUnitTargetType;*/
		}

		private void CreateBehaviorsDialog()
		{
			try
			{
				currentLines = File.ReadAllLines("Data\\DefaultAbilityBehavior.txt");
				if (currentLines.Length > 0)
					for (int i = 0; i < currentLines.Length; i++)
						abilityBehaviorsDialog.behaviors_list_base.Items.Add(currentLines[i]);
			}
			catch
			{
				Button b = new Button();
				b.Content = "DefaultAbilityBehavior.txt";
				b.Click += BehaviorErrorButton_Clicked;

				errorDialog = new PromptDialog("Error");
				errorDialog.AddColoredHeader("File Not Found");
				errorDialog.AddButton(b);
				errorDialog.AddBBCode("An empty file will be created. Feel free to edit it.");
				errorDialog.AddBBCode("[url=http://pastebin.com/raw.php?i=jqWJC05T]Default Values.[/url]");

				errorDialog.ShowDialog();
				CreateBehaviorsDialog();
			}
		}

		private void BehaviorErrorButton_Clicked(object sender, RoutedEventArgs e)
		{
			Utils.ExplorePath(ApplicationSettings.applicationPath + "\\Data");
		}

		private void Behaviors_Click(object sender, RoutedEventArgs e)
		{
			abilityBehaviorsDialog = new AbilityBehaviorsDialog();
			abilityBehaviorsDialog.abilityBehavior_list = a.abilityBehavior.ToList();
			CreateBehaviorsDialog();
			abilityBehaviorsDialog.SetBehaviors();
			abilityBehaviorsDialog.ShowDialog();
			if (abilityBehaviorsDialog.DialogResult == true)
				a.abilityBehavior = abilityBehaviorsDialog.GetNewBehaviors();
			//Console.WriteLine(a.abilityBehavior);
		}

		private void ShopTags_Click(object sender, RoutedEventArgs e)
		{

		}

		private void TargetTypes_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Declarations_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
