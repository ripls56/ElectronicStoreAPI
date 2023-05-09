using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class Nomenclature
    {

        public int? IdNomenclature { get; set; }
        public string? NameNomenclature { get; set; } = null!;
        public decimal? UnitСostNomenclature { get; set; }
        public string? DescriptionNomenclature { get; set; } = null!;
        public int? SuppliesId { get; set; }
        public int? ProductСategoriesId { get; set; }
        public int? BrandsId { get; set; }

    }
}
