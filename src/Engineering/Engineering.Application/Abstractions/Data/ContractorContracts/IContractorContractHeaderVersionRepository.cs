using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Application.Abstractions.Data.ContractorContracts;

public interface IContractorContractHeaderVersionRepository : IBaseRepository<ContractorContractHeaderVersion>
{
    Task<List<ContractorContractHeaderVersion>?> GetCCHVersionByCCHId(
        long id, CT ct);

}
