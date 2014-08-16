using KVLib;
using System.Collections.Generic;
using System.IO;

namespace Dota_Toolbox.Parser
{
	internal class KVDocument
	{
		private List<KeyValue> tempList = new List<KeyValue>();
		private string text;

		public List<KeyValue> ReadKeyValuesFromFile(string path)
		{
			text = "";
			if (!File.Exists(path))
				return tempList;
			text = File.ReadAllText(path);
			KeyValue kv = KVParser.ParseKeyValueText(text);
			foreach (var data in GetChildren(kv))
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