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
	/// Interaction logic for PromptDialog.xaml
	/// </summary>
	public partial class PromptDialog : ModernDialog
	{
		public PromptDialog(Window owner)
		{
			InitializeComponent();
			this.Owner = owner;
			this.Buttons = new Button[] { this.OkButton, this.CancelButton };
		}

		public PromptDialog(string title, Window owner)
		{
			InitializeComponent();
			this.Owner = owner;
			this.Title = title;
			this.Buttons = new Button[] { this.OkButton, this.CancelButton };
		}

		public PromptDialog(bool okButton, bool cancelButton, Window owner)
		{
			InitializeComponent();
			this.Owner = owner;
			if (okButton)
				this.Buttons = new Button[] { this.OkButton };
			if (cancelButton)
				this.Buttons.ToList().Add(this.CancelButton);
		}

		public PromptDialog(string title, bool okButton, bool cancelButton, Window owner)
		{
			InitializeComponent();
			this.Owner = owner;
			if (okButton)
				this.Buttons = new Button[] { this.OkButton };
			if (cancelButton)
				this.Buttons.ToList().Add(this.CancelButton);
			this.Title = title;
		}

		/*public void Clear()
		{
			s.Children.Clear();
		}*/

		public void MissingFile(string fileName, string link)
		{
			File.Create(ApplicationSettings.applicationPath + "\\Data\\" + fileName).Close();
			this.Title = "Error";
			Button b = new Button();
			b.Content = fileName;
			b.Click += MissingFile_Clicked;

			AddColoredHeader("File Not Found");
			AddButton(b);
			AddBBCode("An empty file has been created. Feel free to edit it.");
			AddBBCode("[url=" + link + "]Default Values.[/url]");
		}

		private void MissingFile_Clicked(object sender, RoutedEventArgs e)
		{
			Utils.ExplorePath(ApplicationSettings.applicationPath + "\\Data");
		}

		private Thickness baseMargin = new Thickness(0, 0, 0, 4);
		public void AddHeader(string text)
		{
			BBCodeBlock t = new BBCodeBlock();
			t.BBCode = "[b][size=16]" + text.ToUpper() + "[/b][/size]";
			t.Margin = baseMargin;
			s.Children.Add(t);
		}

		public void AddColoredHeader(string text)
		{
			BBCodeBlock t = new BBCodeBlock();
			t.BBCode = "[color=#" +
				ApplicationSettings.instance.accentColor.R.ToString("X2") +
				ApplicationSettings.instance.accentColor.G.ToString("X2") +
				ApplicationSettings.instance.accentColor.B.ToString("X2") + "]" +
				"[b][size=16]" + text.ToUpper() + "[/b][/size]" +
				"[/color]";
			t.Margin = baseMargin;
			s.Children.Add(t);
		}

		public void AddTextBlock(string text)
		{
			TextBlock t = new TextBlock();
			t.Text = text;
			t.Margin = baseMargin;
			s.Children.Add(t);
		}

		public void AddBBCode(string text)
		{
			BBCodeBlock t = new BBCodeBlock();
			t.BBCode = text;
			t.Margin = baseMargin;
			s.Children.Add(t);
		}

		public void AddColoredBBCode(string text)
		{
			BBCodeBlock t = new BBCodeBlock();
			t.BBCode = "[color=#" +
				ApplicationSettings.instance.accentColor.R.ToString("X2") +
				ApplicationSettings.instance.accentColor.G.ToString("X2") +
				ApplicationSettings.instance.accentColor.B.ToString("X2") + "]" +
				text +
				"[/color]";
			t.Margin = baseMargin;
			s.Children.Add(t);
		}

		public void AddButton(Button b)
		{
			b.Margin = baseMargin;
			s.Children.Add(b);
		}

		public void AddCombobox(ComboBox combobox, string[] items)
		{
			combobox = new ComboBox();
			if (items.Length > 0)
				for (int i = 0; i < items.Length; i++)
					combobox.Items.Add(items[i]);
			combobox.Margin = baseMargin;
			s.Children.Add(combobox);
		}

		public void AddTextBox(TextBox textbox)
		{
			textbox.Margin = baseMargin;
			s.Children.Add(textbox);
		}
	}
}
