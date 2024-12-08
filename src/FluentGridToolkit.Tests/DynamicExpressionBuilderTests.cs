using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentGridToolkit.Tests
{
    using FluentGridToolkit.Sample.Model;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Security.Principal;
    using Xunit;

    public class DynamicExpressionBuilderTests
    {
        private List<Account> GetSampleAccounts()
        {
            return new List<Account>
            {
            new Account { Id = 1, Name = "John Doe", Email = "john@example.com", TotalSales = 500.50, State = "TX", Country = "USA", AccountCreatedDate = DateTime.UtcNow.AddMonths(-1) },
            new Account { Id = 2, Name = "Jane Smith", Email = "jane@example.com", TotalSales = 200.00, State = "CA", Country = "USA", AccountCreatedDate = DateTime.UtcNow.AddMonths(-2) },
            new Account { Id = 3, Name = "Bob Johnson", Email = "bob@example.com", TotalSales = 1000.00, State = "NY", Country = "USA", AccountCreatedDate = DateTime.UtcNow.AddMonths(-3) },
            new Account { Id = 4, Name = "Alice Brown", Email = "alice@example.com", TotalSales = 150.75, State = "TX", Country = "USA", AccountCreatedDate = DateTime.UtcNow.AddMonths(-4) }
        };
        }

        [Fact]
        public void Filter_Accounts_By_Name_Contains()
        {
            // Arrange
            var filters = new List<FilterExpression>
        {
            new FilterExpression
            {
                PropertyName = "Name",
                MethodName = "Contains",
                Value = "ice",
                BinaryExpression = FluentGridToolkit.BinaryOperation.And
            }
        };

            var accounts = GetSampleAccounts();

            // Act
            var predicate = DynamicExpressionBuilder.BuildExpression<Account>(filters);
            var result = accounts.AsQueryable().Where(predicate).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("Alice Brown", result[0].Name);
        }

        [Fact]
        public void Filter_Accounts_By_TotalSales_GreaterThan()
        {
            // Arrange
            var filters = new List<FilterExpression>
        {
            new FilterExpression
            {
                PropertyName = "TotalSales",
                Operator = ComparisonOperator.GreaterThan,
                Value = 300.0,
                BinaryExpression = FluentGridToolkit.BinaryOperation.And
            }
        };

            var accounts = GetSampleAccounts();

            // Act
            var predicate = DynamicExpressionBuilder.BuildExpression<Account>(filters);
            var result = accounts.AsQueryable().Where(predicate).ToList();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void Filter_Accounts_By_Complex_Conditions()
        {
            // Arrange
            var filters = new List<FilterExpression>
        {
            new FilterExpression
            {
                PropertyName = "Name",
                MethodName = "Contains",
                Value = "John",
                BinaryExpression = FluentGridToolkit.BinaryOperation.And
            },
            new FilterExpression
            {
                PropertyName = "AccountCreatedDate",
                Operator = ComparisonOperator.GreaterThan,
                Value = DateTime.UtcNow.AddMonths(-3),
                BinaryExpression = FluentGridToolkit.BinaryOperation.And
            },
            new FilterExpression
            {
                PropertyName = "State",
                Operator = ComparisonOperator.Equal,
                Value = "TX",
                BinaryExpression = FluentGridToolkit.BinaryOperation.And
            },
            new FilterExpression
            {
                PropertyName = "TotalSales",
                Operator = ComparisonOperator.GreaterThan,
                Value = 100.0,
                BinaryExpression = FluentGridToolkit.BinaryOperation.And
            }
        };

            var accounts = GetSampleAccounts();

            // Act
            var predicate = DynamicExpressionBuilder.BuildExpression<Account>(filters);
            var result = accounts.AsQueryable().Where(predicate).ToList();

            // Assert
            Assert.Single(result);
            Assert.Equal("John Doe", result[0].Name);
        }

        [Fact]
        public void Filter_Accounts_With_Or_Condition()
        {
            // Arrange
            var filters = new List<FilterExpression>
        {
            new FilterExpression
            {
                PropertyName = "State",
                Operator = ComparisonOperator.Equal,
                Value = "TX",
                BinaryExpression = FluentGridToolkit.BinaryOperation.Or
            },
            new FilterExpression
            {
                PropertyName = "State",
                Operator = ComparisonOperator.Equal,
                Value = "NY",
                BinaryExpression = FluentGridToolkit.BinaryOperation.Or
            }
        };

            var accounts = GetSampleAccounts();

            // Act
            var predicate = DynamicExpressionBuilder.BuildExpression<Account>(filters);
            var result = accounts.AsQueryable().Where(predicate).ToList();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void Filter_Throws_Exception_When_Method_Not_Found()
        {
            // Arrange
            var filters = new List<FilterExpression>
        {
            new FilterExpression
            {
                PropertyName = "Name",
                MethodName = "NonExistentMethod",
                Value = "John",
                BinaryExpression = FluentGridToolkit.BinaryOperation.And
            }
        };

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                DynamicExpressionBuilder.BuildExpression<Account>(filters));
        }

        [Fact]
        public void Filter_Throws_Exception_When_No_Filters_Provided()
        {
            // Arrange
            var filters = new List<FilterExpression>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                DynamicExpressionBuilder.BuildExpression<Account>(filters));
        }

        [Fact]
        public void Filter_Accounts_With_AndAlso_Condition()
        {
            // Arrange
            var filters = new List<FilterExpression>
    {
        new FilterExpression
        {
            PropertyName = "State",
            Operator = ComparisonOperator.Equal,
            Value = "TX",
            BinaryExpression = BinaryOperation.AndAlso
        },
        new FilterExpression
        {
            PropertyName = "TotalSales",
            Operator = ComparisonOperator.GreaterThan,
            Value = 100.0,
            BinaryExpression = BinaryOperation.AndAlso
        }
    };

            var accounts = GetSampleAccounts();

            // Act
            var predicate = DynamicExpressionBuilder.BuildExpression<Account>(filters);
            var result = accounts.AsQueryable().Where(predicate).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.All(result, account => Assert.Equal("TX", account.State));
            Assert.All(result, account => Assert.True(account.TotalSales > 100.0));
        }

        [Fact]
        public void Filter_Accounts_With_OrElse_Condition()
        {
            // Arrange
            var filters = new List<FilterExpression>
    {
        new FilterExpression
        {
            PropertyName = "State",
            Operator = ComparisonOperator.Equal,
            Value = "CA",
            BinaryExpression = BinaryOperation.OrElse
        },
        new FilterExpression
        {
            PropertyName = "State",
            Operator = ComparisonOperator.Equal,
            Value = "NY",
            BinaryExpression = BinaryOperation.OrElse
        }
    };

            var accounts = GetSampleAccounts();

            // Act
            var predicate = DynamicExpressionBuilder.BuildExpression<Account>(filters);
            var result = accounts.AsQueryable().Where(predicate).ToList();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, account => account.State == "CA");
            Assert.Contains(result, account => account.State == "NY");
        }

    }

}
