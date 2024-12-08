using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    /// <summary>
    /// Specifies the type of comparison operator to use in a dynamic expression.
    /// </summary>
    public enum ComparisonOperator
    {
        /// <summary>
        /// Checks if two values are equal.
        /// </summary>
        Equal,

        /// <summary>
        /// Checks if two values are not equal.
        /// </summary>
        NotEqual,

        /// <summary>
        /// Checks if the left value is greater than the right value.
        /// </summary>
        GreaterThan,

        /// <summary>
        /// Checks if the left value is greater than or equal to the right value.
        /// </summary>
        GreaterThanOrEqual,

        /// <summary>
        /// Checks if the left value is less than the right value.
        /// </summary>
        LessThan,

        /// <summary>
        /// Checks if the left value is less than or equal to the right value.
        /// </summary>
        LessThanOrEqual
    }

}
