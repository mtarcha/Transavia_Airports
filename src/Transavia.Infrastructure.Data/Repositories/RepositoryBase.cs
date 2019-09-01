using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Transavia.Infrastructure.Data.Entities;
using Transavia.Infrastructure.EventDispatching;

namespace Transavia.Infrastructure.Data.Repositories
{
    public class RepositoryBase<TEntity> : IRepository<TEntity, Guid> where TEntity : Entity
    {
        private readonly TransaviaDbContext _ctx;
        private readonly IEventDispatcher _eventDispatcher;

        protected RepositoryBase(TransaviaDbContext ctx, IEventDispatcher eventDispatcher)
        {
            _ctx = ctx;
            _eventDispatcher = eventDispatcher;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken token)
        {
            await _ctx.AddAsync(entity, token);

            _eventDispatcher.DispatchDeferred(new DatabaseUpdatedEvent());

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

        public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token)
        {
            return Task.Run(() =>
            {
                _ctx.Set<TEntity>().Update(entity);

                _eventDispatcher.DispatchDeferred(new DatabaseUpdatedEvent());

                return entity;
            }, token);
        }

        public async Task DeleteAsync(Guid id, CancellationToken token)
        {
            var entity = await GetByIdAsync(id, token);
            _ctx.Set<TEntity>().Remove(entity);

            _eventDispatcher.DispatchDeferred(new DatabaseUpdatedEvent());
        }
    }
}