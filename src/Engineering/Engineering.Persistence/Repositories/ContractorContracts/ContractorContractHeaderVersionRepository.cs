using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractHeaderVersionRepository : BaseRepository<EngineeringDBContext, ContractorContractHeaderVersion>, IContractorContractHeaderVersionRepository
{

    public ContractorContractHeaderVersionRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<ContractorContractHeaderVersion>?> GetCCHVersionByCCHId(
        long id, CT ct)
    {
        return await DbSet
            .Include(x => x.ContractorContractHeader)
            .Include(x => x.ContractorStatusStatement)

            .Where(x => x.ContractorContractHeaderId == id)
            .OrderByDescending(x => x.Version).ToListAsync(ct);
    }

}
