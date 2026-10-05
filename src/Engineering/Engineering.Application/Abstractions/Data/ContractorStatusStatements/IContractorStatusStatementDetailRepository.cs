using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementFContracts;
using Engineering.Application.Services.ContractorStatusStatements.Contracts.GetCStatementSContracts;
using Engineering.Application.Services.ContractorStatusStatements.Models.GetContractorStatusStatementById;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Application.Abstractions.Data.ContractorStatusStatements;

public interface IContractorStatusStatementDetailRepository : IBaseRepository<ContractorStatusStatementDetail>
{

    Task<(List<GetModeledContractorStatusStatementByIdDetail> Data, int RowCount)> GetsContractorStatusStatementDetail(
        long Id,
        int pageIndex,
        int pageSize,
        CT ct);

    Task<List<GetCStatementSContractsModel>> GetCStatementSContracts(
        long Id,
        CT ct);

    Task<List<GetCStatementFContractsModel>> GetCStatementFContracts(
        long Id,
        CT ct);

}
