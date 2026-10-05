using Engineering.Application.Abstractions.Data.EmployerContracts;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetEContractHeaderById;
using Engineering.Application.Services.EmployerContracts.Contracts.GetFltrEContractHeads;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Persistence.Repositories.EmployerContracts;

public class EmployerContractHeadRepository : BaseRepository<EngineeringDBContext, EmployerContractHead>, IEmployerContractHeadRepository
{
    public EmployerContractHeadRepository(EngineeringDBContext context) : base(context)
    {
    }


    public async Task<bool> VerifyCode(
        long? id, string code, long companyId, CT ct)
    {
        if (id == null)
        {
            return await DbSet
                .Where(e => e.CompanyId == companyId && e.Code == code)
                .AnyAsync(ct);
        }
        else
        {
            return await DbSet
                .Where(e => e.Id != id.Value && e.CompanyId == companyId && e.Code == code)
                .AnyAsync(ct);
        }
    }

    public async Task<EmployerContractHead?> DeleteEContractHeader(
        long id, CT ct)
    {
        return await DbSet
            .Include(i => i.EmployerContracts)
                .ThenInclude(o => o.EmployerOperations)
                    .ThenInclude(o => o.ProjectOperation.ProjectOperationDetails)

            .Where(e => e.Id == id).FirstOrDefaultAsync(ct);
    }

    public async Task<EmployerContractHead?> GetEContractHeader(
        long id, CT ct)
    {
        return await DbSet
            .Include(x => x.EmployerContracts)
            .Where(e => e.Id == id).FirstOrDefaultAsync(ct);
    }

    public async Task<GetEContractHeaderByIdResponse?> GetEContractHeaderById(
        long id, CT ct)
    {
        return await DbSet
            .Where(e => e.Id == id)
            .Select(head => new GetEContractHeaderByIdResponse()
            {
                Id = head.Id,
                EmployerId = head.EmployerId,
                CostCenterId = head.CostCenter.Id,
                CostCenterName = head.CostCenter.CostCenterName,
                CostCenterCode = head.CostCenter.CostCenterCode,
                Code = head.Code,
                VolumeTolerance = head.VolumeTolerance,
                PriceTolerance = head.PriceTolerance,
                CurrencyId = head.CurrencyId,
                Type = head.Type,
                StartDate = head.StartDate,
                EndDate = head.EndDate,

                EContracts = head.EmployerContracts.Select(contract => new GetEContractByIdResponse()
                {
                    Id = contract.Id,
                    HeadId = contract.EmployerContractHeadId,
                    ProjectId = contract.Project.Id,
                    ProjectName = contract.Project.ProjectName,
                    ProjectCode = contract.Project.ProjectCode,
                    IsFirst = contract.IsFirst,
                    Status = contract.Status,
                    Code = contract.Code,
                    HeadCode = head.Code,
                    CurrencyRate = contract.CurrencyRate,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    TotalAmount = contract.TotalAmount,
                    AdvancePayment = contract.AdvancePayment,
                    VolumeTolerance = contract.EmployerContractHead.VolumeTolerance,
                    CurrencyId = contract.EmployerContractHead.CurrencyId,
                    PriceTolerance = contract.EmployerContractHead.PriceTolerance,
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
                        Priority = operation.ProjectOperation.Priority,
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
                            Description = history.Description
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
                            ServiceId = service.IsStandard ? service.OperationInfoService!.ServiceInfo.Id :
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

                }).ToList(),
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<EmployerContractHead?> GetEContractHeaderFull(
        long id, CT ct)
    {
        return await DbSet
            .Include(i => i.CostCenter)

            .Include(i => i.EmployerContracts)
                .ThenInclude(p => p.Project)

            .Include(i => i.EmployerContracts)
                .ThenInclude(p => p.EmployerDocs)
                    .ThenInclude(c => c.EmployerDocUrls)

            .Include(i => i.EmployerContracts)
                .ThenInclude(p => p.EmployerCostOvers)
                    .ThenInclude(c => c.CostOver)

            .Include(i => i.EmployerContracts)
                .ThenInclude(p => p.EmployerCostOvers)
                    .ThenInclude(c => c.ChildCostOverImpacts)

            .Include(i => i.EmployerContracts)
                .ThenInclude(i => i.EmployerConsiderations)
                    .ThenInclude(o => o.EmployerConsiderationDeps)
                        .ThenInclude(o => o.EmployerOperation)

            .Include(i => i.EmployerContracts)
                .ThenInclude(i => i.EmployerOperations)
                    .ThenInclude(o => o.ProjectOperation.OperationInfo)

            .Include(i => i.EmployerContracts)
                .ThenInclude(i => i.EmployerOperations)
                    .ThenInclude(o => o.ProjectOperation.ProjectOperationDetails)

            .Where(e => e.Id == id).FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetFltrEContractHeadsModel> Data, int RowCount)> GetFltrEContractHeads(
        List<long>? costCenterIds,
        List<long>? projectIds,
        List<long>? employerIds,
        List<EContractType>? types,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet
            .Where(e =>
                (costCenterIds == null || costCenterIds.Contains(e.CostCenter.Id)) &&
                (projectIds == null || e.EmployerContracts.Any(x => projectIds.Contains(x.Project.Id))) &&
                (employerIds == null || employerIds.Contains(e.EmployerId)) &&
                (types == null || types.Contains(e.Type)) &&
                (startDate == null || e.StartDate.Value.Date >= startDate.Value.Date) &&
                (endDate == null || e.EndDate.Value.Date <= endDate.Value.Date) &&
                (string.IsNullOrWhiteSpace(filterData) || EF.Functions.Like(e.Code, filterData.MakeLikePattern()))
            ).Select(item => new GetFltrEContractHeadsModel()
            {
                Id = item.Id,
                Code = item.Code,
                Type = item.Type,
                EmployerId = item.EmployerId,
                CostCenterId = item.CostCenter.Id,
                CostCenterName = item.CostCenter.CostCenterName,
                CostCenterCode = item.CostCenter.CostCenterCode,
                CurrencyId = item.CurrencyId,
                StartDate = item.StartDate,
                EndDate = item.EndDate,
                VolumeTolerance = item.VolumeTolerance,
                PriceTolerance = item.PriceTolerance,
                Created = item.Created,
                CreatorId = item.CreatorId,
            });

        query = query.OrderByDescending(e => e.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var contracts = await query.ToListAsync(ct);
        return (contracts, count);
    }

}