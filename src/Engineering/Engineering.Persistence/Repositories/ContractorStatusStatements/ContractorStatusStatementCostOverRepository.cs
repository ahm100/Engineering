using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementCostOverRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementCostOver>, IContractorStatusStatementCostOverRepository
{
    public ContractorStatusStatementCostOverRepository(EngineeringDBContext context) : base(context)
    {
    }

}
