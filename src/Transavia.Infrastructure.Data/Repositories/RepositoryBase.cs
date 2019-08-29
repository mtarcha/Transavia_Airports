using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transavia.Infrastructure.Data.Entities;

namespace Transavia.Infrastructure.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity, Guid> where TEntity : Entity
    {
        private readonly TransaviaDbContext _ctx;

        protected RepositoryBase(TransaviaDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token)
        {
            await _ctx.AddAsync(entity, token);

            return entity;
        }

        public async Task<TEntity> GetByIdAsync(Guid id, CancellationToken token)
        {
            var entity = await _ctx.Set<TEntity>().SingleOrDefaultAsync(x => x.Id == id, token);

            if (entity == null)
            {
                throw new BadRequestException($"No item has been found with id: {id}");
            }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token)
        {
            _ctx.Set<TEntity>().Update(entity);

            return entity;
        }

        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            var entity = await GetByIdAsync(id, token);
            _ctx.Set<TEntity>().Remove(entity);
        }
    }
}