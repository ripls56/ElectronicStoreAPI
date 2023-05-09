using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class StoreAddress
    {

        public int? IdStoreAddresses { get; set; }
        public string? StoreAddres { get; set; } = null!;
        public string? Postcode { get; set; } = null!;

    }
}
