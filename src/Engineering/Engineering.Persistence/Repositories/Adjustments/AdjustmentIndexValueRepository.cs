using Engineering.Application.Abstractions.Data.Adjustments;
using Engineering.Domain.Entities.Adjustments;

namespace Engineering.Persistence.Repositories.Adjustments;
public class AdjustmentIndexValueRepository : BaseRepository<EngineeringDBContext, AdjustmentIndexValue>, IAdjustmentIndexValueRepository
{
    public AdjustmentIndexValueRepository(EngineeringDBContext context) : base(context)
    {
    }
}

