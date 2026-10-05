using Engineering.Application.Services.RequestContractors.Models.GetFilteredRequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;
using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Application.Abstractions.Data.RequestContractors;

public interface IRequestContractorRepository : IBaseRepository<RequestContractor>
{
    Task<(List<GetFilteredRequestContractorsModel> Data, int RowCount)> GetFiltered(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? serviceInfoIds,
        List<long>? contractorIds,
        RequestContractorStatus? status,
        DateTime? fromDate,
        DateTime? toDate,
        long? creatorId,
        string? filterData,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<bool> IsRequestContractorDuplicate(
        long projectOperationDetail,
        long serviceInfoId,
        CT ct);

    Task<RequestContractor?> GetById(
        long requestContractorId,
        CT ct);

    Task<GetRequestContractorByIdResponse?> GetModelById(
        long requestContractorId,
        CT ct);

    Task<List<RequestContractor>?> GetByIds(
        List<long> requestContractorIds,
        CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredRequester(CT ct);

    Task<(List<long> Data, int RowCount)> GetsFilteredContractor(
        List<long>? costCenterIds,
        List<long>? projectIds,
        CT ct);
}
