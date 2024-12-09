using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{

    /// <summary>
    /// Represents a filter value with its associated properties and expressions 
    /// used in filtering operations for a specific property type.
    /// </summary>
    public class FilterValue
    {
        /// <summary>
        /// Gets or sets the value used for filtering.
        /// </summary>
        public string? Value { get; set; }

        /// <summary>
        /// Gets or sets the name of the method to be invoked for filtering logic.
        /// </summary>
        public string? MethodName { get; set; }

        /// <summary>
        /// Gets or sets the binary expression used to combine filter conditions.
        /// The default is <see cref="FluentGridToolkit.BinaryExpression.And"/>.
        /// </summary>
        public string? BinaryExpression { get; set; } = nameof(FluentGridToolkit.BinaryExpression.And);

        /// <summary>
        /// Gets or sets the comparison operator used to compare the filter value 
        /// against the target property. The default is <see cref="FluentGridToolkit.ComparisonOperator.Equal"/>.
        /// </summary>
        public string? ComparisonOperator { get; set; } = nameof(FluentGridToolkit.ComparisonOperator.Equal);
    }

}
