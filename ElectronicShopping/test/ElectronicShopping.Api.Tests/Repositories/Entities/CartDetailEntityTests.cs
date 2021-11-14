using ElectronicShopping.Api.Enums;
using ElectronicShopping.Api.Repositories.Entities;
using System;
using Xunit;

namespace ElectronicShopping.Api.Tests.Repositories.Entities
{
    public class CartDetailEntityTests
    {
        [Theory, InlineData(1), InlineData(-1)]
        public void AddQuantity_ShouldAssertValidModel_Quantity(int quantity)
        {
            //Arrange
            var cartDetailEntity = new CartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                Cart = new CartEntity()
                {
                    Amount = 100
                }
            };
            var expectedValue = cartDetailEntity.Quantity + quantity;
            //Act
            cartDetailEntity.AddQuantity(quantity);

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Quantity);
            Assert.Equal(expectedValue, cartDetailEntity.Amount);
            Assert.NotNull(cartDetailEntity.Cart.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Cart.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Cart.Amount);
        }

        [Theory, InlineData(2), InlineData(0)]
        public void ChangeQuantity_ShouldAssertValidModel_Quantity(int quantity)
        {
            //Arrange
            var cartDetailEntity = new CartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                Cart = new CartEntity()
                {
                    Amount = 100
                }
            };

            //Act
            cartDetailEntity.ChangeQuantity(quantity);

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
            Assert.Equal(quantity, cartDetailEntity.Quantity);
            Assert.Equal(quantity, cartDetailEntity.Amount);
            Assert.NotNull(cartDetailEntity.Cart.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Cart.Status);
            Assert.Equal(quantity, cartDetailEntity.Cart.Amount);
        }

        [Theory, InlineData(1), InlineData(-1)]
        public void AddAmount_ShouldAssertValidModel_Quantity(int quantity)
        {
            //Arrange
            var cartDetailEntity = new CartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                Cart = new CartEntity()
                {
                    Amount = 100
                }
            };
            var expectedValue = quantity + cartDetailEntity.Quantity;
            //Act
            cartDetailEntity.AddAmount(quantity);

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Amount);
            Assert.NotNull(cartDetailEntity.Cart.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Cart.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Cart.Amount);
        }

        [Theory, InlineData(1, 10), InlineData(-1, 10)]
        public void AddAmount_ShouldAssertValidModel_QuantityAndPrice(int quantity, decimal price)
        {
            //Arrange
            var cartDetailEntity = new CartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                Cart = new CartEntity()
                {
                    Amount = 100
                }
            };
            var expectedValue = quantity * price + cartDetailEntity.Quantity;

            //Act
            cartDetailEntity.AddAmount(quantity, price);

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Amount);
            Assert.NotNull(cartDetailEntity.Cart.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Cart.Status);
            Assert.Equal(expectedValue, cartDetailEntity.Cart.Amount);
        }

        [Fact]
        public void Add_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartDetailEntity = new CartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                Cart = new CartEntity()
                {
                    Amount = 100
                }
            };

            //Act
            cartDetailEntity.Add();

            //Assert
            Assert.NotEqual(DateTime.MinValue, cartDetailEntity.CreateDate);
            Assert.Null(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
        }

        [Fact]
        public void Update_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartDetailEntity = new CartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                Cart = new CartEntity()
                {
                    Amount = 100
                }
            };

            //Act
            cartDetailEntity.Update();

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.ACTIVE, cartDetailEntity.Status);
        }

        [Fact]
        public void Delete_ShouldAssertValidModel_Default()
        {
            //Arrange
            var cartDetailEntity = new CartDetailEntity()
            {
                Amount = 100,
                Quantity = 100,
                Cart = new CartEntity()
                {
                    Amount = 100
                }
            };

            //Act
            cartDetailEntity.Delete();

            //Assert
            Assert.NotNull(cartDetailEntity.UpdateDate);
            Assert.Equal(RecordStatuses.PASSIVE, cartDetailEntity.Status);
        }
    }
}
