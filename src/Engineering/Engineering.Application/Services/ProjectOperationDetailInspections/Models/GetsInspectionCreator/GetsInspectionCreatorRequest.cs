
namespace Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionCreator;

public record GetsInspectionCreatorRequest(
    string? FilterData,
    int PageIndex,
    int PageSize
     ) : IHttpRequest;
