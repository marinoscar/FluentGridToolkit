using FluentGridToolkit.Sample.Model;
using System.Linq.Expressions;

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
                new Account { Id = 15, Name = "Cascade Waterworks", Email = "cascade@example.com", TotalSales = 73000.45, State = "Illinois", Country = "USA", AccountCreatedDate = new DateTime(2024, 6, 18), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 16, Name = "ClearFlow Systems", Email = "clearflow@example.com", TotalSales = 185000.75, State = "Colorado", Country = "USA", AccountCreatedDate = new DateTime(2015, 4, 21), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 17, Name = "Precision Pipeworks", Email = "precisionpipeworks@example.com", TotalSales = 120000.80, State = "Georgia", Country = "USA", AccountCreatedDate = new DateTime(2015, 6, 12), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 18, Name = "Pinnacle Plumbing", Email = "pinnacleplumbing@example.com", TotalSales = 250000.20, State = "Oregon", Country = "USA", AccountCreatedDate = new DateTime(2015, 8, 15), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 19, Name = "Apex Water Solutions", Email = "apexwater@example.com", TotalSales = 310000.60, State = "Washington", Country = "USA", AccountCreatedDate = new DateTime(2015, 9, 19), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 20, Name = "TruFlow Plumbing", Email = "truflow@example.com", TotalSales = 145000.45, State = "Arizona", Country = "USA", AccountCreatedDate = new DateTime(2015, 11, 5), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 21, Name = "FlowCraft Plumbing", Email = "flowcraft@example.com", TotalSales = 375000.90, State = "Ohio", Country = "USA", AccountCreatedDate = new DateTime(2015, 12, 25), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 22, Name = "RiverRock Solutions", Email = "riverrock@example.com", TotalSales = 520000.00, State = "Nevada", Country = "USA", AccountCreatedDate = new DateTime(2015, 5, 18), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 23, Name = "WaterWorks Plumbing", Email = "waterworks@example.com", TotalSales = 850000.20, State = "Virginia", Country = "USA", AccountCreatedDate = new DateTime(2015, 7, 24), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 24, Name = "StreamGuard Systems", Email = "streamguard@example.com", TotalSales = 620000.00, State = "North Carolina", Country = "USA", AccountCreatedDate = new DateTime(2015, 8, 17), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 25, Name = "FountainFlow Plumbing", Email = "fountainflow@example.com", TotalSales = 295000.75, State = "Pennsylvania", Country = "USA", AccountCreatedDate = new DateTime(2015, 10, 9), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 26, Name = "Precision Aqua Services", Email = "precisionaqua@example.com", TotalSales = 450000.80, State = "Michigan", Country = "USA", AccountCreatedDate = new DateTime(2015, 11, 28), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 27, Name = "PeakFlow Specialists", Email = "peakflow@example.com", TotalSales = 175000.45, State = "Indiana", Country = "USA", AccountCreatedDate = new DateTime(2015, 3, 13), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 28, Name = "RainRiver Plumbing Co.", Email = "rainriver@example.com", TotalSales = 405000.60, State = "Kentucky", Country = "USA", AccountCreatedDate = new DateTime(2015, 4, 5), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 29, Name = "CrystalFlow Services", Email = "crystalflow@example.com", TotalSales = 150000.00, State = "Minnesota", Country = "USA", AccountCreatedDate = new DateTime(2015, 6, 25), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 30, Name = "FlowSphere Plumbing", Email = "flowsphere@example.com", TotalSales = 430000.25, State = "Missouri", Country = "USA", AccountCreatedDate = new DateTime(2015, 9, 8), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 31, Name = "PrimeFlow Systems", Email = "primeflow@example.com", TotalSales = 900000.00, State = "Tennessee", Country = "USA", AccountCreatedDate = new DateTime(2015, 7, 30), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 32, Name = "SilverStream Plumbing", Email = "silverstream@example.com", TotalSales = 320000.50, State = "Wisconsin", Country = "USA", AccountCreatedDate = new DateTime(2015, 5, 19), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 33, Name = "ClearWaters Co.", Email = "clearwaters@example.com", TotalSales = 225000.60, State = "Alabama", Country = "USA", AccountCreatedDate = new DateTime(2015, 3, 6), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 34, Name = "StreamLine AquaTech", Email = "streamlineaqua@example.com", TotalSales = 290000.75, State = "South Carolina", Country = "USA", AccountCreatedDate = new DateTime(2015, 6, 12), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 35, Name = "BlueSpring Solutions", Email = "bluespring@example.com", TotalSales = 500000.25, State = "Iowa", Country = "USA", AccountCreatedDate = new DateTime(2015, 8, 18), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 36, Name = "GoldenFlow Plumbing", Email = "goldenflow@example.com", TotalSales = 410000.75, State = "Arkansas", Country = "USA", AccountCreatedDate = new DateTime(2015, 3, 7), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 37, Name = "RapidRiver Systems", Email = "rapidriver@example.com", TotalSales = 600000.30, State = "Kansas", Country = "USA", AccountCreatedDate = new DateTime(2015, 4, 22), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 38, Name = "Elite Flow Dynamics", Email = "eliteflow@example.com", TotalSales = 520000.90, State = "Mississippi", Country = "USA", AccountCreatedDate = new DateTime(2015, 6, 13), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 39, Name = "FlowForward Plumbing", Email = "flowforward@example.com", TotalSales = 440000.40, State = "Nebraska", Country = "USA", AccountCreatedDate = new DateTime(2015, 5, 25), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 40, Name = "PureStream Solutions", Email = "purestream@example.com", TotalSales = 350000.25, State = "North Dakota", Country = "USA", AccountCreatedDate = new DateTime(2015, 8, 19), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 41, Name = "ClearWay Plumbing", Email = "clearway@example.com", TotalSales = 285000.75, State = "South Dakota", Country = "USA", AccountCreatedDate = new DateTime(2015, 9, 15), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 42, Name = "FluidMotion Systems", Email = "fluidmotion@example.com", TotalSales = 480000.00, State = "Oklahoma", Country = "USA", AccountCreatedDate = new DateTime(2015, 10, 11), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 43, Name = "DynamicFlow Plumbing", Email = "dynamicflow@example.com", TotalSales = 370000.85, State = "Montana", Country = "USA", AccountCreatedDate = new DateTime(2015, 12, 7), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 44, Name = "SwiftWater Systems", Email = "swiftwater@example.com", TotalSales = 425000.15, State = "Wyoming", Country = "USA", AccountCreatedDate = new DateTime(2015, 3, 19), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 45, Name = "HighPeak Plumbing", Email = "highpeak@example.com", TotalSales = 495000.10, State = "Idaho", Country = "USA", AccountCreatedDate = new DateTime(2015, 5, 27), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 46, Name = "RapidFlow Plumbing Co.", Email = "rapidflowplumbing@example.com", TotalSales = 315000.90, State = "Utah", Country = "USA", AccountCreatedDate = new DateTime(2015, 7, 2), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 47, Name = "PrimeStream Systems", Email = "primestream@example.com", TotalSales = 390000.60, State = "New Mexico", Country = "USA", AccountCreatedDate = new DateTime(2015, 8, 25), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 48, Name = "ClearRiver Plumbing", Email = "clearriver@example.com", TotalSales = 355000.30, State = "Maine", Country = "USA", AccountCreatedDate = new DateTime(2015, 9, 9), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 49, Name = "MountainFlow Systems", Email = "mountainflow@example.com", TotalSales = 420000.80, State = "Vermont", Country = "USA", AccountCreatedDate = new DateTime(2015, 11, 20), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 50, Name = "PeakStream Plumbing", Email = "peakstream@example.com", TotalSales = 515000.25, State = "Connecticut", Country = "USA", AccountCreatedDate = new DateTime(2015, 12, 2), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 51, Name = "FlowGuard Plumbing", Email = "flowguard@example.com", TotalSales = 330000.50, State = "Delaware", Country = "USA", AccountCreatedDate = new DateTime(2015, 6, 6), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 52, Name = "StreamCore Solutions", Email = "streamcore@example.com", TotalSales = 365000.00, State = "Rhode Island", Country = "USA", AccountCreatedDate = new DateTime(2015, 5, 11), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 53, Name = "FreshStream Plumbing", Email = "freshstream@example.com", TotalSales = 290000.20, State = "New Hampshire", Country = "USA", AccountCreatedDate = new DateTime(2015, 8, 16), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 54, Name = "AquaGuard Systems", Email = "aquaguard@example.com", TotalSales = 455000.75, State = "West Virginia", Country = "USA", AccountCreatedDate = new DateTime(2015, 10, 13), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 55, Name = "PrimeFlow Innovations", Email = "primeflowinnovations@example.com", TotalSales = 525000.90, State = "Massachusetts", Country = "USA", AccountCreatedDate = new DateTime(2015, 3, 25), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 56, Name = "RapidWave Plumbing", Email = "rapidwave@example.com", TotalSales = 385000.40, State = "Louisiana", Country = "USA", AccountCreatedDate = new DateTime(2015, 9, 18), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 57, Name = "WaterEdge Solutions", Email = "wateredge@example.com", TotalSales = 310000.25, State = "Kentucky", Country = "USA", AccountCreatedDate = new DateTime(2015, 4, 12), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 58, Name = "EliteStream Services", Email = "elitestream@example.com", TotalSales = 290000.80, State = "Indiana", Country = "USA", AccountCreatedDate = new DateTime(2015, 11, 14), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 59, Name = "FreshWater Dynamics", Email = "freshwater@example.com", TotalSales = 485000.30, State = "Missouri", Country = "USA", AccountCreatedDate = new DateTime(2015, 7, 20), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 60, Name = "TruFlow Waterworks", Email = "truflowwater@example.com", TotalSales = 320000.15, State = "Alabama", Country = "USA", AccountCreatedDate = new DateTime(2015, 6, 5), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 61, Name = "StreamShield Plumbing", Email = "streamshield@example.com", TotalSales = 450000.00, State = "Georgia", Country = "USA", AccountCreatedDate = new DateTime(2015, 5, 30), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 62, Name = "AquaCore Systems", Email = "aquacore@example.com", TotalSales = 405000.70, State = "Oklahoma", Country = "USA", AccountCreatedDate = new DateTime(2015, 9, 23), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 63, Name = "HydroEdge Plumbing", Email = "hydroedge@example.com", TotalSales = 490000.50, State = "Iowa", Country = "USA", AccountCreatedDate = new DateTime(2015, 8, 28), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 64, Name = "RainGuard Innovations", Email = "rainguard@example.com", TotalSales = 370000.40, State = "South Carolina", Country = "USA", AccountCreatedDate = new DateTime(2015, 10, 3), UtcCreatedOn = DateTime.UtcNow, Version = 1 },
                new Account { Id = 65, Name = "PeakStream Dynamics", Email = "peakstreamdynamics@example.com", TotalSales = 510000.25, State = "Arkansas", Country = "USA", AccountCreatedDate = new DateTime(2015, 11, 8), UtcCreatedOn = DateTime.UtcNow, Version = 1 }
            };



            Accounts = accounts.AsQueryable();
        }

        private IEnumerable<Account> SampleExpression(string name, string state, DateTime dateTime, double amount)
        {
            return Accounts.Where(i =>
            (i.Name.Contains(name))
            && (i.AccountCreatedDate > dateTime && i.AccountCreatedDate <= dateTime)
            && (i.State == state)
            && (i.TotalSales > amount)
            );
        }
    }


}
