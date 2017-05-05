using YourTV_BLL.Interfaces;
using YourTV_DAL.Repositories;

namespace YourTV_BLL.Services
{
    public class ServiceCreator : IServiceCreator
    {
        public IUserService CreateUserService(string connection)
        {
            return new UserService(new IdentityUnitOfWork(connection));
        }

        public IPlaylistService CreatePlaylistService(string connection)
        {
            return new PlaylistService(new IdentityUnitOfWork(connection));
        }
    }
}