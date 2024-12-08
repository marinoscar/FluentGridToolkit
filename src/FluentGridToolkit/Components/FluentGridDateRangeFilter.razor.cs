using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.DesignTokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit.Components
{
    /// <summary>
    /// A Blazor component that provides a user interface for filtering data by a date range.
    /// </summary>
    public partial class FluentGridDateRangeFilter<TGridItem, TProp> : FluentGridFilterBase<TGridItem, TProp>
    {

        /// <summary>
        /// Gets or sets the start date of the date range filter.
        /// </summary>
        [Parameter]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date of the date range filter.
        /// </summary>
        [Parameter]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Gets or sets the label for the start date picker.
        /// </summary>
        [Parameter]
        public string StartDateLabel { get; set; } = "Start Date";

        /// <summary>
        /// Gets or sets the label for the end date picker.
        /// </summary>
        [Parameter]
        public string EndDateLabel { get; set; } = "End Date";

        /// <summary>
        /// Gets or sets the ARIA label for the start date picker for accessibility purposes.
        /// </summary>
        [Parameter]
        public string StartDateAriaLabel { get; set; } = "Start Date Picker";

        /// <summary>
        /// Gets or sets the ARIA label for the end date picker for accessibility purposes.
        /// </summary>
        [Parameter]
        public string EndDateAriaLabel { get; set; } = "End Date Picker";

        /// <summary>
        /// Event callback invoked when the search button is clicked.
        /// </summary>
        /// <remarks>
        /// The callback provides a tuple containing the <see cref="StartDate"/> and <see cref="EndDate"/> as parameters.
        /// </remarks>
        [Parameter]
        public EventCallback<(DateTime? Start, DateTime? End)> OnSearchClicked { get; set; }

        /// <summary>
        /// Handles the search button click event.
        /// Validates the date range and invokes the <see cref="OnSearchClicked"/> callback.
        /// </summary>
        private async Task HandleSearch()
        {
            if (StartDate > EndDate)
            {
                HasError = true;
                StateHasChanged();
                return;
            }
            HasError = false;
            var filters = new List<FilterExpression>() {
                new FilterExpression()
                {
                    BinaryExpression = BinaryExpression.And,
                    Operator = ComparisonOperator.GreaterThan,
                    PropertyName = Property.Name,
                    Value = StartDate
                },
                new FilterExpression()
                {
                    BinaryExpression = BinaryExpression.And,
                    Operator = ComparisonOperator.LessThanOrEqual,
                    PropertyName = Property.Name,
                    Value = EndDate
                }
            };
            FilterManager.AddOrUpdateFilter(ColumnName, filters);

            if (OnSearchClicked.HasDelegate)
                await OnSearchClicked.InvokeAsync((StartDate, EndDate));

            if (OnValueChanged.HasDelegate)
                await OnValueChanged.InvokeAsync();
        }

        /// <summary>
        /// Handles the clear button click event.
        /// Clears the <see cref="StartDate"/> and <see cref="EndDate"/> values and invokes the <see cref="OnClearClicked"/> callback.
        /// </summary>
        public async Task OnClear()
        {
            StartDate = null;
            EndDate = null;

            //Clears the filter
            FilterManager.RemoveFilter(ColumnName);

            if (OnClearClicked.HasDelegate)
                await OnClearClicked.InvokeAsync();

            if (OnValueChanged.HasDelegate)
                await OnValueChanged.InvokeAsync();
        }

    }

}
