using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementServiceThirdPartyRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementServiceThirdParty>, IContractorStatusStatementServiceThirdPartyRepository
{
    public ContractorStatusStatementServiceThirdPartyRepository(EngineeringDBContext context) : base(context)
    {
    }

}
