using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class Employee
    {

        public int? IdEmployee { get; set; }
        public string? LoginEmployee { get; set; } = null!;
        public string? PasswordEmployee { get; set; } = null!;
        public string? SurnameEmployee { get; set; } = null!;
        public string? NameEmployee { get; set; } = null!;
        public string? MiddleNameEmployee { get; set; }
        public string? SaltEmployee { get; set; } = null!;
        public bool? IsDelete { get; set; }
        public int? PostId { get; set; }
    }
}
