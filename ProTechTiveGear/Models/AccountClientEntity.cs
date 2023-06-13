using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ProTechTiveGear.Models
{
	public class AccountClientEntity
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		
		public long ID { get; set; }
		[Required(ErrorMessage = "Username not null !")]
		[DisplayName("UserName")]
		public string userName { get; set; }
		[DisplayName("PassWord")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password not null !")]
		public string passWord { get; set; }
		[Required(ErrorMessage = "Address not null !")]
		public string Address { get; set; }
		[Required(ErrorMessage = "Name not null !")]
		public string Name { get; set; }
		[Required(ErrorMessage = "PhoneNumber not null !")]
		public string PhoneNumber { get; set; }
		//--------------------
		//public Customer TypeOf_Customer()
		//{
		//	Customer customer = new Customer();
		//	PropertyInfo[] pithis = typeof(AccountClientEntity).GetProperties();
		//	PropertyInfo[] pieClinet = typeof(Customer).GetProperties();
		//	foreach (var item in pithis)
		//	{
		//		foreach (var itempiem in pieClinet)
		//		{
		//			if (itempiem.Name == item.Name)
		//			{
		//				itempiem.SetValue(customer, item.GetValue(this));
		//				break;
		//			}
		//		}
		//	}
		//	return customer;
		//}

		// convert tu model sang view

		public void TypeOf_AccountClientEntity(Customer customer)
		{

			PropertyInfo[] pithis = typeof(AccountClientEntity).GetProperties();
			PropertyInfo[] pieClinet = typeof(Customer).GetProperties();
			foreach (var item in pithis)
			{
				foreach (var itempiem in pieClinet)
				{
					if (itempiem.Name == item.Name)
					{
						item.SetValue(this, itempiem.GetValue(customer));
						break;
					}
				}
			}

		}
		//
		public AccountClientEntity(Customer customer)
		{
			TypeOf_AccountClientEntity(customer);

		}
	
	}
}