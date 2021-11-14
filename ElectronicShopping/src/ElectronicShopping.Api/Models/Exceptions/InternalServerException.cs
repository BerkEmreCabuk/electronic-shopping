using ElectronicShopping.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public class InternalServerException : BaseException
    {
        public InternalServerException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstant.INTERNAL_SERVER_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstant.INTERNAL_SERVER_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerException(string title, List<string> messages) : base(title, messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerException(string title, string message) : base(title, message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }
    }
}
