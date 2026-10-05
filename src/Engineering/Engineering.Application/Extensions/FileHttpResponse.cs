using Gita.Backend.Shared.Domain.Base;

namespace Engineering.Application.Extensions;

public static class FileHttpResponse
{
    public static IResult GetFileHttpResponse(this Result result)
    {
        if (result.IsSuccess)
        {
            return Results.Ok();
        }

#pragma warning disable CS8604 // Possible null reference argument.
        return HandleErrors(result.Error);
#pragma warning restore CS8604 // Possible null reference argument.
    }

    public static IResult HandleErrors(Error error)
    {
        return error.StatusCode switch
        {
            200 => Results.Ok(Result.Create<object>(null, error)),
            400 => ValidationException(error),
            _ => Results.Problem(error.Message, null, title: error.Code, statusCode: error.StatusCode),
        };
    }

    public static IResult ValidationException(Error error)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        Dictionary<string, string[]> data = error.GetData<Dictionary<string, string[]>>();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        string code;
        if (data == null)
        {
            Dictionary<string, string[]> errors = new(0);
            string message = error.Message;
            code = error.Code;
            return Results.ValidationProblem(errors, message, null, null, code);
        }

        string message2 = error.Message;
        code = error.Code;
        return Results.ValidationProblem(data, message2, null, null, code);
    }

    public static IResult NotFoundException(Error error)
    {
        return Results.NotFound(error.Message);
    }

    public static IResult AuthenticationFailedException(Error error)
    {
        return Results.Content(error.Message, null, null, 401);
    }
}
