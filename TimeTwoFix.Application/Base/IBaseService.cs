﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TimeTwoFix.Application.Base
{
    public interface IBaseService<T> where T : class
    {
        Task<T> AddAsyncServiceGeneric(T entity);

        Task UpdateAsyncServiceGeneric(T entity);

        Task DeleteAsyncServiceGeneric(int id);

        Task<T?> GetByIdAsyncServiceGeneric(int id, params Expression<Func<T, object>>[] includeProperties);

        Task<IEnumerable<T>> GetAllAsyncServiceGeneric();

        Task AttachAsyncServiceGeneric(T entity, EntityState entityState);

        Task DetachAsyncServiceGeneric(T entity);

        Task<int> SaveChangesServiceGeneric();

        int CountAsyncServiceGeneric();

        Task<IEnumerable<T>> GetAllWithIncludesAsyncServiceGeneric(params Expression<Func<T, object>>[] includeProperties);
    }
}