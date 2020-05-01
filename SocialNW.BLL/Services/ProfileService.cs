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
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<UProfileDTO> GetAll()
        {
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UserProfile, UProfileDTO>()));

            return mapper.Map<List<UserProfile>, List<UProfileDTO>>(_unitOfWork.Profiles.GetAll().ToList());
        }

        public UProfileDTO GetById(int id)
        {
            var profile = _unitOfWork.Profiles.GetById(id);
            if (profile == null)
                throw new ValidationException("Profile doesn't exist", "");
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UserProfile, UProfileDTO>()));
            
            return mapper.Map<UserProfile, UProfileDTO>(profile);
        }

        public IEnumerable<UProfileDTO> GetFriends(int id)
        {
            var currentUser = _unitOfWork.UserManager.FindById(id);
            var fList = currentUser.Friends.ToList();
            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UserProfile, UProfileDTO>()));

            return mapper.Map<List<UserProfile>, List<UProfileDTO>>(fList);
        }

        public IEnumerable<UProfileDTO> Search(string searchString, string city, string country)
        {
            var users = _unitOfWork.Profiles.GetAll().ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.FirstName.Contains(searchString)).ToList();
            }

            if (!string.IsNullOrEmpty(city))
            {
                users = users.Where(x => x.City != null && x.City.Contains(city)).ToList();
            }

            if (!string.IsNullOrEmpty(country))
            {
                users = users.Where(x => x.Country != null && x.Country.Contains(country)).ToList();
            }

            var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UserProfile, UProfileDTO>()));

            return mapper.Map<List<UserProfile>, List<UProfileDTO>>(users);
        }

        public void Update(UProfileDTO profileDto)
        {
            try
            {
                var mapper = new Mapper(new MapperConfiguration(cfg => cfg.CreateMap<UserProfile, UProfileDTO>()));
                var newProfile = mapper.Map<UProfileDTO, UserProfile>(profileDto);
                _unitOfWork.Profiles.Update(newProfile);
                _unitOfWork.Save();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
