using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProTechTiveGear.Models
{
	public class OrderDetailEntity
	{
		public long ID { get; set; }
		public int Quantity { get; set; }
		public Nullable<long> ItemId { get; set; }
		public Nullable<long> OrderID { get; set; }
		public Nullable<decimal> Totalprice { get; set; }

		public virtual Item Item { get; set; }
		public virtual Order Order { get; set; }
		public OrderDetail TypeOf_Order()
		{
			OrderDetail order = new OrderDetail();
			PropertyInfo[] pithis = typeof(OrderDetailEntity).GetProperties();
			PropertyInfo[] pieClinet = typeof(OrderDetail).GetProperties();
			foreach (var items in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == items.Name)
					{
						itempiem.SetValue(order, items.GetValue(this));
						break;
					}
				}
			}
			return order;
		}

		// convert tu model sang view

		public void TypeOf_OrderEntity(OrderDetail order)
		{

			PropertyInfo[] pithis = typeof(OrderDetailEntity).GetProperties();
			PropertyInfo[] pieClinet = typeof(OrderDetail).GetProperties();
			foreach (var items in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == items.Name)
					{
						items.SetValue(this, itempiem.GetValue(order));
						break;
					}
				}
			}

		}
		//
		public OrderDetailEntity(OrderDetail order)
		{
			TypeOf_OrderEntity(order);

		}
		public OrderDetailEntity()
		{


		}
	}
}