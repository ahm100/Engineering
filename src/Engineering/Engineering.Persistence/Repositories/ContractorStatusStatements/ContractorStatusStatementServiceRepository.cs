using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementServiceRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementService>, IContractorStatusStatementServiceRepository
{
    public ContractorStatusStatementServiceRepository(EngineeringDBContext context) : base(context)
    {
    }

}
