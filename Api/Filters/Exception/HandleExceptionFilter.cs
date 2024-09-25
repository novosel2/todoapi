using Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Api.Filters.Exception
{
    public class HandleExceptionFilter : IExceptionFilter
    {
        private readonly ProblemDetailsFactory _problemDetails;

        public HandleExceptionFilter(ProblemDetailsFactory problemDetails)
        {
            _problemDetails = problemDetails;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;
            var httpContext = context.HttpContext;

            ProblemDetails problemDetails = new ProblemDetails();

            if (exception is UserCreationFailedException)
                problemDetails = _problemDetails.CreateProblemDetails(httpContext, 400, "User Creation Failed", detail: exception.Message);

            else if (exception is RoleAssignFailedException)
                problemDetails = _problemDetails.CreateProblemDetails(httpContext, 500, "Role Assign Failed", detail: exception.Message);

            else if (exception is NotFoundException)
                problemDetails = _problemDetails.CreateProblemDetails(httpContext, 404, "Not Found", detail: exception.Message);

            else if (exception is InvalidLoginException)
                problemDetails = _problemDetails.CreateProblemDetails(httpContext, 401, "Invalid Login Information", detail: exception.Message);

            else if (exception is CreationFailedException)
                problemDetails = _problemDetails.CreateProblemDetails(httpContext, 500, "Creation Failed", detail: exception.Message);

            else if (exception is UpdatingFailedException)
                problemDetails = _problemDetails.CreateProblemDetails(httpContext, 500, "Updating Failed", detail: exception.Message);

            else if (exception is ForbiddenException)
                problemDetails = _problemDetails.CreateProblemDetails(httpContext, 403, "Forbidden", detail: exception.Message);

            else if (exception is SaveChangesException)
                problemDetails = _problemDetails.CreateProblemDetails(httpContext, 500, "Save Changes failed", detail: exception.Message);


            context.Result = new ObjectResult(problemDetails);
            context.ExceptionHandled = true;
        }
    }
}
