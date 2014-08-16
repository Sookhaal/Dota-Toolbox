namespace Dota_Toolbox.Parser
{
	public class DotaData
	{
		public class Hero
		{
			private string _name;
			private bool _enable;

			public string name
			{
				get { return _name; }
				set { _name = value; }
			}

			public bool enable
			{
				get { return _enable; }
				set { _enable = value; }
			}
		}
	}
}