using Engineering.Application.Abstractions.Data.RequestRewards;
using Engineering.Application.Services.RequestRewards.Contracts.GetFilteredRequestRewards;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.RequestRewards.Enums;

namespace Engineering.Persistence.Repositories.RequestRewards;

public class RequestRewardRepository : BaseRepository<EngineeringDBContext, RequestReward>, IRequestRewardRepository
{
    public RequestRewardRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<(List<GetFilteredRequestRewardsModel> Data, int RowCount)>
    GetFilteredRequestRewards(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        long? thirdPartyId,
        long? registerUserId,
        long? managerId,
        long? employerContractId,
        bool? isReward,
        DateTime? fromdate,
        DateTime? todate,
        RequestRewardStatus? status,
        RequestRewardType? type,
        long? projectOperationId,
        long? projectOperationDetailId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602
        var query = DbSet
            .Where(c => !c.IsDeleted
                 && (costCenterId == null || c.CostCenter!.Id == costCenterId)
                 && (projectId == null || c.Project!.Id == projectId)
                 //&& (managerId == null || c.Project!.ProjectManager == managerId)
                 //&& (registerUserId == null || c.CreatorId == registerUserId)
                 && (thirdPartyId == null || c.RequestRewardThirdParties.Any(oo => oo.ThirdPartyId == thirdPartyId))
                 && (fromdate == null || c.RegistrationDate >= fromdate)
                 && (todate == null || c.RegistrationDate <= todate)
                 && (status == null || c.Status == status)
                 && (type == null || c.Type == type)
                 && (ids == null || ids.Count == 0 || ids.Contains(c.Id))
                 && (projectOperationId == null || c.ProjectOperation!.Id == projectOperationId)
                 && (projectOperationDetailId == null || c.ProjectOperationDetail!.Id == projectOperationDetailId)
                 && (
                    isReward == null
                    || (isReward == true && RRewardTypeRules.AllowReward.Contains(c.Type))
                    || (isReward == false && RRewardTypeRules.AllowFine.Contains(c.Type))

                    ) &&
                    (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.Project.ProjectCode, filterData.MakeLikePattern()) ||
                    string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.Project.ProjectName, filterData.MakeLikePattern()))
                 );
