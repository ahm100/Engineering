using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails.Users;

public class UserImplementationsRepository : BaseRepository<EngineeringDBContext, UserImplementation>, IUserImplementationsRepository
{
    public UserImplementationsRepository(EngineeringDBContext context) : base(context)
    {
    }

}