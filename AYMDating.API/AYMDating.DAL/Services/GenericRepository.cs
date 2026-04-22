using AYMDating.DAL.BaseEntity;
using AYMDating.DAL.Contexts;
using AYMDating.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AYMDating.DAL.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Base
    {
        private readonly AYMDatingContext _context;

        public GenericRepository(AYMDatingContext context)
        {
            _context = context;
        }

        public async Task Add(T Entity)
        {
            _context.Set<T>().Add(Entity);
            await Save();
        }

        public async Task Update(T Entity)
        {
            _context.Set<T>().Update(Entity);
            await Save();
        }

        public async void Delete(T Entity)
        {
            _context.Set<T>().Remove(Entity);
            await Save();
        }        

        public T GetByIdAsync(int id)
            =>  _context.Set<T>().Find(id);


        public IReadOnlyList<T> ListAll()
            => _context.Set<T>().ToList();



        public IEnumerable<T> FindByQuery(Expression<Func<T, bool>> expression)
           => _context.Set<T>().Where(expression);

        public async Task<IReadOnlyList<T>> ListWithSpecAsync(ISpecifications<T> spec)
          => await ApplySpecification(spec).ToListAsync();

        private IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
         => SpecificationEvaluator<T>.GetQuery(_context.Set<T>(), specifications);

        private async Task Save()
        {
            await _context.SaveChangesAsync();
            //Dispose();
        }

        public void Dispose()
         => _context.Dispose();

    }
}
