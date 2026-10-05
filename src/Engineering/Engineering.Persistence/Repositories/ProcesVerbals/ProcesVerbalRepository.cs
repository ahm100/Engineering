using Engineering.Application.Abstractions.Data.ProcesVerbals;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbalDetailById;
using Engineering.Application.Services.ProcesVerbal.Contracts.GetProcesVerbals;
using Engineering.Domain.Entities.ProcesVerbal;
using Engineering.Domain.Entities.ProcesVerbal.Enums;
using Gita.Backend.Shared.Domain.Extensions;

namespace Engineering.Persistence.Repositories.ProcesVerbals;

public class ProcesVerbalRepository : BaseRepository<EngineeringDBContext, ProcesVerbal>, IProcesVerbalsRepository
{
    public ProcesVerbalRepository(EngineeringDBContext context) : base(context)
    { }


    public async Task<ProcesVerbal?> GetById(long Id)
        => await DbSet.FirstOrDefaultAsync(e => e.Id == Id);

    public async Task<(List<GetProcesVerbalsResponseModel> Data, int RowCount)> GetProcesVerbals(
        long? targetId, long? projectId, string? title,
        long? contractId, ProcesVerbalType? type,
        int pageIndex, int pageSize, CT ct)
    {
        var query = DbSet.Where(e => !e.IsDeleted);

        if (type > 0)
            query = query.Where(e => e.Type == type);

        if (targetId > 0)
            query = query.Where(e => e.Id == targetId);

        if (projectId > 0)
            query = query.Where(e => e.ProjectId == projectId);

        if (contractId > 0)
            query = query.Where(e => e.ContractId == contractId);

        if (title is not null)
            query = query.Where(e => e.TitleFa.Contains(title) || (e.TitleEn != null && e.TitleEn.Contains(title)));

        var count = await query.CountAsync(ct);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query
            .Include(e => e.Project)
            .Select(e => new GetProcesVerbalsResponseModel(
            e.Id,
            e.TitleFa,
            e.TitleEn,
            e.GenerateCode(e.Project.ProjectCode, e.Project.Created.Year),
            e.Type,
            e.Type.GetEnumDescription(),
            e.ProjectId,
            null,
            e.ContractId,
            null,
            e.RecordDateTime)).ToListAsync(ct);

        return (data, count);
    }

    public async Task<GetProcesVerbalDetailByIdResponse?> GetProcesVerbalDetailById(
        long id, CT ct)
    {
        return await DbSet
            .Where(e => e.Id == id && !e.IsDeleted)
            .Include(e => e.ProcesVerbalPODs)
            .Include(e => e.ProcesVerbalDocuments)
            .Include(e => e.ProcesVerbalItems)
            .Include(e => e.ProcesVerbalProducts)
            .Select(e => new GetProcesVerbalDetailByIdResponse
            {
                Id = e.Id,
                TitleFa = e.TitleFa,
                TitleEn = e.TitleEn,
                Type = e.Type,
                RecordDateTime = e.RecordDateTime,
                Location = e.Location,
                ContractId = e.ContractId,
                ContractNum = e.Contract.ContractNumber,
                ProjectId = e.ProjectId,
                ProjectName = e.Project.ProjectName,
                DeliveryStatus = e.DeliveryStatus,
                Limitations = e.Limitations,
                ProductStatus = e.ProductStatus,
                WorkStatus = e.WorkStatus,
                LimitationStatus = e.LimitationStatus,
                WorkStartStatus = e.WorkStartStatus,
                WorkStopReason = e.WorkStopReason,
                WorkStopStatus = e.WorkStopStatus,
                Docs = e.ProcesVerbalDocuments
                .Where(d => !d.IsDeleted)
                .Select(d => new ProcesVerbalDocDetailModel(
                    d.Id,
                    d.URL)).ToList(),
                Items = e.ProcesVerbalItems
                .Where(i => !i.IsDeleted)
                .Select(i => new ProcesVerbalItemDetailModel(
                    i.Id,
                    i.TitleFa)).ToList(),
                PODs = e.ProcesVerbalPODs
                .Where(p => !p.IsDeleted)
                .Select(p => new ProcesVerbalPODDetailModel(
                    p.Id,
                    p.ProjectOperationDetail.Description!,
                    p.ProjectOperationDetail.FinalAmount,
                    p.NewFinalAmount)).ToList(),
                Products = e.ProcesVerbalProducts
                .Where(p => !p.IsDeleted)
                .Select(p => new ProcesVerbalProductDetailModel(
                    p.Id,
                    p.ConsumableVolumeProduct.ProjectOperationDetail.Description,
                    p.ConsumableVolumeProduct.FinalValue,
                    p.NewFinalValue,
                    Math.Abs(p.ConsumableVolumeProduct.FinalValue - p.NewFinalValue),
                    p.Description,
                    p.ProcesVerbalProductItemStatus,
                    p.ProcesVerbalProductItemStatus.GetEnumDescription())).ToList()
            })
            .FirstOrDefaultAsync(ct);
    }
}
