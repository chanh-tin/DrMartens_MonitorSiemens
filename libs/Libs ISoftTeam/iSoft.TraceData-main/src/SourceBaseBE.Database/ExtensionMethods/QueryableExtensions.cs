using iSoft.DBLibrary.SQLBuilder.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SourceBaseBE.Database.ExtensionMethods
{
    public static class QueryableExtensions
    {
        //public static IQueryable<T> WherePropertyEquals<T, TValue>(this IQueryable<T> query, string propertyName, TValue value)
        //{
        //    var parameter = Expression.Parameter(typeof(T));
        //    var propertyExpression = Expression.Property(parameter, propertyName);
        //    var valueExpression = Expression.Constant(value);
        //    var comparisonExpression = Expression.Equal(propertyExpression, valueExpression);
        //    var lambdaExpression = Expression.Lambda<Func<T, bool>>(comparisonExpression, parameter);
        //    return query.Where(lambdaExpression);
        //}
        public static IQueryable<T> WherePropertyEquals<T, TValue>(this IQueryable<T> query, string propertyName, TValue value)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyExpression = Expression.Property(parameter, propertyName);

            // Ensure the value expression is of the correct type
            var valueExpression = Expression.Constant(value, propertyExpression.Type);

            // Handle nullable types
            Expression comparisonExpression;
            if (Nullable.GetUnderlyingType(propertyExpression.Type) != null)
            {
                // Convert the value to a nullable type if it is not already
                var nullableValue = Expression.Convert(valueExpression, propertyExpression.Type);
                comparisonExpression = Expression.Equal(propertyExpression, nullableValue);
            }
            else
            {
                comparisonExpression = Expression.Equal(propertyExpression, valueExpression);
            }

            var lambdaExpression = Expression.Lambda<Func<T, bool>>(comparisonExpression, parameter);
            return query.Where(lambdaExpression);
        }
        public static IQueryable<T> OrderByPropertyDescending<T>(this IQueryable<T> query, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyExpression = Expression.Property(parameter, propertyName);
            var lambdaExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyExpression, typeof(object)), parameter);
            return query.OrderByDescending(lambdaExpression);
        }
        public static IQueryable<T> OrderByPropertyAscending<T>(this IQueryable<T> query, string propertyName)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            var propertyExpression = Expression.Property(parameter, propertyName);
            var lambdaExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(propertyExpression, typeof(object)), parameter);
            return query.OrderBy(lambdaExpression);
        }

        //// TODO: Unaccent??? ILike???
        //public static IQueryable<T> Search<T>(this IQueryable<T> source, string propertyName, string searchTerm)
        //{
        //    if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(searchTerm))
        //    {
        //        return source;
        //    }

        //    var property = typeof(T).GetProperty(propertyName);

        //    if (property is null)
        //    {
        //        return source;
        //    }

        //    searchTerm = "%" + searchTerm + "%";
        //    var itemParameter = Expression.Parameter(typeof(T), "item");

        //    var functions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions)));
        //    var like = typeof(DbFunctionsExtensions).GetMethod(nameof(DbFunctionsExtensions.Like), new Type[] { functions.Type, typeof(string), typeof(string) });

        //    Expression expressionProperty = Expression.Property(itemParameter, property.Name);

        //    if (property.PropertyType != typeof(string))
        //    {
        //        expressionProperty = Expression.Call(expressionProperty, typeof(object).GetMethod(nameof(ToString), new Type[0]));
        //    }

        //    var selector = Expression.Call(
        //               null,
        //               like,
        //               functions,
        //               expressionProperty,
        //               Expression.Constant(searchTerm));

        //    return source.Where(Expression.Lambda<Func<T, bool>>(selector, itemParameter));
        //}
    }
}
