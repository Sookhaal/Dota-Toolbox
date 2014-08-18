using KVLib;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Dota_Toolbox.Parser
{
	internal class KVDocument
	{
		public KeyValue[] testText;
		private ObservableCollection<KeyValue> tempList = new ObservableCollection<KeyValue>();
		private string a, b;

		public ObservableCollection<KeyValue> ReadKeyValuesFromFile(string path)
		{
			a = "";
			if (!File.Exists(path))
				return tempList;
			a = File.ReadAllText(path);
			KeyValue kv = KVParser.ParseKeyValueText(a);
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