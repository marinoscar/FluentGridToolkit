# FluentGridToolkit

FluentGridToolkit is a powerful and easy-to-use library designed to extend the functionality of the FluentDataGrid component from Microsoft's Fluent UI. This toolkit provides advanced filtering capabilities for numeric, text, and date range columns, allowing developers to create rich, interactive grids effortlessly.

# Features
- **FluentGridFilter:** Filters grid data by numeric, text, and date values.
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
