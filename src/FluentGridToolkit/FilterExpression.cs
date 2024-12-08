using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{

    /// <summary>
    /// Represents a filter expression for dynamically building LINQ queries.
    /// </summary>
    public class FilterExpression
    {
        /// <summary>
        /// Gets or sets the name of the property to filter on.
        /// </summary>
        /// <remarks>
        /// The property name must match the name of a property in the target type being filtered.
        /// </remarks>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the binary expression used to combine this filter with others.
        /// </summary>
        /// <remarks>
        /// Determines how this filter will be logically combined with other filters in the query.
        /// For example, <see cref="BinaryOperation.And"/> combines this filter with another using a logical AND operation.
        /// </remarks>
        public BinaryOperation BinaryExpression { get; set; }

        /// <summary>
        /// Gets or sets the value to compare against the property.
        /// </summary>
        /// <remarks>
        /// The type of this value should match the type of the property being filtered. For example, 
        /// if the property is of type <c>int</c>, this value should also be an <c>int</c>.
        /// </remarks>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the name of a method to invoke on the property for evaluation.
        /// </summary>
        /// <remarks>
        /// This is optional and is typically used for string methods such as <see cref="string.Contains"/>,
        /// <see cref="string.StartsWith"/>, or <see cref="string.EndsWith"/>. 
        /// If not set, the filter will use the specified <see cref="Operator"/> to evaluate the comparison.
        /// </remarks>
        public string MethodName { get; set; }

        /// <summary>
        /// Gets or sets the comparison operator used to evaluate the filter.
        /// </summary>
        /// <remarks>
        /// This property is optional. If set, it specifies the type of comparison to perform, such as 
        /// <see cref="ComparisonOperator.GreaterThan"/> or <see cref="ComparisonOperator.LessThan"/>.
        /// If both <see cref="Operator"/> and <see cref="MethodName"/> are provided, 
        /// the <see cref="MethodName"/> takes precedence.
        /// </remarks>
        public ComparisonOperator? Operator { get; set; }

        /// <summary>
        /// Gets or sets a value to indicate if the string comparison should ignore the case
        /// </summary>
        public bool IgnoreCase { get; set; } = false; // Default to case-sensitive
    }

}
