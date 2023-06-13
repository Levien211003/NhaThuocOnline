using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProTechTiveGear.Models
{
	public class ItemEntity
	{
		public long ID { get; set; }
		public string Name { get; set; }
		public Nullable<Decimal> PurchartPrices { get; set; }
		public Nullable<Decimal> Sellprice { get; set; }
		public DateTime ImportDate { get; set; }
		public int Quantity { get; set; }
		public  string Picture { get; set; }
		public string Status { get; set; }
		public string Describe { get; set; }
		public virtual ICollection<ItemType> ItemTypes { get; set; }

		public virtual ICollection<Brand> Brands { get; set; }
		public Item TypeOf_Item()
		{
			Item item = new Item();
			PropertyInfo[] pithis = typeof(ItemEntity).GetProperties();
			PropertyInfo[] pieClinet = typeof(Item).GetProperties();
			foreach (var items in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == items.Name)
					{
						itempiem.SetValue(item, items.GetValue(this));
						break;
					}
				}
			}
			return item;
		}

		// convert tu model sang view

		public void TypeOf_ItemEntity(Item item)
		{

			PropertyInfo[] pithis = typeof(ItemEntity).GetProperties();
			PropertyInfo[] pieClinet = typeof(Item).GetProperties();
			foreach (var items in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == items.Name)
					{
						items.SetValue(this, itempiem.GetValue(item));
						break;
					}
				}
			}

		}
		//
		public ItemEntity(Item item)
		{
			TypeOf_ItemEntity(item);

		}
		public ItemEntity()
		{


		}
	}
}