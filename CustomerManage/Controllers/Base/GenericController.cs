using AutoMapper;
using BusinessObject;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.Extensions.Options;
using Repository.IRepositories.Base;

namespace CusomterManager.Controllers.Base
{
    public abstract class GenericController<TKeyType, TBusinessObject, TEntity> : BaseController
        where TBusinessObject : class
        where TEntity : class
    {
        protected IGenericRepository<TBusinessObject, TEntity> repository;
        protected IMapper mapper;
        protected AppSetting appSetting;

        public GenericController(IGenericRepository<TBusinessObject, TEntity> repository, IMapper mapper, IOptions<AppSetting> options)
        {
            this.repository = repository;
            this.mapper = mapper;
            appSetting = options.Value;
        }
        [EnableQuery]
        [HttpGet()]
        public virtual ActionResult Get()
        {
            return Ok(repository.Entities.AsQueryable());
        }
        [EnableQuery]
        [HttpGet("single")]
        public virtual async Task<ActionResult> Get([FromODataUri] TKeyType key)
        {
            var data = await repository.Find(key);
            return Ok(data);
        }
        [HttpPost]
        public virtual async Task<TBusinessObject> Create(TBusinessObject businessObject)
        {
            return await repository.Create(businessObject);
        }
        [HttpPut]
        public virtual async Task<TBusinessObject> Update(TBusinessObject businessObject)
        {
            return await repository.Update(businessObject);
        }
        [HttpDelete("{id}")]
        public virtual async Task<TBusinessObject> Delete(TBusinessObject businessObject)
        {
            return await repository.Delete(businessObject);
        }
    }
}
