using System;
namespace SocialNW.BLL.DTO
{
    public class FriendRequestDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }public int RequestedTo { get; set; }
        public DateTime? Date { get; set; }
        public bool IsAccepted { get; set; }
    }
}
