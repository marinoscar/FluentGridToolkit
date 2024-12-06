using Microsoft.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit
{
    /// <summary>
    /// A Blazor component that provides a user interface for filtering data by a date range.
    /// </summary>
    public partial class DateRangeFilter<TGridItem> : FluentComponentBase
    {

        public required FluentDataGrid<TGridItem> ParentGrid { get; set; }

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
        /// Gets or sets the appearance style for the search button.
        /// </summary>
        [Parameter]
        public Appearance? SearchButtonAppearance { get; set; } = Appearance.Lightweight;

        /// <summary>
        /// Gets or sets the appearance style for the clear button.
        /// </summary>
        [Parameter]
        public Appearance? ClearButtonAppearance { get; set; } = Appearance.Lightweight;

        /// <summary>
        /// Gets or sets the icon used for the search button.
        /// </summary>
        [Parameter]
        public Icon SearchIcon { get; set; } = new ToolkitIcons.SearchIcon();

        /// <summary>
        /// Gets or sets the icon used for the clear button.
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

        /// <summary>
        /// Event callback invoked when the search button is clicked.
        /// </summary>
        /// <remarks>
        /// The callback provides a tuple containing the <see cref="StartDate"/> and <see cref="EndDate"/> as parameters.
        /// </remarks>
        [Parameter]
        public EventCallback<(DateTime? Start, DateTime? End)> OnSearchClicked { get; set; }

        /// <summary>
        /// Event callback invoked when the clear button is clicked.
        /// </summary>
        [Parameter]
        public EventCallback OnClearClicked { get; set; }

        /// <summary>
        /// Handles the search button click event.
        /// Validates the date range and invokes the <see cref="OnSearchClicked"/> callback.
        /// </summary>
        private async Task HandleSearch()
        {
            if (StartDate > EndDate)
            {
                // Log or handle invalid date range logic
                return;
            }

            if (OnSearchClicked.HasDelegate)
                await OnSearchClicked.InvokeAsync((StartDate, EndDate));
        }

        /// <summary>
        /// Handles the clear button click event.
        /// Clears the <see cref="StartDate"/> and <see cref="EndDate"/> values and invokes the <see cref="OnClearClicked"/> callback.
        /// </summary>
        public async Task OnClear()
        {
            StartDate = null;
            EndDate = null;

            if (OnClearClicked.HasDelegate)
                await OnClearClicked.InvokeAsync();
        }
    }

}
