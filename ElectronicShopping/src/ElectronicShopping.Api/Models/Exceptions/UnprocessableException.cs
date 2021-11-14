using ElectronicShopping.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public class UnprocessableException : BaseException
    {
        public UnprocessableException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstant.UNPROCCESABLE_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstant.UNPROCCESABLE_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(string title, List<string> messages) : base(title, messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(string title, string message) : base(title, message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }

        public UnprocessableException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status422UnprocessableEntity;
        }
    }
}