using Engineering.Application.Abstractions.Data.ContractorContracts;
using Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailCostOver;
using Engineering.Application.Services.ContractorContracts.Queries.GetsDraftableContractorCostOver;
using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Persistence.Repositories.ContractorContracts;

public class ContractorContractDetailCostOverRepository : BaseRepository<EngineeringDBContext, ContractorContractDetailCostOver>, IContractorContractDetailCostOverRepository
{
    public ContractorContractDetailCostOverRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ContractorContractDetailCostOver?> GetContractorContractDetailCostOverById(long id, CT ct)
    {
        var query = DbSet
            .Include(x => x.ContractorContractDetail)
            .Include(c => c.CostOver)

            .Include(x => x.ContractorContract)
                .ThenInclude(x => x.Details)


            .Where(c => c.Id.Equals(id));

        return await query.SingleOrDefaultAsync(ct);
    }

    public async Task<(List<GetsContractorContractDetailCostOverModel> Data, int RowCount)> GetsContractorContractDetailCostOver(
        long contractorContractHedearId,
        DateTime? startDate,
        DateTime? endDate,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet

            .Where(c =>
                !c.IsDeleted &&
                c.ContractorContractDetail.ContractorContract!.ContractorContractHeader.Id == contractorContractHedearId &&
                (startDate == null || c.ContractorContractDetail.ContractorContract.StartDate >= startDate) &&
                (endDate == null || c.ContractorContractDetail.ContractorContract.EndDate <= endDate) &&
                (string.IsNullOrWhiteSpace(filterData) || c.ContractorContractDetail.ContractorContractDetailServices
                    .Any(x => EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName, filterData.MakeLikePattern())) ||
                string.IsNullOrWhiteSpace(filterData) || c.ContractorContractDetail.ContractorContractDetailServices
                    .Any(x => EF.Functions.Like(x.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode, filterData.MakeLikePattern()))))

            .Select(price => new GetsContractorContractDetailCostOverModel()
            {
                Id = price.Id,
                //ContractorContractDetailId = price.ContractorContractDetailId,
                //StartDate = price.StartDate,
                //EndDate = price.EndDate,
                //Created = price.Created,
                //Price = price.Price,
                //IsActive = price.IsActive,
                ServiceInfoId = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id,
                ServiceInfoName = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName,
                ServiceInfoCode = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode,
                ServiceInfoUnitOfMeasurementId = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId,
                OperationInfoName = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.OperationInfoName,
                OperationInfoCode = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.OperationInfoCode,
                OperationInfoUnitOfMeasurementId = price.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault()!.ProjectOperationDetailContractorService.OperationInfoService.OperationInfo.UnitOfMeasurementId,
            });


        query = query.OrderByDescending(c => c.Created);

        var count = await query.CountAsync(ct);

        if (orderBy?.Length > 0)
            query = query.SortBy(orderBy);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entites = await query.AsNoTrackingWithIdentityResolution().AsSplitQuery().ToListAsync(ct);

        return (entites, count);
    }


    public async Task<(List<GetsDraftableContractorCostOverModel> Data, int RowCount)> GetsDraftableContractorCostOver(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var query = DbSet

            .Where(c => !c.IsDeleted && c.ContractorId == contractorId)

            .Where(c =>
                (c.ContractorContract != null &&
                 c.ContractorContract.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed &&
                 c.ContractorContract.Details.Any(x => x.ContractorContractDetailServices.Any(x => x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id == projectId))) ||

                (c.ContractorContractDetail != null &&
                 c.ContractorContractDetail.ContractorContract.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed &&
                 c.ContractorContractDetail.ContractorContractDetailServices.Any(x => x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id == projectId))
            )

            .Select(costOver => new GetsDraftableContractorCostOverModel
            {
                Id = costOver.Id,
                CostOverId = costOver.CostOver.Id,
                CostOverName = costOver.CostOver.CostOverName,
                CostOverCode = costOver.CostOver.CostOverCode,
                Created = costOver.Created,
                Percentage = costOver.Percentage,
                Amount = costOver.Amount,
                Description = costOver.Description,

                ContractorContractId = costOver.ContractorContract != null
                    ? costOver.ContractorContract.Id
                    : null,

                ContractorContractDetailId = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.Id
                    : null,

                ContractorContractType = costOver.ContractorContract != null
                    ? costOver.ContractorContract.ContractorContractType.GetEnumDescription()
                    : costOver.ContractorContractDetail.ContractorContract.ContractorContractType.GetEnumDescription(),

                OperationInfoName = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoName
                    : "",

                OperationInfoCode = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.OperationInfo.OperationInfoCode
                    : "",

                ProjectOperationUnitOfMeasurementId = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.UnitOfMeasurementId
                    : null,

                ProjectOperationDetail = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.ProjectOperationDetail.OperationLocation.PrivateName
                    : "",

                ProjectOperationDetailDesc = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.ProjectOperationDetail.Description
                    : "",

                ServiceInfoId = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.Id
                    : null,

                ServiceInfoName = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoName
                    : "",

                ServiceInfoCode = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.ServiceInfoCode
                    : "",

                ServiceInfoUnitOfMeasurementId = costOver.ContractorContractDetail != null
                    ? costOver.ContractorContractDetail.ContractorContractDetailServices.FirstOrDefault().ProjectOperationDetailContractorService.OperationInfoService.ServiceInfo.UnitOfMeasurementId
                    : null,

            })
            ;
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.

        query = query.OrderByDescending(c => c.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query
            .ToListAsync(ct);

        return (entities, count);
    }

    public async Task<(List<ContractorContractDetailCostOver> Data, int RowCount)> GetsContractorContractCostOverForCSS(
        long contractorId,
        long projectId,
        int pageIndex,
        int pageSize,
        CT ct)
    {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
        var query = DbSet

            .Where(c => !c.IsDeleted && c.ContractorId == contractorId)

            .Where(c =>
                (c.ContractorContract != null &&
                 c.ContractorContract.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed &&
                 c.ContractorContract.Details.Any(x => x.ContractorContractDetailServices.Any(x => x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id == projectId))) ||

                (c.ContractorContractDetail != null &&
                 c.ContractorContractDetail.ContractorContract.ContractorContractHeader.Status == ContractorContractStatus.ManagementConfirmed &&
                 c.ContractorContractDetail.ContractorContractDetailServices.Any(x => x.ProjectOperationDetailContractorService.ProjectOperationDetail.ProjectOperation.Project.Id == projectId))
            );
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.

        query = query.OrderByDescending(c => c.Created);

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 || pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var entities = await query
            .ToListAsync(ct);

        return (entities, count);
    }

}
