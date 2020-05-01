using SocialNW.BLL.DTO;
using System.Collections.Generic;

namespace SocialNW.BLL.Interfaces
{
    public interface IProfileService
    {
        UProfileDTO GetById(int id);
        void Update(UProfileDTO profileDto); IEnumerable<UProfileDTO> GetAll();
        IEnumerable<UProfileDTO> Search(string searchString, string city, string country);
        IEnumerable<UProfileDTO> GetFriends(int id);
    }
}
