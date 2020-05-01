using SocialNW.BLL.DTO;
using System.Collections.Generic;

namespace SocialNW.BLL.Interfaces
{
    public interface IMessageService
    {
        IEnumerable<MessageDTO> GetAll();
        IEnumerable<MessageDTO> GetUserMessages(int id);
        MessageDTO GetById(int id);
        void Update(int id, MessageDTO newMessage);
        void Remove(int id);
        void SendMessage(MessageDTO messageDto, int fromId, int toId);
        int CountUnreadedMessages(int id);

        void Dispose();
    }
}
