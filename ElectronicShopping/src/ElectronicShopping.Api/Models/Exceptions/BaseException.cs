using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ElectronicShopping.Api.Models.Exceptions
{
    public abstract class BaseException : Exception
    {
        public ProblemDetails ProblemDetailsModel = new ProblemDetails();
        public BaseException() : base()
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
            ProblemDetailsModel.Extensions.Add("DocumentUrl", "http://localhost:5000/swagger/index.html");
        }
        public BaseException(string message)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
            ProblemDetailsModel.Title = message;
            ProblemDetailsModel.Detail = message;
            ProblemDetailsModel.Extensions.Add("DocumentUrl", "http://localhost:5000/swagger/index.html");
        }

        public BaseException(List<string> messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
            ProblemDetailsModel.Detail = string.Join("/ ", messages);
            ProblemDetailsModel.Extensions.Add("DocumentUrl", "http://localhost:5000/swagger/index.html");
        }

        public BaseException(string title, List<string> messages)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
            ProblemDetailsModel.Title = title;
            ProblemDetailsModel.Detail = string.Join("/ ", messages);
            ProblemDetailsModel.Extensions.Add("DocumentUrl", "http://localhost:5000/swagger/index.html");
        }

        public BaseException(string message, string title)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
            ProblemDetailsModel.Title = !string.IsNullOrEmpty(title) ? title : message;
            ProblemDetailsModel.Detail = message;
            ProblemDetailsModel.Extensions.Add("DocumentUrl", "http://localhost:5000/swagger/index.html");
        }

        public BaseException(string message, string title, IDictionary<string, object> extensions)
        {
            ProblemDetailsModel.Status = StatusCodes.Status500InternalServerError;
            ProblemDetailsModel.Title = !string.IsNullOrEmpty(title) ? title : message;
            ProblemDetailsModel.Detail = message;
            if (extensions != null)
            {
                foreach (var extension in extensions)
                {
                    ProblemDetailsModel.Extensions.Add(extension.Key, extension.Value);
                }
            }
            ProblemDetailsModel.Extensions.Add("DocumentUrl", "http://localhost:5000/swagger/index.html");
        }
    }
}
