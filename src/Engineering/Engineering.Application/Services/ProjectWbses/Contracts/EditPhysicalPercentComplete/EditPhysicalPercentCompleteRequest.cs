namespace Engineering.Application.Services.ProjectWbses.Contracts.EditPhysicalPercentComplete;

public record EditPhysicalPercentCompleteRequest(
    long Id,
    decimal? Percent) : IHttpRequest;