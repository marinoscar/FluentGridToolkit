using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    public abstract class FluentGridFilterBase<TGridItem, TProp> : FluentComponentBase
    {
        /// <summary>
        /// True if there is a data error, otherwise false
        /// </summary>
        public bool HasError { get; protected set; }

        /// <summary>
        /// Gets the filter expression
        /// </summary>
        public Expression<Func<TGridItem, bool>> FilterExpression { get; private set; } = default!;

        /// <summary>
        /// Gets or sets the <see cref="FluentGridFilterManager{TEntity}"/>
        /// </summary>
        [Parameter]
        public required FluentGridFilterManager<TGridItem> FilterManager { get; set; }

        /// <summary>
        /// Gets or sets the name of the column to which the filter is applied
        /// </summary>
        [Parameter]
        public required string ColumnName { get; set; }

        /// <summary>
        /// Defines the value to be used in the fiter expression
        /// </summary>
        [Parameter, EditorRequired]
        public Expression<Func<TGridItem, TProp>> Property { get; set; } = default!;


        /// <summary>
        /// Event callback invoked when the clear button is clicked.
        /// </summary>
        [Parameter]
        public EventCallback OnClearClicked { get; set; }

        /// <summary>
        /// Event callback when the filter expression has changed
        /// </summary>
        [Parameter]
        public EventCallback OnValueChanged { get; set; }

    }
}
