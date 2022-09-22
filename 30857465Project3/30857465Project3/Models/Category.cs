using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace _30857465Project3.Models
{
    public partial class Category
    {
        public Category()
        {
            Device = new HashSet<Device>();
        }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public DateTime DateCreated { get; set; }

        public virtual ICollection<Device> Device { get; set; }
    }
}
