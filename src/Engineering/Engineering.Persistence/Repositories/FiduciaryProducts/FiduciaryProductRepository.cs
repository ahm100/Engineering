using Engineering.Application.Abstractions.Data.FiduciaryProducts;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFiduciaryProductById;
using Engineering.Application.Services.FiduciaryProducts.Models.GetFilteredFiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts;
using Engineering.Domain.Entities.FiduciaryProducts.Enums;

namespace Engineering.Persistence.Repositories.FiduciaryProducts;

public class FiduciaryProductRepository : BaseRepository<EngineeringDBContext, FiduciaryProduct>, IFiduciaryProductRepository
{
    public FiduciaryProductRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<List<FiduciaryProduct>> GetFiduciaryProductsByDate(DateTime startDate, DateTime endDate, List<long> projectOperationIds, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.ProjectOperation.OperationInfo)
            .Include(oo => oo.Details.Where(c => !c.IsDeleted))
                            .ThenInclude(oo => oo.Managements.Where(d => !d.IsDeleted))
                                .ThenInclude(oo => oo.Returns.Where(d => !d.IsDeleted))
                                    .ThenInclude(oo => oo.Documents.Where(d => !d.IsDeleted))
                         .Where(oo => oo.Created.Date >= startDate.Date &&
                                     oo.Created.Date <= endDate.Date &&
                                     projectOperationIds.Contains(oo.ProjectOperation.Id)
                                     )
            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }

    public async Task<FiduciaryProduct?> GetByIdAsync(long id, CT ct)
    {
        var query = DbSet.Include(oo => oo.Project)
                            .ThenInclude(oo => oo.ProjectCostCenters)
                                .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.ProjectOperation)
                            .ThenInclude(oo => oo.OperationInfo)
                         .Include(oo => oo.Details)
                            .ThenInclude(oo => oo.Histories)
                         .Include(oo => oo.Details)
                            .ThenInclude(oo => oo.Managements.Where(d => !d.IsDeleted))
                                .ThenInclude(oo => oo.Returns.Where(d => !d.IsDeleted))
                                    .ThenInclude(oo => oo.Documents.Where(d => !d.IsDeleted))
                         .Where(oo => oo.Id.Equals(id));

        return await query.FirstOrDefaultAsync();
    }

    public async Task<GetFiduciaryProductByIdResponse?> GetDataById(long id, CT ct)
    {
        var query = DbSet.Where(oo => oo.Id.Equals(id))
            .Select(x => new GetFiduciaryProductByIdResponse()
            {
                Id = x.Id,
                LastDescription = x.LastDescription,
                StatusDescription = x.StatusDescription,
                RequestNumber = x.Id,
                CostCenter = new GetFiduciaryProductByIdCostCenterModel()
                {
                    Id = x.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                    CostCenterName = x.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                },
                Project = new GetFiduciaryProductByIdProjectModel()
                {
                    Id = x.Project.Id,
                    ProjectName = x.Project.ProjectName,
                },
                ProjectOperation = new GetFiduciaryProductByIdProjectOperationModel()
                {
                    Id = x.ProjectOperation.Id,
                    OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                },
                ThirdParty = new GetFiduciaryProductByIdThirdPartyModel()
                {
                    Id = x.ThirdPartyId,
                },
                Status = x.Status,
                Description = x.Description,
                Created = x.Created,
                CompanyId = x.CompanyId,
                CreatorId = x.CreatorId,
                Details = x.Details.Select(z => new GetFiduciaryProductByIdDetailModel()
                {
                    Id = z.Id,
                    Created = z.Created,
                    CreatorId = z.CreatorId,
                    ProductId = z.ProductId,
                    MeasureunitId = z.MeasureUnitId,
                    CurrencyId = z.CurrencyId,
                    Status = z.Status,
                    DailyLateFine = z.DailyLateFine,
                    LoanCount = z.LoanCount,
                    LoanDays = z.LoanDays,
                    ProductDescription = z.Description,
                    ConfirmedDailyLateFine = z.ConfirmedDailyLateFine,
                    ConfirmedLoanDays = z.ConfirmedLoanDays,
                    LastDescription = x.LastDescription,
                    StatusDescription = x.StatusDescription,
                    Managements = z.Managements.Select(m => new GetFiduciaryProductByIdDetailManagementModel()
                    {
                        Id = m.Id,
                        ConfirmedLoanCount = m.ConfirmedLoanCount,
                        InvoiceId = m.InvoiceId,
                        Status = m.Status,
                        Warehouse = new GetFiduciaryProductByIdDetailManagementWarehouseModel()
                        {
                            Id = m.WarehouseId,
                            ConfirmedLoanCount = m.ConfirmedLoanCount,
                        },
                        Created = m.Created,
                        CreatorId = m.CreatorId,
                        Returns = m.Returns.Select(r => new GetFiduciaryProductByIdDetailManagementReturnedModel()
                        {
                            Created = r.Created,
                            CreatorId = r.CreatorId,
                            CurrencyId = r.CurrencyId,
                            Description = r.Description,
                            Id = r.Id,
                            InvoiceId = r.InvoiceId,
                            LateDay = r.LateDay,
                            LateFine = r.LateFine,
                            ReturnCount = r.ReturnCount,
                            ReturnDate = r.ReturnDate,
                            Type = r.Type,
                        }).ToList()
                    }).ToList()
                }).ToList(),
            });

        return await query.FirstOrDefaultAsync();
    }

    public async Task<FiduciaryProduct?> GetFiduciaryProductForChangeStatus(long id, CT ct)
    {
        var query = DbSet.Include(oo => oo.Project)
                            .ThenInclude(oo => oo.ProjectCostCenters)
                                .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.ProjectOperation)
                         .Include(oo => oo.Details)
                               .ThenInclude(oo => oo.Managements.Where(d => !d.IsDeleted))
                                    .ThenInclude(oo => oo.Returns.Where(d => !d.IsDeleted))

            .Where(oo => oo.Id.Equals(id));

        return await query.FirstOrDefaultAsync();
    }

    public async Task<(List<GetFilteredFiduciaryProductsModel> Data, int RowCount)> GetFilteredAsync(List<long>? ids,
        long? costCenterId,
        long? projectId,
        FiduciaryProductStatus? status,
        List<long>? projectOperationIds,
        long? thirdPartyId,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
                         .Where(f =>
                                     (costCenterId == null || f.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                                     (projectId == null || f.Project.Id.Equals(projectId)) &&
                                     (status == null || f.Status.Equals(status)) &&
                                     (ids == null || ids.Count == 0 || ids.Contains(f.Id)) &&
                                     (projectOperationIds == null || projectOperationIds.Contains(f.ProjectOperation.Id)) &&
                                     (thirdPartyId == null || f.ThirdPartyId.Equals(thirdPartyId)) &&
                                     (fromDate == null || f.Created.Date >= fromDate.Value.Date) &&
                                     (toDate == null || f.Created.Date <= toDate.Value.Date) &&
                                     (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(f.Id.ToString(), filterData.MakeLikePattern())))
                         .Select(x => new GetFilteredFiduciaryProductsModel()
                         {
                             Id = x.Id,
                             LastDescription = x.LastDescription,
                             StatusDescription = x.StatusDescription,
                             Status = x.Status,
                             Description = x.Description,
                             Created = x.Created,
                             CompanyId = x.CompanyId,
                             CreatorId = x.CreatorId,
                             CostCenterId = x.Project.ProjectCostCenters.FirstOrDefault().CostCenter.Id,
                             CostCenterName = x.Project.ProjectCostCenters.FirstOrDefault().CostCenter.CostCenterName,
                             OperationInfoName = x.ProjectOperation.OperationInfo.OperationInfoName,
                             ProjectId = x.Project.Id,
                             ProjectName = x.Project.ProjectName,
                             ProjectOperationId = x.ProjectOperation.Id,
                             ThirdPartyId = x.ThirdPartyId,
                             Details = x.Details.Select(z => new GetFilteredFiduciaryProductDetailsModel()
                             {
                                 Id = z.Id,
                                 FiduciaryProductId = x.Id,
                                 Created = z.Created,
                                 CreatorId = z.CreatorId,
                                 ProductId = z.ProductId,
                                 MeasureunitId = z.MeasureUnitId,
                                 CurrencyId = z.CurrencyId,
                                 Status = z.Status,
                                 DailyLateFine = z.DailyLateFine,
                                 LoanCount = z.LoanCount,
                                 LoanDays = z.LoanDays,
                                 ProductDescription = z.Description,
                                 ConfirmedDailyLateFine = z.ConfirmedDailyLateFine,
                                 ConfirmedLoanDays = z.ConfirmedLoanDays,
                                 LastDescription = x.LastDescription,
                                 StatusDescription = x.StatusDescription,
                             }).ToList(),
                         });

        query = query.OrderByDescending(f => f.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.AsNoTracking().AsSplitQuery().ToListAsync(ct);

        return (entities, count);
    }
}
