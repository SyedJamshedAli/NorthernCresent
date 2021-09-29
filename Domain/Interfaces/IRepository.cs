using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Interfaces
{
   public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetTotalTime(Expression<Func<T, bool>> where);
        IEnumerable<T> GetWeeklyTime(Expression<Func<T, bool>> where);
        T GetById(int id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(long id);
        int Save(T entity);
        T GetInTime(Expression<Func<T, bool>> where);
    }
}
