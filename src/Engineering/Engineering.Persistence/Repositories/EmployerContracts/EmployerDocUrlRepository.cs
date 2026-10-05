using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerDocUrlRepository : BaseRepository<EngineeringDBContext, EmployerDocUrl>, IEmployerDocUrlRepository
{
    public EmployerDocUrlRepository(EngineeringDBContext context) : base(context)
    {
    }

}