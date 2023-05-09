namespace ElectroStoreAPI.Models
{
    public partial class Client
    {
        public int? IdClient { get; set; }
        public string? LoginClient { get; set; } = null!;
        public string? PasswordClient { get; set; } = null!;
        public string? SaltClient { get; set; } = null!;
        public string? PhoneClient { get; set; }
        public string? EmailClient { get; set; } = null!;
        public int? LoyaltyCardId { get; set; }
    }
}
