﻿using System;
using System.Collections.Generic;
using System.Linq;
using YourTV_DAL.Entities;
using YourTV_DAL.EF;
using YourTV_DAL.Interfaces;
using System.Data.Entity;
using System.Threading.Tasks;

namespace YourTV_DAL.Repositories
{
    public class VideoRepository : IRepository<Video>
    {
        private ApplicationContext db;

        public VideoRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Video> GetAll()
        {
            return db.Videos;
        }

        public Video Get(int id)
        {
            return db.Videos.Find(id);
        }

        public async Task<Video> GetAsync(int id)
        {
            return await db.Videos.FindAsync(id);
        }

        public void Create(Video video)
        {
            db.Videos.Add(video);
        }

        public void Update(Video video)
        {
            db.Entry(video).State = EntityState.Modified;
        }

        public IEnumerable<Video> Find(Func<Video, Boolean> predicate)
        {
            return db.Videos.Where(predicate);
        }

        public void Delete(int id)
        {
            Video video = db.Videos.Find(id);
            if (video != null)
                db.Videos.Remove(video);
        }
    }
}