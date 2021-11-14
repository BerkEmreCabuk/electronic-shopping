using ElectronicShopping.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstant.NOTFOUND_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstant.NOTFOUND_DETAIL;
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(string title, List<string> messages) : base(title, messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(string title, string message) : base(title, message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }

        public NotFoundException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status404NotFound;
        }
    }
}
