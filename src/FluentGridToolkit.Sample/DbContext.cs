using FluentGridToolkit.Sample.Model;

namespace FluentGridToolkit.Sample
{
    /// <summary>
    /// Simulates an in-memory database context for Account entities with static data.
    /// </summary>
    public class DbContext
    {
        /// <summary>
        /// List of Account records, exposed as an IQueryable for querying.
        /// </summary>
        public IQueryable<Account> Accounts { get; private set; }

        /// <summary>
        /// Initializes the InMemoryDbDataContext with static sample data.
        /// </summary>
        public DbContext()
        {
            var accounts = new List<Account>
        {
            new Account { Id = 1, Name = "Flow Solutions LLC", Email = "flowsolutions@example.com", TotalSales = 85000.50, State = "California", Country = "USA", AccountCreatedDate = new DateTime(2016, 3, 15), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 2, Name = "PipeMasters Inc.", Email = "pipemasters@example.com", TotalSales = 92000.00, State = "Texas", Country = "USA", AccountCreatedDate = new DateTime(2017, 7, 22), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 3, Name = "Drain Wizard Plumbing", Email = "drainwizard@example.com", TotalSales = 45000.75, State = "Florida", Country = "USA", AccountCreatedDate = new DateTime(2015, 11, 10), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 4, Name = "AquaFlow Services", Email = "aquaflow@example.com", TotalSales = 78000.25, State = "New York", Country = "USA", AccountCreatedDate = new DateTime(2018, 5, 5), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 5, Name = "BlueWave Plumbing Co.", Email = "bluewave@example.com", TotalSales = 67000.40, State = "Illinois", Country = "USA", AccountCreatedDate = new DateTime(2019, 9, 12), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 6, Name = "Allied Pipeworks", Email = "alliedpipeworks@example.com", TotalSales = 54000.60, State = "California", Country = "USA", AccountCreatedDate = new DateTime(2020, 1, 25), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 7, Name = "SwiftFlow Plumbing", Email = "swiftflow@example.com", TotalSales = 63000.90, State = "Texas", Country = "USA", AccountCreatedDate = new DateTime(2021, 6, 15), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 8, Name = "PureWater Technologies", Email = "purewater@example.com", TotalSales = 82000.00, State = "Florida", Country = "USA", AccountCreatedDate = new DateTime(2022, 4, 10), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 9, Name = "Metro Plumbing Solutions", Email = "metroplumbing@example.com", TotalSales = 75000.30, State = "New York", Country = "USA", AccountCreatedDate = new DateTime(2023, 2, 5), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 10, Name = "Riverside Plumbing Co.", Email = "riverside@example.com", TotalSales = 90000.00, State = "Illinois", Country = "USA", AccountCreatedDate = new DateTime(2023, 11, 1), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 11, Name = "Precision Plumbing Group", Email = "precisionplumbing@example.com", TotalSales = 48000.25, State = "California", Country = "USA", AccountCreatedDate = new DateTime(2015, 8, 20), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 12, Name = "Elite Drain Specialists", Email = "elitedrain@example.com", TotalSales = 64000.50, State = "Texas", Country = "USA", AccountCreatedDate = new DateTime(2016, 10, 15), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 13, Name = "HydroPlumb Services", Email = "hydroplumb@example.com", TotalSales = 59000.80, State = "Florida", Country = "USA", AccountCreatedDate = new DateTime(2017, 3, 25), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 14, Name = "Streamline Plumbing Inc.", Email = "streamline@example.com", TotalSales = 86000.20, State = "New York", Country = "USA", AccountCreatedDate = new DateTime(2021, 7, 1), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
            new Account { Id = 15, Name = "Cascade Waterworks", Email = "cascade@example.com", TotalSales = 73000.45, State = "Illinois", Country = "USA", AccountCreatedDate = new DateTime(2024, 6, 18), UtcCreatedOn = DateTime.UtcNow, Version = 1 }
        };

            Accounts = accounts.AsQueryable();
        }
    }

}
