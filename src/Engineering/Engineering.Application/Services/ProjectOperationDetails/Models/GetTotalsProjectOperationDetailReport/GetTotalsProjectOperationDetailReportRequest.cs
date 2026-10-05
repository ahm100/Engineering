using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Application.Services.ProjectOperationDetails.Models.GetTotalsProjectOperationDetailReport;

public record GetTotalsProjectOperationDetailReportRequest(
    DateTime? StartDate,
    DateTime? EndDate,
    DateTime? CreateFrom,
    DateTime? CreateTo,
    long? CostCenterId,
    List<long>? ProjectIds,
    List<long>? OperationInfoIds,
    List<long>? ProjectOperationIds,
    List<long>? ContractorIds,
    List<ProjectOperationDetailStatus>? Statuses,
    string? LocationFilterData,
    string? DescriptionFilterData,
    decimal? MinimumFinalAmount,
    decimal? MinimumDoneFinalAmount,
    decimal? MinimumRemaindedFinalAmount,
    string? DailyDescription,
    string? FilterData
     ) : IHttpRequest;
