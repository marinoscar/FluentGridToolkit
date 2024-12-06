using System.Linq.Expressions;

namespace FluentGridToolkit.Tests
{
    public class GridFilterManagerTests
    {
        private IQueryable<TestEntity> GetTestEntities()
        {
            return new List<TestEntity>
        {
            new TestEntity { Id = 1, Name = "Alice", Age = 25, CreatedDate = new DateTime(2023, 1, 1) },
            new TestEntity { Id = 2, Name = "Bob", Age = 30, CreatedDate = new DateTime(2023, 5, 15) },
            new TestEntity { Id = 3, Name = "Charlie", Age = 35, CreatedDate = new DateTime(2024, 1, 1) }
        }.AsQueryable();
        }

        [Fact]
        public void Constructor_NullBaseQuery_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new GridFilterManager<TestEntity>(null));
        }

        [Fact]
        public void AddOrUpdateFilter_NullColumn_ThrowsArgumentNullException()
        {
            // Arrange
            var manager = new GridFilterManager<TestEntity>(GetTestEntities());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => manager.AddOrUpdateFilter(null, e => e.Age > 30));
        }

        [Fact]
        public void AddOrUpdateFilter_NullFilter_ThrowsArgumentNullException()
        {
            // Arrange
            var manager = new GridFilterManager<TestEntity>(GetTestEntities());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => manager.AddOrUpdateFilter("Age", null));
        }

        [Fact]
        public void AddOrUpdateFilter_ValidInput_AddsFilter()
        {
            // Arrange
            var manager = new GridFilterManager<TestEntity>(GetTestEntities());
            Expression<Func<TestEntity, bool>> filter = e => e.Age > 30;

            // Act
            manager.AddOrUpdateFilter("Age", filter);

            // Assert
            var result = manager.ApplyFilters().ToList();
            Assert.Single(result);
            Assert.Equal("Charlie", result.First().Name);
        }

        [Fact]
        public void RemoveFilter_ValidColumn_RemovesFilter()
        {
            // Arrange
            var manager = new GridFilterManager<TestEntity>(GetTestEntities());
            manager.AddOrUpdateFilter("Age", e => e.Age > 30);

            // Act
            manager.RemoveFilter("Age");

            // Assert
            var result = manager.ApplyFilters().ToList();
            Assert.Equal(3, result.Count); // Should return all items since the filter was removed
        }

        [Fact]
        public void RemoveFilter_NullColumn_ThrowsArgumentNullException()
        {
            // Arrange
            var manager = new GridFilterManager<TestEntity>(GetTestEntities());

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => manager.RemoveFilter(null));
        }

        [Fact]
        public void ClearFilters_ClearsAllFilters()
        {
            // Arrange
            var manager = new GridFilterManager<TestEntity>(GetTestEntities());
            manager.AddOrUpdateFilter("Age", e => e.Age > 30);
            manager.AddOrUpdateFilter("Name", e => e.Name.Contains("Alice"));

            // Act
            manager.ClearFilters();

            // Assert
            var result = manager.ApplyFilters().ToList();
            Assert.Equal(3, result.Count); // Should return all items since all filters were cleared
        }

        [Fact]
        public void ApplyFilters_CombinesFilters_ReturnsFilteredData()
        {
            // Arrange
            var manager = new GridFilterManager<TestEntity>(GetTestEntities());
            manager.AddOrUpdateFilter("Age", e => e.Age > 25);
            manager.AddOrUpdateFilter("Name", e => e.Name.Contains("Bob"));

            // Act
            var result = manager.ApplyFilters().ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Bob", result.First().Name);
        }
    }

    public class TestEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime CreatedDate { get; set; }
    }

}
