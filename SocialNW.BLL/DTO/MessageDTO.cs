using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNW.BLL.DTO
{
    public class MessageDTO
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime? Date { get; set; }
        public int? AppUserId { get; set; }
        public int? RecipientId { get; set; }
        public bool IsReaded { get; set; }
    }
}
