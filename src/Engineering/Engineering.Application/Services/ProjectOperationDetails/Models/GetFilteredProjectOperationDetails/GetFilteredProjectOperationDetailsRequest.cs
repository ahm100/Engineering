namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetFilteredProjectOperationDetails
{
    public record GetFilteredProjectOperationDetailsRequest(
        long CostCenterId,
        List<long>? ProjectIds,
        List<long>? CategoryIds,
        List<long>? BranchIds,
        List<long>? SeasonIds,
        List<long>? OperationInfoIds,
        string? FilterData,
        int PageIndex,
        int PageSize
    ) : IHttpRequest;
}
