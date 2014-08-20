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
		private ItemShopTagsDialog itemShopTagsDialog;
		private string[] currentLines;

		public AddItemWindow()
		{
			InitializeComponent();
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			name.Text = a.name;
			id.Text = a.id.ToString();

			/*abilityBehavior*/
			ParseDataToCombobox("DefaultAbilityUnitTargetTeam.txt", abilityUnitTargetTeam, "http://pastebin.com/raw.php?i=rMdBZKAT");
			abilityUnitTargetTeam.SelectedItem = a.abilityUnitTargetTeam;
			/*abilityUnitTargetType*/
			model.Text = a.model;
			baseClass.Text = a.baseClass;
			abilityTextureName.Text = a.abilityTextureName;
			itemKillable.IsChecked = a.itemKillable;

			abilityCastRange.Text = a.abilityCastRange.ToString();
			abilityCastPoint.Text = a.abilityCastPoint.ToString();

			itemCost.Text = a.itemCost.ToString();
			//itemShopTags
			ParseDataToCombobox("DefaultItemQuality.txt", itemQuality, "http://pastebin.com/raw.php?i=n6kxbGZm");
			itemQuality.SelectedItem = a.itemQuality;
			itemStackable.IsChecked = a.itemStackable;
			ParseDataToCombobox("DefaultItemShareability.txt", itemShareability, "http://pastebin.com/raw.php?i=n3ussZgT");
			itemShareability.SelectedItem = a.itemShareability;
			itemPermanent.IsChecked = a.itemPermanent;
			itemInitialCharges.Text = a.itemInitialCharges.ToString();
			sideShop.IsChecked = a.sideShop;
		}


		//Could use ParseDataToList directly. . .
		private void ParseDataToCombobox(string file, ComboBox comboBox, string link)
		{
			try
			{
				currentLines = File.ReadAllLines("Data\\" + file);
				if (currentLines.Length > 0)
					for (int i = 0; i < currentLines.Length; i++)
						comboBox.Items.Add(currentLines[i]);
			}
			catch
			{
				Button b = new Button();
				b.Content = file;
				b.Click += FileError_Button_Clicked;

				errorDialog = new PromptDialog("Error");
				errorDialog.AddColoredHeader("File Not Found");
				errorDialog.AddButton(b);
				errorDialog.AddBBCode("An empty file will be created. Feel free to edit it.");
				errorDialog.AddBBCode("[url=" + link + "]Default Values.[/url]");

				errorDialog.ShowDialog();
				ParseDataToCombobox(file, comboBox, link);
			}
		}

		private void ParseDataToList(string file, List<string> stringsList, string link)
		{
			try
			{
				currentLines = File.ReadAllLines("Data\\" + file);
				if (currentLines.Length > 0)
					for (int i = 0; i < currentLines.Length; i++)
						stringsList.Add(currentLines[i]);
			}
			catch
			{
				Button b = new Button();
				b.Content = file;
				b.Click += FileError_Button_Clicked;

				errorDialog = new PromptDialog("Error");
				errorDialog.AddColoredHeader("File Not Found");
				errorDialog.AddButton(b);
				errorDialog.AddBBCode("An empty file will be created. Feel free to edit it.");
				errorDialog.AddBBCode("[url=" + link + "]Default Values.[/url]");

				errorDialog.ShowDialog();
				ParseDataToList(file, stringsList, link);
			}
		}

		#region UI Init
		private void CreateBehaviorsDialog()
		{
			try { ParseDataToCombobox("DefaultAbilityBehavior.txt", abilityBehaviorsDialog.behaviors_list_base, "http://pastebin.com/raw.php?i=jqWJC05T"); }
			catch { CreateBehaviorsDialog(); }
		}

		private void CreateItemShopTagsDialog()
		{
			try { ParseDataToList("DefaultItemShopTags.txt", itemShopTagsDialog.itemShopTags_list_base, "http://pastebin.com/raw.php?i=VmWrBWqf"); }
			catch { CreateItemShopTagsDialog(); }
		}
		#endregion

		private void FileError_Button_Clicked(object sender, RoutedEventArgs e)
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
		}

		private void ShopTags_Click(object sender, RoutedEventArgs e)
		{
			itemShopTagsDialog = new ItemShopTagsDialog();
			CreateItemShopTagsDialog();
			itemShopTagsDialog.SetTags();
			itemShopTagsDialog.ShowDialog();
		}

		private void TargetTypes_Click(object sender, RoutedEventArgs e)
		{

		}

		private void TargetFlags_Click(object sender, RoutedEventArgs e)
		{

		}

		private void Declarations_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
