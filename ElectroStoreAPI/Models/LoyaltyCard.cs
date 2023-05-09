using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class LoyaltyCard
    {
        public int? IdLoyaltyCard { get; set; }
        public string? NameLoyaltyCard { get; set; } = null!;
        public int? DiscountValueLoyaltyCard { get; set; }

    }
}
