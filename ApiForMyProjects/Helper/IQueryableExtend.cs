using System.Linq.Expressions;
using System.Reflection;

namespace ApiForMyProjects.Helper
{
    public static class IQueryableExtend
    {
        public static IQueryable<T> SortDynamicByColumn<T>(this IQueryable<T> query, string sortColumn, string order)
        {
            // Dynamically creates a call like this: query.OrderBy(p =&gt; p.SortColumn)
            var parameter = Expression.Parameter(typeof(T), "p");

            string command = "OrderBy";

            if (order == OrderBy.descending)
            {
                command = "OrderByDescending";
            }

            Expression resultExpression = null;

            var property = typeof(T).GetProperty(sortColumn);

            if (property == null)
                throw new NullReferenceException($"Invalid Column Name [{sortColumn}]");

            // this is the part p.SortColumn
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            // this is the part p =&gt; p.SortColumn
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // finally, call the "OrderBy" / "OrderByDescending" method with the order by lamba expression
            resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { typeof(T), property.PropertyType },
               query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }

        // More Accurate and Real Dynamic
        public static IQueryable<T> SortByDynamic<T>(this IQueryable<T> query, string orderByMember, string direction)
        {
            var queryElementTypeParam = Expression.Parameter(typeof(T));

            var memberAccess = Expression.PropertyOrField(queryElementTypeParam, orderByMember);

            var keySelector = Expression.Lambda(memberAccess, queryElementTypeParam);

            var orderBy = Expression.Call(
                typeof(Queryable),
                direction == OrderBy.ascending ? "OrderBy" : "OrderByDescending",
                new Type[] { typeof(T), memberAccess.Type },
                query.Expression,
                Expression.Quote(keySelector));

            return query.Provider.CreateQuery<T>(orderBy);
        }

        #region Dynamic Search

        // String.Contains(string)
        static MethodInfo containsMI = typeof(string).GetMethod("Contains", 0, new[] { typeof(string) });

        // generate r => r.{columnname}.Contains(value)
        static Expression<Func<T, bool>> WhereContainsExpr<T>(string columnname, string value)
        {
            // (T r)
            var rParm = Expression.Parameter(typeof(T), "r");
            // r.{columnname}
            var rColExpr = Expression.Property(rParm, columnname);
            // r.{columnname}.Contains(value)
            var bodyExpr = Expression.Call(rColExpr, containsMI, Expression.Constant(value));
            return Expression.Lambda<Func<T, bool>>(bodyExpr, rParm);
        }

        public static IQueryable<T> WhereByColumn<T>(this IQueryable<T> src, string columname, string value) => src.Where(WhereContainsExpr<T>(columname, value));

        #endregion
    }

    public class OrderBy
    {
        public const string ascending = "ascend";
        public const string descending = "descend";
    }
}