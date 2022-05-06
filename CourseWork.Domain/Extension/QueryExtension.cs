using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Domain.Extension
{
    public static class QueryExtension
    {
        public static IQueryable<T> IncludeMultiple<T>(
            this IQueryable<T> query,
            params Expression<Func<T, object>>[] includes) where T : class
        {
            if (includes != null)
            {
                query = includes.Aggregate(
                    query,
                    (current, include) => current.Include(include));
            }
            return query;
        }
    }
}