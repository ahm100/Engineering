using Engineering.Application.Abstractions.Data.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperationDetails.Users;

namespace Engineering.Persistence.Repositories.ProjectOperationDetails.Users;

public class UserTechnicalsRepository : BaseRepository<EngineeringDBContext, UserTechnical>, IUserTechnicalsRepository
{
    public UserTechnicalsRepository(EngineeringDBContext context) : base(context)
    {
    }

}