using Engineering.Application.Abstractions.Data.RequestMachineries;
using Engineering.Application.Services.RequestMachineryBills.Models.GetSupplierDrivers;
using Engineering.Domain.Entities.RequestMachineries;
using Engineering.Domain.Entities.RequestMachineries.Enums;
using Engineering.Domain.Entities.RequestMachineryStatusStatements.Enums;

namespace Engineering.Persistence.Repositories.RequestMachineries;

public class RequestMachineryRepository : BaseRepository<EngineeringDBContext, RequestMachinery>, IRequestMachineryRepository
{
    public RequestMachineryRepository(EngineeringDBContext context) : base(context)
    {
    }
    public async Task<long> RequestNumberCreator(long? companyId, CT ct)
    {
        var query = await DbSet
            .Where(x =>
            (companyId == null || x.CompanyId == companyId) &&
            (x.RequestNumber != null && x.RequestNumber > 0))
            .Select(x => (long)x.RequestNumber!).ToListAsync(ct);

        long suggestedRequestedNumber = 1;
        if (query is not null && query.Any())
            suggestedRequestedNumber = query.Max() + 1;

        return suggestedRequestedNumber;
    }
    public async Task<RequestMachinery?> GetByIdAsync(long requestMachineryId, CT ct)
    {
        var query = DbSet
            .Include(oo => oo.Project)
                .ThenInclude(oo => oo.ProjectCostCenters)
                    .ThenInclude(oo => oo.CostCenter)
            .Include(oo => oo.Machinery.MachineriesGroup)
            .Include(oo => oo.Histories)
            .Include(oo => oo.RequestMachineryDocuments)
            .Include(oo => oo.RequestMachineryBillDocuments)
            .Include(oo => oo.InquiryOperators)
                .ThenInclude(x => x.Inquiries)
                    .ThenInclude(z => z.RequestMachineryInquiryDocuments)
            .Include(oo => oo.ProjectOperations)
                .ThenInclude(oo => oo.ProjectOperation)
                    .ThenInclude(oo => oo.OperationInfo)
            .Include(oo => oo.ProjectOperationDetails)
                .ThenInclude(oo => oo.ProjectOperationDetail)
                    .ThenInclude(oo => oo.OperationLocation)
            .Include(oo => oo.RequestMachineryAssignments)
            .Include(oo => oo.DailyMachineries)
            .Include(x => x.MachineryReservations)
                .ThenInclude(x => x.FixAssetMachinery)
                    .ThenInclude(x => x.FixAssetMachineryRates)
            .Where(oo => oo.Id.Equals(requestMachineryId))
            .OrderByDescending(oo => oo.Created);

        return await query.FirstOrDefaultAsync(ct);
    }
    public async Task<RequestMachinery?> GetByIdIncludeLess(long requestMachineryId, CT ct)
    {
        var query = DbSet
            .Include(x => x.Project)
                .ThenInclude(x => x.ProjectCostCenters)
                    .ThenInclude(x => x.CostCenter)
            .Include(x => x.InquiryOperators)
                .ThenInclude(x => x.Inquiries)
            .Include(x => x.Machinery)
            .Include(x => x.RequestMachineryBills)
            .Include(x => x.RequestMachineryBillDocuments)
            .Include(x => x.RequestMachineryDocuments)
            .Include(x => x.ContractorMachinery)
            .Include(x => x.Histories)
            .Include(x => x.MachineryReservations)
               .ThenInclude(x => x.FixAssetMachinery)
                    .ThenInclude(x => x.FixAssetMachineryRates)
            .Where(oo => oo.Id.Equals(requestMachineryId))
            .OrderByDescending(oo => oo.Created);

        return await query.FirstOrDefaultAsync(ct);
    }
    public async Task<RequestMachinery?> GetByIdForUpdateMachineryAsync(long requestMachineryId, CT ct)
    {
        var query = DbSet
                         .Include(oo => oo.Machinery.MachineriesGroup)
                         .Include(oo => oo.InquiryOperators)
                            .ThenInclude(x => x.Inquiries)
                         .Include(x => x.ContractorMachinery)
                         .Include(x => x.MachineryReservations)
                            .ThenInclude(x => x.FixAssetMachinery)
                                .ThenInclude(x => x.FixAssetMachineryRates)
                         .Where(oo => oo.Id.Equals(requestMachineryId))
            .OrderByDescending(oo => oo.Created);

        return await query.FirstOrDefaultAsync(ct);
    }
    public async Task<List<RequestMachinery>?> GetByIdsAsync(List<long> requestMachineryIds, CT ct)
    {
        var query = DbSet
            .Include(x => x.Histories)
            .Include(x => x.Machinery)
            .Where(oo => requestMachineryIds.Contains(oo.Id))
            .OrderByDescending(oo => oo.Created);

        return await query.ToListAsync(ct);
    }
    public async Task<List<RequestMachinery>?> GetByFixAssetId(long? FixAssetId, CT ct)
    => await DbSet.Where(r => r.MachineryReservations.Any(x => x.FixAssetMachinery.Id == FixAssetId)).ToListAsync(ct);

