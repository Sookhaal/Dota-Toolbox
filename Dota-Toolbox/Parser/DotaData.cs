namespace Dota_Toolbox.Parser
{
	public class DotaData
	{
		public class KeyValue
		{
			private string _key;
			private string _value;

			public string key
			{
				get { return _key; }
				set { _key = value; }
			}

			public string value
			{
				get { return _value; }
				set { _value = value; }
			}
		}
	}
}