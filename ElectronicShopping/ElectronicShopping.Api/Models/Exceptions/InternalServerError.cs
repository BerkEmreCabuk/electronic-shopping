using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public class InternalServerError : BaseException
    {
        public InternalServerError()
        {
            ProblemDetailsModel.Title = "Unexpected Error Occured";
            ProblemDetailsModel.Detail = "Status for internal server error";
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerError(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerError(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerError(string title, List<string> messages) : base(title, messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerError(string message, string title) : base(message, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }

        public InternalServerError(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
        }
    }
}
