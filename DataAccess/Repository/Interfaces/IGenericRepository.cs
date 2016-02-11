/* Copyright (C) Miron George - All Rights Reserved
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * Written by Miron George <george.miron2003@gmail.com>, 2016
 *
 * Role:
 * Separates the data access logic and maps it to the entities in the business logic
 *
 * History:
 * 09.02.2016    Miron George       Created interface.
 */
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        void Add(T entity);
        void Delete(T entity);
        void Delete(IQueryable<T> entities);
        void Update(T entity);
        IQueryable<T> FindAllBy(Expression<Func<T, bool>> predicate);
        T FindFirstBy(Expression<Func<T, bool>> predicate);
        int Count();
    }
}
