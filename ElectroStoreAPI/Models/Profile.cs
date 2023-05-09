using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class Profile
    {
        public int? IdProfile { get; set; }
        public string? NameProfile { get; set; } = null!;
        public string? SurnameProfile { get; set; } = null!;
        public string? MiddleNameProfile { get; set; }
        public int? ClientId { get; set; }

    }
}
