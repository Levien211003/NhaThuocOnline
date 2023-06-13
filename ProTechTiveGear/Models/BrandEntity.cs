using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProTechTiveGear.Models
{
	public class BrandEntity
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public BrandEntity()
		{
			this.Items = new HashSet<Item>();
		}

		[Key]
		public long ID { get; set; }
	

		[Required(ErrorMessage = "Password not null !")]
		public string Name { get; set; }
		public Nullable<long> MenuID { get; set; }

		public virtual Menu Menu { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Item> Items { get; set; }
	}
}