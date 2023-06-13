using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProTechTiveGear.Models
{
	public class CartEntity
	{
		ProTechTiveGearEntities data = new ProTechTiveGearEntities(); 
		public long IdItem { set; get; }
		public string Name { set; get; }
		public string Picture { set; get; }
		public Double Prices { set; get; }
		public int Quantity { set; get; }
		public Double TotalPrices
		{
			get { return Quantity * Prices; }

		}
		//Khoi tao gio hàng theo Masach duoc truyen vao voi Soluong mac dinh la 1
		public CartEntity(long id)
		{
			IdItem =  id;
			Item product = data.Items.Single(n => n.ID == IdItem);
			Name = product.Name;
			Picture = product.Picture;
			Prices = Double.Parse(product.SellPrice.ToString());
			Quantity = 1;
		}
	}
}