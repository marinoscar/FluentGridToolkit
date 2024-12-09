using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    public static class Extensions
    {
        /// <summary>
        /// Extracts the property name from a lambda expression.
        /// </summary>
        /// <typeparam name="TGridItem">The type of the entity.</typeparam>
        /// <typeparam name="TProp">The type of the property.</typeparam>
        /// <param name="expression">The expression representing the property access.</param>
        /// <returns>The name of the property.</returns>
        /// <exception cref="ArgumentException">Thrown if the expression is not a valid member expression.</exception>
        public static string GetPropertyName<TGridItem, TProp>(this Expression<Func<TGridItem, TProp>> expression)
        {
            if (expression == null)
                throw new ArgumentNullException(nameof(expression));

            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }

            throw new ArgumentException("Expression must be a MemberExpression.", nameof(expression));
        }
    }
}
