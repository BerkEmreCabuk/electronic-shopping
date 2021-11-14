using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ElectronicShopping.Api.Tests.Repositories.Entities
{
    public class CartEntityTests
    {
        [Theory, InlineData(1), InlineData(-1)]
        public void AddAmount_ShouldAssertValidModel_QuantityAndPrice(decimal amount)
        {
            //Arrange
            var cartEntity = new CartEntity()
            {
                Amount = 100
            };

            var expectedValue = cartEntity.Amount + amount;

            //Act
            cartEntity.AddAmount(amount);

            //Assert
            Assert.NotNull(cartEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartEntity.Status);
            Assert.Equal(expectedValue, cartEntity.Amount);
        }

        [Fact]
        public void Add_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartEntity = new CartEntity();

            //Act
            cartEntity.Add();

            //Assert
            Assert.NotEqual(DateTime.MinValue, cartEntity.CreateDate);
            Assert.Null(cartEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartEntity.Status);
        }

        [Fact]
        public void Update_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartEntity = new CartEntity();

            //Act
            cartEntity.Update();

            //Assert
            Assert.NotNull(cartEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartEntity.Status);
        }

        [Fact]
        public void Delete_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartEntity = new CartEntity();

            //Act
            cartEntity.Delete();

            //Assert
            Assert.NotNull(cartEntity.UpdateDate);
            Assert.Equal(RecordStatuses.PASSIVE, cartEntity.Status);
        }
    }
}
