using KVLib;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Dota_Toolbox.Parser
{
	internal class KVDocument
	{
		public bool emptyFile;
		private ObservableCollection<KeyValue> tempList = new ObservableCollection<KeyValue>();
		private string a;

		public ObservableCollection<KeyValue> ReadKeyValuesFromFile(string path, string rootKey)
		{
			a = "";
			if (!File.Exists(path))
			{
				File.Create(path).Close();
				emptyFile = true;
			}
			a = File.ReadAllText(path);
			KeyValue kv = KVParser.ParseKeyValueText(a);
			Console.WriteLine(kv.Key);
			if (kv.Key != rootKey)
				Utils.CreateNewFile(path, rootKey);
			foreach (KeyValue data in GetChildren(kv))
				tempList.Add((KeyValue)data);
			return tempList;
		}

		public IEnumerable<object> GetChildren(object parent)
		{
			var dataParent = (KeyValue)parent;
			if (dataParent == null)
				yield break;

			if (!dataParent.HasChildren)
				yield break;

			foreach (var data in dataParent.Children)
				yield return data;
		}
	}
}