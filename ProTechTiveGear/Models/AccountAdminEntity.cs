using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProTechTiveGear.Models
{
	public class AccountAdminEntity
	{
		[Required(ErrorMessage = "Username not null !")]
		[DisplayName("UserName")]
		public string userName { get; set; }
		[DisplayName("PassWord")]
		[DataType(DataType.Password)]
		[Required(ErrorMessage = "Password not null !")]
		public string passWord { get; set; }
	}
}