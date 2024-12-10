# FluentGridToolkit

FluentGridToolkit is a powerful and easy-to-use library designed to extend the functionality of the FluentDataGrid component from Microsoft's Fluent UI. This toolkit provides advanced filtering capabilities for numeric, text, and date range columns, allowing developers to create rich, interactive grids effortlessly.

# Features
- **[FluentGridFilter](#FluentGridFilter):** Filters grid data by numeric, text, and date values.
- **FluentGridTextFilter:** Filters text data containing the specified value.
- **FluentGridListFilter:** Allows filtering by selecting values from a dropdown list populated by column data.
- **FluentGridDateRangeFilter:** Filters data based on a date range.

# Installation
To install the FluentGridToolkit NuGet package, run the following command in the Package Manager Console:

```bash
Install-Package FluentGridToolkit
```
Alternatively, use the .NET CLI:

```bash
dotnet add package FluentGridToolkit
```

# Usage
Below is an example of how to use the FluentGridToolkit in a Blazor Server application.

## Step 1: Add Necessary Using Statements
In your component, include the following namespaces:

```razor
@using FluentGridToolkit.Components
@using FluentGridToolkit.Sample.Model
```
## Step 2: Inject Dependencies
Make sure to inject the necessary dependencies, such as DbContext, to retrieve data for the grid.

```razor
@inject DbContext dbContext
```
## Step 3: Define the Grid
Here is an example of how to set up a grid with advanced filtering functionality for various column types:

```razor
@page "/"
@rendermode InteractiveServer

<PageTitle>Home</PageTitle>

<h1>DataGrid Example</h1>

@* Provides the filter manager value for all the items in the grid *@
<CascadingValue Value="@filterManager">

    <FluentDataGrid Items="@filterManager.Data" ShowHover="true"
                    Pagination="@pagination"
                    ResizableColumns="true">
        <PropertyColumn Property="@(p => p.Name)" Title="Name" Sortable="true">
            <ColumnOptions>
                <div class="search-box">
                    <FluentGridTextFilter Property="@(p => p.Name)"
                                          IgnoreCase="true"
                                          OnValueChanged="OnFilterChange" />
                </div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="@(p => p.Email)" Title="Email" Sortable="true">
            <ColumnOptions>
                <div class="search-box"></div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="@(p => p.TotalSales)" Title="Total Sales" Format="N2" Align="Align.End" Sortable="true">
            <ColumnOptions>
                <div class="search-box">
                    <FluentGridFilter Property="@(p => p.TotalSales)"
                                      OnValueChanged="OnFilterChange"
                                      DataType="@FilterDataType.Numeric" />
                </div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="@(p => p.AccountCreatedDate)" Title="Open Date" Format="yyyy-MM-dd" Sortable="true">
            <ColumnOptions>
                <div class="search-box">
                    <FluentGridDateRangeFilter Property="@(p => p.AccountCreatedDate)"
                                               OnValueChanged="OnFilterChange" />
                </div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="@(p => p.State)" Title="State" Sortable="true">
            <ColumnOptions>
                <div class="search-box">
                    <FluentGridListFilter GridItems="@filterManager.Data"
                                          Property="@(p => p.State)"
                                          OnValueChanged="OnFilterChange" />
                </div>
            </ColumnOptions>
        </PropertyColumn>
    </FluentDataGrid>

</CascadingValue>

<FluentPaginator State="@pagination" />
```
## Step 4: Manage Filters
In the @code block, manage filtering and pagination state using the FluentGridFilterManager. Here's an example:

```razor
@code {
    PaginationState pagination = new PaginationState { ItemsPerPage = 10 };
    private FluentGridFilterManager<Account> filterManager = default;

    protected override void OnInitialized()
    {
        // Initialize the filter manager with data
        filterManager = new FluentGridFilterManager<Account>(dbContext.Accounts);
    }

    private void OnFilterChange()
    {
        // Apply all filters when a change occurs
        filterManager.ApplyFilters();
    }
}
```

# How It Works

- **CascadingValue:** Provides the filter manager value to all child components within the grid.
- **FluentGridFilterManager:** Handles filtering logic for the data displayed in the grid.
- **FluentGridFilter Components:** Bind to specific properties and allow filtering based on user input.

# FluentGridFilter Component
The FluentGridFilter component is a versatile and reusable filter component in the FluentGridToolkit. It allows developers to filter data in a Fluent UI grid based on numeric, text, or date values. It supports multi-condition filtering and provides options to customize the appearance and behavior of the filter.

## Features
- **Multi-condition Filtering:** Add multiple filter conditions using AND or OR operators.
- **Data Type Support:** Supports filtering for numeric, text, and date values.
- **Customizable Appearance:** Configure placeholders, styles, and icons.
- **Error Handling:** Displays an error message when required fields are empty.
- **Interactive Buttons:** Includes search and clear buttons for user convenience.

## Parameters
- Placeholder: The placeholder text for the input field (default: "Enter text...").
- InputStyle: CSS style for the input box (default: "flex: 1;width:200px").
- DataType: Specifies the type of data being filtered (e.g., Text, Numeric, DateTime).
- PlusIcon: Icon for the "+" button (default: ToolkitIcons.PlusIcon).

 ## Usages
Here are examples of how to use the FluentGridFilter for different data types: Numeric, Text, and DateTime.

## 1. Numeric Filter
Filters numeric data, such as sales figures, prices, or quantities.

### Usage Example:
```razor
<FluentGridFilter Property="@(p => p.TotalSales)" 
                  DataType="@FilterDataType.Numeric" 
                  Placeholder="Enter sales value" 
                  InputStyle="flex: 1;width:250px" 
                  OnValueChanged="OnFilterChange" />
```

### Description:
- **Property:** Specifies the numeric property to filter (`TotalSales` in this case).
- **DataType:** Set to `FilterDataType.Numeric` to indicate a numeric filter.
- **Placeholder:** Placeholder text for the input field.
- **InputStyle:** Optional style customization.
- **OnValueChanged:** Callback triggered when the filter value changes.

## 2. Text Filter
Filters text data, such as names, descriptions, or comments.

### Usage Example:
```razor
<FluentGridFilter Property="@(p => p.Name)" 
                  DataType="@FilterDataType.Text" 
                  Placeholder="Enter name" 
                  InputStyle="flex: 1;width:200px" 
                  OnValueChanged="OnFilterChange" />
```

### Description:
- **Property:** Specifies the text property to filter (Name in this case).
- **DataType:** Set to FilterDataType.Text for filtering string values.
- **Placeholder:** Placeholder text for user guidance.
- **InputStyle:** Optional style to customize width and layout.
- **OnValueChanged:** Callback invoked when the text filter value is updated.

## 3. Date Range Filter
Filters data within a date range, such as transaction dates, creation dates, or deadlines.

### Usage Example:
```razor
<FluentGridFilter Property="@(p => p.AccountCreatedDate)" 
                  DataType="@FilterDataType.DateTime" 
                  Placeholder="Select a date" 
                  InputStyle="flex: 1;width:300px" 
                  OnValueChanged="OnFilterChange" />
```

### Description:
- **Property:** Specifies the date property to filter (AccountCreatedDate in this case).
- **DataType:** Set to FilterDataType.DateTime to enable date range filtering.
- **Placeholder:** Instructional text displayed in the filter input.
- **InputStyle:** Style customization for date input fields.
- **OnValueChanged:** Callback invoked when the date range is changed.
  
## Common Callback
Ensure you handle the OnValueChanged callback in your component to apply filters when the user modifies the filter values:

```razor
@code {
    private void OnFilterChange()
    {
        // Apply filters and update the grid
        filterManager.ApplyFilters();
    }
}
```

## Example Grid with Filters
Here’s a sample grid implementation using all three filters:

```razor
<FluentDataGrid Items="@filterManager.Data" ShowHover="true" ResizableColumns="true">
    <PropertyColumn Property="@(p => p.Name)" Title="Name" Sortable="true">
        <ColumnOptions>
            <FluentGridFilter Property="@(p => p.Name)" 
                              DataType="@FilterDataType.Text" 
                              Placeholder="Search by name" 
                              OnValueChanged="OnFilterChange" />
        </ColumnOptions>
    </PropertyColumn>
    <PropertyColumn Property="@(p => p.TotalSales)" Title="Total Sales" Sortable="true" Format="N2" Align="Align.End">
        <ColumnOptions>
            <FluentGridFilter Property="@(p => p.TotalSales)" 
                              DataType="@FilterDataType.Numeric" 
                              Placeholder="Filter by sales" 
                              OnValueChanged="OnFilterChange" />
        </ColumnOptions>
    </PropertyColumn>
    <PropertyColumn Property="@(p => p.AccountCreatedDate)" Title="Account Created" Format="yyyy-MM-dd" Sortable="true">
        <ColumnOptions>
            <FluentGridFilter Property="@(p => p.AccountCreatedDate)" 
                              DataType="@FilterDataType.DateTime" 
                              Placeholder="Filter by date" 
                              OnValueChanged="OnFilterChange" />
        </ColumnOptions>
    </PropertyColumn>
</FluentDataGrid>
```
## Notes
- **DataType:** Always specify the appropriate FilterDataType (e.g., Text, Numeric, DateTime).
- **Placeholder and InputStyle:** These parameters help enhance usability and design consistency.
- **OnValueChanged:** Ensure you handle filter changes to apply the filters to your grid.
- **Validation:** The component automatically validates filter values and shows an error message if required fields are empty.
- **Customization:** Adjust icons, styles, and placeholders to fit your application’s design.

# FluentGridTextFilter Component
The FluentGridTextFilter component is a simple and effective filter designed for text-based columns in a grid. It allows users to filter text data by typing a search term. The component is lightweight, easy to configure, and seamlessly integrates with the FluentDataGrid.

## Features
- **Text Filtering:** Enables filtering by text, matching values in the specified column.
- **Search and Clear Buttons:** Provides intuitive buttons for triggering searches and clearing filters.
- **Customizable Appearance:** Configure placeholders and styles to fit your design requirements.
- 
## Parameters
|Parameter|Type|Description|Default Value|
|---------|----|-----------|-------------|
|Placeholder|`string`|Placeholder text displayed in the input field.|`"Enter text..."`|
|SearchText|`string`|The text entered by the user for filtering.|`""` (empty string)|
|InputStyle|`string`|CSS style for customizing the input field.|`"flex: 1;"`|

## Basic Example
```razor
<FluentGridTextFilter Property="@(p => p.Name)" 
                      Placeholder="Search by name" 
                      InputStyle="flex: 1; width: 200px;" 
                      OnValueChanged="OnFilterChange" />
Property: Specifies the property to filter (e.g., Name).
Placeholder: Displays Search by name in the input box.
InputStyle: Sets a custom width and layout for the filter box.
OnValueChanged: Invokes a callback when the filter is applied.
```

## Example in a Grid
```razor
<FluentDataGrid Items="@filterManager.Data" ResizableColumns="true">
    <PropertyColumn Property="@(p => p.Name)" Title="Name" Sortable="true">
        <ColumnOptions>
            <FluentGridTextFilter Property="@(p => p.Name)" 
                                  Placeholder="Filter by name" 
                                  InputStyle="flex: 1; width: 250px;" 
                                  OnValueChanged="OnFilterChange" />
        </ColumnOptions>
    </PropertyColumn>
</FluentDataGrid>
```

- Adds a text filter to the Name column in a grid.
- Provides an intuitive filtering experience directly within the column options.
