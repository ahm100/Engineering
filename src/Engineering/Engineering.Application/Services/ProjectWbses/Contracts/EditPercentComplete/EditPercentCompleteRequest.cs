namespace Engineering.Application.Services.ProjectWbses.Contracts.EditPercentComplete;

public record EditPercentCompleteRequest(
    long Id,
    decimal? Percent) : IHttpRequest;