using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNW.DAL.Entities
{
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }
        public DateTime? Date { get; set; }

        public int? ApplicationUserId { get; set; }
        public virtual AppUser ApplicationUser { get; set; }
        public virtual AppUser Recipient { get; set; }
        public bool IsReaded { get; set; }

        public Message()
        {
            Date = DateTime.Now;
            IsReaded = false;
        }
    }
}
