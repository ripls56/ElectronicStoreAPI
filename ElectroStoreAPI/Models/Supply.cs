using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class Supply
    {

        public int? IdSupplies { get; set; }
        public string? NameSupplies { get; set; } = null!;
        public string? EmailSupplies { get; set; } = null!;
        public string? PhoneSupplies { get; set; } = null!;
        public string? Inn { get; set; } = null!;
        public int? VendorTypeId { get; set; }

    }
}
