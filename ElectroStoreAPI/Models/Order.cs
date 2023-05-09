using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class Order
    {

        public int? IdOrder { get; set; }
        public string? NumOrder { get; set; } = null!;
        public int? EmployeeAddressesId { get; set; }
        public int? ClientId { get; set; }
        public int? ClientPromocodeId { get; set; }

    }
}
