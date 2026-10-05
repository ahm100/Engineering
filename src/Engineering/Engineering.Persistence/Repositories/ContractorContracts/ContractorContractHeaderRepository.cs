using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetCContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetContractorContractHeaderById;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsDraftableContractorContractHeader;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsFilteredContractorContractHeader;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.ContractorStatusStatements.Enums;

namespace Engineering.Persistence.Repositories.ContractorContractHeaders;

public class ContractorContractHeaderRepository : BaseRepository<EngineeringDBContext, ContractorContractHeader>, IContractorContractHeaderRepository
{
    public ContractorContractHeaderRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ContractorContractHeader?> GetContractorContractHeaderByIdIncludeless(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ContractorContractHeaderDocuments)
            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.ContractorStatusStatementDetails)

            .Where(c => c.Id == id && !c.IsDeleted);

        return await query.AsSplitQuery().FirstOrDefaultAsync(ct);
    }

    public async Task<ContractorContractHeader?> GetContractorContractHeaderForDelete(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.Details)
                    .ThenInclude(x => x.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectServiceDetail.ProjectService.ServiceInfo)


            .Where(c => c.Id == id && !c.IsDeleted);

        return await query.AsSplitQuery().FirstOrDefaultAsync(ct);
    }

    public async Task<GetContractorContractHeaderByIdResponse?> GetContractorContractHeaderByIdNew(
        long id, long companyId, CT ct)
    {
        var query = DbSet
            .Where(c => c.Id == id && c.CompanyId == companyId && !c.IsDeleted)

            .Select(item => new GetContractorContractHeaderByIdResponse()
            {
                Id = item.Id,
                HaveAVersion = item.ContractorContractHeaderVersions.Any(),
                Version = item.ContractorContractHeaderVersions.Any() ? item.ContractorContractHeaderVersions.Max(x => x.Version) : 0,
                CostCenterId = item.CostCenter!.Id,
                CostCenterName = item.CostCenter.CostCenterName,
                ContractorId = item.ContractorId,
                Status = item.Status,
                CurrencyId = item.CurrencyId,
                Description = item.Description,
                Urls = item.ContractorContractHeaderDocuments.Select(x => x.Url).ToList(),
                Contracts = item.ContractorContracts.Select(contract => new GetHeaderContractorContractModel()
                {
                    Id = contract.Id,
                    ContractorContractTypeId = contract.ContractorContractType,
                    ProjectId = contract.Project!.Id,
                    ProjectName = contract.Project.ProjectName,
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

                    CostOvers = contract.ContractorContractDetailCostOvers.Select(cost => new GetContractorContractHeaderByIdDetailCostOverModel()
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

                    Details = contract.Details.Select(detail => new GetContractorContractHeaderByIdDetailedModel()
                    {
                        Id = detail.Id,
                        UnitAmount = detail.UnitAmount,
                        TotalAmount = detail.TotalAmount,
                        StartDate = detail.StartDate,
                        EndDate = detail.EndDate,

                        Services = detail.ContractorContractDetailServices.Select(service => new GetContractorContractHeaderByIdDetailServiceModel()
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
                            service.ProjectOperationDetailContractorService
                                .OperationInfoService.ServiceInfo.ServiceInfoName,
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

                        Prices = detail.ContractorContractDetailPrices.Select(price => new GetContractorContractHeaderByIdDetailPriceModel()
                        {
                            Id = price.Id,
                            StartDate = price.StartDate,
                            EndDate = price.EndDate,
                            Price = price.Price,
                            CurrencyId = price.CurrencyId,
                            IsActive = price.IsActive,
                        }).ToList(),

                        CostOvers = detail.ContractorContractDetailCostOvers.Select(cost => new GetContractorContractHeaderByIdDetailCostOverModel()
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

                }).ToList(),
            });

        var item = await query.FirstOrDefaultAsync(ct);
        return item;
    }


    public async Task<ContractorContractHeader?> GetContractorContractHeaderById(long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.CostCenter)
            .Include(x => x.ContractorContractHeaderDocuments)
            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.Project)
            .Include(x => x.ContractorContractHeaderHistories)
            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.ContractorStatusStatementDetails)
                    .ThenInclude(z => z.ContractorStatusStatement)
            .Include(x => x.ContractorContracts)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectServiceDetail.ProjectService.ServiceInfo)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.DailyOperationServices)
                            .ThenInclude(c => c.DailyProjectOperation)
                                .ThenInclude(c => c.DailyProjectOperationExperts)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectServiceDetail.ProjectService)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailPrices)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.ContractorContractDetailCostOvers)
                    .ThenInclude(c => c.CostOver)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailCostOvers)
                        .ThenInclude(c => c.CostOver)

            .Include(x => x.ContractorContracts)

            .Where(c => c.Id == id && !c.IsDeleted);
