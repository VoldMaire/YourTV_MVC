using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using YourTV_WEB.Models;
using YourTV_BLL.DTO;
using System.Security.Claims;
using YourTV_BLL.Interfaces;
using YourTV_BLL.Infrastructure;
using System.Configuration;
using System.Text;
using Microsoft.AspNet.Identity;
using YourTV_BLL.Services;

namespace YourTV_WEB.Controllers
{
    public class PlaylistController : Controller
    {
        private string Connection
        {
            get
            {
                return ConfigurationManager.AppSettings["Connection"];
            }
        }
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }

        [Authorize]
        public ActionResult Playlists()
        {
            PlaylistsViewModel viewModel = new PlaylistsViewModel();
            ServiceCreator serviceCreator = new ServiceCreator();
            using (IPlaylistService playlistService = serviceCreator.CreatePlaylistService(Connection))
            {
                viewModel.Playlists = playlistService.GetAllByUser(User.Identity.GetUserId());
            }
            return View(viewModel);
        }

        [Authorize]
        public ActionResult AddingPlaylist()
        {
            return PartialView(new PlaylistConcreteViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PlaylistConcrete(PlaylistConcreteViewModel model)
        {
            if (ModelState.IsValid)
            {
                PlaylistDTO playlistDto = new PlaylistDTO { Name = model.Name,
                                                         Description = model.Description,
                                                         ApplicationUserId = User.Identity.GetUserId() };
                ServiceCreator serviceCreator = new ServiceCreator();
                using(IPlaylistService playlistService = serviceCreator.CreatePlaylistService(Connection))
                {
                    await playlistService.AddAsync(playlistDto);
                }
                return View(model);
            }
            return HttpNotFound();
        }
    }
}