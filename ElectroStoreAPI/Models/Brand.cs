namespace ElectroStoreAPI.Models
{
    public partial class Brand : IEquatable<Brand>
    {
        public int? IdBrands { get; set; }
        public string? NameBrands { get; set; } = null!;

        public bool Equals(Brand? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return IdBrands == other.IdBrands && NameBrands == other.NameBrands;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Brand)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(IdBrands, NameBrands);
        }
    }
}
