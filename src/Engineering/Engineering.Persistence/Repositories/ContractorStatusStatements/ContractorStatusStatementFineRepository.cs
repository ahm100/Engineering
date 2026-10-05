using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementFineRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementFine>, IContractorStatusStatementFineRepository
{
    public ContractorStatusStatementFineRepository(EngineeringDBContext context) : base(context)
    {
    }

}
