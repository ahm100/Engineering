using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerOperationServiceRepository : BaseRepository<EngineeringDBContext, EmployerOperationService>, IEmployerOperationServiceRepository
{
    public EmployerOperationServiceRepository(EngineeringDBContext context) : base(context)
    {
    }
}