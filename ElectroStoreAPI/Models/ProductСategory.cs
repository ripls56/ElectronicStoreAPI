using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class ProductСategory
    {

        public int? IdProductСategories { get; set; }
        public string? NameProductСategories { get; set; } = null!;
        public string? DescriptionProductСategories { get; set; } = null!;
    }
}
