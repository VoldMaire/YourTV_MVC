using YourTV_DAL.Identity;
using System;
using System.Threading.Tasks;
using YourTV_DAL.Entities;

namespace YourTV_DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationUserManager UserManager { get; }
        IClientManager ClientManager { get; }
        ApplicationRoleManager RoleManager { get; }
        IRepository<Playlist> Playlists { get; }
        IRepository<Video> Videos { get; }
        IRepository<Comment> Comments { get; }
        IRepository<Category> Categories { get; }
        Task SaveAsync();
    }
}