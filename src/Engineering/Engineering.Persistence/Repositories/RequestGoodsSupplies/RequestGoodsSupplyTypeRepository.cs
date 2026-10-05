using Engineering.Application.Abstractions.Data.RequestGoodsSupplies;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetDetailByRGSTypeId;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetReferenceTypeHistory;
using Engineering.Application.Services.RequestGoodsSupplies.Models.GetRGSTypeByRGSId;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestGoodsSupplies.Enums;

namespace Engineering.Persistence.Repositories.RequestGoodsSupplies;

public class RequestGoodsSupplyTypeRepository : BaseRepository<EngineeringDBContext, RequestGoodsSupplyType>, IRequestGoodsSupplyTypeRepository
{
    public RequestGoodsSupplyTypeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestGoodsSupplyType?> GetById(long id,
        CT ct)
    {
        return await
            DbSet
            .Include(x => x.RequestGoodsSupplyTypeDetails)
            .Where(x => x.Id == id).FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetRGSTypeByRGSIdModel>? Data, int RowCount)> GetRGSTypeByRGSId(long id,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet.Where(x => x.RequestGoodsSupplyId == id).Select(x => new GetRGSTypeByRGSIdModel
        {
            Id = x.Id,
            Importance = x.Importance,
            ReferenceId = x.ReferenceId,
            Type = x.Type,
            Status = x.Status,
            RequestedCount = x.RequestedCount,
            DelivaryDeadLine = x.DelivaryDeadLine,
            UnitPrice = x.UnitPrice,
            TotalPrice = x.TotalPrice,
            PackingPrice = x.PackingPrice,
            FinalPrice = x.FinalPrice,
            Description = x.Description,
            ManagementDescription = x.ManagementDescription,
            ContractorId = x.ContractorId,
            PackageId = x.PackageId,
            PackageCount = x.PackageCount,
            PackageUnitPrice = x.PackageUnitPrice,
            ProjectName = x.ProjectName,
            ProjectCode = x.ProjectCode,
            ProjectEnName = x.ProjectEnName,
            LastDescription = x.LastDescription,
            ProjectTypeModel = x.Type == SupplyType.Project ? new ResProjectTypeModel
            {
                ProjectName = x.ProjectName,
                ProjectEnName = x.ProjectEnName,
                ProjectCode = x.ProjectCode
            } : null,
            Details = x.RequestGoodsSupplyTypeDetails.Select(x => new GetDetailByRGSTypeIdModel
            {
                Id = x.Id,
                CostCenterId = x.CostCenterId,
                CostCenterName = x.CostCenterId != null ? x.CostCenter!.CostCenterName : null,
                DeliveryDeadLine = x.DelivaryDeadLine,
                RequestedCount = x.RequestedCount,
                Description = x.Description,
                DescriptionEn = x.DescriptionEn,
                Urls = x.RequestGoodsSupplyTypeDetailDocuments.Select(x => x.Url).ToList()
            }).ToList()
        });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }

    public async Task<(List<GetReferenceTypeHistoryModel>? Data, int RowCount)> GetReferenceTypeHistory(
    long referenceId,
    List<RGSTypeStatus>? statuses,
    SupplyType supplyType,
    int pageIndex,
    int pageSize, CT ct)
    {
        var query = DbSet
            .Where(x => x.ReferenceId == referenceId
            && x.Type == supplyType &&
            (statuses == null || statuses.Contains(x.Status)))
            .Select(x => new GetReferenceTypeHistoryModel
            {
                Id = x.Id,
                Importance = x.Importance,
                ReferenceId = x.ReferenceId,
                Type = x.Type,
                RequestedCount = x.RequestedCount,
                DelivaryDeadLine = x.DelivaryDeadLine,
                UnitPrice = x.UnitPrice,
                TotalPrice = x.TotalPrice,
                PackingPrice = x.PackingPrice,
                FinalPrice = x.FinalPrice,
                Description = x.Description,
                ManagementDescription = x.ManagementDescription,
                ContractorId = x.ContractorId,
                PackageId = x.PackageId,
                CreatorId = x.CreatorId,
                Created = x.Created,
                PackageCount = x.PackageCount,
                PackageUnitPrice = x.PackageUnitPrice,
                ProjectName = x.ProjectName,
                ProjectCode = x.ProjectCode,
                ProjectEnName = x.ProjectEnName,
                LastDescription = x.LastDescription,
                IsReExamination = x.RequestGoodsSupplyTypeHistories.Any(x => RGSTypeRules.IsReExamination.Contains(x.Status)),
                IsClosed = x.RequestGoodsSupplyTypeHistories.Any(x => RGSTypeRules.IsClosed.Contains(x.Status)),
                ProjectModel = new RGSProjectModel
                {
                    ProjectId = x.RequestGoodsSupply.ProjectId,
                    ProjectName = x.RequestGoodsSupply.Project.ProjectName,
                    ProjectNameEn = x.RequestGoodsSupply.Project.ProjectEnName
                }
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var baseQuery = await query.ToListAsync(ct);
        return (baseQuery, count);
    }
}