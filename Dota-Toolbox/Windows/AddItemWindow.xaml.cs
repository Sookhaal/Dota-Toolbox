using Dota_Toolbox.Pages.Tools;
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
		private ItemDeclarationsDialog itemDeclarationsDialog;
		private string[] currentLines;
		private string abilityBehaviorString, abilityUnitTargetTypeString, itemShopTagsString, itemDeclarationsString;

		public AddItemWindow()
		{
			InitializeComponent();
			this.Topmost = true;
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			//this.Topmost = true;
			name.Text = a.name;
			id.Text = a.id.ToString();

			abilityBehaviorString = String.Join(" | ", a.abilityBehavior);
			ParseDataToCombobox("DefaultAbilityUnitTargetTeam.txt", abilityUnitTargetTeam, "http://pastebin.com/raw.php?i=rMdBZKAT");
			abilityUnitTargetTeam.SelectedItem = a.abilityUnitTargetTeam;
			abilityUnitTargetTypeString = String.Join(" | ", a.abilityUnitTargetType);
			model.Text = a.model;
			baseClass.Text = a.baseClass;
			abilityTextureName.Text = a.abilityTextureName;
			itemKillable.IsChecked = a.itemKillable;

			abilityCastRange.Text = a.abilityCastRange.ToString();
			abilityCastPoint.Text = a.abilityCastPoint.ToString();

			itemCost.Text = a.itemCost.ToString();
			itemShopTagsString = String.Join(";", a.itemShopTags);
			ParseDataToCombobox("DefaultItemQuality.txt", itemQuality, "http://pastebin.com/raw.php?i=n6kxbGZm");
			itemQuality.SelectedItem = a.itemQuality;
			itemStackable.IsChecked = a.itemStackable;
			ParseDataToCombobox("DefaultItemShareability.txt", itemShareability, "http://pastebin.com/raw.php?i=n3ussZgT");
			itemShareability.SelectedItem = a.itemShareability;
			itemPermanent.IsChecked = a.itemPermanent;
			itemInitialCharges.Text = a.itemInitialCharges.ToString();
			sideShop.IsChecked = a.sideShop;

			itemStockInitial.Text = a.itemStockInitial.ToString();
			itemStockMax.Text = a.itemStockMax.ToString();
			itemStockTime.Text = a.itemStockTime.ToString();
			itemDeclarationsString = String.Join(" | ", a.itemDeclarations);
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
				errorDialog = new PromptDialog(true, false);
				errorDialog.MissingFile(file, link);
				errorDialog.Owner = ModernWindow.GetWindow(this);
				this.errorDialog.ShowDialog();
				if (errorDialog.DialogResult == true)
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
				errorDialog = new PromptDialog(true, false);
				errorDialog.MissingFile(file, link);
				errorDialog.Owner = ModernWindow.GetWindow(this);
				errorDialog.ShowDialog();
				ParseDataToList(file, stringsList, link);
			}
		}

		#region UI Init
		private void CreateBehaviorsDialog()
		{
			try { ParseDataToCombobox("DefaultAbilityBehavior.txt", abilityBehaviorsDialog.abilityBehavior_combobox_base, "http://pastebin.com/raw.php?i=jqWJC05T"); }
			catch { CreateBehaviorsDialog(); }
		}

		private void CreateItemShopTagsDialog()
		{
			try { ParseDataToCombobox("DefaultItemShopTags.txt", itemShopTagsDialog.shopTags_combobox_base, "http://pastebin.com/raw.php?i=VmWrBWqf"); }
			catch { CreateItemShopTagsDialog(); }
		}

		private void CreateitemDeclarationsDialog()
		{
			try { ParseDataToCombobox("DefaultItemDeclarations.txt", itemDeclarationsDialog.itemDeclarations_combobox_base, "http://pastebin.com/raw.php?i=grDzMrNs"); }
			catch { CreateitemDeclarationsDialog(); }
		}
		#endregion

		private void FileError_Button_Clicked(object sender, RoutedEventArgs e)
		{
			Utils.ExplorePath(ApplicationSettings.applicationPath + "\\Data");
		}

		private void Behaviors_Click(object sender, RoutedEventArgs e)
		{
			abilityBehaviorsDialog = new AbilityBehaviorsDialog();
			abilityBehaviorsDialog.Owner = ModernWindow.GetWindow(this);
			abilityBehaviorsDialog.abilityBehavior_list = a.abilityBehavior.ToList();
			CreateBehaviorsDialog();
			abilityBehaviorsDialog.SetBehaviors();
			abilityBehaviorsDialog.ShowDialog();
			if (abilityBehaviorsDialog.DialogResult == true)
				a.abilityBehavior = abilityBehaviorsDialog.GetNewBehaviors();
			abilityBehaviorString = String.Join(" | ", a.abilityBehavior);
		}

		private void ShopTags_Click(object sender, RoutedEventArgs e)
		{
			itemShopTagsDialog = new ItemShopTagsDialog();
			itemShopTagsDialog.Owner = ModernWindow.GetWindow(this);
			itemShopTagsDialog.itemShopTags_list = a.itemShopTags.ToList();
			CreateItemShopTagsDialog();
			itemShopTagsDialog.SetTags();
			itemShopTagsDialog.ShowDialog();
			if (itemShopTagsDialog.DialogResult == true)
				a.itemShopTags = itemShopTagsDialog.GetNewTags();
		}

		private void TargetTypes_Click(object sender, RoutedEventArgs e)
		{

		}

		private void TargetFlags_Click(object sender, RoutedEventArgs e)
		{

		}

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			SaveAll();
			ItemEditor.kv_list[0] = a.item;
			Utils.SaveToFile(ItemEditor.kv_list, ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + "TestingItem.txt");
			this.Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void Declarations_Click(object sender, RoutedEventArgs e)
		{
			itemDeclarationsDialog = new ItemDeclarationsDialog();
			itemDeclarationsDialog.Owner = ModernWindow.GetWindow(this);
			itemDeclarationsDialog.itemDeclarations_list = a.itemDeclarations.ToList();
			CreateitemDeclarationsDialog();
			itemDeclarationsDialog.SetTags();
			itemDeclarationsDialog.ShowDialog();
			if (itemDeclarationsDialog.DialogResult == true)
				a.itemDeclarations = itemDeclarationsDialog.GetNewTags();
		}

		private void SaveAll()
		{
			a.item.Key = name.Text;
			a.SetString("ID", id.Text);

			a.SetString("AbilityBehavior", abilityBehaviorString);
			a.SetString("AbilityUnitTargetTeam", abilityUnitTargetTeam.Text);
			a.SetString("AbilityUnitTargetType", abilityUnitTargetTypeString);
			a.SetString("Model", model.Text);
			a.SetString("BaseClass", baseClass.Text);
			a.SetString("AbilityTextureName", abilityTextureName.Text);
			//a.SetBool("ItemKillable", itemKillable.IsChecked.Value);

			a.SetString("AbilityCastRange", abilityCastRange.Text);
			a.SetString("AbilityCastPoint", abilityCastPoint.Text);

			a.SetString("ItemCost", itemCost.Text);
			a.SetString("ItemShopTags", itemShopTagsString);
			//a.SetString("ItemQuality", itemQuality.Text);
			//a.SetBool("ItemStackable", itemStackable.IsChecked.Value);
			a.SetString("ItemShareability", itemShareability.Text);
			//a.SetBool("ItemPermanent", itemPermanent.IsChecked.Value);
			a.SetString("ItemInitialCharges", itemInitialCharges.Text);
			//a.SetBool("SideShop", sideShop.IsChecked.Value);

			a.SetString("ItemStockInitial", itemStockInitial.Text);
			a.SetString("ItemStockMax", itemStockMax.Text);
			a.SetString("itemStockTime", itemStockTime.Text);
			a.SetString("ItemDeclarations", itemDeclarationsString);
		}
	}
}
