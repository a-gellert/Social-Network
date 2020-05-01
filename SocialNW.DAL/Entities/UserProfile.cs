﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNW.DAL.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("AppUser")]
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public Gender? Gender { get; set; }
        public string Phone { get; set; }
        public byte[] UserPhoto { get; set; }
        public virtual AppUser AppUser { get; set; }

    }
}
