using AutoMapper;
using FluentValidation;
using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Service.Services
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        private readonly IBaseRepository<TEntity> _baseRepository;
        private readonly IMapper _mapper;
        public BaseService(IBaseRepository<TEntity> baseRepository, IMapper mapper)
        {
            _baseRepository = baseRepository;
            _mapper = mapper;
        }
        public TModel Add<TModel, TValidator>(TModel inputModel)
        where TValidator : AbstractValidator<TEntity>
        where TModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Insert(entity);
            TModel outputModel = _mapper.Map<TModel>(entity);
            return outputModel;
        }
        public void Delete(int id) => _baseRepository.Delete(id);

       
        public IEnumerable<TModel> Get<TModel>() where TModel : class
        {
            var entities = _baseRepository.Select();
            var outputModels = entities.Select(s => _mapper.Map<TModel>(s));
            return outputModels;
        }
        public TModel GetById<TModel>(int id) where TModel : class
        {
            var entity = _baseRepository.Select(id);
            var outputModel = _mapper.Map<TModel>(entity);
            return outputModel;
        }
        public TModel Update<TModel, TValidator>(TModel inputModel)
        where TValidator : AbstractValidator<TEntity>
        where TModel : class
        {
            TEntity entity = _mapper.Map<TEntity>(inputModel);
            Validate(entity, Activator.CreateInstance<TValidator>());
            _baseRepository.Update(entity);
            TModel outputModel = _mapper.Map<TModel>(entity);
            return outputModel;
        }
        public IEnumerable<TModel> GetFiltro<TModel>(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? take = null)
        where TModel : class
        {
            var entities = _baseRepository.GetFiltro<TEntity>(filter, orderBy, includeProperties, take);
            var outputModels = entities.Select(s => _mapper.Map<TModel>(s));
            return outputModels;
        }
        private void Validate(TEntity obj, AbstractValidator<TEntity> validator)
        {
            if (obj == null)
                throw new Exception("Registros não detectados!");
            validator.ValidateAndThrow(obj);
        }
    }
}
