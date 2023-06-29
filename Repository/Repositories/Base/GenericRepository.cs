using AutoMapper;
using BusinessObject;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.IRepositories.Base;

namespace Repository.Repositories.Base
{
    public abstract class GenericRepository<TBusinessObject, TEntity> : IGenericRepository<TBusinessObject, TEntity>
        where TBusinessObject : class
        where TEntity : class
    {
        protected DbSet<TEntity> entities { get; }
        protected CustomerManageContext context { get; }
        protected IMapper mapper { get; }

        public virtual DbSet<TEntity> Entities => entities;

        public GenericRepository(CustomerManageContext context, IMapper mapper)
        {
            this.context = context;
            entities = this.context.Set<TEntity>();
            this.mapper = mapper;
        }
        public virtual async Task<TBusinessObject> Create(TBusinessObject businessObject)
        {
            TEntity entity = mapper.Map<TEntity>(businessObject);
            entity = (await entities.AddAsync(entity)).Entity;
            await context.SaveChangesAsync();
            return mapper.Map<TBusinessObject>(entity);
        }

        public virtual async Task<TBusinessObject> Delete(params object[] objectKeys)
        {
            TEntity entity = await Find(objectKeys);
            entity = entities.Remove(entity).Entity;
            await context.SaveChangesAsync();
            return mapper.Map<TBusinessObject>(entity);
        }

        public async Task<TBusinessObject> FindMapping(params object[] objectKeys)
        {
            TEntity entity = await Find(objectKeys);
            return mapper.Map<TBusinessObject>(entity);
        }

        public virtual async Task<TBusinessObject> Update(TBusinessObject businessObject)
        {
            TEntity entity = mapper.Map<TEntity>(businessObject);
            entity = entities.Update(entity).Entity;
            await context.SaveChangesAsync();
            return mapper.Map<TBusinessObject>(entity);
        }
        public Task<IEnumerable<TBusinessObject>> GetAll()
        {
            return Task.FromResult(mapper.Map<IEnumerable<TEntity>, IEnumerable<TBusinessObject>>(entities.AsEnumerable()));
        }

        public async Task<TEntity> Find(params object[] objectKeys)
        {
            var data = await entities.FindAsync(objectKeys)
                 ?? throw new CustomerManagementException(4040);
            foreach (var navigation in context.Entry(data).Navigations)
            {
                await navigation.LoadAsync();
            }
            return data;
        }
    }
}
