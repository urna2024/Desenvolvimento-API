﻿using MapeiaVoto.Domain.Entidades;
using MapeiaVoto.Domain.Interfaces;
using MapeiaVoto.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MapeiaVoto.Infrastructure.Data.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly SqlServerContext _sqlServerContext;
        public BaseRepository(SqlServerContext sqlServerContext)
        {
            _sqlServerContext = sqlServerContext;
        }
        public void Insert(TEntity obj)
        {

            _sqlServerContext.Set<TEntity>().Add(obj);
            _sqlServerContext.SaveChanges();
        }
        public void Update(TEntity obj)
        {
            _sqlServerContext.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _sqlServerContext.SaveChanges();
        }
        public void Delete(int id)
        {

            _sqlServerContext.Set<TEntity>().Remove(Select(id));
            _sqlServerContext.SaveChanges();
        }
        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
        Expression<Func<TEntity, bool>> filter = null,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
         string includeProperties = null,
         int? skip = null,
         int? take = null)
         where TEntity : class
        {
            includeProperties ??= string.Empty;
            IQueryable<TEntity> query = _sqlServerContext.Set<TEntity>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
           .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }
            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }
            return query;
        }

        public IList<TEntity> GetFiltro<TEntity>(
       Expression<Func<TEntity, bool>> filter = null,
       Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
        string includeProperties = null,
        int? take = null)
        where TEntity : class
        {
            return GetQueryable<TEntity>(filter, orderBy, includeProperties, take).ToList();
        }
        public IList<TEntity> Select() =>
        _sqlServerContext.Set<TEntity>().ToList();
        public TEntity Select(int id) =>
        _sqlServerContext.Set<TEntity>().Find(id);

    }

}


