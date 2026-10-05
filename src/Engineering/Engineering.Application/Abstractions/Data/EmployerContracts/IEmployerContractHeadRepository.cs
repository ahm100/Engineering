using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Abstractions.Data.EmployerContracts;

public interface IEmployerContractHeadRepository : IBaseRepository<EmployerContractHead>
{
    Task<bool> VerifyCode(
        long? id, string code, long companyId, CT ct);

    Task<EmployerContractHead?> DeleteEContractHeader(
        long id, CT ct);

    Task<EmployerContractHead?> GetEContractHeader(
        long id, CT ct);

    Task<EmployerContractHead?> GetEContractHeaderFull(
        long id, CT ct);

    Task<GetEContractHeaderByIdResponse?> GetEContractHeaderById(
        long id, CT ct);

    Task<(List<GetFltrEContractHeadsModel> Data, int RowCount)> GetFltrEContractHeads(
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? employerIds,
        List<EContractType>? types,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);
}