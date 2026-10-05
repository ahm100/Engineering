using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCByHeaderId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCCThirdParties;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractsDate;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractsByProjectId;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostPaidCC;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCostCentersMostRecentCC;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedFixCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetDraftedServiceCCs;
using Engineering.Application.Services.ContractorContracts.Contracts.GetFltrProjectContractors;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractRepository : BaseRepository<EngineeringDBContext, ContractorContract>, IContractorContractRepository
{
    public ContractorContractRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ContractorContract?> GetContractorContractById(
        long id,
        long companyId,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(c => c.ContractorContractHeader)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters)
                        .ThenInclude(c => c.CostCenter)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailPrices)

            .Where(c => c.Id == id && c.CompanyId == companyId && !c.IsDeleted);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.AsSplitQuery()
                          .FirstOrDefaultAsync(ct);
    }

    public async Task<ContractorContract?> GetContractorContractByIdIncludeless(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)
                        .ThenInclude(c => c.OperationInfoService.ServiceInfo)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)
                        .ThenInclude(c => c.ProjectOperationDetail.ProjectOperation)

            .Include(c => c.Details)
                .ThenInclude(c => c.ProjectOperation)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailPrices)

                         .Where(c => c.Id == id && c.CompanyId == companyId && !c.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<List<GetCCByHeaderIdModel>?> GetCCByHeaderId(
        long id,
        ContractorContractType type,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Where(x => x.ContractorContractHeaderId == id &&
            x.ContractorContractType == type &&
            x.CompanyId == companyId)
            .Select(contract => new GetCCByHeaderIdModel
            {
                Id = contract.Id,
                ContractorContractTypeId = contract.ContractorContractType,
                ProjectId = contract.Project!.Id,
                ProjectName = contract.Project.ProjectName,
                ContractorId = contract.ContractorContractHeader.ContractorId,
                CurrencyId = contract.ContractorContractHeader.CurrencyId,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                TotalAmount = contract.TotalAmount,
                PercentageDoingJobWell = contract.PercentageDoingJobWell,
                DoingJobWellAmount = contract.DoingJobWellAmount,
                PercentageAdvancePayment = contract.PercentageAdvancePayment,
                AdvancePaymentAmount = contract.AdvancePaymentAmount,
                DailyLatenessPenalty = contract.DailyLatenessPenalty,
                WorkDonePercent = contract.WorkDonePercent,
                WorkDeliveryPercent = contract.WorkDeliveryPercent,
                WorkCompletionPercent = contract.WorkCompletionPercent,
                Description = contract.Description,

                CostOvers = contract.ContractorContractDetailCostOvers.Select(cost => new GetCCByHeaderIdDetailCostOverModel()
                {
                    Id = cost.Id,
                    ContractorId = cost.ContractorId,
                    CostOverId = cost.CostOver.Id,
                    CostOverName = cost.CostOver.CostOverName,
                    CostOverCode = cost.CostOver.CostOverCode,
                    Percentage = cost.Percentage,
                    Amount = cost.Amount,
                    Description = cost.Description,
                }).ToList(),

                Details = contract.Details.Select(detail => new GetCCByHeaderIdDetailedModel()
                {
                    Id = detail.Id,
                    UnitAmount = detail.UnitAmount,
                    TotalAmount = detail.TotalAmount,
                    StartDate = detail.StartDate,
                    EndDate = detail.EndDate,

                    Services = detail.ContractorContractDetailServices.Select(service => new GetCCByHeaderIdDetailServiceModel()
                    {
                        Id = service.Id,
                        ProjectOperationDetailContractorServiceId = service.ProjectOperationDetailContractorService.Id,
                        ProjectServiceId =
                        service.ProjectOperationDetailContractorService.ProjectServiceDetail != null
                            ? service.ProjectOperationDetailContractorService.ProjectServiceDetail.ProjectService.Id
                            : null,
                        ProjectServiceDetailId =
                        service.ProjectOperationDetailContractorService.ProjectServiceDetail != null
                            ? service.ProjectOperationDetailContractorService.ProjectServiceDetail.Id
                            : null,
                        ProjectOperationId = service.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Id,
                        ProjectOperationDetailId = service.ProjectOperationDetailContractorService.ProjectOperationDetail.Id,
                        OperationInfoName = service.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName,
                        OperationInfoCode = service.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode,
                        Workload = service.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Workload,
                        ProjectOperationUnitOfMeasurementId = service.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId,
                        ServiceInfoId =
                        service.ProjectOperationDetailContractorService.OperationInfoService != null
                            ? service.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id
                            : 0,
                        ServiceInfoName =
                        service.ProjectOperationDetailContractorService.OperationInfoService != null
                            ? service.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName
                            : null,
                        ServiceInfoCode =
                        service.ProjectOperationDetailContractorService.OperationInfoService != null
                            ? service.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode
                            : null,
                        ServiceInfoVolume = service.ProjectOperationDetailContractorService.Volume,
                        FinalAmount = service.ProjectOperationDetailContractorService.ProjectOperationDetail.FinalAmount,
                        ServiceInfoUnitOfMeasurementId =
                        service.ProjectOperationDetailContractorService.OperationInfoService != null
                            ? service.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId
                            : 0,
                        PrivateCode = service.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateCode,
                        PrivateName = service.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateName,
                        PublicCode = service.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicCode,
                        PublicName = service.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PublicName,
                        Description = service.ProjectOperationDetailContractorService.ProjectOperationDetail.Description,
                    }).ToList(),

                    Prices = detail.ContractorContractDetailPrices.Select(price => new GetCCByHeaderIdDetailPriceModel()
                    {
                        Id = price.Id,
                        StartDate = price.StartDate,
                        EndDate = price.EndDate,
                        Price = price.Price,
                        CurrencyId = price.CurrencyId,
                        IsActive = price.IsActive,
                    }).ToList(),

                    CostOvers = detail.ContractorContractDetailCostOvers.Select(cost => new GetCCByHeaderIdDetailCostOverModel()
                    {
                        Id = cost.Id,
                        ContractorId = cost.ContractorId,
                        CostOverId = cost.CostOver.Id,
                        CostOverName = cost.CostOver.CostOverName,
                        CostOverCode = cost.CostOver.CostOverCode,
                        Percentage = cost.Percentage,
                        Amount = cost.Amount,
                        Description = cost.Description,
                    }).ToList(),
                }).ToList(),
            });

        return await query.ToListAsync(ct);
    }

    public async Task<ContractorContract?> GetServiceContractorContractByIdIncludeless(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)
                        .ThenInclude(c => c.OperationInfoService.ServiceInfo)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectServiceDetail.ProjectService)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)
                        .ThenInclude(c => c.ProjectOperationDetail.ProjectOperation)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailPrices)

            .Where(c => c.Id == id && c.CompanyId == companyId && !c.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractorContract?> GetFixContractorContractByIdIncludeless(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)
            //            .ThenInclude(c => c.DailyOperationServices)
            //                .ThenInclude(c => c.ContractorStatusStatementServiceDailies)

            //.Include(c => c.Details)
            //    .ThenInclude(c => c.ContractorStatusStatementServices)

            .Where(c => c.Id == id && c.CompanyId == companyId && !c.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractorContract?> GetContractorContractForDelete(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)
                        .ThenInclude(c => c.DailyOperationServices)
                            .ThenInclude(c => c.ContractorStatusStatementServiceDailies)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)
                        .ThenInclude(c => c.ProjectServiceDetail.ProjectService)

            .Include(c => c.Details)
                .ThenInclude(c => c.ContractorStatusStatementServices)

            .Where(c => c.Id == id && c.CompanyId == companyId && !c.IsDeleted);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<List<ContractorContractsDateModel>?> GetContractorContractsDate(
        long projectId,
        long contractorId,
        long companyId,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(c =>
                c.Details.Any(x => x.ProjectOperation.Project.Id.Equals(projectId) ||
                x.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id.Equals(projectId)))
                && c.ContractorContractHeader.ContractorId == contractorId
                && c.CompanyId == companyId)
            .Select(cc => new ContractorContractsDateModel()
            {
                StartDate = cc.StartDate,
                EndDate = cc.EndDate,
            });
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        return await query.ToListAsync(ct);
    }

    public async Task<List<GetDraftedFixCCsModel>?> GetDraftedFixCCs(
        long projectId,
        long contractorId,
        DateTime? startDate,
        DateTime? endDate,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Where(c =>
                !c.ContractorContractHeader.IsDeleted &&
                !c.Project!.IsDeleted &&
                c.ContractorContractType == ContractorContractType.Fixed &&
                c.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed &&
                c.Project!.Id == projectId &&
                c.CompanyId == companyId &&
                c.ContractorContractHeader.ContractorId == contractorId //&&
                                                                        //(startDate == null || c.StartDate.Date >= startDate.Value.Date) &&
                                                                        //(endDate == null || c.EndDate.Date <= endDate.Value.Date)
                )
            .Select(cc => new GetDraftedFixCCsModel()
            {
                Id = cc.Id,
                ContractorContractHeaderId = cc.ContractorContractHeader.Id,
                ContractorContractHeaderDescription = cc.ContractorContractHeader.Description,
                ContractorContractType = cc.ContractorContractType.GetEnumDescription(),
                ProjectId = cc.Project!.Id,
                ContractorId = cc.ContractorContractHeader.ContractorId,
                StartDateMiladi = cc.StartDate,
                EndDateMiladi = cc.EndDate,
                TotalAmount = cc.TotalAmount,
                PercentageDoingJobWell = cc.PercentageDoingJobWell,
                DoingJobWellAmount = cc.DoingJobWellAmount,
                PercentageAdvancePayment = cc.PercentageAdvancePayment,
                AdvancePaymentAmount = cc.AdvancePaymentAmount,
                DailyLatenessPenalty = cc.DailyLatenessPenalty,
                Description = cc.Description,
                PODContractorServiceIds = cc.Details.SelectMany(x => x.ContractorContractDetailServices
                    .Select(x => x.ProjectOperationDetailContractorService.Id)).ToList(),
            });

        return await query.ToListAsync(ct);
    }

    public async Task<List<GetDraftedServiceCCsModel>?> GetDraftedServiceCCs(
        long projectId,
        long contractorId,
        DateTime? startDate,
        DateTime? endDate,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Where(c =>
                !c.ContractorContractHeader.IsDeleted &&
                !c.Project!.IsDeleted &&
                c.ContractorContractType == ContractorContractType.Service &&
                c.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed &&
                c.Project!.Id == projectId &&
                c.CompanyId == companyId &&
                c.ContractorContractHeader.ContractorId == contractorId //&&
                                                                        //(startDate == null || c.StartDate.Date >= startDate.Value.Date) &&
                                                                        //(endDate == null || c.EndDate.Date <= endDate.Value.Date)
                )
            .Select(cc => new GetDraftedServiceCCsModel()
            {
                Id = cc.Id,
                ContractorContractHeaderId = cc.ContractorContractHeader.Id,
                ContractorContractHeaderDescription = cc.ContractorContractHeader.Description,
                ContractorContractType = cc.ContractorContractType.GetEnumDescription(),
                ProjectId = cc.Project!.Id,
                ContractorId = cc.ContractorContractHeader.ContractorId,
                StartDateMiladi = cc.StartDate,
                EndDateMiladi = cc.EndDate,
                TotalAmount = cc.TotalAmount,
                PercentageDoingJobWell = cc.PercentageDoingJobWell,
                DoingJobWellAmount = cc.DoingJobWellAmount,
                PercentageAdvancePayment = cc.PercentageAdvancePayment,
                AdvancePaymentAmount = cc.AdvancePaymentAmount,
                DailyLatenessPenalty = cc.DailyLatenessPenalty,
                Description = cc.Description,

                PODContractorServiceIds = cc.Details.SelectMany(x => x.ContractorContractDetailServices
                    .Select(x => x.ProjectOperationDetailContractorService.Id)).ToList(),

                Prices = cc.Details.SelectMany(x => x.ContractorContractDetailPrices
                    .Select(price => new GetDraftedPricesModel()
                    {
                        Id = price.Id,
                        ContractorContractDetailId = price.ContractorContractDetailId,
                        StartDate = price.StartDate,
                        EndDate = price.EndDate,
                        Price = price.Price,
                        IsActive = price.IsActive,
                    })).ToList(),
            });

        return await query.ToListAsync(ct);
    }

    public async Task<(List<ContractorContract> Data, int RowCount)> GetFilteredAsync(
        long? contractorId,
        long? costCenterId,
        List<long>? projectIds,
        List<long>? projectOperationIds,
        List<long>? employerContracts,
        DateTime? fromDate,
        DateTime? toDate,
        ContractorContractStatus? status,
        ContractorContractType? contractorContractTypeId,
        string? filterData,
        string[]? orderBy,
        long companyId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(c =>
                c.CompanyId == companyId &&
                (contractorId == null || c.ContractorContractHeader.ContractorId == contractorId) &&
                (costCenterId == null ||
                    c.Details.Any(oo => oo.ProjectOperation!.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) ||
                    c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)))) &&
                (projectIds == null ||
                    c.Details.Any(oo => projectIds.Contains(oo.ProjectOperation!.Project.Id)) ||
                    c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => projectIds.Contains(s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.Id)))) &&
                (projectOperationIds == null ||
                    c.Details.Any(oo => projectOperationIds.Contains(oo.ProjectOperation!.Id)) ||
                    c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => projectOperationIds.Contains(s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Id)))) &&
                (fromDate == null || c.Created > fromDate) &&
                (toDate == null || c.Created < toDate) &&
                (status == null || c.ContractorContractHeader.Status == status) &&
                (contractorContractTypeId == null || c.ContractorContractType == contractorContractTypeId) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.Id.ToString(), filterData.MakeLikePattern())));

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }

    public async Task<List<ContractorContract>> GetsContractorByContractorContractType(
        ContractorContractType? contractorContractTypeId,
        long companyId,
        CT ct)
    {
        var query = DbSet

            .Include(c => c.ContractorContractHeader)

            .Where(c => c.ContractorContractType == contractorContractTypeId && c.CompanyId == companyId && !c.IsDeleted);

        var count = await query.CountAsync(ct);
        var entites = await query
            .ToListAsync(ct);

        return entites;
    }

    public async Task<(List<ContractorContract> Data, int RowCount)> GetFilteredByContractorIdAsync(
        long contrctorId,
        long? costCenterId,
        List<long>? projects,
        List<long>? contracts,
        DateTime? fromDate,
        DateTime? toDate,
        string? filterData,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(oo => oo.Details)
                .ThenInclude(oo => oo.ProjectOperation)
                    .ThenInclude(oo => oo.Project)
                        .ThenInclude(oo => oo.ProjectCostCenters)
                            .ThenInclude(oo => oo.CostCenter)

             .Include(oo => oo.Details)
                .ThenInclude(oo => oo.ContractorContractDetailServices)
                    .ThenInclude(oo => oo.ProjectOperationDetailContractorService)
                        .ThenInclude(oo => oo.ProjectOperationDetail)
                            .ThenInclude(oo => oo.ProjectOperation)
                                .ThenInclude(oo => oo.Project)
                                    .ThenInclude(oo => oo.ProjectCostCenters)
                                        .ThenInclude(oo => oo.CostCenter)
                         .Where(c =>
                                     c.CompanyId == companyId &&
                                     (projects == null || c.Details.Any(oo => projects.Contains(oo.ProjectOperation!.Project.Id)) ||
                                        c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => projects.Contains(s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.Id)))) &&
                                     (contracts == null || contracts.Contains(c.Id)) &&
                                     (costCenterId == null || c.Details.Any(oo => oo.ProjectOperation!.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) ||
                                        c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)))) &&
                                     (fromDate == null || c.Created > fromDate) &&
                                     (toDate == null || c.Created < toDate) &&
                                     (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.Id.ToString(), filterData.MakeLikePattern())));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }


    public async Task<(List<ContractorContract> Data, int RowCount)> GetsContractorContractByContractorId(
        long contrctorId,
        long? costCenterId,
        List<long>? projectIds,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        long companyId,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
                        .Include(oo => oo.Details)
                            .ThenInclude(oo => oo.ProjectOperation)
                                .ThenInclude(oo => oo.Project)
                                    .ThenInclude(oo => oo.ProjectCostCenters)
                                        .ThenInclude(oo => oo.CostCenter)

                          .Include(oo => oo.Details)
                            .ThenInclude(oo => oo.ContractorContractDetailServices)
                            .ThenInclude(oo => oo.ProjectOperationDetailContractorService)
                                .ThenInclude(oo => oo.ProjectOperationDetail)
                                    .ThenInclude(oo => oo.ProjectOperation)
                                         .ThenInclude(oo => oo.Project)
                                             .ThenInclude(oo => oo.ProjectCostCenters)
                                                .ThenInclude(oo => oo.CostCenter)
                         .Where(c =>
                                     c.CompanyId == companyId &&
                                     (c.ContractorContractHeader.ContractorId == contrctorId) &&
                                     (projectIds == null ||
                                     c.Details.Any(oo => projectIds.Contains(oo.ProjectOperation.Project.Id)) ||
                                        c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => projectIds.Contains(s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.Id)))) &&
                                     (costCenterId == null ||
                                     c.Details.Any(oo => oo.ProjectOperation!.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) ||
                                        c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)))) &&
                                     (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.Id.ToString(), filterData.MakeLikePattern())));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<long> Data, int RowCount)> GetsFilteredContractors(
        List<long>? contractorContractIds,
        long companyId,
        CT ct)
    {
        var query = DbSet

            .Where(c => c.CompanyId == companyId &&
                (contractorContractIds == null || contractorContractIds.Count == 0 || contractorContractIds.Contains(c.ContractorContractHeader.ContractorId)))

            .Select(c => c.ContractorContractHeader.ContractorId).Where(x => x > 0).Distinct();

        var count = await query.CountAsync(ct);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }


    public async Task<long?> GetContractorContractCurrency(
        long projectOperationDetailId,
        long contractorId,
        long companyId,
        CT ct)
    {
        ///TODO
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Possible null reference argument.
#pragma warning disable CS8602 // Possible null reference argument.
        var query = DbSet
                        .Include(c => c.Details)
                            .ThenInclude(x => x.ContractorContractDetailServices)
                            .ThenInclude(x => x.ProjectOperationDetailContractorService)
                                .ThenInclude(p => p.ProjectOperationDetail)
                        .Where(c =>
                                    c.CompanyId == companyId &&
                                    //c.ContractorId == contractorId &&
                                    c.Details.Any(x => x.ContractorContractDetailServices != null && x.ContractorContractDetailServices.Any(x => x.ProjectOperationDetailContractorService.ProjectOperationDetail.Id == projectOperationDetailId)) &&
                                   !c.IsDeleted);
#pragma warning restore CS8602 // Possible null reference argument.
#pragma warning restore CS8625 // Possible null reference argument.
#pragma warning restore CS8604 // Possible null reference argument.

        //var result = await query.Select(c => c.CurrencyId).FirstOrDefaultAsync();
        var result = await query.Select(c => c.Id).FirstOrDefaultAsync();
        return result;
    }

    public async Task<(List<ContractorContract> Data, int RowCount)> GetsFilteredContractorContractReports(
       List<long>? ids,
       long? contractorId,
       long? costCenterId,
       List<long>? projectIds,
       List<long>? contractorContractIds,
       DateTime? fromDate,
       DateTime? toDate,
       long companyId,
       string? filterData,
       string[]? orderBy,
       int pageIndex,
       int pageSize,
       CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.ContractorContractHeader)

                .Include(x => x.Details)
                    .ThenInclude(x => x.ProjectOperation)
                        .ThenInclude(x => x.Project)
                            .ThenInclude(x => x.ProjectCostCenters)
                                .ThenInclude(x => x.CostCenter)

            .Include(x => x.Details)
                    .ThenInclude(x => x.ProjectOperation)
                        .ThenInclude(x => x.OperationInfo)
                            .ThenInclude(x => x.OperationInfoServices)
                                .ThenInclude(x => x.ServiceInfo)

            .Include(x => x.Details)
                    .ThenInclude(x => x.ContractorContractDetailServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.ProjectOperationDetail)
                            .ThenInclude(x => x.OperationLocation)

            .Include(x => x.Details)
                    .ThenInclude(x => x.ContractorContractDetailServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.OperationInfoService)
                            .ThenInclude(x => x.ServiceInfo)

            .Include(x => x.Details)
                    .ThenInclude(x => x.ContractorContractDetailServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.ProjectOperationDetail)
                            .ThenInclude(x => x.ProjectOperation)
                            .ThenInclude(x => x.OperationInfo)

           .Include(x => x.Details)
                    .ThenInclude(x => x.ContractorContractDetailServices)
                    .ThenInclude(x => x.ProjectOperationDetailContractorService)
                        .ThenInclude(x => x.ProjectOperationDetail)
                            .ThenInclude(x => x.ProjectOperation)
                                .ThenInclude(x => x.Project)
                                    .ThenInclude(x => x.ProjectCostCenters)
                                        .ThenInclude(x => x.CostCenter)

            .Where(c =>
                c.CompanyId == companyId &&
                      (contractorId == null || c.ContractorContractHeader.ContractorId == contractorId) &&
                      (contractorContractIds == null || contractorContractIds.Count == 0 || contractorContractIds.Contains(c.Id)) &&

                      (costCenterId == null || c.Details.Any(oo => oo.ProjectOperation!.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)) ||
                        c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.ProjectCostCenters.Any(x => x.CostCenterId == costCenterId)))) &&

                      (projectIds == null || projectIds.Count == 0 || c.Details.Any(oo => projectIds.Contains(oo.ProjectOperation!.Project.Id)) ||
                        c.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => projectIds.Contains(s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Project.Id)))) &&

                      (fromDate == null || c.Created > fromDate) &&
                      (toDate == null || c.Created < toDate) &&
                      (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&
                      (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.Id.ToString(), filterData.MakeLikePattern())));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<GetContractsByProjectIdModel> Data, int RowCount)> GetContractsByProjectId(
        long projectId,
        int pageIndex,
        int pageSize,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Where(c => c.ProjectId == projectId && c.CompanyId == companyId)
            .Select(x => new GetContractsByProjectIdModel()
            {
                Id = x.Id,
                ContractorId = x.ContractorContractHeader.ContractorId,
                ContractNumber = x.ContractorContractHeader.Id,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                ContractPrice = x.TotalAmount,
                ContractType = x.ContractorContractType,
            });

        var rowCount = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);

        return (newQuery, rowCount);
    }

    public async Task<List<GetFltrProjectContractorsModel>> GetFltrProjectContractors(
        List<long>? costCenterIds,
        List<long>? projectIds,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Where(c =>
            c.CompanyId == companyId &&
            (projectIds == null || projectIds.Contains(c.ProjectId.Value)) &&
            (costCenterIds == null || costCenterIds.Contains(c.ContractorContractHeader.CostCenterId.Value)))
            .Select(x => new GetFltrProjectContractorsModel()
            {
                CostCenterId = x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenterId,
                CostCenterName = x.Project.ProjectCostCenters.FirstOrDefault()!.CostCenter.CostCenterName,
                ProjectId = x.ProjectId,
                ProjectName = x.Project.ProjectName,
                ContractorId = x.ContractorContractHeader.ContractorId,
            });

        var newQuery = await query.ToListAsync(ct);

        return newQuery;
    }

    public async Task<(List<GetCCThirdPartiesModel> Data, int RowCount)> GetCCThirdParties(
        long projectId,
        List<long>? contractorIds,
        int pageIndex,
        int pageSize,
        long companyId,
        CT ct)
    {
        var query = DbSet
            .Where(c => c.ProjectId == projectId && c.CompanyId == companyId &&
            (contractorIds == null || contractorIds.Contains(c.ContractorContractHeader.ContractorId)))
            .Select(x => new GetCCThirdPartiesModel()
            {
                Id = x.Id,
                ContractorId = x.ContractorContractHeader.ContractorId,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                ContractRequestNumber = x.ContractorContractHeader.Id
            });

        var rowCount = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);

        return (newQuery, rowCount);
    }

    public async Task<List<GetCostCentersMostPaidCCModel>> GetCostCentersMostPaidCC(
    long companyId,
    CT ct)
    {
        var data = await DbSet
            .Where(x => x.CompanyId == companyId && x.TotalAmount.HasValue)
            .Select(x => new
            {
                x.ContractorContractHeader.CostCenterId,
                x.ContractorContractHeader.CostCenter!.CostCenterName,
                x.ContractorContractHeader.Id,
                Amount = x.TotalAmount!.Value,
                x.Created
            })
            .ToListAsync(ct);

        return data
            .GroupBy(x => new { x.CostCenterId, x.CostCenterName })
            .Select(g => g.OrderByDescending(x => x.Created).First())
            .OrderByDescending(x => x.Amount)
            .Take(10)
            .Select(x => new GetCostCentersMostPaidCCModel
            {
                CostCenterId = x.CostCenterId.Value,
                CostCenterName = x.CostCenterName,
                ContractorContractHeaderId = x.Id,
                ContractorContractAmount = x.Amount
            })
            .ToList();
    }

    public async Task<List<GetCostCentersMostRecentCCModel>> GetCostCentersMostRecentCC(
    long companyId,
    CT ct)
    {
        var data = await DbSet
            .Where(x => x.CompanyId == companyId && x.TotalAmount.HasValue)
            .Select(x => new
            {
                x.ContractorContractHeader.CostCenterId,
                x.ContractorContractHeader.CostCenter!.CostCenterName,
                HeaderId = x.ContractorContractHeader.Id,
                Amount = x.TotalAmount!.Value,
                x.Created
            })
            .ToListAsync(ct);

        return data
            .GroupBy(x => new { x.CostCenterId, x.CostCenterName })
            .Select(g => g.OrderByDescending(x => x.Created).First())
            .OrderByDescending(x => x.Created)
            .Take(10)
            .Select(x => new GetCostCentersMostRecentCCModel
            {
                CostCenterId = x.CostCenterId.Value,
                CostCenterName = x.CostCenterName,
                ContractorContractHeaderId = x.HeaderId,
                ContractorContractAmount = x.Amount
            })
            .ToList();
    }
}
