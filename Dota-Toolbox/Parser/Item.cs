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
		private string _id;

		//General
		private string[] _abilityBehavior;
		private string[] _abilityUnitTargetFlags;
		private string _abilityUnitTargetTeam;
		private string[] _abilityUnitTargetType;
		private string _model;
		private string _baseClass;
		private string _abilityTextureName;
		private bool _itemKillable;
		private string _abilityCastAnimation;

		//Stats
		private string _abilityCastRange;
		private string _abilityCastPoint;
		private string _abilityCooldown;
		private string _abilityChannelTime;
		private string _abilityManaCost;

		//Item Info
		private string _itemCost;
		private string[] _itemShopTags;
		private string _itemQuality;
		private bool _itemStackable;
		private string _itemShareability;
		private bool _itemPermanent;
		private string _itemInitialCharges;
		private bool _sideShop;

		//Misc
		private string _itemStockInitial;
		private string _itemStockMax;
		private string _itemStockTime;
		private bool _itemSupport;
		private string[] _itemDeclarations;

		//Spell
		private KeyValue _abilitySpecials;

		#region Get/Set
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
		public string id
		{
			get { return _id; }
			set { _id = value; }
		}

		#region General
		//General
		public string[] abilityBehavior
		{
			get { return _abilityBehavior; }
			set { _abilityBehavior = value; }
		}
		public string[] abilityUnitTargetFlags
		{
			get { return _abilityUnitTargetFlags; }
			set { _abilityUnitTargetFlags = value; }
		}
		public string abilityUnitTargetTeam
		{
			get { return _abilityUnitTargetTeam; }
			set { _abilityUnitTargetTeam = value; }
		}
		public string[] abilityUnitTargetType
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
		public string abilityCastAnimation
		{
			get { return _abilityCastAnimation; }
			set { _abilityCastAnimation = value; }
		}
		#endregion
		#region Stats
		//Stats
		public string abilityCastRange
		{
			get { return _abilityCastRange; }
			set { _abilityCastRange = value; }
		}
		public string abilityCastPoint
		{
			get { return _abilityCastPoint; }
			set { _abilityCastPoint = value; }
		}
		public string abilityCooldown
		{
			get { return _abilityCooldown; }
			set { _abilityCooldown = value; }
		}
		public string abilityChannelTime
		{
			get { return _abilityChannelTime; }
			set { _abilityChannelTime = value; }
		}
		public string abilityManaCost
		{
			get { return _abilityManaCost; }
			set { _abilityManaCost = value; }
		}
		#endregion
		#region Item Info
		//Item Info
		public string itemCost
		{
			get { return _itemCost; }
			set { _itemCost = value; }
		}
		public string[] itemShopTags
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
		public string itemInitialCharges
		{
			get { return _itemInitialCharges; }
			set { _itemInitialCharges = value; }
		}
		public bool sideShop
		{
			get { return _sideShop; }
			set { _sideShop = value; }
		}
		#endregion
		#region Misc
		//Misc
		public string itemStockInitial
		{
			get { return _itemStockInitial; }
			set { _itemStockInitial = value; }
		}
		public string itemStockMax
		{
			get { return _itemStockMax; }
			set { _itemStockMax = value; }
		}
		public string itemStockTime
		{
			get { return _itemStockTime; }
			set { _itemStockTime = value; }
		}
		public bool itemSupport
		{
			get { return _itemSupport; }
			set { _itemSupport = value; }
		}
		public string[] itemDeclarations
		{
			get { return _itemDeclarations; }
			set { _itemDeclarations = value; }
		}
		#endregion
		#region Spell
		public KeyValue abilitySpecials
		{
			get { return _abilitySpecials; }
			set { _abilitySpecials = value; }
		}
		#endregion
		#endregion

		public Item() { }
		public Item(KeyValue item)
		{
			this.item = item;
			this.name = item.Key;
			this.id = GetString("ID");

			//General
			this.abilityBehavior = GetStringArray("AbilityBehavior");
			this.abilityUnitTargetFlags = GetStringArray("AbilityUnitTargetFlags");
			this.abilityUnitTargetTeam = GetString("AbilityUnitTargetTeam");
			this.abilityUnitTargetType = GetStringArray("AbilityUnitTargetType");
			this.model = GetString("Model");
			this.baseClass = GetString("BaseClass");
			this.abilityTextureName = GetString("AbilityTextureName");
			this.itemKillable = GetBool("ItemKillable");
			this.abilityCastAnimation = GetString("AbilityCastAnimation");

			//Stats
			this.abilityCastRange = GetString("AbilityCastRange");
			this.abilityCastPoint = GetString("AbilityCastPoint");
			this.abilityCooldown = GetString("AbilityCooldown");
			this.abilityChannelTime = GetString("AbilityChannelTime");
			this.abilityManaCost = GetString("AbilityManaCost");

			//Item Info
			this.itemCost = GetString("ItemCost");
			this.itemShopTags = GetStringArraySemi("ItemShopTags");
			this.itemQuality = GetString("ItemQuality");
			this.itemStackable = GetBool("ItemStackable");
			this.itemShareability = GetString("ItemShareability");
			this.itemPermanent = GetBool("ItemPermanent");
			this.itemInitialCharges = GetString("ItemInitialCharges");
			this.sideShop = GetBool("SideShop");

			//Misc
			this.itemStockInitial = GetString("ItemStockInitial");
			this.itemStockMax = GetString("ItemStockMax");
			this.itemStockTime = GetString("itemStockTime");
			this.itemSupport = GetBool("ItemSupport");
			this.itemDeclarations = GetStringArray("ItemDeclarations");

			//Spell
			this.abilitySpecials = GetKV("AbilitySpecial");
		}

		#region Methods
		public string GetString(string key)
		{
			for (int i = 0; i < item.children.Count; i++)
				if (String.Equals(key, item.children[i].Key))
					return item.children[i].Value;
			return "";
		}
		public bool GetBool(string key)
		{
			for (int i = 0; i < item.children.Count; i++)
				if (String.Equals(key, item.children[i].Key))
					return Convert.ToBoolean(Convert.ToInt16(item.children[i].Value));
			return false;
		}
		public string[] GetStringArray(string key)
		{
			string[] split = GetString(key).Split(new Char[] { '|' });
			for (int i = 0; i < split.Length; i++)
			{
				split[i] = Regex.Replace(split[i], " ", "");
			}
			return split;
		}
		public string[] GetStringArraySemi(string key)
		{
			string[] split = GetString(key).Split(new Char[] { ';' });
			return split;
		}
		public KeyValue GetKV(string key)
		{
			for (int i = 0; i < item.children.Count; i++)
				if (String.Equals(key, item.children[i].Key))
					return item.children[i];
			return new KeyValue("");
		}


		public void SetString(string key, string value)
		{
			if (value != "")
			{
				for (int i = 0; i < item.children.Count; i++)
					if (String.Equals(key, item.children[i].Key))
					{
						item.children[i].Value = value;
						return;
					}
				KeyValue newKV = new KeyValue(key, value);
				item.AddChild(newKV);
			}
			else
			{
				for (int i = 0; i < item.children.Count; i++)
					if (String.Equals(key, item.children[i].Key))
					{
						Console.WriteLine("Removed " + key);
						item.children.RemoveAt(i);
					}
			}
		}
		public void SetBool(string key, bool value)
		{
			SetString(key, Convert.ToInt16(value).ToString());
		}
		public void SetStringArray(string key, string[] value)
		{
			SetString(key, String.Join(" | ", value));
		}
		public void SetStringArraySemi(string key, string[] value)
		{
			SetString(key, String.Join(";", value));
		}
		public void SetKV(KeyValue kv)
		{
			kv.TabIndex = item.Parent.TabIndex + 2;
			for (int i = 0; i < item.children.Count; i++)
				if (String.Equals(kv.Key, item.children[i].Key))
				{
					item.children[i] = kv;
					return;
				}

			if (kv.Key != "" && kv.HasChildren)
				item.AddChild(kv);
		}
		#endregion
	}
}
