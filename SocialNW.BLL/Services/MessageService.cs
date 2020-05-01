using AutoMapper;
using Microsoft.AspNet.Identity;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Infrastructure;
using SocialNW.BLL.Interfaces;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNW.BLL.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int CountUnreadedMessages(int id)
        {
            return _unitOfWork.Messages.GetAll().Where(x => x.Recipient.Id == id && x.IsReaded == false).ToList().Count;
        }
        
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<MessageDTO> GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Message, MessageDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<MessageDTO>>(_unitOfWork.Messages.GetAll()); ;
        }

        public MessageDTO GetById(int id)
        {
            var message = _unitOfWork.Messages.GetById(id);
            if (message == null)
                throw new ValidationException("Message doesn't exist", "");
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Message, MessageDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<MessageDTO>(_unitOfWork.Messages.GetAll()); ;

        }

        public IEnumerable<MessageDTO> GetUserMessages(int id)
        {
            var messages = _unitOfWork.Messages.GetAll().Where(x => x.ApplicationUserId == id || x.Recipient.Id == id)
               .OrderByDescending(m => m.Date).ToList();
            MarkMessagesAsReaded(id);

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Message, MessageDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<List<Message>, List<MessageDTO>>(messages);
        }

        public void Remove(int id)
        {
            try
            {
                _unitOfWork.Messages.Delete(id);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SendMessage(MessageDTO messageDto, int fromId, int toId)
        {
            try
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<MessageDTO, Message>());
                var mapper = new Mapper(config);
                var message = mapper.Map<MessageDTO, Message>(messageDto);

                message.ApplicationUser = _unitOfWork.UserManager.FindById(fromId);
                var userTo = _unitOfWork.UserManager.FindById(toId);
                message.Recipient = userTo;
                _unitOfWork.Messages.Create(message);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(int id, MessageDTO newMessage)
        {
            var message = _unitOfWork.Messages.GetById(id);
            if (message == null)
                throw new ValidationException("Message doesn't exist", "");

            var config = new MapperConfiguration(cfg => cfg.CreateMap<MessageDTO, Message>());
            var mapper = new Mapper(config);
            var upMessage = mapper.Map<MessageDTO, Message>(newMessage);

            _unitOfWork.Messages.Update(upMessage);
        }


        private void MarkMessagesAsReaded(int id)
        {
            var messages = _unitOfWork.Messages.GetAll().Where(x => x.Recipient.Id == id && x.IsReaded == false).ToList();
            foreach (var message in messages)
            {
                message.IsReaded = true;
                _unitOfWork.Messages.Update(message);
            }
            _unitOfWork.Save();
        }
    }
}
