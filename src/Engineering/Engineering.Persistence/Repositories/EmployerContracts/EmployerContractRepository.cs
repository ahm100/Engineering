using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContracts;
using Engineering.Application.Services.EmployerEmployees.Contracts.GetECThirdParties;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public partial class EmployerContractRepository : BaseRepository<EngineeringDBContext, EmployerContract>, IEmployerContractRepository
{
    public EmployerContractRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<EmployerContract?> GetEConteract(
        long id, CT ct)
    {
        var query = DbSet
            .Include(p => p.EmployerDocs)
                .ThenInclude(c => c.EmployerDocUrls)

            .Include(p => p.EmployerCostOvers)
                .ThenInclude(c => c.CostOver)

            .Include(p => p.EmployerCostOvers)
                .ThenInclude(c => c.ChildCostOverImpacts)
            //.ThenInclude(c => c.ChildCostOver.CostOver)

            .Include(i => i.EmployerConsiderations)
                .ThenInclude(o => o.EmployerConsiderationDeps)
                    .ThenInclude(o => o.EmployerOperation)

            .Include(i => i.EmployerOperations)
                .ThenInclude(o => o.EmployerOperationProducts)

            .Include(i => i.EmployerOperations)
                .ThenInclude(o => o.EmployerOperationServices)

            .Include(i => i.EmployerOperations)
                .ThenInclude(o => o.ProjectOperation.OperationInfo)

            .Include(i => i.EmployerOperations)
                .ThenInclude(o => o.ProjectOperation.ProjectOperationDetails)
            .Include(p => p.Project)
                .ThenInclude(c => c.ProjectCostCenters)
                    .ThenInclude(c => c.CostCenter)

            .Where(e => e.Id == id);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<List<EmployerContract>?> GetEConteracts(
        List<long> ids, CT ct)
    {
        return await DbSet
            .Where(e => ids.Contains(e.Id)).ToListAsync();
    }

    public async Task<List<long>> GetFltrEmployers(
        List<long>? costCenterIds,
        List<long>? projectIds,
        CT ct)
    {
        return await DbSet
            .Where(e =>
            (costCenterIds == null || costCenterIds.Contains(e.EmployerContractHead.CostCenter.Id)) &&
            (projectIds == null || projectIds.Contains(e.Project.Id)))
            .Select(x => x.EmployerContractHead.EmployerId).Distinct()
            .ToListAsync();
    }

    public async Task<EmployerContract?> DeleteEContract(
        long id,
        CT ct)
    {
        var query = DbSet
            .Where(e => e.Id == id)
            .Include(i => i.Project)
            .Include(i => i.EmployerConsiderations)
                .ThenInclude(o => o.EmployerConsiderationDeps)
                    .ThenInclude(o => o.EmployerOperation.ProjectOperation.OperationInfo)
            .Include(i => i.EmployerOperations)
                .ThenInclude(o => o.ProjectOperation.OperationInfo)
            .Include(i => i.EmployerOperations)
                .ThenInclude(o => o.ProjectOperation.ProjectOperationDetails)
            .Include(p => p.EmployerContractHead.CostCenter);

        var result = await query.FirstOrDefaultAsync(ct);
        return result;
    }

    public async Task<GetEContractByIdResponse?> GetEContractById(
        long id,
        CT ct)
    {
        return await DbSet
            .Where(e => e.Id == id)
            .Select(contract => new GetEContractByIdResponse()
            {
                Id = contract.Id,
                HeadId = contract.EmployerContractHeadId,
                IsFirst = contract.IsFirst,
                Status = contract.Status,
                Code = contract.Code,
                HeadCode = contract.EmployerContractHead.Code,
                ProjectId = contract.Project.Id,
                ProjectName = contract.Project.ProjectName,
                CurrencyRate = contract.CurrencyRate,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                TotalAmount = contract.TotalAmount,
                CurrencyId = contract.EmployerContractHead.CurrencyId,
                VolumeTolerance = contract.EmployerContractHead.VolumeTolerance,
                PriceTolerance = contract.EmployerContractHead.PriceTolerance,
                AdvancePayment = contract.AdvancePayment,
                Description = contract.Description,
                EmployerDocs = contract.EmployerDocs.Select(doc => new GetEDocModel()
                {
                    Id = doc.Id,
                    Type = doc.Type,
                    RegistrationDate = doc.RegistrationDate,
                    Version = doc.Version,
                    Description = doc.Description,
                    urls = doc.EmployerDocUrls.Select(x => x.URL).ToList(),
                }).ToList(),

                EmployerConsiderations = contract.EmployerConsiderations.Select(cons => new GetEConsiderationModel()
                {
                    Id = cons.Id,
                    Type = cons.Type,
                    Description = cons.Description,
                }).ToList(),

                EmployerCostOvers = contract.EmployerCostOvers.Select(cost => new GetECostOverModel()
                {
                    Id = cost.Id,
                    CostOverId = cost.CostOverId,
                    CostOverName = cost.CostOver.CostOverName,
                    CostOverCode = cost.CostOver.CostOverCode,
                    Percent = cost.Percent,
                    Impacts = cost.ParentCostOverImpacts.Select(impact => new GetECostOverImpactsModel()
                    {
                        Id = impact.Id,
                        ChildCostOverId = impact.ChildCostOverId,
                        CostOverId = impact.ChildCostOver.CostOver.Id,
                        CostOverName = impact.ChildCostOver.CostOver.CostOverName,
                        CostOverCode = impact.ChildCostOver.CostOver.CostOverCode,
                        Percent = impact.Percent,
                    }).ToList(),
                }).ToList(),

                EmployerOperations = contract.EmployerOperations.Select(operation => new GetEOperationModel()
                {
                    Id = operation.Id,
                    ProjectOperationId = operation.ProjectOperation.Id,
                    OperationInfoId = operation.ProjectOperation.OperationInfo.Id,
                    OperationInfoName = operation.ProjectOperation.OperationInfo.OperationInfoName,
                    OperationInfoCode = operation.ProjectOperation.OperationInfo.OperationInfoCode,
                    Workload = operation.ProjectOperation.Workload,
                    TolerancePercentage = operation.ProjectOperation.TolerancePercentage,
                    UnitOfMeasurementId = operation.ProjectOperation.UnitOfMeasurementId,
                    Status = operation.ProjectOperation.ProjectOperationStatus,
                    GoodsInProgress = operation.ProjectOperation.GoodsInProgress,
                    ProjectOperationDescription = operation.ProjectOperation.Description,
                    UnitPrice = operation.ProjectOperation.Price,
                    TotalPrice = operation.TotalPrice,
                    IncreaseRate = operation.IncreaseRate,
                    Description = operation.Description,
                    FlagIds = operation.EmployerConsiderationDeps.Select(x => x.EmployerConsideration.Id.ToString()).ToList(),

                    Details = operation.EmployerOperationDetails.Select(detail => new GetEOperationDetailModel()
                    {
                        Id = detail.Id,
                        ProjectOperationDetailId = detail.ProjectOperationDetail.Id,
                        PrivateName = detail.ProjectOperationDetail.OperationLocation.PrivateName,
                        PrivateCode = detail.ProjectOperationDetail.OperationLocation.PrivateCode,
                        PublicName = detail.ProjectOperationDetail.OperationLocation.PublicName,
                        PublicCode = detail.ProjectOperationDetail.OperationLocation.PublicCode,
                        StartDate = detail.ProjectOperationDetail.StartDate,
                        EndDate = detail.ProjectOperationDetail.EndDate,
                        Length = detail.ProjectOperationDetail.Length,
                        Width = detail.ProjectOperationDetail.Width,
                        Height = detail.ProjectOperationDetail.Height,
                        Weight = detail.ProjectOperationDetail.Weight,
                        Number = detail.ProjectOperationDetail.Number,
                        Status = detail.ProjectOperationDetail.Status,
                        Description = detail.ProjectOperationDetail.Description,
                    }).ToList(),
                    Histories = operation.EmployerOperationHistories.Select(history => new GetEOperationHistoryModel()
                    {
                        Id = history.Id,
                        Price = history.UnitPrice,
                        Workload = history.Workload,
                        Description = history.Description,
                    }).ToList(),
                    Products = operation.EmployerOperationProducts.Select(product => new GetEOperationProductModel()
                    {
                        Id = product.Id,
                        ProductGroupId = product.ProductGroupId,
                        ProductId = product.ProductId,
                        MinPrice = product.MinPrice,
                        MaxPrice = product.MaxPrice,
                        Count = product.Count,
                        Tax = product.Tax,
                        TaxPercent = product.TaxPercent,
                        TransportationCost = product.TransportationCost,
                        TransportationCostPercent = product.TransportationCostPercent,
                        ProfitCost = product.ProfitCost,
                        ProfitCostPercent = product.ProfitCostPercent,
                        OtherCost = product.OtherCost,
                        OtherCostPercent = product.OtherCostPercent,
                        IsStandard = product.IsStandard,
                        Description = product.Description,
                    }).ToList(),
                    Services = operation.EmployerOperationServices.Select(service => new GetEOperationServiceModel()
                    {
                        Id = service.Id,
                        ServiceId = service.IsStandard == true ? service.OperationInfoService!.ServiceInfo.Id :
                            service.ServiceInfoId!.Value!,
                        ServiceName = service.IsStandard ? service.OperationInfoService!.ServiceInfo.ServiceInfoName :
                            service.ServiceInfo!.ServiceInfoName,
                        MinPrice = service.MinPrice,
                        MaxPrice = service.MaxPrice,
                        Tax = service.Tax,
                        TaxPercent = service.TaxPercent,
                        TransportationCost = service.TransportationCost,
                        TransportationCostPercent = service.TransportationCostPercent,
                        ProfitCost = service.ProfitCost,
                        ProfitCostPercent = service.ProfitCostPercent,
                        OtherCost = service.OtherCost,
                        OtherCostPercent = service.OtherCostPercent,
                        Description = service.Description,
                    }).ToList()
                }).ToList(),

            }).FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetFltrEContractsModel> Data, int RowCount)> GetFltrEContracts(
        long? headId,
        List<long>? Ids,
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? employerIds,
        List<EContractType>? types,
        List<EContractStatus>? statuses,
        List<EContractStatus>? removeStatuses,
        bool? isPrimaryManager,
        bool? isFinalManager,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8629 // Nullable value type may be null.
        var query = DbSet
            .Where(e =>
                (headId == null || e.EmployerContractHead.Id == headId) &&
                (Ids == null || Ids.Contains(e.Id)) &&
                (costCenterIds == null || costCenterIds.Contains(e.EmployerContractHead.CostCenter.Id)) &&
                (projectIds == null || projectIds.Contains(e.Project.Id)) &&
                (employerIds == null || employerIds.Contains(e.EmployerContractHead.EmployerId)) &&
                (types == null || types.Contains(e.EmployerContractHead.Type)) &&
                (statuses == null || statuses.Contains(e.Status)) &&
                (removeStatuses == null || !removeStatuses.Contains(e.Status)) &&
                (isPrimaryManager == null || !e.EmployerContractHistory.Any(x => x.Status == EContractStatus.PrimaryManagerConfirmed)) &&
                (isFinalManager == null || !e.EmployerContractHistory.Any(x => x.Status == EContractStatus.FinalManagerConfirmed)) &&
                (startDate == null || e.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || e.EndDate.Value.Date <= endDate.Value.Date) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(e.Code, filterData.MakeLikePattern()))
            ).Select(item => new GetFltrEContractsModel()
            {
                Id = item.Id,
                HeadId = item.EmployerContractHead.Id,
                Type = item.EmployerContractHead.Type,
                HeadCode = item.EmployerContractHead.Code,
                EmployerId = item.EmployerContractHead.EmployerId,
                CurrencyId = item.EmployerContractHead.CurrencyId,
                CostCenterId = item.EmployerContractHead.CostCenter.Id,
                CostCenterName = item.EmployerContractHead.CostCenter.CostCenterName,
                CostCenterCode = item.EmployerContractHead.CostCenter.CostCenterCode,
                ProjectId = item.Project.Id,
                ProjectName = item.Project.ProjectName,
                ProjectCode = item.Project.ProjectCode,
                IsFirst = item.IsFirst,
                Status = item.Status,
                Code = item.Code,
                CurrencyRate = item.CurrencyRate,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                TotalAmount = item.TotalAmount,
                AdvancePayment = item.AdvancePayment,
                Description = item.Description,
                Created = item.Created,
                CreatorId = item.CreatorId,
                IsFinalManagerConfirmed = item.EmployerContractHistory.Any(s => s.Status == EContractStatus.FinalManagerConfirmed),
                IsPrimaryManagerConfirmed = item.EmployerContractHistory.Any(s => s.Status == EContractStatus.PrimaryManagerConfirmed),
            });
#pragma warning restore CS8629 // Nullable value type may be null.

        query = query.OrderByDescending(e => e.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var contracts = await query.ToListAsync(ct);
        return (contracts, count);
    }

    public async Task<bool> VerifyCode(
        long? id, string code, long contractHeadId, CT ct)
    {
        if (id == null)
        {
            return await DbSet
                .Where(e => e.EmployerContractHeadId == contractHeadId && e.Code == code)
                .AnyAsync(ct);
        }
        else
        {
            return await DbSet
                .Where(e => e.Id != id.Value && e.EmployerContractHeadId == contractHeadId && e.Code == code)
                .AnyAsync(ct);
        }
    }

    public async Task<string> CodeCreator(
        long contractHeadId, CT ct)
    {
        var query = await DbSet
            .Where(w =>
                (w.EmployerContractHeadId == contractHeadId) &&
                EF.Functions.IsNumeric(w.Code))
            .Select(s =>
                Convert.ToInt64(s.Code))
            .ToListAsync(ct);

        long suggestedCode = 1;
        if (query is not null && query.Any())
            suggestedCode = query.Max() + 1;

        return suggestedCode.ToString();
    }

    public async Task<(List<GetECThirdPartiesModel> Data, int RowCount)> GetECThirdParties(
            long projectId,
            List<long>? employerIds,
            int pageIndex,
            int pageSize,
            CT ct)
    {
        var query = DbSet
            .Where(x => x.ProjectId == projectId && !x.IsDeleted &&
            (employerIds == null || employerIds.Contains(x.EmployerContractHead.EmployerId)))
            .Select(x => new GetECThirdPartiesModel
            {
                Id = x.Id,
                EmployerId = x.EmployerContractHead.EmployerId,
                EContractHeadCode = x.EmployerContractHead.Code,
                StartDate = x.StartDate.Value,
                EndDate = x.EndDate.Value
            });

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var newQuery = await query.ToListAsync(ct);
        return (newQuery, count);
    }
}