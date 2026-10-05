using Engineering.Application.Abstractions.Data.ContractorStatusStatements;
using Engineering.Domain.Entities.ContractorStatusStatements;

namespace Engineering.Persistence.Repositories.ContractorStatusStatements;

public class ContractorStatusStatementProductRepository : BaseRepository<EngineeringDBContext, ContractorStatusStatementProduct>, IContractorStatusStatementProductRepository
{
    public ContractorStatusStatementProductRepository(EngineeringDBContext context) : base(context)
    {
    }

}
