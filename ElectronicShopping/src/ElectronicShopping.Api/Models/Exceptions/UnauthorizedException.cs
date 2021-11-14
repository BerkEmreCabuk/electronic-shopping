using ElectronicShopping.Api.Constants;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public class UnauthorizedException : BaseException
    {
        public UnauthorizedException()
        {
            ProblemDetailsModel.Title = ExceptionMessageKeyConstant.UNAUTHORIZED_TITLE;
            ProblemDetailsModel.Detail = ExceptionMessageKeyConstant.UNAUTHORIZED_DETAIL;
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

        public UnauthorizedException(string title, string message) : base(title, message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }

        public UnauthorizedException(string message, string title, IDictionary<string, object> extensions) : base(message, title, extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status401Unauthorized;
        }
    }
}
