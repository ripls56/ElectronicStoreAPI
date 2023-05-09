using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class Promocode
    {

        public int? IdPromocode { get; set; }
        public string? ValuePromocode { get; set; } = null!;
        public DateTime? DateOfCompletionPromocode { get; set; }
        public DateTime? DateOfCreationPromocode { get; set; }
        public decimal? RequiredAmountPromocode { get; set; }
        public int? DiscountValuePromocode { get; set; }
    }
}
