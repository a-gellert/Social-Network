using System;
namespace SocialNW.BLL.DTO
{
    public class CommentDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }
        public int PostId { get; set; }
        public int AppUserId { get; set; }  

        public CommentDTO()
        {
            CommentDate = DateTime.Now;
        }
    }
}
