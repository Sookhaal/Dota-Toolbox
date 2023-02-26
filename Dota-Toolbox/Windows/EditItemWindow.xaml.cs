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
	public partial class EditItemWindow : ModernWindow
	{
		private string[] currentLines;
		public Item a;
		private string rootKey = "DOTAAbilities";
		private PromptDialog errorDialog;
		private AbilityBehaviorsDialog abilityBehaviorsDialog;
		private AbilityUnitTargetTypeDialog abilityUnitTargetTypeDialog;
		private AbilityUnitTargetFlagsDialog abilityUnitTargetFlagsDialog;
		private ItemShopTagsDialog itemShopTagsDialog;
		private ItemDeclarationsDialog itemDeclarationsDialog;
		private AbilitySpecialDialog abilitySpecialDialog;
		private ModifiersDialog modifiersDialog;

		public EditItemWindow()
		{
			InitializeComponent();
			this.Topmost = true;
		}

		private void WindowLoaded(object sender, RoutedEventArgs e)
		{
			name.Text = a.name;
			id.Text = a.id.ToString();

			//abilityBehaviorString = String.Join(" | ", a.abilityBehavior);	//Not Needed
			ParseDataToCombobox("DefaultAbilityUnitTargetTeam.txt", abilityUnitTargetTeam, "http://pastebin.com/raw.php?i=rMdBZKAT");
			abilityUnitTargetTeam.SelectedItem = a.abilityUnitTargetTeam;
			//abilityUnitTargetTypeString = String.Join(" | ", a.abilityUnitTargetType);//Not needed
			model.Text = a.model;
			//baseClass.Text = a.baseClass;
			ParseDataToCombobox("DefaultBaseClass.txt", baseClass, "");
			baseClass.SelectedItem = a.baseClass;
			abilityTextureName.Text = a.abilityTextureName;
			itemKillable.IsChecked = a.itemKillable;
			abilityCastAnimation.Text = a.abilityCastAnimation;

			abilityCastRange.Text = a.abilityCastRange.ToString();
			abilityCastPoint.Text = a.abilityCastPoint.ToString();
			abilityCooldown.Text = a.abilityCooldown.ToString();
			abilityChannelTime.Text = a.abilityChannelTime.ToString();
			abilityManaCost.Text = a.abilityManaCost.ToString();

			itemCost.Text = a.itemCost.ToString();
			//itemShopTagsString = String.Join(";", a.itemShopTags);	//Not needed
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
			//itemDeclarationsString = String.Join(" | ", a.itemDeclarations);	//Not Needed
		}


		//Could use ParseDataToList directly.
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
				errorDialog = new PromptDialog(true, false, ModernWindow.GetWindow(this));
				errorDialog.MissingFile(file, link);
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
				errorDialog = new PromptDialog(true, false, ModernWindow.GetWindow(this));
				errorDialog.MissingFile(file, link);
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

		private void CreateAbilityUnitTargetTypeDialog()
		{
			try { ParseDataToCombobox("DefaultAbilityUnitTargetType.txt", abilityUnitTargetTypeDialog.abilityUnitTargetType_combobox_base, "http://pastebin.com/raw.php?i=rh8XeH8Q"); }
			catch { CreateAbilityUnitTargetTypeDialog(); }
		}

		private void CreateAbilityUnitTargetFlagsDialog()
		{
			try { ParseDataToCombobox("DefaultAbilityUnitTargetFlags.txt", abilityUnitTargetFlagsDialog.abilityUnitTargetFlags_combobox_base, "http://pastebin.com/raw.php?i=b5jtUZvW"); }
			catch { CreateAbilityUnitTargetFlagsDialog(); }
		}

		private void CreateAbilitySpecialDialog()
		{
			try { ParseDataToCombobox("DefaultAbilitySpecials.txt", abilitySpecialDialog.specialType_combobox_base, ""); }
			catch { CreateAbilitySpecialDialog(); }
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

		private void Declarations_Click(object sender, RoutedEventArgs e)
		{
			itemDeclarationsDialog = new ItemDeclarationsDialog();
			itemDeclarationsDialog.Owner = ModernWindow.GetWindow(this);
			itemDeclarationsDialog.itemDeclarations_list = a.itemDeclarations.ToList();
			CreateitemDeclarationsDialog();
			itemDeclarationsDialog.SetDeclarations();
			itemDeclarationsDialog.ShowDialog();
			if (itemDeclarationsDialog.DialogResult == true)
				a.itemDeclarations = itemDeclarationsDialog.GetNewDeclarations();
		}

		private void TargetTypes_Click(object sender, RoutedEventArgs e)
		{
			abilityUnitTargetTypeDialog = new AbilityUnitTargetTypeDialog();
			abilityUnitTargetTypeDialog.Owner = ModernWindow.GetWindow(this);
			abilityUnitTargetTypeDialog.abilityUnitTargetTypes_list = a.abilityUnitTargetType.ToList();
			CreateAbilityUnitTargetTypeDialog();
			abilityUnitTargetTypeDialog.SetTypes();
			abilityUnitTargetTypeDialog.ShowDialog();
			if (abilityUnitTargetTypeDialog.DialogResult == true)
				a.abilityUnitTargetType = abilityUnitTargetTypeDialog.GetNewTypes();
		}

		private void TargetFlags_Click(object sender, RoutedEventArgs e)
		{
			abilityUnitTargetFlagsDialog = new AbilityUnitTargetFlagsDialog();
			abilityUnitTargetFlagsDialog.Owner = ModernWindow.GetWindow(this);
			abilityUnitTargetFlagsDialog.abilityUnitTargetFlags_list = a.abilityUnitTargetFlags.ToList();
			CreateAbilityUnitTargetFlagsDialog();
			abilityUnitTargetFlagsDialog.SetFlags();
			abilityUnitTargetFlagsDialog.ShowDialog();
			if (abilityUnitTargetFlagsDialog.DialogResult == true)
				a.abilityUnitTargetFlags = abilityUnitTargetFlagsDialog.GetNewFlags();
		}

		private void Special_Click(object sender, RoutedEventArgs e)
		{
			abilitySpecialDialog = new AbilitySpecialDialog();
			abilitySpecialDialog.Owner = ModernWindow.GetWindow(this);
			abilitySpecialDialog.abilitySpecials_list = a.abilitySpecials.Children.ToList();
			CreateAbilitySpecialDialog();
			abilitySpecialDialog.SetSpecials();
			abilitySpecialDialog.ShowDialog();
			if (abilitySpecialDialog.DialogResult == true)
				a.abilitySpecials = abilitySpecialDialog.GetNewSpecials();
		}

		private void Modifiers_Click(object sender, RoutedEventArgs e)
		{
			modifiersDialog = new ModifiersDialog();
			modifiersDialog.Owner = ModernWindow.GetWindow(this);
			modifiersDialog.modifiers_list = a.modifiers.Children.ToList();
		}

		private void OK_Click(object sender, RoutedEventArgs e)
		{
			if (name.Text == "")
			{
				TextBox t = new TextBox();
				errorDialog = new PromptDialog("Error", ModernWindow.GetWindow(this));
				errorDialog.AddColoredHeader("Invalid Name");
				errorDialog.AddTextBlock("Please enter a valid name.");
				errorDialog.ShowDialog();
				if (errorDialog.DialogResult == true && t.Text != "")
					a.name = t.Text;
				else
					errorDialog.ShowDialog();
				return;
			}
			SaveAll();
			ItemEditor.UpdateItemNameAt(ItemEditor.index);
			if (ApplicationSettings.instance.autoSave)
				Utils.SaveToFile(ItemEditor.kv_list, ApplicationSettings.instance.currentModPath + "\\scripts\\npc\\" + ItemEditor.file, rootKey);
			this.Close();
		}

		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void SaveAll()
		{
			a.item.Key = name.Text;
			a.SetString("ID", id.Text);

			a.SetStringArray("AbilityBehavior", a.abilityBehavior);
			a.SetString("AbilityUnitTargetTeam", abilityUnitTargetTeam.Text);
			a.SetStringArray("AbilityUnitTargetType", a.abilityUnitTargetType);
			a.SetString("Model", model.Text);
			a.SetString("BaseClass", baseClass.Text);
			a.SetString("AbilityTextureName", abilityTextureName.Text);
			a.SetBool("ItemKillable", itemKillable.IsChecked.Value);

			a.SetString("AbilityCastRange", abilityCastRange.Text);
			a.SetString("AbilityCastPoint", abilityCastPoint.Text);

			a.SetString("ItemCost", itemCost.Text);
			a.SetStringArraySemi("ItemShopTags", a.itemShopTags);
			a.SetString("ItemQuality", itemQuality.Text);
			a.SetBool("ItemStackable", itemStackable.IsChecked.Value);
			a.SetString("ItemShareability", itemShareability.Text);
			a.SetBool("ItemPermanent", itemPermanent.IsChecked.Value);
			a.SetString("ItemInitialCharges", itemInitialCharges.Text);
			a.SetBool("SideShop", sideShop.IsChecked.Value);

			a.SetString("ItemStockInitial", itemStockInitial.Text);
			a.SetString("ItemStockMax", itemStockMax.Text);
			a.SetString("itemStockTime", itemStockTime.Text);
			a.SetStringArray("ItemDeclarations", a.itemDeclarations);

			a.SetKV(a.abilitySpecials);
		}
	}
}
