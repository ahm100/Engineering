using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerOperationProductRepository : BaseRepository<EngineeringDBContext, EmployerOperationProduct>, IEmployerOperationProductRepository
{
    public EmployerOperationProductRepository(EngineeringDBContext context) : base(context)
    {
    }

}