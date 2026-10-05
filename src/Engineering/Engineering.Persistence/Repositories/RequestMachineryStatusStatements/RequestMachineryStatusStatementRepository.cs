using Engineering.Application.Abstractions.Data.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Persistence.Repositories.RequestMachineryStatusStatements;

public class RequestMachineryStatusStatementRepository : BaseRepository<EngineeringDBContext, RequestMachineryStatusStatement>, IRequestMachineryStatusStatementRepository
{
    public RequestMachineryStatusStatementRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<RequestMachineryStatusStatement?> GetRequestMachineryStatusStatementById(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.Project.ProjectCostCenters)
                    .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.Machinery)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.RequestMachinery)
                    .ThenInclude(oo => oo.MachineryReservations)
                        .ThenInclude(oo => oo.FixAssetMachinery)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.RequestMachinery)
                    .ThenInclude(oo => oo.InquiryOperators)
                        .ThenInclude(oo => oo.Inquiries)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.StatusStatementDetailProjectOperations)
                    .ThenInclude(oo => oo.ProjectOperation)
                        .ThenInclude(oo => oo.OperationInfo)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.StatusStatementDetailProjectOperationDetails)
                    .ThenInclude(oo => oo.ProjectOperationDetail)
                        .ThenInclude(oo => oo.OperationLocation)
            .Where(oo => oo.Id.Equals(id));

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<RequestMachineryStatusStatement?> GetStatusStatementById(long id, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.RequestMachinery)
            .Where(oo => oo.Id.Equals(id));

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<(List<RequestMachineryStatusStatement> Data, int RowCount)> GetFilteredRequestMachineryStatusStatement(
        List<long>? ids,
        List<long>? contractorIds,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? machineryIds,
        List<RequestMachineryStatusStatementStatus>? statuses,
        RequestMachineryStatusStatementUnit? unit,
        DateTime? startDate,
        DateTime? endDate,
        long? companyId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.Project.ProjectCostCenters)
                    .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.Machinery)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.RequestMachinery)
                    .ThenInclude(oo => oo.MachineryReservations)
                        .ThenInclude(oo => oo.FixAssetMachinery)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.RequestMachinery)
                    .ThenInclude(oo => oo.InquiryOperators)
                        .ThenInclude(oo => oo.Inquiries)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.StatusStatementDetailProjectOperations)
                    .ThenInclude(oo => oo.ProjectOperation)
                        .ThenInclude(oo => oo.OperationInfo)
            .Include(oo => oo.RequestMachineryStatusStatementDetails)
                .ThenInclude(oo => oo.StatusStatementDetailProjectOperationDetails)
                    .ThenInclude(oo => oo.ProjectOperationDetail)
                        .ThenInclude(oo => oo.OperationLocation)
            .Where(oo =>
                oo.Status != RequestMachineryStatusStatementStatus.Invalidated &&
                (ids == null || ids.Contains(oo.Id)) &&
                (companyId == null || oo.CompanyId.Equals(companyId)) &&
                (contractorIds == null || contractorIds.Contains(oo.ContractorId)) &&
                (machineryIds == null || oo.RequestMachineryStatusStatementDetails.Any(x => machineryIds.Contains(x.Machinery.Id))) &&
                (costCenterIds == null || oo.RequestMachineryStatusStatementDetails.Any(x => x.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId)))) &&
                (projectIds == null || oo.RequestMachineryStatusStatementDetails.Any(x => projectIds.Contains(x.Project.Id))) &&
                (statuses == null || statuses.Contains(oo.Status)) &&
                (unit == null || oo.RequestMachineryStatusStatementDetails.Any(x => x.Unit == unit)) &&
                (startDate == null || oo.FromDate >= startDate) &&
                (endDate == null || oo.ToDate <= endDate) &&
                (filterData == null || string.IsNullOrEmpty(filterData) ||
                    EF.Functions.Like(oo.Description, filterData.MakeLikePattern()) ||
                    oo.RequestMachineryStatusStatementDetails.Any(z => EF.Functions.Like(z.RequestMachinery!.RequestNumber.ToString(), filterData.MakeLikePattern())))
                );
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query.ToListAsync(ct);
        return (entities, count);
    }

    public async Task<RequestMachineryStatusStatement?> GetLastRequestMachineryStatusStatement(
        long contractorId,
        long? companyId,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.RequestMachineryStatusStatementDetails)
            .Where(oo =>
                oo.Status != RequestMachineryStatusStatementStatus.Invalidated &&
                (companyId == null || oo.CompanyId.Equals(companyId)) &&
                (contractorId.Equals(oo.ContractorId)));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        var result = await query.FirstOrDefaultAsync();

        return result;
    }
}
