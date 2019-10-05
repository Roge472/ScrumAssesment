using BaseModelLibrary.IRepositories;
using BaseModelLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseModelLibrary.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T: BaseModel
    {
        protected DbContext Context { get; set; }
        protected DbSet<T> Entities { get; set; }
        public BaseRepository(DbContext context)
        {
            Context = context;
            Entities = Context.Set<T>();
        }
        public virtual T Get(int id)
        {
            return Entities.SingleOrDefault(x => x.Id == id);
        }

        public virtual List<T> GetAll()
        {
            return Entities.ToList();
        }

        public virtual void Remove(int id)
        {
            Entities.Remove(Get(id));
            Context.SaveChanges();
        }

        public virtual void Remove(T model)
        {
            Entities.Remove(model);
            Context.SaveChanges();
        }

        public virtual T Add(T model)
        {
            Entities.Add(model);
            Context.SaveChanges();
            return model;
        }
    }
}
