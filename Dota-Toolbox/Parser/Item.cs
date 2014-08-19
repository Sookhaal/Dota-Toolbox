using KVLib;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dota_Toolbox.Parser
{
	public class Item
	{
		private KeyValue _item;
		private string _name;
		private int _id;

		//General
		private string[] _abilityBehavior;
		private string _abilityUnitTargetTeam;
		private string _abilityUnitTargetType;
		private string _model;
		private string _baseClass;
		private string _abilityTextureName;
		private bool _itemKillable;

		//Stats
		private int _abilityCastRange;
		private float _abilityCastPoint;

		//Item Info
		private int _itemCost;
		private string _itemShopTags;
		private string _itemQuality;
		private bool _itemStackable;
		private string _itemShareability;
		private bool _itemPermanent;
		private int _itemInitialCharges;
		private bool _sideShop;

		public KeyValue item
		{
			get { return _item; }
			set { _item = value; }
		}
		public string name
		{
			get { return _name; }
			set { _name = value; }
		}
		public int id
		{
			get { return _id; }
			set { _id = value; }
		}

		public string[] abilityBehavior
		{
			get { return _abilityBehavior; }
			set { _abilityBehavior = value; }
		}
		public string abilityUnitTargetTeam
		{
			get { return _abilityUnitTargetTeam; }
			set { _abilityUnitTargetTeam = value; }
		}

		public string abilityUnitTargetType
		{
			get { return _abilityUnitTargetType; }
			set { _abilityUnitTargetType = value; }
		}
		public string model
		{
			get { return _model; }
			set { _model = value; }
		}
		public string baseClass
		{
			get { return _baseClass; }
			set { _baseClass = value; }
		}
		public string abilityTextureName
		{
			get { return _abilityTextureName; }
			set { _abilityTextureName = value; }
		}
		public bool itemKillable
		{
			get { return _itemKillable; }
			set { _itemKillable = value; }
		}

		public int abilityCastRange
		{
			get { return _abilityCastRange; }
			set { _abilityCastRange = value; }
		}
		public float abilityCastPoint
		{
			get { return _abilityCastPoint; }
			set { _abilityCastPoint = value; }
		}

		public int itemCost
		{
			get { return _itemCost; }
			set { _itemCost = value; }
		}
		public string itemShopTags
		{
			get { return _itemShopTags; }
			set { _itemShopTags = value; }
		}
		public string itemQuality
		{
			get { return _itemQuality; }
			set { _itemQuality = value; }
		}
		public bool itemStackable
		{
			get { return _itemStackable; }
			set { _itemStackable = value; }
		}
		public string itemShareability
		{
			get { return _itemShareability; }
			set { _itemShareability = value; }
		}
		public bool itemPermanent
		{
			get { return _itemPermanent; }
			set { _itemPermanent = value; }
		}
		public int itemInitialCharges
		{
			get { return _itemInitialCharges; }
			set { _itemInitialCharges = value; }
		}
		public bool sideShop
		{
			get { return _sideShop; }
			set { _sideShop = value; }
		}




		public Item() { }
		public Item(KeyValue item)
		{
			this.item = item;
			this.name = item.Key;
			this.id = FindInt("ID");

			//General
			this.abilityBehavior = FindStringArray("AbilityBehavior");
			this.abilityUnitTargetTeam = FindString("AbilityUnitTargetTeam");
			this.abilityUnitTargetType = FindString("AbilityUnitTargetType");
			this.model = FindString("Model");
			this.baseClass = FindString("BaseClass");
			this.abilityTextureName = FindString("AbilityTextureName");
			this.itemKillable = FindBool("ItemKillable");

			//Stats
			this._abilityCastRange = FindInt("AbilityCastRange");
			this._abilityCastPoint = FindFloat("AbilityCastPoint");

			//Item Info
			this.itemCost = FindInt("ItemCost");
			this.itemShopTags = FindString("ItemShopTags");
			this.itemQuality = FindString("ItemQuality");
			this.itemStackable = FindBool("ItemStackable");
			this.itemShareability = FindString("ItemShareability");
			this.itemPermanent = FindBool("ItemPermanent");
			this.itemInitialCharges = FindInt("ItemInitialCharges");
			this.sideShop = FindBool("SideShop");
		}

		#region Methods
		public string FindString(string key)
		{
			for (int i = 0; i < item.children.Count; i++)
				if (String.Equals(key, item.children[i].Key))
					return item.children[i].Value;
			return "";
		}

		public int FindInt(string key)
		{
			for (int i = 0; i < item.children.Count; i++)
				if (String.Equals(key, item.children[i].Key))
					return Convert.ToInt16(item.children[i].Value);
			return -1;
		}

		public float FindFloat(string key)
		{
			for (int i = 0; i < item.children.Count; i++)
				if (String.Equals(key, item.children[i].Key))
					return float.Parse(item.children[i].Value, CultureInfo.InvariantCulture.NumberFormat);
			return -1;
		}

		public bool FindBool(string key)
		{
			for (int i = 0; i < item.children.Count; i++)
				if (String.Equals(key, item.children[i].Key))
					return Convert.ToBoolean(Convert.ToInt16(item.children[i].Value));
			return false;
		}

		public string[] FindStringArray(string key)
		{
			string[] split = FindString(key).Split(new Char[] { '|' });
			for (int i = 0; i < split.Length; i++)
			{
				split[i] = Regex.Replace(split[i], " ", "");
			}
			return split;
		}
		#endregion
	}
}
