using System.Collections.Generic;
using System.Threading.Tasks;
using YourTV_BLL.Infrastructure;
using YourTV_BLL.DTO;
using System.Security.Claims;

namespace YourTV_BLL.Interfaces
{
    public interface IUserService
    {
        Task<OperationDetails> Create(UserDTO userDto);
        Task<ClaimsIdentity> Authenticate(UserDTO userDto);
        Task SetInitialData(UserDTO adminDto, List<string> roles);
    }
}