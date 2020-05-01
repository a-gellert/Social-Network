using System.Data.Entity.Validation;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SocialNW.BLL.DTO;
using SocialNW.BLL.Infrastructure;
using SocialNW.BLL.Interfaces;
using SocialNW.DAL.Entities;
using SocialNW.DAL.Repositories;

namespace SocialNW.BLL.Services
{
    internal class UserService : IUserService
    {
        private UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClaimsIdentity> Authenticate(AppUserDTO userDto)
        {
            ClaimsIdentity claim = null;
            AppUser user = await _unitOfWork.UserManager.FindAsync(userDto.Email, userDto.Password);
            if (user != null)
                claim = await _unitOfWork.UserManager.CreateIdentityAsync(user,
                                            DefaultAuthenticationTypes.ApplicationCookie);
            return claim;
        }

        public async Task<OperationDetails> Create(AppUserDTO userDto)
        {
                AppUser user = await _unitOfWork.UserManager.FindByEmailAsync(userDto.Email);
            if (user == null)
            {
                string error;
                try
                {
                    user = new AppUser { Email = userDto.Email, UserName = userDto.Email, Profile = new UserProfile { FirstName = userDto.FirstName, LastName = userDto.LastName } };
                    await _unitOfWork.UserManager.CreateAsync(user, userDto.Password);
                    await _unitOfWork.UserManager.AddToRoleAsync(user.Id, userDto.Role);
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                        error = validationError.Entry.Entity.ToString();

                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                            error += err.ErrorMessage;
                        }
                    }

                }
               
                _unitOfWork.Save();

                return new OperationDetails(true, "Регистрация успешно пройдена", "");
            }
            else
            {
                return new OperationDetails(false, "Пользователь с таким логином уже существует", "Email");
            }
        }

     

        public void Dispose()
        {
            _unitOfWork.Dispose();
        }
    }
}