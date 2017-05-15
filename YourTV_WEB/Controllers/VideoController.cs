using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourTV_WEB.Models;
using YourTV_BLL.DTO;
using YourTV_BLL.Interfaces;
using YourTV_BLL.Services;
using System.Configuration;
using YourTV_BLL.Infrastructure;
using System.Threading.Tasks;

namespace YourTV_WEB.Controllers
{
    public class VideoController : Controller
    {
        private string Connection
        {
            get
            {
                return ConfigurationManager.AppSettings["Connection"];
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
        public ActionResult AddingVideo(VideoViewModel model)
        {
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public ActionResult AddingVideo(int playlistId)
        {
            VideoViewModel videoVM = new VideoViewModel();
            videoVM.PlaylistId = playlistId;
            return View(videoVM);
        }

        [HttpPost]
        [Authorize]
        public JsonResult UploadVideo()
        {
            bool isSavedSuccessfully = true;
            string fileName = "";

            foreach (string name in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[name];
                fileName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Videos\\" + User.Identity.Name + "\\temporary", Server.MapPath(@"\")));
                    string pathString = Path.Combine(originalDirectory.ToString(), "videopath");
                    bool isExists = Directory.Exists(pathString);
                    if (!isExists)
                        Directory.CreateDirectory(pathString);
                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);
                }
            }
            if (isSavedSuccessfully)
            {
                return Json(new { Message = fileName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SaveVideo(VideoViewModel model)
        {
            string virtualSrcPath = "~/Videos/" + User.Identity.Name + "/temporary/videopath/" + model.FileName;
            string srcPath = Server.MapPath(virtualSrcPath);
            if (System.IO.File.Exists(srcPath))
            {
                if (ModelState.IsValid)
                {
                    using (IVideoService videoService = ServiceCreator.CreateVideoService(Connection))
                    {
                        string virtualDestPath = "~/Videos/" + User.Identity.Name + "/" + model.PlaylistId + "/";
                        string destPath = Server.MapPath(virtualDestPath);
                        bool isExists = Directory.Exists(destPath);
                        if (!isExists)
                            Directory.CreateDirectory(destPath);
                        destPath += model.FileName;
                        System.IO.File.Copy(srcPath, destPath);
                        System.IO.File.Delete(srcPath);

                        VideoDTO videoDto = new VideoDTO();
                        videoDto.Name = model.Name;
                        videoDto.IsDeleted = false;
                        videoDto.Description = model.Description;
                        videoDto.UserName = User.Identity.Name;
                        videoDto.Path = virtualDestPath;
                        videoDto.PlaylistId = model.PlaylistId;
                        OperationDetails operationDetail = await videoService.AddVideo(videoDto);
                        return RedirectToAction("PlaylistConcrete", "Playlist", new { playlistId = model.PlaylistId });
                    }
                }
                else
                {
                    return AddingVideo(model);
                }
            }
            else
            {
                ModelState.AddModelError("FileNotFound", "Can't found your file. Please try to upload it again.");
                return AddingVideo(model);
            }
        }

        [Authorize]
        public ActionResult VideoConcrete()
        {
            return View();
        }
    }
}