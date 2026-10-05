using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails.Users;

public class UserPlanersRepository : BaseRepository<EngineeringDBContext, UserPlaner>, IUserPlanersRepository
{
    public UserPlanersRepository(EngineeringDBContext context) : base(context)
    {
    }

}