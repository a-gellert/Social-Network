using System;
using System.ComponentModel.DataAnnotations;

namespace SocialNW.PL.Models
{
    public class MessageViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "You can't send empty message")]
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public int? AppUserId { get; set; }
        public int? RecipientId { get; set; }
        public bool IsReaded { get; set; }

        public virtual ProfileViewModel Profile { get; set; }
        public virtual ProfileViewModel Recipient { get; set; }

        public MessageViewModel()
        {
            Date = DateTime.Now;
            IsReaded = false;
        }
    }
}