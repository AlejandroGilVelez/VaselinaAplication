using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace VaselinaWeb.DataModel.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        internal DbContext context;
        public GenericRepository(DbContext context)
        {
            this.context = context;
        }
        public async Task<T> Add(T entity)
        {
            context.Set<T>().Add(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            context.Set<T>().Remove(entity);
            await context.SaveChangesAsync();

            return entity;
        }

        public async Task<T> Edit(T entity)
        {
            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return entity;            
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate)
        {
            return await context.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            return await context.Set<T>().Include(includes.FirstOrDefault()).Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<IList<T>> FindAll(Expression<Func<T, bool>> predicate)
        {
            IList<T> consulta = await context.Set<T>().Where(predicate).ToListAsync();

            return consulta;            
        }

        public async Task<IList<T>> GetAll()
        {
            var consulta = await context.Set<T>().ToListAsync();

            return consulta;            
        }
    }
}
