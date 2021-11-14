using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException()
        {
            ProblemDetailsModel.Title = "Unauthorized";
            ProblemDetailsModel.Detail = "Status for unauthorized request";
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(string message) : base(message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(List<string> messages) : base(messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(string title, List<string> messages) : base(title, messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(string message, string title) : base(message, title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }
    }
}
