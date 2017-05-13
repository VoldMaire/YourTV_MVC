using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YourTV_BLL.DTO;
using YourTV_DAL.Interfaces;
using YourTV_DAL.Repositories;
using YourTV_BLL.Interfaces;
using YourTV_DAL.Entities;
using AutoMapper;
using YourTV_BLL.Infrastructure;


namespace YourTV_BLL.Services
{
    public class VideoService : IVideoService
    {
        IUnitOfWork Database { get; set; }

        public VideoService(IUnitOfWork uow)
        {
            Database = uow;
        }

        //public OperationDetails AddVideo(VideoDTO video)
        //{
        //    Vide
        //}
    }
}
