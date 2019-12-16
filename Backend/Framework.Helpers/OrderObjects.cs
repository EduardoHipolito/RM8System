using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framework.Helpers
{
    public class OrderObjects<TEntity> where TEntity : class
    {

        private static OrderObjects<TEntity> _instance;

        private OrderObjects()
        {
        }

        public static OrderObjects<TEntity> Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrderObjects<TEntity>();
                }

                return _instance;
            }
        }
        private PropertyInfo GetPropertyInfo(Type objType, string name)
        {
            var properties = objType.GetProperties();
            var matchedProperty = properties.FirstOrDefault(p => p.Name == name);
            if (matchedProperty == null)
                throw new ArgumentException("name");

            return matchedProperty;
        }
        private LambdaExpression GetOrderExpression(Type objType, PropertyInfo pi)
        {
            var paramExpr = Expression.Parameter(objType);
            var propAccess = Expression.PropertyOrField(paramExpr, pi.Name);
            var expr = Expression.Lambda(propAccess, paramExpr);
            return expr;
        }
        public IQueryable<TEntity> OrderBy(IQueryable<TEntity> query, string name, string direction)
        {
            var propInfo = GetPropertyInfo(typeof(TEntity), name);
            var expr = GetOrderExpression(typeof(TEntity), propInfo);

            //var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == (direction == "desc" ? "OrderBy" : "OrderByDesc") && m.GetParameters().Length == 2);
            var order = (direction.ToLower() == "asc") ? "OrderBy" : "OrderByDescending";

            var method = typeof(Queryable).GetMethods().FirstOrDefault(m => m.Name == order && m.GetParameters().Length == 2);
            var genericMethod = method.MakeGenericMethod(typeof(TEntity), propInfo.PropertyType);
            return (IQueryable<TEntity>)genericMethod.Invoke(null, new object[] { query, expr });
        }
    }
}
