using System.Linq.Expressions;
using System;
namespace MVCT.IRepository
{
    public interface IRepository <T> where T : class
    {
        IEnumerable<T> GetAll(string? includeProperties = null);
        T Get(Expression<Func<T ,bool>> filter,string? includeProperties = null);
        void Add(T entity);
        
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entity);
    }
    
}