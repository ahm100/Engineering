using Engineering.Application.Services.ProjectOperationDetails.Models.GetFilteredProjectOperationDetails;

namespace Engineering.Application.Services.ProjectOperationDetails.Queries.GetFilteredProjectOperationDetails;

public record GetFilteredProjectOperationDetailsQuery(
    long CostCenterId,
    List<long>? ProjectIds,
    List<long>? CategoryIds,
    List<long>? BranchIds,
    List<long>? SeasonIds,
    List<long>? OperationInfoIds,
    string? FilterData,
    int PageIndex,
    int PageSize
    ) : IQuery<GetFilteredProjectOperationDetailsResponse>;