#pragma warning restore CS8602

        query = query.OrderBy(oo => oo.Status).ThenByDescending(oo => oo.RegistrationDate);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .AsNoTracking()
            .Select(c => new GetFilteredRequestRewardsModel
            {
                Id = c.Id,
                Status = c.Status,
                Type = c.Type,
                CostCenterId = c.CostCenter.Id,
                CostCenterName = c.CostCenter.CostCenterName,
                ProjectId = c.Project.Id,
                ProjectName = c.Project.ProjectName,
                ProjectOperationId = c.ProjectOperation.Id,
                OperationInfoName = c.ProjectOperation.OperationInfo.OperationInfoName,
                OperationInfoCode = c.ProjectOperation.OperationInfo.OperationInfoCode,
                ProjectOperationDetailId = c.ProjectOperationDetail.Id,
                PublicName = c.ProjectOperationDetail.OperationLocation.PublicName,
                PublicCode = c.ProjectOperationDetail.OperationLocation.PublicCode,
                RegistrationDate = c.RegistrationDate,
                OfferedPrice = c.OfferedPrice,
                ConfirmedPrice = c.ConfirmedPrice,
                ManagerDescription = c.ManagerDescription,
                CurrencyId = c.CurrencyId,
                Description = c.Description,
                CompanyId = c.CompanyId,
                // Add more DTO mappings as needed
                Documents = c.RequestRewardDocuments.Select(doc => new GetRequestRewardDocumentModel
                {
                    Id = doc.Id,
                    Url = doc.Url
                }).ToList(),
                ThirdPartiesModel = c.RequestRewardThirdParties.Select(t => new GetRequestRewardThirdPartyModel
                {
                    Id = t.Id,
                    ThirdPartyId = t.ThirdPartyId,
                }).ToList()
            })
            .ToListAsync(ct);

        return (items, count);
    }


    public async Task<(List<RequestReward> Data, int RowCount)> GetsConfirmedContractorRequestReward(
        long projectId,
        long contractorId,
        DateTime fromDate,
        DateTime toDate,
        int pageIndex,
        int pageSize, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(oo => oo.ProjectOperationDetail.OperationLocation)
            .Include(oo => oo.ProjectOperation.OperationInfo)
            .Include(oo => oo.RequestRewardThirdParties)
            .Include(oo => oo.RequestRewardDocuments)

            .Where(c =>
                c.Project!.Id == projectId &&
                c.RequestRewardThirdParties.Any(oo => oo.ThirdPartyId == contractorId) &&
                c.RegistrationDate.Date >= fromDate.Date &&
                c.RegistrationDate.Date <= toDate.Date &&
                c.Status == RequestRewardStatus.Confirmed);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query
            .ToListAsync(ct);
        return (items, count);
    }

    public async Task<RequestReward?> GetById(long id, CT ct)
    {
        var result = await
         DbSet
         .Include(oo => oo.RequestRewardThirdParties)
         .Include(oo => oo.RequestRewardDocuments)
         .Include(oo => oo.RequestRewardProducts)
         .Include(oo => oo.ProjectOperationDetail)
            .ThenInclude(oo => oo!.OperationLocation)
         .Include(oo => oo.ProjectOperation)
                .ThenInclude(oo => oo!.OperationInfo)
         .Include(oo => oo.CostCenter)
         .Include(oo => oo.Project)
         .Include(oo => oo.FiduciaryProductDetailReturn)
         .Include(oo => oo.DailyProjectOperationRequestRewards)

        .Where(c => c.Id == id && !c.IsDeleted)
        .FirstOrDefaultAsync(ct);

        return result;
    }

    public async Task<List<RequestReward>> GetRequestRewardsByDate(DateTime startDate, DateTime endDate, List<long>? projectOperationIds, List<long>? FiduciaryProductDetailReturnIds, RequestRewardType type, CT ct)
    {
        var query = DbSet
            .Where(oo => oo.Created.Date >= startDate.Date &&
                        oo.Created.Date <= endDate.Date &&
                        (projectOperationIds == null || projectOperationIds!.Contains(oo.ProjectOperationDetail!.ProjectOperation.Id)) &&
                        (FiduciaryProductDetailReturnIds == null || FiduciaryProductDetailReturnIds!.Contains(oo.FiduciaryProductDetailReturn!.Id)) &&
                        oo.Type == type)
            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }

    public async Task<(List<RequestReward> Data, int RowCount)> GetRequestRewardsByIds(List<long> ids, CT ct)
    {
        var query = DbSet.Include(oo => oo.RequestRewardThirdParties)
                         .Include(oo => oo.RequestRewardDocuments)
                         .Include(oo => oo.RequestRewardProducts)
                         .Include(oo => oo.ProjectOperationDetail)
                            .ThenInclude(oo => oo!.OperationLocation)
                         .Include(oo => oo.ProjectOperation)
                                .ThenInclude(oo => oo!.OperationInfo)
                         .Include(oo => oo.CostCenter)
                         .Include(oo => oo.Project)
                         .Where(oo => ids.Any(x => x == oo.Id))
                         .OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);
        var items = await query.Page(1, count)
                               .AsNoTrackingWithIdentityResolution()
                               .ToListAsync(ct);

        return (items, count);
    }

    public async Task<List<RequestReward>> GetDiscounts(
        long projectId,
        long contractorId, CT ct)
    {
        var query = DbSet

            .Where(c =>
                c.Project!.Id == projectId &&
                c.Type == RequestRewardType.Discount &&
                c.RequestRewardThirdParties.Any(oo => oo.ThirdPartyId == contractorId) &&
                c.Status == RequestRewardStatus.Confirmed);

        var items = await query
            .ToListAsync(ct);
        return (items);
    }
}
