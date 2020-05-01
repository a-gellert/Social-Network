using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNW.DAL.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Комментарий не может быть пустым")]
        public string Text { get; set; }
        public DateTime CommentDate { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int ApplicationUserId { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
