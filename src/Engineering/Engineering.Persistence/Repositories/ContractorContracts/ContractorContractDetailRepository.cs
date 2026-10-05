using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ProjectOperationDetails.Enums;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractDetailRepository
    : BaseRepository<EngineeringDBContext, ContractorContractDetail>,
      IContractorContractDetailRepository
{
    public ContractorContractDetailRepository(
        EngineeringDBContext context)
        : base(context)
    {
    }

    public async Task<ContractorContractDetail?> GetContractorContractDetailById(
        long id,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.ContractorStatusStatementServices)
                .ThenInclude(c =>
                    c.ContractorStatusStatementDetail.ContractorStatusStatement)

            .Include(c => c.ContractorContractDetailPrices)

            // PODCS
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService)

            // ServiceBased
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .OperationInfoService!)
                .ThenInclude(c => c.ServiceInfo)

            // ProjectService
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectServiceDetail!)
                .ThenInclude(c => c.ProjectService)
                .ThenInclude(c => c.ServiceInfo)

            // OperationBased
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectOperationDetail
                        .ProjectOperation)
                .ThenInclude(c => c.OperationInfo)

            .Where(c => c.Id == id);

        return await query
            .AsSplitQuery()
            .SingleOrDefaultAsync(ct);
    }

    public async Task<(List<ContractorContractDetail> Data, int RowCount)>
        GetFilteredContractorContractDetail(
            long? projectOperationServiceId,
            long? projectOperationId,
            DateTime? startDate,
            DateTime? endDate,
            long? contractorId,
            string? filterData,
            long? companyId,
            string[]? orderBy,
            int pageIndex,
            int pageSize,
            CT ct)
    {
        var filterPattern = string.IsNullOrWhiteSpace(filterData)
            ? null
            : filterData.MakeLikePattern();

        var query = DbSet

            .Include(c =>
                c.ContractorContract.ContractorContractHeader)

            .Include(c =>
                c.ContractorContractDetailPrices)

            // ServiceBased
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .OperationInfoService!)
                .ThenInclude(c => c.ServiceInfo)

            // ProjectService
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectServiceDetail!)
                .ThenInclude(c => c.ProjectService)
                .ThenInclude(c => c.ServiceInfo)

            // OperationBased
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectOperationDetail
                        .ProjectOperation)
                .ThenInclude(c => c.OperationInfo)

            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectOperationDetail
                        .OperationLocation)

            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectOperationDetail
                        .ProjectOperation
                        .Project
                        .ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)

            .Where(c =>
                !c.IsDeleted &&

                (
                    projectOperationServiceId == null ||
                    c.ContractorContractDetailServices.Any(s =>
                        s.ProjectOperationDetailContractorService.Id ==
                        projectOperationServiceId)
                ) &&

                (
                    projectOperationId == null ||
                    (
                        c.ProjectOperation != null &&
                        c.ProjectOperation.Id == projectOperationId
                    )
                ) &&

                (
                    companyId == null ||
                    c.ContractorContract
                        .ContractorContractHeader
                        .CompanyId == companyId
                ) &&

                (
                    startDate == null ||
                    c.ContractorContract.StartDate >= startDate
                ) &&

                (
                    endDate == null ||
                    c.ContractorContract.EndDate <= endDate
                ) &&

                (
                    filterPattern == null ||

                    // نام OperationInfo خود ContractDetail
                    (
                        c.ProjectOperation != null &&
                        EF.Functions.Like(
                            c.ProjectOperation
                                .OperationInfo
                                .OperationInfoName,
                            filterPattern)
                    )

                    ||

                    c.ContractorContractDetailServices.Any(s =>

                        // -------------------------
                        // ServiceBased
                        // -------------------------
                        (
                            s.ProjectOperationDetailContractorService.Type ==
                                PODContractorServiceType.ServiceBased &&

                            s.ProjectOperationDetailContractorService
                                .OperationInfoService != null &&

                            (
                                EF.Functions.Like(
                                    s.ProjectOperationDetailContractorService
                                        .OperationInfoService!
                                        .ServiceInfo
                                        .ServiceInfoName,
                                    filterPattern)

                                ||

                                EF.Functions.Like(
                                    s.ProjectOperationDetailContractorService
                                        .OperationInfoService!
                                        .OperationInfo
                                        .OperationInfoName,
                                    filterPattern)
                            )
                        )

                        ||

                        // -------------------------
                        // OperationBased
                        // -------------------------
                        (
                            s.ProjectOperationDetailContractorService.Type ==
                                PODContractorServiceType.OperationBased &&

                            EF.Functions.Like(
                                s.ProjectOperationDetailContractorService
                                    .ProjectOperationDetail
                                    .ProjectOperation
                                    .OperationInfo
                                    .OperationInfoName,
                                filterPattern)
                        )
                    )
                )
            );

        query = query.OrderByDescending(
            c => c.ContractorContract.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query
            .AsSplitQuery()
            .ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<ContractorContractDetail> Data, int RowCount)>
        GetsFilteredContractorContractDetailReports(
            List<long>? ids,
            long? contractorContractId,
            long? contractorId,
            DateTime? startDate,
            DateTime? endDate,
            long? companyId,
            string? filterData,
            string[]? orderBy,
            int pageIndex,
            int pageSize,
            CT ct)
    {
        var filterPattern = string.IsNullOrWhiteSpace(filterData)
            ? null
            : filterData.MakeLikePattern();

        var query = DbSet

            .Include(c =>
                c.ContractorContract.ContractorContractHeader)

            .Include(c =>
                c.ContractorContractDetailPrices)

            // ServiceBased
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .OperationInfoService!)
                .ThenInclude(c => c.ServiceInfo)

            // ProjectService
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectServiceDetail!)
                .ThenInclude(c => c.ProjectService)
                .ThenInclude(c => c.ServiceInfo)

            // OperationBased
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectOperationDetail
                        .ProjectOperation)
                .ThenInclude(c => c.OperationInfo)

            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectOperationDetail
                        .OperationLocation)

            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectOperationDetail
                        .ProjectOperation
                        .Project
                        .ProjectCostCenters)
                .ThenInclude(c => c.CostCenter)

             .Include(c => c.ProjectOperation)
                .ThenInclude(po => po.OperationInfo)

            .Include(c => c.ProjectOperation)
                .ThenInclude(po => po.Project)
            .ThenInclude(p => p.ProjectCostCenters)
                .ThenInclude(pc => pc.CostCenter)

            .Where(c =>
                   !c.IsDeleted &&

                   (
                       ids == null ||
                       ids.Count == 0 ||
                       ids.Contains(c.Id)
                   ) &&

                   (
                       contractorContractId == null ||
                       c.ContractorContract.Id == contractorContractId
                   ) &&

                   (
                       companyId == null ||
                       c.ContractorContract
                           .ContractorContractHeader
                           .CompanyId == companyId
                   ) &&

                   (
                       startDate == null ||
                       c.StartDate == null ||
                       c.StartDate >= startDate
                   ) &&

                   (
                       endDate == null ||
                       c.EndDate == null ||
                       c.EndDate <= endDate
                   ) &&

                   (
                       contractorId == null ||
                       c.ContractorContract
                           .ContractorContractHeader
                           .ContractorId == contractorId
                   ) &&

                   (
                       filterPattern == null ||

                       (
                           c.ProjectOperation != null &&
                           EF.Functions.Like(
                               c.ProjectOperation
                                   .OperationInfo
                                   .OperationInfoName,
                               filterPattern)
                       )

                       ||

                       c.ContractorContractDetailServices.Any(s =>

                           (
                               s.ProjectOperationDetailContractorService.Type ==
                                   PODContractorServiceType.ServiceBased &&

                               s.ProjectOperationDetailContractorService
                                   .OperationInfoService != null &&

                               (
                                    EF.Functions.Like(
                                        s.ProjectOperationDetailContractorService
                                            .OperationInfoService!
                                            .ServiceInfo
                                            .ServiceInfoName,
                                        filterPattern)

                                    ||

                                    EF.Functions.Like(
                                        s.ProjectOperationDetailContractorService
                                            .OperationInfoService!
                                            .OperationInfo
                                            .OperationInfoName,
                                        filterPattern)
                               )
                            )

                            ||

                            (
                                s.ProjectOperationDetailContractorService.Type ==
                                    PODContractorServiceType.OperationBased &&

                                EF.Functions.Like(
                                    s.ProjectOperationDetailContractorService
                                        .ProjectOperationDetail
                                        .ProjectOperation
                                        .OperationInfo
                                        .OperationInfoName,
                                    filterPattern)
                            )
                        )
                    )
                );

        query = query.OrderByDescending(
            c => c.ContractorContract.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query
            .AsSplitQuery()
            .ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<ContractorContractDetail> Data, int RowCount)>
        GetsContractorContractDetailByIds(
            List<long> ids,
            CT ct)
    {
        var query = DbSet

            .Include(c =>
                c.ContractorContract.ContractorContractHeader)

            .Include(c =>
                c.ContractorContractDetailPrices)

            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService)

            // ServiceBased
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .OperationInfoService!)
                .ThenInclude(c => c.ServiceInfo)

            // ProjectService
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectServiceDetail!)
                .ThenInclude(c => c.ProjectService)

            // OperationBased
            .Include(c => c.ContractorContractDetailServices)
                .ThenInclude(c =>
                    c.ProjectOperationDetailContractorService
                        .ProjectOperationDetail
                        .ProjectOperation)
                .ThenInclude(c => c.OperationInfo)

            .Where(c =>
                !c.IsDeleted &&
                ids.Contains(c.Id));

        var count = await query.CountAsync(ct);

        var entities = await query
            .AsSplitQuery()
            .ToListAsync(ct);

        return (entities, count);
    }
}