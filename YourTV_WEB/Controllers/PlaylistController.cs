using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using YourTV_WEB.Models;
using YourTV_BLL.DTO;
using System.Security.Claims;
using YourTV_BLL.Interfaces;
using System.Configuration;
using Microsoft.AspNet.Identity;
using YourTV_BLL.Services;
using YourTV_BLL.Infrastructure;

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

        private IServiceCreator ServiceCreator
        {
            get
            {
                return new ServiceCreator();
            }
        }

        [Authorize]
        public ActionResult Playlists()
        {
            PlaylistsViewModel viewModel = new PlaylistsViewModel();
            using (IPlaylistService playlistService = ServiceCreator.CreatePlaylistService(Connection))
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
        public async Task<ActionResult> AddingPlaylist(PlaylistConcreteViewModel model)
        {
            if (ModelState.IsValid)
            {
                PlaylistDTO playlistDto = new PlaylistDTO { Name = model.Name,
                                                         Description = model.Description,
                                                         ApplicationUserId = User.Identity.GetUserId() };
                using(IPlaylistService playlistService = ServiceCreator.CreatePlaylistService(Connection))
                {
                    await playlistService.CreateAsync(playlistDto);
                    playlistDto = playlistService.GetLastByName(model.Name);
                }
                return RedirectToAction("PlaylistConcrete", "Playlist", new { playlistId = playlistDto.Id });
            }
            ModelState.AddModelError("", "Can't create such playlist.");
            return PartialView(model);
        }

        [Authorize]
        public ActionResult PlaylistConcrete(int playlistId)
        {
            PlaylistConcreteViewModel modelPlaylist = new PlaylistConcreteViewModel();
            using (IPlaylistService playlistService = ServiceCreator.CreatePlaylistService(Connection))
            {
                PlaylistDTO playlistDto = playlistService.GetById(playlistId);                
                if(playlistDto != null)
                {
                    if (playlistDto.ApplicationUser.UserName != User.Identity.Name)
                        return HttpNotFound();

                    modelPlaylist.Id = playlistDto.Id;
                    modelPlaylist.Name = playlistDto.Name;
                    modelPlaylist.Description = playlistDto.Description;
                    modelPlaylist.Videos = playlistDto.Videos;
                }
            }

            if (modelPlaylist == null)
                return HttpNotFound();

            return View(modelPlaylist);
        }

        [Authorize]
        public async Task<ActionResult> DeletePlaylist(int playlistid)
        {
            using (IPlaylistService playlistService = ServiceCreator.CreatePlaylistService(Connection))
            {
                PlaylistDTO playlistDto = playlistService.GetById(playlistid);
                if(playlistDto.ApplicationUser.UserName == User.Identity.Name)
                {
                    OperationDetails details = await playlistService.DeletePlaylist(playlistid);
                    if (details.Succedeed)
                        return RedirectToAction("Playlists");
                    else
                        return HttpNotFound();
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }
    }
}