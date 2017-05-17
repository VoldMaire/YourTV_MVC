using AutoMapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YourTV_BLL.DTO;
using YourTV_BLL.Interfaces;
using YourTV_BLL.Services;
using YourTV_WEB.Models;

namespace YourTV_WEB.Controllers
{
    public class HomeController : Controller
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

        public ActionResult Index()
        {
            IEnumerable<VideoViewModel> model;
            using(IVideoService videoService = ServiceCreator.CreateVideoService(Connection))
            {
                IEnumerable<VideoDTO> videosDto = videoService.GetAllVideos();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<VideoDTO, VideoViewModel>();
                });
                var mapper = config.CreateMapper();                
                model = mapper.Map<IEnumerable<VideoDTO>, IEnumerable<VideoViewModel>>(videosDto);               
            }
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}