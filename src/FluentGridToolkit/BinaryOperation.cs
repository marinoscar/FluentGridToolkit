using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    /// <summary>
    /// Specifies the type of binary operation used to combine expressions.
    /// </summary>
    public enum BinaryOperation
    {
        /// <summary>
        /// Logical AND operator, combining expressions with "&&".
        /// </summary>
        And,

        /// <summary>
        /// Short-circuiting logical AND operator, combining expressions with "&&" and stops evaluating once false is encountered.
        /// </summary>
        AndAlso,

        /// <summary>
        /// Logical OR operator, combining expressions with "||".
        /// </summary>
        Or,

        /// <summary>
        /// Short-circuiting logical OR operator, combining expressions with "||" and stops evaluating once true is encountered.
        /// </summary>
        OrElse
    }
}
