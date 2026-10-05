using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerOperationDetailRepository : BaseRepository<EngineeringDBContext, EmployerOperationDetail>, IEmployerOperationDetailRepository
{
    public EmployerOperationDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

}