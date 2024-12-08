using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// A static utility class for dynamically building LINQ expressions based on filter criteria.
    /// </summary>
    public static class DynamicExpressionBuilder
    {
        /// <summary>
        /// Dynamically constructs a LINQ expression for filtering a collection based on a list of filter criteria.
        /// </summary>
        /// <typeparam name="T">The type of the objects being filtered.</typeparam>
        /// <param name="filters">A list of filter criteria represented as <see cref="FilterExpression"/> objects.</param>
        /// <returns>
        /// A lambda expression of type <see cref="Expression{Func{T, bool}}"/> that can be used in LINQ queries to filter objects of type <typeparamref name="T"/>.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item>
        /// <description>No filters are provided in the <paramref name="filters"/> list.</description>
        /// </item>
        /// <item>
        /// <description>A specified method (e.g., <c>Contains</c>) is not found on the target property type.</description>
        /// </item>
        /// <item>
        /// <description>Both <see cref="FilterExpression.MethodName"/> and <see cref="FilterExpression.Operator"/> are unset for a filter.</description>
        /// </item>
        /// <item>
        /// <description>An unsupported <see cref="BinaryOperation"/> or <see cref="ComparisonOperator"/> is encountered.</description>
        /// </item>
        /// </list>
        /// </exception>
        /// <remarks>
        /// This method dynamically combines multiple filter criteria into a single expression. 
        /// It supports both method-based filters (e.g., <see cref="string.Contains"/> or <see cref="string.StartsWith"/>) 
        /// and operator-based filters (e.g., <see cref="ComparisonOperator.GreaterThan"/> or <see cref="ComparisonOperator.Equal"/>).
        /// Logical combinations (e.g., AND, OR) of multiple filters are achieved using the <see cref="BinaryOperation"/> property of each filter.
        /// </remarks>
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

                if (!string.IsNullOrEmpty(filter.MethodName))
                {
                    // Handle method calls like Contains
                    var method = typeof(string).GetMethod(filter.MethodName, new[] { typeof(string) });
                    if (method == null)
                        throw new InvalidOperationException($"Method '{filter.MethodName}' not found on type '{typeof(string)}'.");

                    condition = Expression.Call(property, method, constant);
                }
                else if (filter.Operator.HasValue)
                {
                    // Handle comparison operators
                    condition = filter.Operator switch
                    {
                        ComparisonOperator.Equal => Expression.Equal(property, constant),
                        ComparisonOperator.NotEqual => Expression.NotEqual(property, constant),
                        ComparisonOperator.GreaterThan => Expression.GreaterThan(property, constant),
                        ComparisonOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, constant),
                        ComparisonOperator.LessThan => Expression.LessThan(property, constant),
                        ComparisonOperator.LessThanOrEqual => Expression.LessThanOrEqual(property, constant),
                        _ => throw new NotSupportedException($"Operator '{filter.Operator}' is not supported.")
                    };
                }
                else
                {
                    throw new InvalidOperationException("Filter must specify either a MethodName or an Operator.");
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
                        case BinaryOperation.And:
                        case BinaryOperation.AndAlso:
                            combinedExpression = Expression.AndAlso(combinedExpression, condition);
                            break;
                        case BinaryOperation.Or:
                        case BinaryOperation.OrElse:
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
