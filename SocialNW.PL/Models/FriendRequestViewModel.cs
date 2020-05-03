using System;

namespace SocialNW.PL.Models
{
    public class FriendRequestViewModel
    {
        public int Id { get; set; }
        public int ApplicationUserId { get; set; }
        public int RequestedTo { get; set; }
        public DateTime? Date { get; set; }
        public bool IsAccepted { get; set; }
        public virtual ProfileViewModel Profile { get; set; }
    }
}