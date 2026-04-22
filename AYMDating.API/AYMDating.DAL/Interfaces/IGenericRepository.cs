using AYMDating.DAL.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Interfaces
{
    public interface IGenericRepository<T> : IDisposable
    {
        T GetByIdAsync(int id);
        IReadOnlyList<T> ListAll();
        IEnumerable<T> FindByQuery(Expression<Func<T, bool>> predicate);
        void Delete(T Entity);
        Task Add(T Entity);
        Task Update(T Entity);
        //Task Save();
        Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecifications<T> spec);
    }
}
