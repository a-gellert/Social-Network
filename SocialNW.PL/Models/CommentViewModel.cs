using System;

namespace SocialNW.PL.Models
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostId { get; set; }
        public int AppUserId { get; set; }
    }
}