namespace Engineering.Domain.Errors;

public record FailureModel(
    bool IsSuccess,
    bool IsFailure,
    ErrorModel Error
    );

public record ErrorModel
{
    public int StatusCode { get; set; }
    public string? Code { get; set; }
    public string? Message { get; set; }

};

public record MessageErrorModel
{
    public string? Type { get; set; }
    public int Status { get; set; }
    public string? Ttitle { get; set; }
    public string? Detail { get; set; }

};

public class ErrorMessages
{

}
public class ExceptionMessages
{

}
