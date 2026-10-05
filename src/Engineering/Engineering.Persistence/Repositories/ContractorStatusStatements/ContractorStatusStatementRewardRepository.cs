using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementRewardRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementReward>, IContractorStatusStatementRewardRepository
{
    public ContractorStatusStatementRewardRepository(EngineeringDBContext context) : base(context)
    {
    }

}
