using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelEnums;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewardsExcelExporter;

public record GetFilteredRequestRewardsExcelExporterRequest(
    List<long>? Ids,
    long? CostCenterId,
    long? ProjectId,
    long? ThirdPartyId,
    long? EmployerContractId,
    bool? IsReward,
    DateTime? Fromdate,
    DateTime? Todate,
    RequestRewardStatus? Status,
    RequestRewardType? Type,
    long? ProjectOperationId,
    long? ProjectOperationDetailId,
    List<RequestRewardsExcelEnum>? ExcelFilters,
    string[]? OrderBy,
    int PageIndex,
    int PageSize) : IHttpRequest;
