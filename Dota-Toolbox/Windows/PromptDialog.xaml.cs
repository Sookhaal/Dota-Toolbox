using Dota_Toolbox.Settings;
using FirstFloor.ModernUI.Windows.Controls;
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
	/// Interaction logic for PromptDialog.xaml
	/// </summary>
	public partial class PromptDialog : ModernDialog
	{
		public PromptDialog(string title)
		{
			InitializeComponent();
			this.Title = title;
			this.Buttons = new Button[] { this.OkButton, this.CancelButton };
		}

		public PromptDialog(string title, bool okButton, bool cancelButton)
		{
			InitializeComponent();
			if (okButton)
				this.Buttons = new Button[] { this.OkButton };
			if (cancelButton)
				this.Buttons.ToList().Add(this.CancelButton);
			this.Title = title;
		}

		public void AddHeader(string text)
		{
			BBCodeBlock t = new BBCodeBlock();
			t.BBCode = "[b][size=16]" + text.ToUpper() + "[/b][/size]";
			t.Margin = new Thickness(0, 0, 0, 4);
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
			t.Margin = new Thickness(0, 0, 0, 4);
			s.Children.Add(t);
		}

		public void AddTextBlock(string text)
		{
			TextBlock t = new TextBlock();
			t.Text = text;
			t.Margin = new Thickness(0, 0, 0, 4);
			s.Children.Add(t);
		}

		public void AddBBCode(string text)
		{
			BBCodeBlock t = new BBCodeBlock();
			t.BBCode = text;
			t.Margin = new Thickness(0, 0, 0, 4);
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
			t.Margin = new Thickness(0, 0, 0, 4);
			s.Children.Add(t);
		}

		public void AddButton(Button b)
		{
			b.Margin = new Thickness(0, 0, 0, 4);
			s.Children.Add(b);
		}

		public void AddCombobox(string[] items)
		{
			ComboBox c = new ComboBox();
			if (items.Length > 0)
				for (int i = 0; i < items.Length; i++)
					c.Items.Add(items[i]);
			c.Margin = new Thickness(0, 0, 0, 4);
			s.Children.Add(c);
		}
	}
}
