using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DatabaseContext.DataBaseContext db;
        private DbSet<T> entities;

        public Repository(DatabaseContext.DataBaseContext _db)
        {
            db = _db;
            entities = db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        public IEnumerable<T> GetTotalTime(Expression<Func<T, bool>> where)
        {
            return entities.Where(where).ToList();
        }
        public IEnumerable<T> GetWeeklyTime(Expression<Func<T, bool>> where)
        {
            var data = entities.ToList();
            return entities.Where(where).ToList();
        }

        public T GetById(int id)
        {
            return entities.Find((long)id);
        }

        public void Insert(T entity)
        {
            entities.Add(entity);
            db.SaveChanges();
        }

        public void Update(T entity)
        {
            entities.Update(entity);
            db.SaveChanges();
        }

        public void Delete(long id)
        {
            T existing = entities.Find(id);
            
            entities.Remove(existing);
            db.SaveChanges();
        }

        public int Save(T entity)
        {
            entities.Add(entity);
            db.SaveChanges();
            var entityName = entity.GetType().Name;
            var id = entity.GetType().GetProperty(entityName + "Id").GetValue(entity, null);
            return (int)id;
        }
        public T GetInTime(Expression<Func<T, bool>> where)
        {
            //db.TimeTrackings.Where(x => x.Date == timeDate && x.UserId == UserId).FirstOrDefault();
            return entities.Where(where).FirstOrDefault();
        }

    }
}
