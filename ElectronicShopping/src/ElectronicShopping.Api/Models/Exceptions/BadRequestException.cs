using ElectronicShopping.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public class BadRequestException : BaseException
    {
        public BadRequestException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstant.BAD_REQUEST_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstant.BAD_REQUEST_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }
        public BadRequestException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(string title, List<string> messages) : base(title, messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(string title, string message) : base(title,message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }

        public BadRequestException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status400BadRequest;
        }
    }
}
