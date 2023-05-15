namespace ElectroStoreAPI.Models
{
    public partial class Nomenclature : IEquatable<Nomenclature>
    {
        public int? IdNomenclature { get; set; }
        public string? NameNomenclature { get; set; } = null!;
        public decimal? UnitСostNomenclature { get; set; }
        public string? DescriptionNomenclature { get; set; } = null!;
        public int? SuppliesId { get; set; }
        public int? ProductСategoriesId { get; set; }
        public bool? IsDelete { get; set; }
        public int? BrandsId { get; set; }

        public bool Equals(Nomenclature? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IdNomenclature == other.IdNomenclature && NameNomenclature == other.NameNomenclature && UnitСostNomenclature == other.UnitСostNomenclature && DescriptionNomenclature == other.DescriptionNomenclature && SuppliesId == other.SuppliesId && ProductСategoriesId == other.ProductСategoriesId && IsDelete == other.IsDelete && BrandsId == other.BrandsId;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Nomenclature)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdNomenclature, NameNomenclature, UnitСostNomenclature, DescriptionNomenclature, SuppliesId, ProductСategoriesId, IsDelete, BrandsId);
        }
    }
}
