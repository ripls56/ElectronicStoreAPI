using System;
using System.Collections.Generic;

namespace ElectroStoreAPI.Models
{
    public partial class Feedback
    {
        public int? IdFeedback { get; set; }
        public int? MarkFeedback { get; set; }
        public string? CommentFeedback { get; set; } = null!;
        public DateTime? PublicationDateFeedback { get; set; }
        public int? OrderId { get; set; }
    }
}
