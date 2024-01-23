using HotelManagementRepositoryStructure.Model;

using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HotelManagementRepositoryStructure.Exceptions

{
    public class ApplicationExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public ApplicationExceptionFilterAttribute()
        {

        }
        public override void OnException(ExceptionContext context)
        {
            //DEfault response skelton on exception
            var problemDetails = new SystemProblemDetail()
            {
                Instance = UriHelper.GetDisplayUrl(context.HttpContext.Request),
                Title = context.Exception.Message,
                Detail = context.Exception.InnerException?.Message,
                //Status = StatusCodes.Status500InternalServerError,
                //Title = "An error occurred",
                // Detail = context.Exception.Message

            };

            //_logger.Error(problemDetails.Detail);
            switch (context.Exception)
            {
                case ValidationException _:
                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest;
                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                    context.Result = BuildErrorResponse(problemDetails); //new BadRequestObjectResult(problemDetails);
                    return;
                case BadRequestException _:
                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest;
                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                    context.Result = BuildErrorResponse(problemDetails); //new BadRequestObjectResult(problemDetails);
                    return;
                case ConflictException _:
                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status409Conflict;
                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                    context.Result = BuildErrorResponse(problemDetails); //new ConflictObjectResult(problemDetails);
                    return;
                case EntityNotFoundException _:
                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound;
                    // problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                    context.Result = BuildErrorResponse(problemDetails); // new NotFoundObjectResult(problemDetails);
                    return;
                case NoContentException _:
                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
                    // problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status200OK;
                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                    context.Result = BuildErrorResponse(problemDetails); // new NotFoundObjectResult(problemDetails);
                    return;
                case ForbiddenException _:
                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden;
                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                    context.Result = BuildErrorResponse(problemDetails); //new ObjectResult(problemDetails);
                    return;
                case UnauthorizedException _:
                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                    context.Result = BuildErrorResponse(problemDetails); //new ObjectResult(problemDetails);
                    return;
                case InternalServerException _:
                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                    context.Result = BuildErrorResponse(problemDetails); //new ObjectResult(problemDetails);
                    return;
                //case Exception _:
                //    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                //    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                //    context.Result = BuildErrorResponse(problemDetails);// new ObjectResult(problemDetails);
                //    return;
                //case TcIntegrationException _:
                //    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
                //    problemDetails.SubStatus = 101;
                //    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                //    context.Result = BuildErrorResponse(problemDetails); //new ObjectResult(problemDetails);
                //    return;
                //case TcInvalidPhoneNumberException _:
                //    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
                //    problemDetails.SubStatus = 102;
                //    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;
                //    context.Result = BuildErrorResponse(problemDetails); //new ObjectResult(problemDetails);
                //    return;
                default:

                    problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError;
                    // var defaultResult = new ObjectResult(problemDetails);
                    //defaultResult.StatusCode = problemDetails.Status;
                    if (context.Exception.GetType() == typeof(System.Web.Http.HttpResponseException))
                    {
                        if (((System.Web.Http.HttpResponseException)context.Exception).Response != null)
                        {
                            var statusCode = ((System.Web.Http.HttpResponseException)context.Exception).Response.StatusCode;
                            if ((int)statusCode == Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)
                            {
                                problemDetails.Status = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
                                var errorMsg = ((System.Web.Http.HttpResponseException)context.Exception).Response.ReasonPhrase;
                                problemDetails.Title = errorMsg;
                            }
                        }
                    }

                    context.HttpContext.Response.StatusCode = (int)problemDetails.Status;

                    context.Result = BuildErrorResponse(problemDetails);// new ObjectResult(problemDetails);
                    return;
            }
        }
        public static JsonResult BuildErrorResponse(SystemProblemDetail problemDetails)
        {
            JsonResponse model = new JsonResponse();
            model.success = false;
            model.data = null;

            if (problemDetails.Status == 200)
            {
                model.success = true;
                model.data = new List<string>();
            }


            MetaData objmetadata = new MetaData();
            model.metadata = objmetadata;

            if (problemDetails.Status.HasValue)
            {
                Error objerror = new Error();
                objerror.code = problemDetails.Status;
                objerror.subCode = problemDetails.SubStatus;
                objerror.detail = problemDetails.Detail;
                objerror.message = problemDetails.Title;
                objerror.isUserError = problemDetails.Status == Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError ? false : true;
                model.error = objerror;
            }

            return new JsonResult(model);
        }
    }
}
