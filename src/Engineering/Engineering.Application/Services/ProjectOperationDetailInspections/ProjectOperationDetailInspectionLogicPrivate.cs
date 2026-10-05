using Engineering.Application.Services.ProjectOperationDetailInspections.Models.GetsInspectionReport;
using Engineering.Application.Services.ProjectOperationDetailInspections.Queries.GetsInspectionReport;

namespace Engineering.Application.Services.ProjectOperationDetailInspections;

public partial class ProjectOperationDetailInspectionLogic : IProjectOperationDetailInspectionLogic
{
    public async Task<Result<(List<GetsInspectionReportModel>? data, int count)>> GetFilteredInspectionReports(
        List<long>? Ids,
        List<long>? CostCenterIds,
        List<long>? ProjectIds,
        List<long>? OperationInfoIds,
        List<long>? OperationLocationIds,
        List<long>? ProjectOperationIds,
        List<long>? ProjectOperationDetailIds,
        List<long>? CreatorIds,
        DateTime? FromDate,
        DateTime? ToDate,
        string? FilterData,
        string[]? OrderBy,
        int PageIndex,
        int PageSize,
        bool IsTotal,
        IMediator mediator,
        CT ct)
    {
        var getFiltered = await mediator.Send(new GetsInspectionReportQuery(
            Ids,
            CostCenterIds,
            ProjectIds,
            OperationInfoIds,
            OperationLocationIds,
            ProjectOperationIds,
            ProjectOperationDetailIds,
            CreatorIds,
            FromDate,
            ToDate,
            FilterData,
            OrderBy,
            PageIndex,
            PageSize),
            ct);
        if (getFiltered.IsFailure)
            return Result.Failure<(List<GetsInspectionReportModel>? data, int count)>(getFiltered.Error!);
        var inspections = getFiltered.Value!.Data!;

        if (IsTotal == false)
        {
            var creatorIds = inspections.Where(x => x.CreatorId is not null && x.CreatorId > 0).Select(x => (long)x.CreatorId!).Distinct().ToList();
            var creators = await WebServicesLogic.UserDataReceiver(creatorIds, null, _mediator, ct);

            inspections.ForEach(item =>
            {
                item.Creator = creators?.FirstOrDefault(x => x.UserId == item.CreatorId)?.FullName;
            });
        }

        return (inspections, getFiltered.Value?.RowCount ?? 0);
    }

}
