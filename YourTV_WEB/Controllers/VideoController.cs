using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourTV_WEB.Models;

namespace YourTV_WEB.Controllers
{
    public class VideoController : Controller
    {
        [Authorize]
        public ActionResult AddingVideo()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public JsonResult UploadVideo(HttpPostedFileBase inputFile)
        {
            bool isSavedSuccessfully = true;
            string fileName = "";

            foreach (string name in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[name];
                fileName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {

                    var originalDirectory = new DirectoryInfo(string.Format("{0}Videos\\" + User.Identity.Name + "\\" + "PlaylistName", Server.MapPath(@"\")));
                    string pathString = Path.Combine(originalDirectory.ToString(), "imagepath");
                    var fileName1 = Path.GetFileName(file.FileName);
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
        public ActionResult SaveVideo(VideoViewModel model)
        {
            return AddingVideo();
        }
    }
}