using Engineering.Application.Abstractions.Data.RequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetFilteredRequestContractors;
using Engineering.Application.Services.RequestContractors.Models.GetRequestContractorById;
using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.RequestContractors.Enums;

namespace Engineering.Persistence.Repositories.RequestContractors;

public class RequestContractorRepository : BaseRepository<EngineeringDBContext, RequestContractor>, IRequestContractorRepository
{
    public RequestContractorRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestContractor?> GetById(long requestContractorId, CT ct)
    {
        var query = DbSet
                         .Include(r => r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
                            .ThenInclude(r => r.CostCenter)
                         .Include(r => r.ProjectOperationDetail.ProjectOperation.OperationInfo)
                         .Include(r => r.ProjectOperationDetail.OperationLocation)
                         .Include(r => r.Histories)
                         .Include(r => r.Inquiries)
                             .ThenInclude(z => z.RequestContractorInquiryDocuments)
                         .Where(r => r.Id.Equals(requestContractorId))
                        .OrderByDescending(r => r.Created);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<bool> IsRequestContractorDuplicate(long projectOperationDetail, long serviceInfoId, CT ct)
    => await DbSet.AnyAsync(r => r.ProjectOperationDetail.Id == projectOperationDetail && r.ServiceInfo.Id == serviceInfoId);

    public async Task<GetRequestContractorByIdResponse?> GetModelById(long requestContractorId, CT ct)
    {
        var query = DbSet
                         .Where(r => r.Id.Equals(requestContractorId))
                         .Select(r => new GetRequestContractorByIdResponse()
                         {
                             RequestContractorId = r.Id,
                             Status = r.Status,
                             RequestNumber = r.RequestNumber,
                             CostCenterId = r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any() ?
                             r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId :
                             null,
                             CostCenterName = r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any() ?
                             r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName :
                             null,
                             ProjectId = r.ProjectOperationDetail.ProjectOperation.Project.Id,
                             ProjectName = r.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                             ProjectOperationId = r.ProjectOperationDetail.ProjectOperation.Id,
                             OperationInfoName = r.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                             OperationInfoCode = r.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                             ProjectOperationDetailId = r.ProjectOperationDetail.Id,
                             OperationLocationId = r.ProjectOperationDetail.OperationLocation.Id,
                             PublicName = r.ProjectOperationDetail.OperationLocation.PublicName,
                             PublicCode = r.ProjectOperationDetail.OperationLocation.PublicCode,
                             ServiceInfoId = r.ServiceInfo.Id,
                             ServiceInfoName = r.ServiceInfo.ServiceInfoName,
                             ServiceInfoCode = r.ServiceInfo.ServiceInfoCode,
                             Volume = r.Volume,
                             Description = r.Description,
                             DescriptionStatus = r.StatusDescription,
                             CreatorId = r.CreatorId,
                             Created = r.Created,
                             CompanyId = r.CompanyId,
                             ConfirmInquiry = r.ConfirmInquiry
                         })
                        .OrderByDescending(r => r.Created);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<List<RequestContractor>?> GetByIds(List<long> requestContractorIds, CT ct)
    {
        var query = DbSet

                         .Where(r => requestContractorIds.Contains(r.Id))
            .OrderByDescending(r => r.Created);

        return await query.ToListAsync(ct);
    }

    public async Task<(List<GetFilteredRequestContractorsModel> Data, int RowCount)> GetFiltered(
        List<long>? ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? projectOperationDetailIds,
        List<long>? serviceInfoIds,
        List<long>? contractorIds,
        RequestContractorStatus? status,
        DateTime? fromDate,
        DateTime? toDate,
        long? creatorId,
        string? filterData,
        string[]? orderBy,
        long? companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var c = DbSet;

        var query = DbSet
                         .Where(r =>
                               (companyId == null || r.CompanyId == companyId) &&
                               (contractorIds == null || (r.ConfirmInquiry != null && contractorIds.Contains(r.ConfirmInquiry.ContractorId))) &&
                               (costCenterIds == null || r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                               (projectIds == null || projectIds.Contains(r.ProjectOperationDetail.ProjectOperation.Project.Id)) &&
                               (ids == null || ids.Count == 0 || ids.Contains(r.Id)) &&
                               (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(r.RequestNumber.ToString(), filterData.MakeLikePattern())) &&
                               (status == null || r.Status == status) &&
                               (fromDate == null || (r.ProjectOperationDetail.StartDate != null && r.ProjectOperationDetail.StartDate.Value.Date >= fromDate.Value.Date)) &&
                               (toDate == null || (r.ProjectOperationDetail.EndDate != null && r.ProjectOperationDetail.EndDate.Value.Date <= toDate.Value.Date)) &&
                               (projectOperationIds == null || projectOperationIds.Contains(r.ProjectOperationDetail.ProjectOperation.Id)) &&
                               (projectOperationDetailIds == null || projectOperationDetailIds.Contains(r.ProjectOperationDetail.Id)) &&
                               (serviceInfoIds == null || serviceInfoIds.Contains(r.ServiceInfo.Id)) &&
                               (creatorId == null || r.CreatorId == creatorId))
                         .Select(r => new GetFilteredRequestContractorsModel()
                         {
                             RequestContractorId = r.Id,
                             Status = r.Status,
                             RequestNumber = r.RequestNumber,
                             CostCenterId = r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any() ?
                             r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId :
                             null,
                             CostCenterName = r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any() ?
                             r.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName :
                             null,
                             ProjectId = r.ProjectOperationDetail.ProjectOperation.Project.Id,
                             ProjectName = r.ProjectOperationDetail.ProjectOperation.Project.ProjectName,
                             ProjectOperationId = r.ProjectOperationDetail.ProjectOperation.Id,
                             OperationInfoName = r.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                             OperationInfoCode = r.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                             ProjectOperationDetailId = r.ProjectOperationDetail.Id,
                             OperationLocationId = r.ProjectOperationDetail.OperationLocation.Id,
                             PublicName = r.ProjectOperationDetail.OperationLocation.PublicName,
                             PublicCode = r.ProjectOperationDetail.OperationLocation.PublicCode,
                             ServiceInfoId = r.ServiceInfo.Id,
                             ServiceInfoName = r.ServiceInfo.ServiceInfoName,
                             ServiceInfoCode = r.ServiceInfo.ServiceInfoCode,
                             Volume = r.Volume,
                             Description = r.Description,
                             DescriptionStatus = r.StatusDescription,
                             CreatorId = r.CreatorId,
                             Created = r.Created,
                             CompanyId = r.CompanyId,
                             ConfirmInquiry = r.ConfirmInquiry
                         });
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8604 // Possible null reference argument.

        query = query.OrderByDescending(r => r.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredRequester(CT ct)
    {
        var query = DbSet
           .Where(x => !x.IsDeleted && x.CreatorId != 0)
           .Select(c => c.CreatorId).Distinct();

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredContractor(
        List<long>? costCenterIds,
        List<long>? projectIds,
        CT ct)
    {
        var query = DbSet
           //.Where(x =>
           //!x.IsDeleted &&
           // x.Status == RequestContractorStatus.OnProject &&
           //(costCenterIds == null || costCenterIds.Contains(x.Project.CostCenter.Id)) &&
           //(projectIds == null || projectIds.Contains(x.Project.Id)) &&
           ////(x.ContractorId != null && x.ContractorId > 0))
           //.Select(c => (long)c.ContractorId!)
           .Distinct();

        var count = await query.CountAsync(ct);
        var items = new List<long>();// await query.ToListAsync(ct);
        return (items, count);
    }
}
