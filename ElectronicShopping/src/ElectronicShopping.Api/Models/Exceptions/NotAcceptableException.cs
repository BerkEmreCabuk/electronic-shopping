using ElectronicShopping.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public class NotAcceptableException : BaseException
    {
        public NotAcceptableException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstant.NOTACCEPTABLE_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstant.NOTACCEPTABLE_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(string title, List<string> messages) : base(title, messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(string title, string message) : base(title, message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }

        public NotAcceptableException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status406NotAcceptable;
        }
    }
}
