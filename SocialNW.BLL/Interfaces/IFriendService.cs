using SocialNW.BLL.DTO;
using System.Collections.Generic;

namespace SocialNW.BLL.Interfaces
{
    public interface IFriendService
    {
        IEnumerable<FriendRequestDTO> GetRequestsById(int id);
        void Create(int userId, int friendUserId);
        void Reject(int id);
        void Accept(int id);
        int HasWaitingRequest(int id);

        void Dispose();
    }
}
