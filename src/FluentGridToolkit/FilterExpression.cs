using System;
using System.Collections.Generic;
using System.Linq;
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
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the binary expression used to combine this filter with others.
        /// </summary>
        public BinaryExpression BinaryExpression { get; set; }

        /// <summary>
        /// Gets or sets the value to compare against the property.
        /// </summary>
        /// <remarks>
        /// The type of this value should match the type of the property being filtered.
        /// </remarks>
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the name of a method to invoke on the property for evaluation.
        /// </summary>
        /// <remarks>
        /// This is optional and is typically used for methods such as <see cref="string.Contains"/> or <see cref="string.StartsWith"/>.
        /// If not set, default comparison operators will be used.
        /// </remarks>
        public string MethodName { get; set; }
    }

}
