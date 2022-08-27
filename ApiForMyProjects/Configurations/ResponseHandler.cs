using System.Net;
using ApiForMyProjects.Helper;
using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;

public class ResponseHandler : ControllerBase
{
    protected new IActionResult Ok<T>(Result<T> result) => OkTResult(result);
    protected new IActionResult Created<T>(Result<T> result) => CreatedTResult(result);
    protected IActionResult NotFound() => NotFoundTResult();
    protected IActionResult NotFound(string Message) => NotFoundTResult(Message);
    protected IActionResult BadRequest() => BadRequestTResult();
    protected IActionResult BadRequest(string Message) => BadRequestTResult(Message);


    private IActionResult BadRequestTResult(string Message = "Not Found") => base.BadRequest(new MessageHelper(Message, (long)HttpStatusCode.BadRequest));
    private IActionResult NotFoundTResult(string Message = "Not Found") => base.NotFound(new MessageHelper(Message, (long)HttpStatusCode.NotFound));
    private IActionResult OkTResult<T>(Result<T> result) => result.Match<IActionResult>(OnSuccessOk<T>(), OnFail());
    private IActionResult CreatedTResult<T>(Result<T> result) => result.Match<IActionResult>(OnSuccessCreated<T>(), OnFail());


    private Func<T, IActionResult> OnSuccessOk<T>() => success => base.Ok(success);

    private Func<T, IActionResult> OnSuccessCreated<T>() => success => base.Created(nameof(T), success);

    private Func<Exception, IActionResult> OnFail() => failure => BadRequest(new MessageHelper(failure.Message, (long)HttpStatusCode.BadRequest));

    private IActionResult CheckResponse(object value)
    {
        var data = (MessageHelper)value;

        return data.statuscode switch
        {
            (long)HttpStatusCode.Created => base.Created(string.Empty, value),
            (long)HttpStatusCode.BadRequest => base.BadRequest(value),
            (long)HttpStatusCode.NotFound => base.NotFound(value),
            (long)HttpStatusCode.InternalServerError => base.StatusCode((int)HttpStatusCode.InternalServerError, value),
            (long)HttpStatusCode.OK => base.Ok(value),
            _ => base.StatusCode((int)HttpStatusCode.InternalServerError, value)
        };
    }
}