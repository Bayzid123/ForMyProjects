using System.Net;
using ApiForMyProjects.Helper;
using Microsoft.AspNetCore.Mvc;

namespace ApiForMyProjects.Configurations
{
    public static class ModelValidationHandler
    {
        public static void AddModelValidationResponse(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(api =>
            api.InvalidModelStateResponseFactory = actionContext =>
                {
                    return new BadRequestObjectResult(new MessageHelper
                    {
                        statuscode = (int)HttpStatusCode.NotAcceptable,
                        Error = "Model validation failed",
                        Message ="Model validation failed: " + actionContext.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).JoinAsString(" | ")
                    });
                }
            );
        }
    }
}