using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerOperationRepository : BaseRepository<EngineeringDBContext, EmployerOperation>, IEmployerOperationRepository
{
    public EmployerOperationRepository(EngineeringDBContext context) : base(context)
    {
    }
}