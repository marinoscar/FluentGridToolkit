using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit.Components
{
    public partial class FluentGridListFilter<TGridItem, TProp> : FluentGridFilterBase<TGridItem, TProp>
    {
        /// <summary>
        /// Gets or sets the placeholder text for the combobox.
        /// </summary>
        [Parameter]
        public string Placeholder { get; set; } = "Make a selection...";

        /// <summary>
        /// Gets or sets the label text for the combobox.
        /// </summary>
        [Parameter]
        public string Label { get; set; } = "Select an item";

        /// <summary>
        /// Gets or sets the height of the combobox.
        /// </summary>
        [Parameter]
        public string ComboboxHeight { get; set; } = "200px";

        /// <summary>
        /// Gets the selected value
        /// </summary>
        public string SelectedValue { get; protected set; } = default;

        /// <summary>
        /// Gets or sets the items from the grid that will be used to populate the list
        /// </summary>
        [Parameter]
        public required IQueryable<TGridItem> GridItems { get; set; }

        /// <summary>
        /// Gets the items in the specified property
        /// </summary>
        public IQueryable<TProp> Items { get; private set; }

        protected override void OnInitialized()
        {
            Items = GridItems.Select(Property).Distinct();
        }

        /// <summary>
        /// Handles the click event of the search button.
        /// </summary>
        private async Task HandleSearch()
        {
            if (string.IsNullOrEmpty(SelectedValue))
                return;
            await HandleTextSearch(SelectedValue);

        }
    }
}