#pragma warning restore CS8602 // Dereference of a possibly null reference.;

        return await query.AsSplitQuery().FirstOrDefaultAsync(ct);
    }

    public async Task<GetCCHByIdResponse?> GetCCHById(
        long id, long companyId, CancellationToken ct)
    {
        return await DbSet
            .Include(x => x.ContractorContracts)
            .Where(x => x.Id == id && x.CompanyId == companyId)
            .Select(x => new GetCCHByIdResponse
            {
                Id = x.Id,
                CostCenterId = x.CostCenterId,
                CostCenterName = x.CostCenter.CostCenterName,
                ProjectId = x.ContractorContracts.FirstOrDefault().ProjectId,
                ProjectName = x.ContractorContracts.FirstOrDefault().Project.ProjectName,
                ContractorId = x.ContractorId,
                Status = x.Status,
                CurrencyId = x.CurrencyId,
                StartDate = x.StartDate,
                EndDate = x.EndDate,
                FinalTotalAmount = x.ContractorContracts.Sum(x => x.TotalAmount),
                TotalPercentageDoingJobWell = (x.ContractorContracts.Sum(x => x.PercentageDoingJobWell) / x.ContractorContracts.Count()),
                TotalDoingJobWellAmount = (x.ContractorContracts.Sum(x => x.DoingJobWellAmount) / x.ContractorContracts.Count()),
                TotalAdvancePaymentAmount = x.ContractorContracts.Sum(x => x.AdvancePaymentAmount),
                TotalDailyLatenessPenalty = (x.ContractorContracts.Sum(x => x.DailyLatenessPenalty) / x.ContractorContracts.Count()),
                TotalWorkDonePercent = (x.ContractorContracts.Sum(x => x.WorkDonePercent) / x.ContractorContracts.Count()),
                TotalWorkDeliveryPercent = (x.ContractorContracts.Sum(x => x.WorkDeliveryPercent) / x.ContractorContracts.Count()),
                TotalWorkCompletionPercent = (x.ContractorContracts.Sum(x => x.WorkCompletionPercent) / x.ContractorContracts.Count()),
                Description = x.Description,
                HasFixed = x.ContractorContracts.Any(x => x.ContractorContractType == ContractorContractType.Fixed),
                HasService = x.ContractorContracts.Any(x => x.ContractorContractType == ContractorContractType.Service),
                HasOperation = x.ContractorContracts.Any(x => x.ContractorContractType == ContractorContractType.OperationBased),
                HasProfessionalWorkday = x.ContractorContracts.Any(x => x.ContractorContractType == ContractorContractType.ProfessionalWorkday),
                Urls = x.ContractorContractHeaderDocuments.Select(x => x.Url).ToList(),
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<ContractorContractHeader?> GetHeaderByIdForContractorStatusStatement(long id, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.ContractorContractHeaderDocuments)
            .Include(x => x.CostCenter)

            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.Project)

            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.ContractorStatusStatementDetails)
                    .ThenInclude(z => z.ContractorStatusStatement).Include(x => x.ContractorContracts)

            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)

            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.DailyOperationServices)
                    .ThenInclude(c => c.DailyProjectOperation)
                    .ThenInclude(c => c.DailyProjectOperationExperts)
                    .ThenInclude(c => c.ConsumableVolumeExpert)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ProjectOperation)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailPrices)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation)

            .Include(x => x.ContractorContracts)

            .Where(c => c.Id == id && !c.IsDeleted);
