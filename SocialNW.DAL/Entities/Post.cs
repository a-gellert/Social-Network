using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNW.DAL.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required(ErrorMessage = "Пост не может быть пустым")]
        public string Text { get; set; }
        public DateTime? Date { get; set; }

        public int? AppUserId { get; set; }
        public virtual AppUser ApplicationUser { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}
