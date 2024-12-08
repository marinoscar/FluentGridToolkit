using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    /// <summary>
    /// Provides functionality for dynamically building LINQ expressions based on filter criteria.
    /// </summary>
    public static class DynamicExpressionBuilder
    {
        /// <summary>
        /// Builds a dynamic LINQ expression based on the provided filter criteria.
        /// </summary>
        /// <typeparam name="T">The type of the object being filtered.</typeparam>
        /// <param name="filters">A list of filter criteria used to construct the expression.</param>
        /// <returns>
        /// A lambda expression of type <see cref="Expression{Func{T, bool}}"/> that can be used for filtering objects of type <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if no filters are provided, or if a specified method (e.g., Contains) is not found on the target property type.
        /// </exception>
        /// <remarks>
        /// The method combines multiple filter expressions using logical operators (e.g., AND, OR).
        /// If a method name is provided in the filter (e.g., "Contains"), it dynamically invokes that method on the target property.
        /// </remarks>
        /// <example>
        /// Example usage:
        /// <code>
        /// var filters = new List&lt;FilterExpression&gt;
        /// {
        ///     new FilterExpression { PropertyName = "Name", MethodName = "Contains", Value = "John", BinaryExpression = BinaryExpression.And },
        ///     new FilterExpression { PropertyName = "State", Value = "TX", BinaryExpression = BinaryExpression.And },
        ///     new FilterExpression { PropertyName = "TotalSales", Value = 100.0, BinaryExpression = BinaryExpression.And }
        /// };
        ///
        /// var predicate = DynamicExpressionBuilder.BuildExpression&lt;Account&gt;(filters);
        /// var filteredAccounts = accounts.AsQueryable().Where(predicate).ToList();
        /// </code>
        /// </example>
        public static Expression<Func<T, bool>> BuildExpression<T>(List<FilterExpression> filters)
        {
            var parameter = Expression.Parameter(typeof(T), "x");
            Expression? combinedExpression = null;

            foreach (var filter in filters)
            {
                // Access the property (e.g., x.PropertyName)
                var property = Expression.Property(parameter, filter.PropertyName);

                // Create the constant (filter.Value)
                var constant = Expression.Constant(filter.Value);

                Expression condition;

                // Handle method calls like Contains
                if (!string.IsNullOrEmpty(filter.MethodName))
                {
                    var method = typeof(string).GetMethod(filter.MethodName, new[] { typeof(string) });
                    if (method == null)
                        throw new InvalidOperationException($"Method '{filter.MethodName}' not found on type '{typeof(string)}'.");

                    condition = Expression.Call(property, method, constant);
                }
                else
                {
                    // Default comparison
                    condition = Expression.Equal(property, constant); // Adjust as needed for other operators
                }

                // Combine expressions
                if (combinedExpression == null)
                {
                    combinedExpression = condition;
                }
                else
                {
                    switch (filter.BinaryExpression)
                    {
                        case BinaryExpression.And:
                        case BinaryExpression.AndAlso:
                            combinedExpression = Expression.AndAlso(combinedExpression, condition);
                            break;
                        case BinaryExpression.Or:
                        case BinaryExpression.OrElse:
                            combinedExpression = Expression.OrElse(combinedExpression, condition);
                            break;
                        default:
                            throw new NotSupportedException($"Binary expression '{filter.BinaryExpression}' is not supported.");
                    }
                }
            }

            if (combinedExpression == null)
                throw new InvalidOperationException("No filters provided to build the expression.");

            return Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
        }
    }

}