#pragma warning restore CS8602 // Dereference of a possibly null reference.;

        return await query.AsSplitQuery().FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetsDraftableContractorContractHeaderModel> Data, int RowCount)> GetsDraftableContractorContractHeader(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(c =>
                c.Status == ContractorContractStatus.ManagementConfirmed &&
                c.ContractorId == contractorId &&
                c.ContractorContracts.Any(x => x.Project!.Id == projectId))
            .Select(item => new GetsDraftableContractorContractHeaderModel()
            {
                Id = item.Id,
                ContractorId = item.ContractorId,
                CurrencyId = item.CurrencyId,
                StartDate = item.ContractorContracts.Min(x => x.StartDate),
                EndDate = item.ContractorContracts.Max(x => x.EndDate),
                CreatorId = item.CreatorId,
                Created = item.Created,
                Description = item.Description,
                Urls = item.ContractorContractHeaderDocuments.Select(x => x.Url).ToList(),
                ContractorContracts = item.ContractorContracts
                    .Select(contract => new GetsDraftableContractorContractTypeModel()
                    {
                        Id = contract.Id,
                        ContractorContractTypeId = contract.ContractorContractType,
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
                    }).ToList(),
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<GetsFilteredContractorContractHeaderModel> Data, int RowCount)> GetContractorContractHeaderByFilter(
        List<long>? ids,
        long? contractorId,
        long? costCenterId,
        List<long>? projectIds,
        long? projectManagerId,
        List<long>? projectOperationIds,
        DateTime? fromDate,
        DateTime? toDate,
        List<ContractorContractStatus>? statuses,
        List<ContractorContractStatus>? removeStatuses,
        ContractorContractType? contractorContractTypeId,
        string? filterData,
        long? companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize, CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Where(c =>
                (companyId == null || c.CompanyId == companyId) &&
                (contractorId == null || c.ContractorId == contractorId) &&
                (costCenterId == null || c.CostCenter.Id == costCenterId) &&
                (projectIds == null || c.ContractorContracts.Any(x => projectIds.Contains(x.Project.Id))) &&
                (projectManagerId == null || c.ContractorContracts.Any(x => x.Project.ProjectManager == projectManagerId)) &&

                (projectOperationIds == null || c.ContractorContracts.Any(x => x.Details.Any(oo => projectOperationIds.Contains(oo.ProjectOperation!.Id)) ||
                   x.Details.Any(oo => oo.ContractorContractDetailServices.Any(s => projectOperationIds.Contains(s.ProjectOperationDetailContractorService!.ProjectOperationDetail.ProjectOperation.Id))))) &&

                (contractorContractTypeId == null || c.ContractorContracts.Any(x => x.ContractorContractType == contractorContractTypeId)) &&

                (fromDate == null || c.Created > fromDate) &&
                (toDate == null || c.Created < toDate) &&
                (statuses == null || statuses.Contains(c.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(c.Status)) &&
                (ids == null || ids.Count == 0 || ids.Contains(c.Id)) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(c.Id.ToString(), filterData.MakeLikePattern())))
            .Select(item => new GetsFilteredContractorContractHeaderModel()
            {
                Id = item.Id,
                HaveAVersion = item.ContractorContractHeaderVersions.Any(),
                Version = item.ContractorContractHeaderVersions.Any() ? item.ContractorContractHeaderVersions.Max(x => x.Version) : 0,
                ContractorId = item.ContractorId,
                CostCenterId = item.CostCenter.Id,
                CostCenterName = item.CostCenter.CostCenterName,
                CompanyId = item.CompanyId,
                CurrencyId = item.CurrencyId,
                Status = item.Status,
                CreatorId = item.CreatorId,
                Created = item.Created,
                Urls = item.ContractorContractHeaderDocuments.Select(x => x.Url).ToList(),
                Description = item.Description,
                ProjectManagerDescription = item.ContractorContractHeaderHistories.OrderBy(x => x.Created).LastOrDefault(x => x.Status == ContractorContractStatus.ProjectManagerConfirmed).Description,
                HavePrices = item.ContractorContracts.Any(x => x.Details.Any(z => z.ContractorContractDetailPrices.Any())) ? true : false,
                HaveStatusStatement = item.ContractorContracts.Any(x => x.ContractorStatusStatementDetails.Any()),
                ReviewAble = item.ContractorContracts.All(x => x.ContractorStatusStatementDetails
                    .All(c => CSSStatusRules.AllowForUpdate.Contains(c.ContractorStatusStatement.Status))),

                Contracts = item.ContractorContracts.Select(contract => new GetsFilteredContractorContractModel()
                {
                    Id = contract.Id,
                    ContractorContractTypeId = contract.ContractorContractType,
                    ProjectId = contract.Project!.Id,
                    ProjectName = contract.Project.ProjectName,
                    ProjectCode = contract.Project.ProjectCode,
                    ProjectManagerId = contract.Project.ProjectManager,
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
                }).ToList(),
            });
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

    public async Task<(List<ContractorContractHeader> Data, int RowCount)> GetsContractorContractHeaderByIds(
        List<long> ids,
        int pageIndex,
        int pageSize, CT ct)
    {
        var query = DbSet
            .Include(x => x.ContractorContracts)

            .Where(c => ids.Contains(c.Id));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<ContractorContractHeader> Data, int RowCount)> GetsContractorContractHeader(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.ContractorStatusStatementDetails)
                    .ThenInclude(z => z.ContractorStatusStatement)
            .Include(x => x.ContractorContracts)
                .ThenInclude(z => z.ContractorContractType)
            .Include(x => x.ContractorContracts)
                .ThenInclude(z => z.Details)
                    .ThenInclude(y => y.ProjectOperation)
            .Include(x => x.ContractorContracts)
                .ThenInclude(z => z.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(y => y.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo)
            .Include(x => x.ContractorContracts)
                .ThenInclude(z => z.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(y => y.ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation)
            .Include(x => x.ContractorContracts)
                .ThenInclude(z => z.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(y => y.ProjectOperationDetailContractorService)
                            .ThenInclude(y => y.DailyOperationServices)
                                .ThenInclude(y => y.DailyProjectOperation)
                                    .ThenInclude(y => y.ProjectOperationDetail)

            .Include(x => x.ContractorContracts)
                .ThenInclude(z => z.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(y => y.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)
            .Include(x => x.ContractorContracts)
                .ThenInclude(z => z.Details)
                    .ThenInclude(y => y.ContractorContractDetailPrices)
            .Include(x => x.ContractorContracts)
                .ThenInclude(z => z.Details)
                    .ThenInclude(y => y.ContractorStatusStatementServices)

            .Where(c => c.Status == ContractorContractStatus.ManagementConfirmed &&
                       (c.ContractorId == contractorId) &&
                       (c.ContractorContracts.Any(x => x.Project.Id == projectId)));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }

    public async Task<(List<ContractorContractHeader> Data, int RowCount)> GetsHeaderForContractorStatusStatement(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        var query = DbSet
            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.ContractorStatusStatementDetails)
                    .ThenInclude(z => z.ContractorStatusStatement)
            .Include(x => x.ContractorContracts)

            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo)

            .Include(x => x.ContractorContracts)
                .ThenInclude(x => x.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                    .ThenInclude(c => c.ProjectOperationDetailContractorService.DailyOperationServices)
                    .ThenInclude(c => c.DailyProjectOperation.ProjectOperationDetail)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailServices)
                        .ThenInclude(c => c.ProjectOperationDetailContractorService.ProjectServiceDetail.ProjectService)

            .Include(x => x.ContractorContracts)
                .ThenInclude(c => c.Details)
                    .ThenInclude(c => c.ContractorContractDetailPrices)

            .Include(x => x.ContractorContracts)

            .Where(c =>
                (c.ContractorId == contractorId) &&
                (c.ContractorContracts.Any(x => x.Project.Id == projectId)));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        query = query.OrderByDescending(oo => oo.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.ToListAsync(ct);

        return (entites, count);
    }
}
