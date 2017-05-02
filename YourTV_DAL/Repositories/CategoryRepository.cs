using System;
using System.Collections.Generic;
using System.Linq;
using YourTV_DAL.Entities;
using YourTV_DAL.EF;
using YourTV_DAL.Interfaces;
using System.Data.Entity;

namespace YourTV_DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
    {
        private ApplicationContext db;

        public CategoryRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Category> GetAll()
        {
            return db.Categories;
        }

        public Category Get(int id)
        {
            return db.Categories.Find(id);
        }

        public void Create(Category category)
        {
            db.Categories.Add(category);
        }

        public void Update(Category category)
        {
            db.Entry(category).State = EntityState.Modified;
        }

        public IEnumerable<Category> Find(Func<Category, Boolean> predicate)
        {
            return db.Categories.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Category category = db.Categories.Find(id);
            if (category != null)
                db.Categories.Remove(category);
        }
    }
}