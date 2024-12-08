using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit.Components
{
    public abstract class FluentGridFilterBase<TGridItem, TProp> : FluentComponentBase
    {
        /// <summary>
        /// True if there is a data error, otherwise false
        /// </summary>
        public bool HasError { get; protected set; }

        /// <summary>
        /// Gets the list of <see cref="FilterExpression"/> to be applied
        /// </summary>
        public List<FilterExpression> FilterExpressions { get; protected set; } = new List<FilterExpression>();

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

        /// <summary>
        /// Gets or sets a value indicating whether the search button should be displayed.
        /// </summary>
        [Parameter]
        public bool ShowSearchButton { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether the clear button should be displayed.
        /// </summary>
        [Parameter]
        public bool ShowClearButton { get; set; } = true;

        /// <summary>
        /// Gets or sets the appearance of the search button.
        /// </summary>
        [Parameter]
        public Appearance SearchButtonAppearance { get; set; } = Appearance.Lightweight;

        /// <summary>
        /// Gets or sets the appearance of the clear button.
        /// </summary>
        [Parameter]
        public Appearance ClearButtonAppearance { get; set; } = Appearance.Lightweight;

        /// <summary>
        /// Gets or sets the icon for the search button.
        /// </summary>
        [Parameter]
        public Icon SearchIcon { get; set; } = new ToolkitIcons.SearchIcon();

        /// <summary>
        /// Gets or sets the icon for the clear button.
        /// </summary>
        [Parameter]
        public Icon ClearIcon { get; set; } = new ToolkitIcons.XMarkIcon();

        /// <summary>
        /// Gets or sets the tooltip text for the search button.
        /// </summary>
        [Parameter]
        public string SearchButtonTooltip { get; set; } = "Apply Filters";

        /// <summary>
        /// Gets or sets the tooltip text for the clear button.
        /// </summary>
        [Parameter]
        public string ClearButtonTooltip { get; set; } = "Clear Filters";


    }
}
