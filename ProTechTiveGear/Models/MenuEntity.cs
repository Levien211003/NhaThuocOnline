using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProTechTiveGear.Models
{
	public class MenuEntity
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public MenuEntity()
		{
			this.Brands = new HashSet<Brand>();
			this.ItemTypes = new HashSet<ItemType>();
		}

		public long ID { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<Brand> Brands { get; set; }
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<ItemType> ItemTypes { get; set; }
	}
}