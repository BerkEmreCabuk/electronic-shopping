using ElectronicShopping.Api.Constants;
using ElectronicShopping.Api.Models.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Xunit;

namespace ElectronicShopping.Api.Tests.Models.Exceptions
{
    public class UnprocessableExceptionTests
    {
        private readonly int _expectedStatusCode = StatusCodes.Status422UnprocessableEntity;
        private readonly string _expectedTitle = ExceptionMessageKeyConstant.UNPROCCESABLE_TITLE;
        private readonly string _expectedDetail = ExceptionMessageKeyConstant.UNPROCCESABLE_DETAIL;

        [Fact]
        public void Constructor_ShouldAssertTrue_Default()
        {
            //Act
            var result = new UnprocessableException();

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Equal(_expectedTitle, result.ProblemDetailsModel.Title);
            Assert.Equal(_expectedDetail, result.ProblemDetailsModel.Detail);
        }

        [Theory, InlineData("test message")]
        public void Constructor_ShouldAssertTrue_Message(string message)
        {
            //Act
            var result = new UnprocessableException(message);

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Equal(message, result.ProblemDetailsModel.Detail);
            Assert.Equal(message, result.ProblemDetailsModel.Title);
        }

        [Theory, InlineData("test message")]
        public void Constructor_ShouldAssertTrue_MessageList(string message)
        {
            //Arrange 
            var messageList = new List<string>() { message };

            //Act
            var result = new UnprocessableException(messageList);

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Contains(message, result.ProblemDetailsModel.Detail);
        }

        [Theory, InlineData("test title","test message")]
        public void Constructor_ShouldAssertTrue_TitleAndMessageList(string title,string message)
        {
            // Arrange
            var messageList = new List<string>() { message };

            //Act
            var result = new UnprocessableException(title,messageList);

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Equal(title, result.ProblemDetailsModel.Title);
            Assert.Contains(message, result.ProblemDetailsModel.Detail);
        }

        [Theory, InlineData("test title", "test message")]
        public void Constructor_ShouldAssertTrue_TitleAndMessage(string title, string message)
        {
            //Act
            var result = new UnprocessableException(title, message);

            //Assert
            Assert.NotNull(result.ProblemDetailsModel);
            Assert.Equal(_expectedStatusCode, result.ProblemDetailsModel.Status);
            Assert.Equal(title, result.ProblemDetailsModel.Title);
            Assert.Contains(message, result.ProblemDetailsModel.Detail);
        }
    }
}