    public async Task<(List<RequestMachinery> Data, int RowCount)> GetFiltered(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        List<long>? contractorIds,
        List<long>? projectOperationIds,
        List<long>? operationInfoIds,
        long? machineriesGroupId,
        long? machineryId,
        RequestMachineryStatus? status,
        RequestMachineryPaymentType? paymentType,
        DateTime? fromDate,
        DateTime? toDate,
        DateTime? confirmedFromDate,
        DateTime? confirmedToDate,
        long? creatorId,
        long? operatorAppoinmentUserId,
        int? requestNumber,
        string? driverName,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Include(r => r.Machinery)
            .Include(r => r.Project.ProjectCostCenters)
            .ThenInclude(r => r.CostCenter)
            .Include(r => r.Histories)
            .Include(r => r.RequestMachineryDocuments)
            .Include(r => r.RequestMachineryBillDocuments)
            .Include(r => r.Machinery.MachineriesGroup)
            .Include(r => r.ProjectOperations)
            .ThenInclude(r => r.ProjectOperation.OperationInfo)
            .Include(r => r.ProjectOperationDetails)
            .ThenInclude(r => r.ProjectOperationDetail.OperationLocation)
            .Include(r => r.InquiryOperators)
            .ThenInclude(r => r.Inquiries)
            .Include(r => r.RequestMachineryStatusStatementDetails)
            .ThenInclude(r => r.RequestMachineryStatusStatement)
            .Where(r => !r.Machinery.IsDeleted && !r.Machinery.MachineriesGroup.IsDeleted &&
            (companyId == null || r.CompanyId == companyId) &&
            (contractorIds == null || (r.ContractorId.HasValue && contractorIds.Contains(r.ContractorId.Value))) &&
            (costCenterId == null || r.Project!.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
            (projectId == null || r.Project!.Id == projectId) &&
            (requestNumber == null || r.RequestNumber == requestNumber) &&
            (ids == null || ids.Count == 0 || ids.Contains(r.Id)) &&
            (operatorAppoinmentUserId == null || r.OperatorAppoinmentUserId == operatorAppoinmentUserId) &&
            (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(r.RequestNumber.ToString(), filterData.MakeLikePattern()) || EF.Functions.Like(r.Machinery!.MachineryName.ToString(), filterData.MakeLikePattern())) &&
            (driverName == null || string.IsNullOrEmpty(driverName) || (!string.IsNullOrEmpty(r.DriverName) && EF.Functions.Like(r.DriverName.ToString(), driverName.MakeLikePattern()))) &&
            (machineriesGroupId == null || r.Machinery.MachineriesGroup!.Id == machineriesGroupId) &&
            (machineryId == null || r.Machinery!.Id == machineryId) &&
            (status == null || r.Status == status) &&
            (fromDate == null || (r.FromDate != null && r.FromDate.Value.Date >= fromDate.Value.Date)) &&
            (toDate == null || (r.ToDate != null && r.ToDate.Value.Date <= toDate.Value.Date)) &&
            (confirmedFromDate == null || (r.ConfirmFromDate.HasValue && r.ConfirmFromDate.Value.Date >= confirmedFromDate.Value.Date)) &&
            (confirmedToDate == null || (r.ConfirmToDate.HasValue && r.ConfirmToDate.Value.Date <= confirmedToDate.Value.Date)) &&
            (projectOperationIds == null || r.ProjectOperations.Any(c => projectOperationIds.Contains(c.ProjectOperation.Id))) &&
            (operationInfoIds == null || r.ProjectOperations.Any(c => operationInfoIds.Contains(c.ProjectOperation.OperationInfo.Id))) &&
                                     (creatorId == null || r.CreatorId.Equals(creatorId)));

        if (paymentType != null)
        {
            if (paymentType == RequestMachineryPaymentType.Paid)
            {
                query = query
                    .Where(x => x.RequestMachineryStatusStatementDetails.Any() &&
                                x.RequestMachineryStatusStatementDetails.Any(z => !z.RequestMachineryStatusStatement.IsDeleted) &&
                                (x.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentDate != null) ||
                                 x.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentOrderId != null)));
            }

            if (paymentType == RequestMachineryPaymentType.NotPaid)
            {
                query = query.Where(x => !x.RequestMachineryStatusStatementDetails.Any());
            }
        }

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }
    public async Task<(List<RequestMachinery> Data, int RowCount)> GetTotalFiltered(
        List<long>? ids,
        long? costCenterId,
        long? projectId,
        List<long>? contractorIds,
        List<long>? projectOperationIds,
        List<long>? operationInfoIds,
        long? machineriesGroupId,
        long? machineryId,
        RequestMachineryStatus? status,
        RequestMachineryPaymentType? paymentType,
        DateTime? fromDate,
        DateTime? toDate,
        DateTime? confirmedFromDate,
        DateTime? confirmedToDate,
        long? creatorId,
        long? operatorAppoinmentUserId,
        int? requestNumber,
        string? driverName,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Include(r => r.InquiryOperators)
                .ThenInclude(x => x.Inquiries)
            .Include(x => x.ContractorMachinery)
            .Include(x => x.MachineryReservations)
                .ThenInclude(x => x.FixAssetMachinery)
                    .ThenInclude(x => x.FixAssetMachineryRates)
            .Where(r => !r.Machinery.IsDeleted && !r.Machinery.MachineriesGroup.IsDeleted &&
                (companyId == null || r.CompanyId == companyId) &&
                (contractorIds == null || (r.ContractorId.HasValue && contractorIds.Contains(r.ContractorId.Value))) &&
                (costCenterId == null || r.Project!.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                (projectId == null || r.Project!.Id == projectId) &&
                (requestNumber == null || r.RequestNumber == requestNumber) &&
                (ids == null || ids.Count == 0 || ids.Contains(r.Id)) &&
                (operatorAppoinmentUserId == null || r.OperatorAppoinmentUserId == operatorAppoinmentUserId) &&
                (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(r.RequestNumber.ToString(), filterData.MakeLikePattern()) || EF.Functions.Like(r.Machinery!.MachineryName.ToString(), filterData.MakeLikePattern())) &&
                (driverName == null || string.IsNullOrEmpty(driverName) || (!string.IsNullOrEmpty(r.DriverName) && EF.Functions.Like(r.DriverName.ToString(), driverName.MakeLikePattern()))) &&
                (machineriesGroupId == null || r.Machinery.MachineriesGroup!.Id == machineriesGroupId) &&
                (machineryId == null || r.Machinery!.Id == machineryId) &&
                (status == null || r.Status == status) &&
                (fromDate == null || (r.FromDate != null && r.FromDate.Value.Date >= fromDate.Value.Date)) &&
                (toDate == null || (r.ToDate != null && r.ToDate.Value.Date <= toDate.Value.Date)) &&
                (confirmedFromDate == null || (r.ConfirmFromDate.HasValue && r.ConfirmFromDate.Value.Date >= confirmedFromDate.Value.Date)) &&
                (confirmedToDate == null || (r.ConfirmToDate.HasValue && r.ConfirmToDate.Value.Date <= confirmedToDate.Value.Date)) &&
                (projectOperationIds == null || r.ProjectOperations.Any(c => projectOperationIds.Contains(c.ProjectOperation.Id))) &&
                                     (creatorId == null || r.CreatorId.Equals(creatorId)));

        if (paymentType != null)
        {
            if (paymentType == RequestMachineryPaymentType.Paid)
            {
                query = query.Where(x => x.RequestMachineryStatusStatementDetails.Any() &&
                                         x.RequestMachineryStatusStatementDetails.Any(z => !z.RequestMachineryStatusStatement.IsDeleted) &&
                                         (x.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentDate != null) ||
                                          x.RequestMachineryStatusStatementDetails.Any(z => z.RequestMachineryStatusStatement.PaymentOrderId != null)));
            }

            if (paymentType == RequestMachineryPaymentType.NotPaid)
            {
                query = query.Where(x => !x.RequestMachineryStatusStatementDetails.Any());
            }
        }

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestMachinery> Data, int RowCount)> GetOnProjectMachineryReports(
       List<long>? ids,
       List<long>? costCenterIds,
       List<long>? projectIds,
       long? contractorId,
       List<long>? projectOperationIds,
       List<long>? projectOperationDetailIds,
       List<long>? machineryIds,
       RequestMachineryUnit? unit,
       List<RequestMachineryStatus>? statuses,
       DateTime? startDate,
       DateTime? endDate,
       DateTime? confirmedFromDate,
       DateTime? confirmedToDate,
       string? filterData,
       bool? ownCompany,
       string[]? orderBy,
       long? companyId,
       int pageIndex,
       int pageSize,
       CT ct)
    {
        var query = DbSet.Include(oo => oo.Machinery.ContractorMachineries)
                         .Include(oo => oo.MachineryReservations)
                            .ThenInclude(x => x.FixAssetMachinery)
                                .ThenInclude(x => x.FixAssetMachineryRates)
                         .Include(oo => oo.Project.ProjectCostCenters)
                            .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.InquiryOperators)
                            .ThenInclude(oo => oo.Inquiries)
                         .Include(oo => oo.Histories)
                         .Include(oo => oo.RequestMachineryDocuments)
                         .Include(oo => oo.RequestMachineryBillDocuments)
                         .Include(oo => oo.Machinery.MachineriesGroup)
                         .Include(oo => oo.ProjectOperations)
                            .ThenInclude(oo => oo.ProjectOperation.OperationInfo)
                         .Include(oo => oo.ProjectOperationDetails)
                            .ThenInclude(oo => oo.ProjectOperationDetail.OperationLocation)
                         .Where(oo => RequestMachineryStatusValidator.AllowforShowInOnProjectReport.Contains(oo.Status) &&

                                     ((oo.RequestMachineryStatusStatementDetails == null || oo.RequestMachineryStatusStatementDetails.Count <= 0) ||
                                      oo.RequestMachineryStatusStatementDetails.Any(x => x.RequestMachineryStatusStatement.PaymentOrderId == null ||
                                                                                        x.RequestMachineryStatusStatement.PaymentOrderId <= 0) ||
                                      oo.RequestMachineryStatusStatementDetails.All(x => x.RequestMachineryStatusStatement.Status == RequestMachineryStatusStatementStatus.Invalidated)) &&

                                     (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                                     (companyId == null || oo.CompanyId == companyId) &&
                                     (costCenterIds == null || oo.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                                     (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
                                     (projectOperationIds == null || oo.ProjectOperations.Any(c => projectOperationIds.Contains(c.ProjectOperation.Id))) &&
                                     (projectOperationDetailIds == null || oo.ProjectOperationDetails.Any(c => projectOperationDetailIds.Contains(c.ProjectOperationDetail.Id))) &&
                                     (ownCompany == true ? (oo.ContractorId == null) : (contractorId == null || oo.ContractorId == contractorId)) &&
                                     (machineryIds == null || machineryIds.Contains(oo.Machinery.Id)) &&
                                     (unit == null || oo.Unit == unit) &&
                                     (statuses == null || statuses.Count == 0 || statuses.Contains(oo.Status)) &&
                                     (filterData == null || string.IsNullOrEmpty(filterData) ||
                                     EF.Functions.Like(oo.RequestNumber.ToString(), filterData.MakeLikePattern()) ||
                                     EF.Functions.Like(oo.Machinery!.MachineryName.ToString(), filterData.MakeLikePattern())) &&
                                     (startDate == null || (oo.FromDate.HasValue && oo.FromDate.Value.Date >= startDate.Value.Date)) &&
                                     (endDate == null || (oo.ToDate.HasValue && oo.ToDate.Value.Date <= endDate.Value.Date)) &&
                                     (confirmedFromDate == null || (oo.ConfirmFromDate.HasValue && oo.ConfirmFromDate.Value.Date >= confirmedFromDate.Value.Date)) &&
                                     (confirmedToDate == null || (oo.ConfirmToDate.HasValue && oo.ConfirmToDate.Value.Date <= confirmedToDate.Value.Date)));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestMachinery> Data, int RowCount)> GetSendManagerMachineryReports(
       List<long>? ids,
       List<long>? costCenterIds,
       List<long>? projectIds,
       long? contractorId,
       List<long>? projectOperationIds,
       List<long>? projectOperationDetailIds,
       List<long>? machineryIds,
       RequestMachineryUnit? unit,
       List<RequestMachineryStatus>? statuses,
       DateTime? startDate,
       DateTime? endDate,
       DateTime? confirmedFromDate,
       DateTime? confirmedToDate,
       string? filterData,
       bool? ownCompany,
       string[]? orderBy,
       long? companyId,
       int pageIndex,
       int pageSize,
       CT ct)
    {
        var query = DbSet.Include(oo => oo.Machinery.ContractorMachineries)
                         .Include(oo => oo.MachineryReservations)
                            .ThenInclude(x => x.FixAssetMachinery)
                                .ThenInclude(x => x.FixAssetMachineryRates)
                         .Include(oo => oo.Project.ProjectCostCenters)
                            .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.InquiryOperators)
                            .ThenInclude(oo => oo.Inquiries)
                         .Include(oo => oo.Histories)
                         .Include(oo => oo.RequestMachineryDocuments)
                         .Include(oo => oo.RequestMachineryBillDocuments)
                         .Include(oo => oo.Machinery.MachineriesGroup)
                         .Include(oo => oo.ProjectOperations)
                            .ThenInclude(oo => oo.ProjectOperation.OperationInfo)
                         .Include(oo => oo.ProjectOperationDetails)
                            .ThenInclude(oo => oo.ProjectOperationDetail.OperationLocation)
                         .Where(oo => RequestMachineryStatusValidator.AllowforShowInSendManagerReport.Contains(oo.Status) &&

                                     ((oo.RequestMachineryStatusStatementDetails == null || oo.RequestMachineryStatusStatementDetails.Count <= 0) ||
                                      oo.RequestMachineryStatusStatementDetails.Any(x => x.RequestMachineryStatusStatement.PaymentOrderId == null ||
                                                                                        x.RequestMachineryStatusStatement.PaymentOrderId <= 0) ||
                                      oo.RequestMachineryStatusStatementDetails.All(x => x.RequestMachineryStatusStatement.Status == RequestMachineryStatusStatementStatus.Invalidated)) &&

                                     (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                                     (companyId == null || oo.CompanyId == companyId) &&
                                     (costCenterIds == null || oo.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                                     (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
                                     (projectOperationIds == null || oo.ProjectOperations.Any(c => projectOperationIds.Contains(c.ProjectOperation.Id))) &&
                                     (projectOperationDetailIds == null || oo.ProjectOperationDetails.Any(c => projectOperationDetailIds.Contains(c.ProjectOperationDetail.Id))) &&
                                     (ownCompany == true ? (oo.ContractorId == null) : (contractorId == null || oo.ContractorId == contractorId)) &&
                                     (machineryIds == null || machineryIds.Contains(oo.Machinery.Id)) &&
                                     (unit == null || oo.Unit == unit) &&
                                     (statuses == null || statuses.Count == 0 || statuses.Contains(oo.Status)) &&
                                     (filterData == null || string.IsNullOrEmpty(filterData) ||
                                     EF.Functions.Like(oo.RequestNumber.ToString(), filterData.MakeLikePattern()) ||
                                     EF.Functions.Like(oo.Machinery!.MachineryName.ToString(), filterData.MakeLikePattern())) &&
                                     (startDate == null || (oo.FromDate.HasValue && oo.FromDate.Value.Date >= startDate.Value.Date)) &&
                                     (endDate == null || (oo.ToDate.HasValue && oo.ToDate.Value.Date <= endDate.Value.Date)) &&
                                     (confirmedFromDate == null || (oo.ConfirmFromDate.HasValue && oo.ConfirmFromDate.Value.Date >= confirmedFromDate.Value.Date)) &&
                                     (confirmedToDate == null || (oo.ConfirmToDate.HasValue && oo.ConfirmToDate.Value.Date <= confirmedToDate.Value.Date)));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestMachinery> Data, int RowCount)> GetManagerConfirmedMachineryReports(
       List<long>? ids,
       List<long>? costCenterIds,
       List<long>? projectIds,
       long? contractorId,
       List<long>? projectOperationIds,
       List<long>? projectOperationDetailIds,
       List<long>? machineryIds,
       RequestMachineryUnit? unit,
       List<RequestMachineryStatus>? statuses,
       DateTime? startDate,
       DateTime? endDate,
       DateTime? confirmedFromDate,
       DateTime? confirmedToDate,
       string? filterData,
       bool? ownCompany,
       string[]? orderBy,
       long? companyId,
       int pageIndex,
       int pageSize,
       CT ct)
    {
        var query = DbSet.Include(oo => oo.Machinery.ContractorMachineries)
                         .Include(oo => oo.MachineryReservations)
                            .ThenInclude(x => x.FixAssetMachinery)
                                .ThenInclude(x => x.FixAssetMachineryRates)
                         .Include(oo => oo.Project.ProjectCostCenters)
                            .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.InquiryOperators)
                            .ThenInclude(oo => oo.Inquiries)
                         .Include(oo => oo.Histories)
                         .Include(oo => oo.RequestMachineryDocuments)
                         .Include(oo => oo.RequestMachineryBillDocuments)
                         .Include(oo => oo.Machinery.MachineriesGroup)
                         .Include(oo => oo.ProjectOperations)
                            .ThenInclude(oo => oo.ProjectOperation.OperationInfo)
                         .Include(oo => oo.ProjectOperationDetails)
                            .ThenInclude(oo => oo.ProjectOperationDetail.OperationLocation)
                         .Where(oo => oo.Status == RequestMachineryStatus.ManagerConfirm &&

                                     ((oo.RequestMachineryStatusStatementDetails == null || oo.RequestMachineryStatusStatementDetails.Count <= 0) ||
                                      oo.RequestMachineryStatusStatementDetails.Any(x => x.RequestMachineryStatusStatement.PaymentOrderId == null ||
                                                                                        x.RequestMachineryStatusStatement.PaymentOrderId <= 0) ||
                                      oo.RequestMachineryStatusStatementDetails.All(x => x.RequestMachineryStatusStatement.Status == RequestMachineryStatusStatementStatus.Invalidated)) &&

                                     (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                                     (companyId == null || oo.CompanyId == companyId) &&
                                     (statuses == null || statuses.Count == 0 || statuses.Contains(oo.Status)) &&
                                     (costCenterIds == null || oo.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                                     (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
                                     (projectOperationIds == null || oo.ProjectOperations.Any(c => projectOperationIds.Contains(c.ProjectOperation.Id))) &&
                                     (projectOperationDetailIds == null || oo.ProjectOperationDetails.Any(c => projectOperationDetailIds.Contains(c.ProjectOperationDetail.Id))) &&
                                     (ownCompany == true ? (oo.ContractorId == null) : (contractorId == null || oo.ContractorId == contractorId)) &&
                                     (machineryIds == null || machineryIds.Contains(oo.Machinery.Id)) &&
                                     (unit == null || oo.Unit == unit) &&
                                     (filterData == null || string.IsNullOrEmpty(filterData) ||
                                     EF.Functions.Like(oo.RequestNumber.ToString(), filterData.MakeLikePattern()) ||
                                     EF.Functions.Like(oo.Machinery!.MachineryName.ToString(), filterData.MakeLikePattern())) &&
                                     (startDate == null || (oo.FromDate.HasValue && oo.FromDate.Value.Date >= startDate.Value.Date)) &&
                                     (endDate == null || (oo.ToDate.HasValue && oo.ToDate.Value.Date <= endDate.Value.Date)) &&
                                     (confirmedFromDate == null || (oo.ConfirmFromDate.HasValue && oo.ConfirmFromDate.Value.Date >= confirmedFromDate.Value.Date)) &&
                                     (confirmedToDate == null || (oo.ConfirmToDate.HasValue && oo.ConfirmToDate.Value.Date <= confirmedToDate.Value.Date)));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestMachinery> Data, int RowCount)> GetOnProjectRequestReports(
       List<long>? ids,
       List<long>? costCenterIds,
       List<long>? projectIds,
       long? contractorId,
       List<long>? projectOperationIds,
       List<long>? projectOperationDetailIds,
       List<long>? machineryIds,
       RequestMachineryUnit? unit,
       RequestMachineryPaymentType? paymentType,
       DateTime? startDate,
       DateTime? endDate,
       DateTime? confirmedFromDate,
       DateTime? confirmedToDate,
       string? filterData,
       bool? ownCompany,
       string[]? orderBy,
       long? companyId,
       int pageIndex,
       int pageSize,
       CT ct)
    {
        var query = DbSet
                         .Include(oo => oo.Machinery.ContractorMachineries)
                         .Include(oo => oo.MachineryReservations)
                            .ThenInclude(x => x.FixAssetMachinery)
                                .ThenInclude(x => x.FixAssetMachineryRates)
                         .Include(oo => oo.Project.ProjectCostCenters)
                            .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.InquiryOperators)
                            .ThenInclude(oo => oo.Inquiries)
                         .Include(oo => oo.Histories)
                         .Include(oo => oo.RequestMachineryDocuments)
                         .Include(oo => oo.RequestMachineryBillDocuments)
                         .Include(oo => oo.Machinery.MachineriesGroup)
                         .Include(oo => oo.ProjectOperations)
                            .ThenInclude(oo => oo.ProjectOperation.OperationInfo)
                         .Include(oo => oo.ProjectOperationDetails)
                            .ThenInclude(oo => oo.ProjectOperationDetail.OperationLocation)
                         .Include(oo => oo.RequestMachineryStatusStatementDetails)
                            .ThenInclude(oo => oo.RequestMachineryStatusStatement)
                         .Where(oo => RequestMachineryStatusValidator.AllowForShowInReport.Any(x => x == oo.Status) &&
                                     (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                                     (companyId == null || oo.CompanyId == companyId) &&
                                     (costCenterIds == null || oo.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
                                     (projectIds == null || projectIds.Contains(oo.Project.Id)) &&
                                     (projectOperationIds == null || oo.ProjectOperations.Any(c => projectOperationIds.Contains(c.ProjectOperation.Id))) &&
                                     (projectOperationDetailIds == null || oo.ProjectOperationDetails.Any(c => projectOperationDetailIds.Contains(c.ProjectOperationDetail.Id))) &&
                                     (ownCompany == true ? (oo.ContractorId == null) : (contractorId == null || oo.ContractorId == contractorId)) &&
                                     (machineryIds == null || machineryIds.Contains(oo.Machinery.Id)) &&
                                     (unit == null || oo.Unit == unit) &&
                                     (filterData == null || string.IsNullOrEmpty(filterData) ||
                                     EF.Functions.Like(oo.RequestNumber.ToString(), filterData.MakeLikePattern()) ||
                                     EF.Functions.Like(oo.Machinery!.MachineryName.ToString(), filterData.MakeLikePattern())) &&
                                     (startDate == null || (oo.FromDate.HasValue && oo.FromDate.Value.Date >= startDate.Value.Date)) &&
                                     (endDate == null || (oo.ToDate.HasValue && oo.ToDate.Value.Date <= endDate.Value.Date)) &&
                                     (confirmedFromDate == null || (oo.ConfirmFromDate.HasValue && oo.ConfirmFromDate.Value.Date >= confirmedFromDate.Value.Date)) &&
                                     (confirmedToDate == null || (oo.ConfirmToDate.HasValue && oo.ConfirmToDate.Value.Date <= confirmedToDate.Value.Date)));

        if (paymentType != null)
        {
            if (paymentType == RequestMachineryPaymentType.Paid)
            {
                query = query.Where(x => x.RequestMachineryStatusStatementDetails.Any() &&
                                         (x.RequestMachineryStatusStatementDetails.Any(z =>
                                             z.RequestMachineryStatusStatement.Status != RequestMachineryStatusStatementStatus.Invalidated &&
                                             z.RequestMachineryStatusStatement.PaymentOrderId != null)));
            }

            if (paymentType == RequestMachineryPaymentType.NotPaid)
            {
                query = query.Where(x => !x.RequestMachineryStatusStatementDetails.Any());
            }
        }

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<RequestMachinery> Data, int RowCount)> GetsMachineryRequesteExcelExporter(List<long>? ids,
        long? costCenterId,
        long? projectId,
        List<long>? projectOperationIds,
        long? machineriesGroupId,
        long? machineryId,
        RequestMachineryStatus? status,
        DateTime? fromDate,
        DateTime? toDate,
        DateTime? confirmedFromDate,
        DateTime? confirmedToDate,
        long? creatorId,
        long? operatorAppoinmentUserId,
        int? requestNumber,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Include(oo => oo.Machinery)
                         .Include(oo => oo.Project.ProjectCostCenters)
                            .ThenInclude(oo => oo.CostCenter)
                         .Include(oo => oo.InquiryOperators)
                            .ThenInclude(oo => oo.Inquiries)
                         .Include(oo => oo.Histories)
                         .Include(oo => oo.Machinery.MachineriesGroup)
                         .Include(oo => oo.ProjectOperations)
                            .ThenInclude(oo => oo.ProjectOperation.OperationInfo)
                         .Include(oo => oo.ProjectOperationDetails)
                            .ThenInclude(oo => oo.ProjectOperationDetail.OperationLocation)

                         .Where(oo =>
                                     (companyId == null || oo.CompanyId == companyId) &&
                                     (costCenterId == null || oo.Project!.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) &&
                                     (projectId == null || oo.Project!.Id == projectId) &&
                                     (requestNumber == null || oo.RequestNumber == requestNumber) &&
                                     (ids == null || ids.Count == 0 || ids.Contains(oo.Id)) &&
                                     (operatorAppoinmentUserId == null || oo.OperatorAppoinmentUserId == operatorAppoinmentUserId) &&
                                     (filterData == null || string.IsNullOrEmpty(filterData) || EF.Functions.Like(oo.RequestNumber.ToString(), filterData.MakeLikePattern()) || EF.Functions.Like(oo.Machinery!.MachineryName.ToString(), filterData.MakeLikePattern())) &&
                                     (machineriesGroupId == null || oo.Machinery.MachineriesGroup!.Id == machineriesGroupId) &&
                                     (machineryId == null || oo.Machinery!.Id == machineryId) &&
                                     (status == null || oo.Status == status) &&
                                     (fromDate == null || (oo.FromDate.HasValue && oo.FromDate.Value.Date >= fromDate.Value.Date)) &&
                                     (toDate == null || (oo.ToDate.HasValue && oo.ToDate.Value.Date <= toDate.Value.Date)) &&
                                     (confirmedFromDate == null || (oo.ConfirmFromDate.HasValue && oo.ConfirmFromDate.Value.Date >= confirmedFromDate.Value.Date)) &&
                                     (confirmedToDate == null || (oo.ConfirmToDate.HasValue && oo.ConfirmToDate.Value.Date <= confirmedToDate.Value.Date)) &&
                                     (projectOperationIds == null || oo.ProjectOperations.Any(c => projectOperationIds.Contains(c.ProjectOperation.Id))) &&
                                     (creatorId == null || oo.CreatorId.Equals(creatorId)));

        query = query.OrderByDescending(oo => oo.Created);
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
           .Where(x =>
           !x.IsDeleted &&
           (costCenterIds == null || x.Project.ProjectCostCenters.Any(x => costCenterIds.Contains(x.CostCenterId))) &&
           (projectIds == null || projectIds.Contains(x.Project.Id)) &&
           (x.ContractorId != null && x.ContractorId > 0))
           .Select(c => (long)c.ContractorId!).Distinct();

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetOperationContractors(
        long? RequestMachineryId,
        CT ct)
    {
        var query = DbSet
            .Include(x => x.ProjectOperations)
                .ThenInclude(x => x.ProjectOperation.OperationInfo.OperationInfoServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorServices)
           .Where(x =>
           !x.IsDeleted &&
           (RequestMachineryId == null || x.Id == RequestMachineryId))
           .SelectMany(c => c.ProjectOperations).SelectMany(z => z.ProjectOperation.OperationInfo.OperationInfoServices)
           .SelectMany(y => y.ProjectOperationDetailContractorServices).Where(g => g.ContractorId != null && g.ContractorId > 0).Select(g => (long)g.ContractorId!);

        var count = await query.CountAsync(ct);
        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public Task<bool> IsExistRequestMachinery(long projectId, long machineryId, decimal timeRequired, int requestCount, long? companyId, CT ct)
    {
        var cDate = DateTime.Now.AddMinutes(-5);
        var query = DbSet.AnyAsync(oo => oo.Project.Id == projectId &&
                                oo.Machinery.Id == machineryId &&
                                oo.TimeRequired == timeRequired &&
                                oo.RequestCount == requestCount &&
                                (companyId == null || oo.CompanyId == companyId) &&
                                oo.Created > cDate
                                , ct);
        return query;
    }

    public async Task<(List<RequestMachinery> Data, int RowCount)> GetFilteredForDailyAsync(
        long projectOperationDetailId,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.Include(oo => oo.Project.ProjectOperations)
                            .ThenInclude(x => x.ProjectOperationDetails)
                                .ThenInclude(oo => oo.OperationLocation)
                         .Include(oo => oo.Project.ProjectOperations)
                            .ThenInclude(x => x.ProjectOperationDetails)
                                .ThenInclude(oo => oo.ConsumableVolumeMachineries)
                                    .ThenInclude(oo => oo.Machinery)
                         .Include(oo => oo.Machinery)
                                .ThenInclude(oo => oo!.MachineriesGroup)
                            .Include(oo => oo.RequestMachineryAssignments)
                         .Where(oo => oo.Project.ProjectOperations.Any(x => x.ProjectOperationDetails.Any(z => z.Id.Equals(projectOperationDetailId))) &&
                                  oo.Status == Domain.Entities.RequestMachineries.Enums.RequestMachineryStatus.OnProject &&
                                  (filterData == null || string.IsNullOrWhiteSpace(filterData) ||
                                   EF.Functions.Like(oo.Machinery!.MachineryName, filterData.MakeLikePattern()) ||
                                   EF.Functions.Like(oo.Machinery!.MachineryCode, filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);
        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var items = await query.ToListAsync(ct);
        return (items, count);
    }

    public async Task<GetRequestMachineryDriverModel?> GetRequestMachineryDriver(long? requestMachineryId, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet.Where(oo => oo.Id == requestMachineryId &&
                               (oo.MachineryReservations != null && oo.MachineryReservations.Count > 0))
                         .Select(x => new GetRequestMachineryDriverModel()
                         {
                             DriverId = x.MachineryReservations.FirstOrDefault().FixAssetMachinery.DriverId,
                             DriverName = x.MachineryReservations.FirstOrDefault().FixAssetMachinery.DriverName
                         });
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        var item = await query.FirstOrDefaultAsync(ct);

        return item;
    }
}
