using ApiForMyProjects.Helper;
using Microsoft.Data.SqlClient;
using System.Net;

namespace ApiForMyProjects.ErrorHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;


        public ExceptionMiddleware(RequestDelegate next)
        {

            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (SqlException ex)
            {
                var (error, message) = ex.GetErrorAndMessage();
                await HandleExceptionAsync(httpContext, error, message);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex.Message);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, string error, string message = ResponseMessage.DefaultError)
        {
            var status = (long)HttpStatusCode.BadRequest;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(new MessageHelper()
            {
                statuscode = status,
                Error = error,
                Message = message
            }.ToString());
        }
    }
}
