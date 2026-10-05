using Engineering.Application.Abstractions.Data.DailyProjectOperations;
using Engineering.Application.Services.DailyProjectOperations.Models.GetDailyProjectOperationHistoryById;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Persistence.Repositories.DailyProjectOperations;

public partial class DailyProjectOperationHistoryRepository : BaseRepository<EngineeringDBContext, DailyProjectOperationHistory>, IDailyProjectOperationHistoryRepository
{
    public DailyProjectOperationHistoryRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<GetDailyProjectOperationHistoryByIdModel>?> GetDailyProjectOperationHistoryById(long id, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.DailyProjectOperationId.Equals(id))
            .Select(x => new GetDailyProjectOperationHistoryByIdModel
            {
                Id = x.Id,
                Status = x.Status,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Length = x.Length,
                Width = x.Width,
                Height = x.Height,
                Weight = x.Weight,
                Number = x.Number,
                Description = x.Description,
                CreatorId = x.CreatorId,
                Created = x.Created,
                IsDeleted = x.IsDeleted
            });

        var entities = await query.ToListAsync();
        return entities;
    }
}