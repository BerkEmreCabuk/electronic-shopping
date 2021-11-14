using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Repositories.Entities;
using System;
using Xunit;

namespace ElectronicShopping.Api.Tests.Repositories.Entities
{
    public class StockEntityTests
    {
        [Theory, InlineData(1), InlineData(10)]
        public void InsufficientFreeQuantity_ShouldAssertTrue_Quantity(int quantity)
        {
            //Arrange
            var stockEntity = new StockEntity()
            {
                FreeQuantity = 5
            };
            var expected = quantity > stockEntity.FreeQuantity;
            //Act
            var actual = stockEntity.InsufficientFreeQuantity(quantity);

            //Assert
            Assert.Equal(expected, actual);
        }

        [Theory, InlineData(1), InlineData(-1)]
        public void ReduceFreeQuantity_ShouldAssertTrue_Quantity(int quantity)
        {
            //Arrange
            var stockEntity = new StockEntity()
            {
                FreeQuantity = 5
            };
            var expected = stockEntity.FreeQuantity - quantity;
            //Act
            stockEntity.ReduceFreeQuantity(quantity);

            //Assert
            Assert.NotNull(stockEntity.UpdateDate);
            Assert.Equal(expected,stockEntity.FreeQuantity);
            Assert.Equal(RecordStatuses.ACTIVE, stockEntity.Status);
        }

        [Fact]
        public void Add_ShouldAssertValidModel_Default()
        {
            //Arrange
            var stockEntity = new StockEntity();

            //Act
            stockEntity.Add();

            //Assert
            Assert.NotEqual(DateTime.MinValue, stockEntity.CreateDate);
            Assert.Null(stockEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, stockEntity.Status);
        }

        [Fact]
        public void Update_ShouldAssertValidModel_Default()
        {
            //Arrange
            var stockEntity = new StockEntity();

            //Act
            stockEntity.Update();

            //Assert
            Assert.NotNull(stockEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, stockEntity.Status);
        }

        [Fact]
        public void Delete_ShouldAssertValidModel_Default()
        {
            //Arrange
            var stockEntity = new StockEntity();

            //Act
            stockEntity.Delete();

            //Assert
            Assert.NotNull(stockEntity.UpdateDate);
            Assert.Equal(RecordStatuses.PASSIVE, stockEntity.Status);
        }
    }
}
