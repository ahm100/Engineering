using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Application.Abstractions.Data.EmployerContracts;

public interface IEmployerContractRepository : IBaseRepository<EmployerContract>
{
    Task<EmployerContract?> GetEConteract(
        long id, CT ct);

    Task<List<EmployerContract>?> GetEConteracts(
        List<long> ids, CT ct);

    Task<EmployerContract?> DeleteEContract(
        long id, CT ct);

    Task<GetEContractByIdResponse?> GetEContractById(
        long id, CT ct);

    Task<(List<GetFltrEContractsModel> Data, int RowCount)> GetFltrEContracts(
        long? headId,
        List<long>? Ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? employerIds,
        List<EContractType>? types,
        List<EContractStatus>? statuses,
        List<EContractStatus>? removeStatuses,
        bool? isPrimaryManager,
        bool? isFinalManager,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<long>> GetFltrEmployers(
        List<long>? costCenterIds,
        List<long>? projectIds,
        CT ct);

    Task<bool> VerifyCode(
        long? id,
        string code,
        long contractHeadId,
        CT ct);

    Task<string> CodeCreator(
        long contractHeadId, CT ct);

    Task<(List<GetECThirdPartiesModel> Data, int RowCount)> GetECThirdParties(
            long projectId,
            List<long>? employerIds,
            int pageIndex,
            int pageSize,
            CT ct);
}