using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Heznek.DataAccess.Extensions
{
    public static class IncludeExtension
    {
        public static IIncludableQueryable<TEntity, TProperty> IncludeEntity<TEntity, TProperty>(this IQueryable<TEntity> source, Expression<Func<TEntity, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return source.Include(navigationPropertyPath);
        }

        public static IIncludableQueryable<TEntity, TProperty> ThenIncludeEntity<TEntity, TPreviousProperty, TProperty>(this IIncludableQueryable<TEntity, TPreviousProperty> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return source.ThenInclude(navigationPropertyPath);
        }
        public static IIncludableQueryable<TEntity, TProperty> ThenIncludeEntity<TEntity, TPreviousProperty, TProperty>(this IIncludableQueryable<TEntity, IEnumerable<TPreviousProperty>> source, Expression<Func<TPreviousProperty, TProperty>> navigationPropertyPath) where TEntity : class
        {
            return source.ThenInclude(navigationPropertyPath);
        }
    }
}
