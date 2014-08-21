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

		public static void SaveToFile(ObservableCollection<KeyValue> kv_list, string outputPath)
		{
			if (File.Exists(outputPath))
			{
				string root = "";
				root = "\"" + kv_list[0].Parent.Key + "\"";
				root += "\r\n{\r\n";
				Console.WriteLine("Writing " + kv_list.Count + "items");
				Stopwatch sw = new Stopwatch();
				sw.Start();
				root += Write(kv_list[0].Parent.children);
				sw.Stop();
				Console.WriteLine("in " + sw.Elapsed + "seconds");
				root += "}";
				File.WriteAllText(outputPath, root);
			}
			else
			{
				File.Create(outputPath).Close();
				SaveToFile(kv_list, outputPath);
			}
		}

		private static string Write(List<KeyValue> parent)
		{
			string o = "";
			//Console.WriteLine(" count: " + parent.Count);
			try
			{
				for (int i = 0; i < parent.Count; i++)
				{
					try
					{
						parent[i].TabIndex = parent[i].Parent.TabIndex + 1;
					}
					catch
					{
						Console.WriteLine("Error: " + parent[i].Key);
					}

					for (int a = 0; a < parent[i].TabIndex; a++)
						o += "\t";
					o += "\"" + parent[i].Key + "\"";
					//if (parent[i].Value != "")
					if (!parent[i].HasChildren)
					{
						o += "\t\"" + parent[i].Value + "\"\r\n";
					}
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