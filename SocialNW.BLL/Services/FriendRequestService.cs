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
    public class FriendRequestService : IFriendService
    {
        private readonly IUnitOfWork _unitOfWork;

        public FriendRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Accept(int id)
        {
            var request = _unitOfWork.Requests.GetAll().FirstOrDefault(x => x.Id == id);
            if (request == null)
                throw new ValidationException("request doesn't exist", "");
            var userFrom = _unitOfWork.UserManager.FindById(request.AppUserId);
            var userTo = _unitOfWork.UserManager.FindById(request.RequestedTo);

            var userProfileFrom = _unitOfWork.Profiles.GetById(request.AppUserId);
            var userProfileTo = _unitOfWork.Profiles.GetById(request.RequestedTo);

            userFrom.Friends.Add(userProfileFrom);
            userTo.Friends.Add(userProfileTo);
            request.IsAccepted = true;
            _unitOfWork.Save();
        }

        public void Create(int userId, int friendUserId)
        {
            var requests = _unitOfWork.Requests.GetAll().Where(x => x.AppUserId == userId && x.RequestedTo == friendUserId).ToList();
            if (requests.Count == 0 && userId != friendUserId)
            {
                try
                {
                    var request = new FriendRequest()
                    {
                        AppUserId = userId,
                        RequestedTo = friendUserId,
                        Date = DateTime.Now
                    };
                    _unitOfWork.Requests.Create(request);
                    _unitOfWork.Save();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
           
        }

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }

        public IEnumerable<FriendRequestDTO> GetRequestsById(int id)
        {

            var requests = _unitOfWork.Requests.GetAll().Where(x => x.RequestedTo == id && x.IsAccepted == false).ToList();

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<FriendRequest, FriendRequestDTO>()));

            return mapper.Map<IEnumerable<FriendRequest>, List < FriendRequestDTO >> (requests); ;
            
        }

        public int HasWaitingRequest(int id)
        {
            var requests = _unitOfWork.Requests.GetAll().Where(x => x.RequestedTo == id && x.IsAccepted == false).ToList();

            return requests.Count;

        }

        public void Reject(int id)
        {
            try
            {
                _unitOfWork.Requests.Delete(id);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
