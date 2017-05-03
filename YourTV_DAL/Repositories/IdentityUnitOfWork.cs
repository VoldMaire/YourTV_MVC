using YourTV_DAL.EF;
using YourTV_DAL.Entities;
using YourTV_DAL.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;
using YourTV_DAL.Identity;

namespace YourTV_DAL.Repositories
{
    public class IdentityUnitOfWork : IUnitOfWork
    {
        private ApplicationContext db;

        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        private IClientManager clientManager;
        private IRepository<Video> videoRepository;
        private IRepository<Category> categoryRepository;
        private IRepository<Comment> commentRepository;
        private IRepository<Playlist> playlistRepository;

        public IdentityUnitOfWork()
        {
            db = new ApplicationContext();
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
            videoRepository = new VideoRepository(db);
            categoryRepository = new CategoryRepository(db);
            commentRepository = new CommentRepository(db);
            playlistRepository = new PlaylistRepository(db);
        }

        public IdentityUnitOfWork(string connectionString)
        {
            db = new ApplicationContext(connectionString);
            userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(db));
            roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(db));
            clientManager = new ClientManager(db);
            videoRepository = new VideoRepository(db);
            categoryRepository = new CategoryRepository(db);
            commentRepository = new CommentRepository(db);
            playlistRepository = new PlaylistRepository(db);
        }

        public ApplicationUserManager UserManager
        {
            get { return userManager; }
        }

        public IClientManager ClientManager
        {
            get { return clientManager; }
        }

        public ApplicationRoleManager RoleManager
        {
            get { return roleManager; }
        }

        public IRepository<Video> Videos
        {
            get { return videoRepository; }
        }

        public IRepository<Category> Categories
        {
            get { return categoryRepository; }
        }

        public IRepository<Playlist> Playlists
        {
            get { return playlistRepository; }
        }

        public IRepository<Comment> Comments
        {
            get { return commentRepository; }
        }

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    userManager.Dispose();
                    roleManager.Dispose();
                    clientManager.Dispose();
                    db.Dispose();
                }
                this.disposed = true;
            }
        }
    }
}