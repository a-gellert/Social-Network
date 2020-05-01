using SocialNW.DAL.Entities;
using System;

namespace SocialNW.BLL.DTO
{
    public class UProfileDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Gender? Gender { get; set; }
        public string Phone { get; set; }
        public byte[] UserPhoto { get; set; }
    }
}
