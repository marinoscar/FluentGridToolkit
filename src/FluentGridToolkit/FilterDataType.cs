using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    /// <summary>
    /// Indicates the underlying data type for a filter
    /// </summary>
    public enum FilterDataType
    {
        /// <summary>
        /// Filter will be applied for numeric data of type system.double?
        /// </summary>
        Numeric,
        /// <summary>
        /// Filter will be applied for numeric data of type system.string?
        /// </summary>
        Text,
        /// <summary>
        /// Filter will be applied for numeric data of type system.DateTime?
        /// </summary>
        DateTime
    }
}
