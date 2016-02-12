/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 * Repository implementation. 
 *
 * History:
 * 09.02.2016    Miron George       Created class.
 */
using System;
using System.Linq;
using DataAccess.Context;
using System.Linq.Expressions;
using DataAccess.Repository.Interfaces;

namespace DataAccess.Repository.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        #region Public Members
        public PollAppDBContext Context { get; set; }
        #endregion

        #region Constructor
        public GenericRepository(PollAppDBContext context)
        {
            Context = context;
        }
        #endregion

        #region Public Methods
        public void Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
        }

        public int Count()
        {
            return Context.Set<T>().Count();
        }

        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        public virtual void Delete(IQueryable<T> entities)
        {
            foreach (T entity in entities.ToList())
            {
                Context.Set<T>().Remove(entity);
            }
            Context.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return Context.Set<T>();
        }

        public virtual IQueryable<T> FindAllBy(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate);
        }

        public virtual T FindFirstBy(Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().FirstOrDefault(predicate);
        }

        public void Update(T entity)
        {
            Context.Entry<T>(entity).State = System.Data.Entity.EntityState.Modified;
            Context.SaveChanges();
        }

        #endregion
    }
}
