using System;
namespace SocialNW.PL.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public int? AppUserId { get; set; }
        public virtual ProfileViewModel Profile { get; set; }
    }
}