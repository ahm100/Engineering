using Engineering.Application.Abstractions.Data.EmployerStatusStatements;
using Engineering.Application.Services.EmployerStatusStatements.Models.GetEmployerStatusStatementLetterheadExcel;
using Engineering.Domain.Entities.EmployerStatusStatements;
using Engineering.Domain.Entities.EmployerStatusStatements.Enums;

namespace Engineering.Persistence.Repositories.EmployerStatusStatements;

public class EmployerStatusStatementRepository : BaseRepository<EngineeringDBContext, EmployerStatusStatement>, IEmployerStatusStatementRepository
{
    public EmployerStatusStatementRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<EmployerStatusStatement?> GetEmployerStatusStatementById(long id, CT ct)
    {
        var query = DbSet
            .Include(p => p.EmployerStatusStatementDocuments)
            .Include(p => p.Project)
                .ThenInclude(c => c.ProjectCostCenters)
                    .ThenInclude(c => c.CostCenter)
            .Include(i => i.Project)
                .ThenInclude(c => c.ProjectOperations)
                    .ThenInclude(c => c.OperationInfo)
            .Include(i => i.EmployerContract)
            .Include(i => i.EmployerStatusStatementProjectOperations)
                .ThenInclude(i => i.EmployerStatusStatementProjectOperationDetails)

            .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<EmployerStatusStatement?> GetEmployerStatusStatementByIdForDocs(long id, CT ct)
    {
        var query = DbSet
            .Include(p => p.EmployerStatusStatementDocuments)
            .Where(oo => oo.Id == id);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<EmployerStatusStatement?> GetLast(long? employerId, long costCenterId, long projectId, string? contractCode, CT ct)
    {
        var query = DbSet
            .Include(p => p.EmployerStatusStatementDocuments)
            .Include(p => p.Project)
                .ThenInclude(c => c.ProjectCostCenters)
                    .ThenInclude(c => c.CostCenter)
            .Include(i => i.Project)
                .ThenInclude(c => c.ProjectOperations)
                    .ThenInclude(c => c.OperationInfo)
            .Include(i => i.EmployerContract)
            .Include(i => i.EmployerStatusStatementProjectOperations)
                .ThenInclude(i => i.EmployerStatusStatementProjectOperationDetails)

            .Where(oo => oo.Project.Id == projectId
             && oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)
             && (employerId == null || oo.Project.EmployerId == employerId)
             && (contractCode == null || oo.EmployerContract.Code!.Contains(contractCode))
            )

            .OrderByDescending(oo => oo.Created);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<string> CodeCreator(long? companyId, CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
        var query = await DbSet
           .Where(x => (companyId == null || x.CompanyId == companyId) &&
           EF.Functions.IsNumeric(x.StatusStatementCode)).Select(x => Convert.ToInt64(x.StatusStatementCode)).ToListAsync(ct);
#pragma warning restore CS8604 // Possible null reference argument.

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<EmployerStatusStatement?> ChangeEmployerStatusStatementStatus(long id, CT ct)
    {
        var query = DbSet
            .Include(i => i.EmployerContract)
            .Include(i => i.EmployerStatusStatementProjectOperations)
                .ThenInclude(i => i.EmployerStatusStatementProjectOperationDetails)
                    .ThenInclude(i => i.EmployerStatusStatementProjectOperationDetailDailies)

            .Where(oo => oo.Id == id)

            .OrderByDescending(oo => oo.Created);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<EmployerStatusStatement?> GetEmployerStatusStatementExcelExporter(long id, CT ct)
    {
        var query = DbSet
            .Include(p => p.Project)
                .ThenInclude(c => c.ProjectCostCenters)
                    .ThenInclude(c => c.CostCenter)
            .Include(i => i.Project)
                .ThenInclude(c => c.ProjectOperations)
                    .ThenInclude(c => c.OperationInfo)
            .Include(i => i.EmployerContract)
            .Include(i => i.EmployerStatusStatementProjectOperations)
                .ThenInclude(i => i.EmployerStatusStatementProjectOperationDetails)
                .ThenInclude(i => i.ProjectOperationDetail)
                .ThenInclude(i => i.OperationLocation)

            .Where(oo => oo.Id == id)

            .OrderByDescending(oo => oo.Created);

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<GetEmployerStatusStatementLetterheadExcelModel?> GetEmployerStatusStatementLetterheadExcel(long id, CT ct)
    {
        var query = DbSet

            .Where(oo => oo.Id == id)

            .Select(x => new GetEmployerStatusStatementLetterheadExcelModel()
            {
                Id = x.Id,
                SendStatusType = x.Status,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                Created = x.Created,
                StatusStatementCode = x.StatusStatementCode,
                ProjectId = x.EmployerContract.Project.Id,
                ProjectName = x.EmployerContract.Project.ProjectName,
                ProjectCode = x.EmployerContract.Project.ProjectCode,
                EmployerId = x.EmployerContract.Project.EmployerId,
                EmployerContractId = x.EmployerContract.Id,
                EmployerContractCode = x.EmployerContract.Code,
                Description = x.Description,
                CostCenterId = x.EmployerContract.EmployerContractHead.CostCenter.Id,
                CostCenterName = x.EmployerContract.EmployerContractHead.CostCenter.CostCenterName,
                PercentageOfWorkDone = x.PercentageOfWorkDone,
                CalculatedAmount = x.CalculatedAmount,
                Urls = x.EmployerStatusStatementDocuments.Select(x => x.Url).ToList(),
                LastEmployerStatusStatementCode = x.LastEmployerStatusStatement.StatusStatementCode,
                CurrencyId = x.EmployerContract.EmployerContractHead.CurrencyId,
                ProjectOperations = x.EmployerStatusStatementProjectOperations.Select(p => new GetEmployerStatusStatementLetterheadExcelProjectOperation()
                {
                    Id = p.Id,
                    ProjectOperationId = p.ProjectOperation.Id,
                    EmployerContractId = x.EmployerContract.Id,
                    EmployerContractCode = x.EmployerContract.Code,
                    ProjectId = x.Project.Id,
                    ProjectName = x.Project.ProjectName,
                    OperationInfoId = p.ProjectOperation.OperationInfo.Id,
                    UnitOfMeasurementId = p.ProjectOperation.OperationInfo.UnitOfMeasurementId,
                    OperationInfoName = p.ProjectOperation.OperationInfo.OperationInfoName,
                    OperationInfoCode = p.ProjectOperation.OperationInfo.OperationInfoCode,
                    Workload = p.ProjectOperation.Workload,
                    Description = p.ProjectOperation.Description,
                    DailyProjectOperationDetails = p.ProjectOperation.ProjectOperationDetails.SelectMany(o => o.DailyOperations.Select(d => new GetEmployerStatusStatementDailyProjectOperationDetail()
                    {
                        Id = d.Id,
                        ProjectOperationDetailId = d.ProjectOperationDetail.Id,
                        ProjectOperationId = p.ProjectOperation.Id,
                        ProjectId = x.Project.Id,
                        ProjectName = x.Project.ProjectName,
                        OperationInfoId = p.ProjectOperation.OperationInfo.Id,
                        OperationInfoName = p.ProjectOperation.OperationInfo.OperationInfoName,
                        OperationInfoCode = p.ProjectOperation.OperationInfo.OperationInfoCode,
                        OperationLocationId = d.ProjectOperationDetail.OperationLocation.Id,
                        PrivateName = d.ProjectOperationDetail.OperationLocation.PrivateName,
                        PublicName = d.ProjectOperationDetail.OperationLocation.PublicName,
                        Length = d.Length,
                        Width = d.Width,
                        Height = d.Height,
                        Weight = d.Weight,
                        Number = d.Number,

                    })).ToList(),
                }).ToList(),
            });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredEmployerRequester(CT ct)
    {
        var query = DbSet
            .Include(x => x.Project)
            .Where(x => x.Project.EmployerId.HasValue)
            .Select(x => x.Project.EmployerId.Value).Distinct();

        var count = await query.CountAsync(ct);
        var contracts = await query
            .ToListAsync(ct);

        return (contracts, count);
    }

    public async Task<(List<EmployerStatusStatement> Data, int RowCount)> GetsFilteredEmployerStatusStatement(
        long employerId,
        long costCenterId,
        long projectId,
        long employerContractId,
        string? statusStatementCode,
        List<long>? projectOperationIds,
        DateTime? startDate,
        DateTime? endDate,
        EmployerStatusStatementStatus? status,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(p => p.EmployerStatusStatementDocuments)
            .Include(p => p.Project)
                .ThenInclude(c => c.ProjectCostCenters)
                    .ThenInclude(c => c.CostCenter)
            .Include(i => i.Project)
                .ThenInclude(c => c.ProjectOperations)
                    .ThenInclude(c => c.OperationInfo)
            .Include(i => i.EmployerContract)
            .Include(i => i.EmployerStatusStatementProjectOperations)
                .ThenInclude(i => i.EmployerStatusStatementProjectOperationDetails)

            .Where(oo =>
                oo.Project.EmployerId == employerId &&
                oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                oo.Project.Id == projectId &&
                oo.EmployerContract.Id == employerContractId &&
                (string.IsNullOrWhiteSpace(statusStatementCode) || EF.Functions.Like(oo.StatusStatementCode!.ToString() + "-" + oo.Id.ToString(), statusStatementCode.MakeLikePattern())) &&
                (projectOperationIds == null || oo.EmployerStatusStatementProjectOperations.Any(p => projectOperationIds.Contains(p.ProjectOperation.Id))) &&
                (startDate == null || oo.StartDate >= startDate) &&
                (endDate == null || oo.EndDate <= endDate) &&
                (status == null || oo.Status.Equals(status))
                );

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var contracts = await query.ToListAsync(ct);

        return (contracts, count);
    }

    public async Task<(List<EmployerStatusStatement> Data, int RowCount)> GetsEmployerStatusStatementExcelExporter(
        List<long>? ids,
        long costCenterId,
        long projectId,
        long? employerId,
        long? employerContractId,
        string? statusStatementCode,
        List<long>? projectOperationIds,
        DateTime? startDate,
        DateTime? endDate,
        EmployerStatusStatementStatus? status,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(p => p.Project)
                .ThenInclude(c => c.ProjectCostCenters)
                    .ThenInclude(c => c.CostCenter)
            .Include(i => i.EmployerContract)

            .Where(oo =>
                oo.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId) &&
                oo.Project.Id == projectId &&
                (employerId == null || oo.Project.EmployerId == employerId) &&
                (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                (employerContractId == null || oo.EmployerContract.Id == employerContractId) &&
                (string.IsNullOrWhiteSpace(statusStatementCode) || EF.Functions.Like(oo.StatusStatementCode! + "-" + oo.Id.ToString(), statusStatementCode.MakeLikePattern())) &&
                (projectOperationIds == null || oo.EmployerStatusStatementProjectOperations.Any(p => projectOperationIds.Contains(p.ProjectOperation.Id))) &&
                (startDate == null || oo.StartDate >= startDate) &&
                (endDate == null || oo.EndDate <= endDate) &&
                (status == null || oo.Status.Equals(status))
                );

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var contracts = await query.ToListAsync(ct);

        return (contracts, count);
    }

}
