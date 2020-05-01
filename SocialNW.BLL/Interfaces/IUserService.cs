using SocialNW.BLL.DTO;
using SocialNW.BLL.Infrastructure;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNW.BLL.Interfaces
{
    public interface IUserService : IDisposable
    {
        Task<OperationDetails> Create(AppUserDTO userDto);
        Task<ClaimsIdentity> Authenticate(AppUserDTO userDto);
    }
}
