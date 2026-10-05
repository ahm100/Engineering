using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProductManages.Models.GetFiduciaryProductDetailReturnByDetailId;
using Engineering.Domain.Entities.FiduciaryProducts;

namespace Engineering.Persistence.Repositories.FiduciaryProducts;

public class FiduciaryProductDetailRepository : BaseRepository<EngineeringDBContext, FiduciaryProductDetail>, IFiduciaryProductDetailRepository
{
    public FiduciaryProductDetailRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<FiduciaryProductDetail?> GetByIdAsync(long id, CT ct)
    {
        var query = DbSet.Include(oo => oo.Managements.Where(c => !c.IsDeleted))
                           .ThenInclude(oo => oo.Returns.Where(c => !c.IsDeleted))
                            .ThenInclude(oo => oo.Documents.Where(c => !c.IsDeleted))
                         .Include(oo => oo.FiduciaryProduct)
                            .ThenInclude(oo => oo.Project)
                               .ThenInclude(oo => oo.ProjectCostCenters)
                         .Where(oo => oo.Id == id);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<GetFiduciaryProductDetailReturnByDetailIdResponse?> GetDetailReturnById(long id, CT ct)
    {
        var query = DbSet.Where(x => x.Id == id)
            .Select(x => new GetFiduciaryProductDetailReturnByDetailIdResponse()
            {
                DeliverDate = DateTime.Now,
                FiduciaryProductDetailId = x.Id,
                MeasureUnitId = x.MeasureUnitId,
                ProductId = x.ProductId,
                Returns = x.Managements.SelectMany(z => z.Returns).Select(r => new GetFiduciaryProductDetailReturnByDetailIdModel()
                {
                    Created = r.Created,
                    CreatorId = r.CreatorId,
                    CurrencyId = r.CurrencyId,
                    Description = r.Description,
                    FiduciaryProductDetailReturnId = r.Id,
                    LateDay = r.LateDay,
                    LateFine = r.LateFine,
                    ReturnCount = r.ReturnCount,
                    ReturnDate = r.ReturnDate,
                    Type = r.Type,
                    Documents = r.Documents.Select(d => d.Url).ToList()
                }).ToList()
            });

        return await query.FirstOrDefaultAsync();
    }
}
