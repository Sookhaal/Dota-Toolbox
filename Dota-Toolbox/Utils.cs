using Dota_Toolbox.Settings;
using Dota_Toolbox.Windows;
using KVLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Dota_Toolbox
{
	public static class Utils
	{
		public static void ExplorePath(string path)
		{
			if (Directory.Exists(path))
				Process.Start(path);
			else
				Console.WriteLine(path);
		}

		public static void CreateNewFile(string outputPath, string rootKey)
		{
			string root = "";
			root = "\"" + rootKey + "\"";
			root += "\r\n{\r\n";
			root += "}";
			File.WriteAllText(outputPath, root);
		}

		private static string tempRootKey;
		public static void SaveToFile(ObservableCollection<KeyValue> kv_list, string outputPath, string rootKey)
		{
			if (File.Exists(outputPath))
			{
				string root = "";
				try
				{
					root = "\"" + rootKey + "\"";
					root += "\r\n{\r\n";
					Stopwatch sw = new Stopwatch();
					sw.Start();
					root += Write(kv_list[0].Parent.children);
					sw.Stop();
					root += "}";
					File.WriteAllText(outputPath, root);

					Console.WriteLine("Wrote " + kv_list.Count + " items");
					Console.WriteLine("in " + sw.Elapsed + " seconds");
				}
				catch
				{
					root = "\"" + rootKey + "\"";
					root += "\r\n{\r\n";
					root += "}";
					File.WriteAllText(outputPath, root);
				}
			}
			else
			{
				File.Create(outputPath).Close();
				SaveToFile(kv_list, outputPath, rootKey);
			}
		}

		private static string Write(List<KeyValue> parent)
		{
			string o = "";
			try
			{
				for (int i = 0; i < parent.Count; i++)
				{
					try { parent[i].TabIndex = parent[i].Parent.TabIndex + 1; }
					catch { Console.WriteLine("Error: " + parent[i].Key); }

					for (int a = 0; a < parent[i].TabIndex; a++)
						o += "\t";
					o += "\"" + parent[i].Key + "\"";
					if (!parent[i].HasChildren)
						o += "\t\"" + parent[i].Value + "\"\r\n";
					else
					{
						o += "\r\n";
						for (int a = 0; a < parent[i].TabIndex; a++)
							o += "\t";
						o += "{";
						o += "\r\n";
						o += Write(parent[i].children);
						for (int a = 0; a < parent[i].TabIndex; a++)
							o += "\t";
						o += "}\r\n";
					}
				}
				return o;
			}
			catch
			{
				Console.WriteLine("Error: " + parent.ToString());
				return o;
			}
		}
	}
}