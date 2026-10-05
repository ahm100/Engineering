using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Services.RequestContractors.Models.GetFilteredRequestContractors;

public record GetFilteredRequestContractorsRequest(
        List<long>? Ids,
        List<long>? CostCenterIds,
        List<long>? ProjectIds,
        List<long>? ProjectOperationIds,
        List<long>? ProjectOperationDetailIds,
        List<long>? ServiceInfoIds,
        List<long>? ContractorIds,
        RequestContractorStatus? Status,
        DateTime? FromDate,
        DateTime? ToDate,
        long? CreatorId,
        string? FilterData,
        string[]? OrderBy,
        long? CompanyId,
        int PageIndex,
        int PageSize) : IHttpRequest;
