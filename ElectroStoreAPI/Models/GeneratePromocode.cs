using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class GeneratePromocode
    {
        public string? PromocodeValue { get; set; }
        public DateTime? DateOfCreation { get; set; }
        public DateTime? DateOfCompletion { get; set; }
        public double? RequiredAmount { get; set; }
        public double? DiscountValue { get; set; }
    }
}
