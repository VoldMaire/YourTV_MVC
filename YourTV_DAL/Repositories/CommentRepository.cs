using System;
using System.Collections.Generic;
using System.Linq;
using YourTV_DAL.Entities;
using YourTV_DAL.EF;
using System.Data.Entity;
using YourTV_DAL.Interfaces;

namespace YourTV_DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private ApplicationContext db;

        public CommentRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments;
        }

        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }

        public void Create(Comment comment)
        {
            db.Comments.Add(comment);
        }

        public void Update(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
        }

        public IEnumerable<Comment> Find(Func<Comment, Boolean> predicate)
        {
            return db.Comments.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment != null)
                db.Comments.Remove(comment);
        }
    }
}