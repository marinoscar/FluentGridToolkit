using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
        /// Gets or sets the number value used for filtering.
        /// </summary>
        public double? NumberValue { get; set; }

        /// <summary>
        /// Gets or sets the string value used for filtering.
        /// </summary>
        public string? StringValue { get; set; }

        /// <summary>
        /// Gets or sets the DateTime value used for filtering.
        /// </summary>
        public DateTime? DateTimeValue { get; set; }

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

        /// <summary>
        /// Converts to a <see cref="FilterExpression"/> object
        /// </summary>
        public FilterExpression ToFilterExpression(string propertyName, FilterDataType filterDataType)
        {
            object value = null;

            switch (filterDataType)
            {
                case FilterDataType.Numeric:
                    value = NumberValue;
                    break;
                case FilterDataType.DateTime:
                    value = DateTimeValue;
                    break;
                default:
                    value = StringValue;
                    break;
            }

            return new FilterExpression()
            {
                BinaryExpression = BinaryExpression == "And" ? FluentGridToolkit.BinaryExpression.And : FluentGridToolkit.BinaryExpression.Or,
                Operator = (ComparisonOperator)Enum.Parse(typeof(ComparisonOperator), ComparisonOperator),
                PropertyName = propertyName,
                Value = value
            };
        }

        /// <summary>
        /// Gets a value to indicate if the value of the class is null
        /// </summary>
        /// <param name="filterDataType">Data Type to evaluate</param>
        public bool IsNullValue(FilterDataType filterDataType)
        {
            switch (filterDataType)
            {
                case FilterDataType.Numeric:
                    return null == NumberValue;
                case FilterDataType.DateTime:
                    return null == DateTimeValue;
                default:
                    return null == StringValue;
            }
        }
    }
}

