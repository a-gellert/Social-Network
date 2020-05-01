using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNW.DAL.Entities
{
    public class FriendRequest
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        public int RequestedTo { get; set; }
        public DateTime? Date { get; set; }
        public bool IsAccepted { get; set; }

        public FriendRequest()
        {
            Date = DateTime.Now;
            IsAccepted = false;
        }
    }
}
