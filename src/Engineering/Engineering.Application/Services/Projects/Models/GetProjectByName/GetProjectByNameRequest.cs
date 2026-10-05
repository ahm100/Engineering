namespace Engineering.Application.Services.Projects.Models.GetProjectByName;

public record GetProjectByNameRequest(
    string ProjectName,
    long? CostCenterId
     ) : IHttpRequest;
