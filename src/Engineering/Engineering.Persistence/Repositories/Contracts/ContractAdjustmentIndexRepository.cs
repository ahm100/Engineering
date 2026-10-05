using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentIndexes;
using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractAdjustmentIndexRepository
    : BaseRepository<EngineeringDBContext, ContractAdjustmentIndex>,
        IContractAdjustmentIndexRepository
{
    public ContractAdjustmentIndexRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<bool> IsContractAdjustmentIndexCodeDuplicate(
        long referenceId,
        string code,
        long? excludedId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (referenceId > 0)
            query = query.Where(index => index.ContractAdjustmentReferenceId == referenceId);

        if (!string.IsNullOrWhiteSpace(code))
            query = query.Where(index => index.Code == code);

        if (excludedId.HasValue)
            query = query.Where(index => index.Id != excludedId.Value);

        query = query.AsNoTracking();

        return await query.AnyAsync(ct);
    }

    public async Task<bool> HasActiveIndex(
        long referenceId,
        long indexId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (indexId > 0)
            query = query.Where(index => index.Id == indexId);

        if (referenceId > 0)
            query = query.Where(index => index.ContractAdjustmentReferenceId == referenceId);

        query = query.Where(index => index.IsActive);
        query = query.Where(index => index.ContractAdjustmentReference.IsActive);
        query = query.AsNoTracking();

        return await query.AnyAsync(ct);
    }

    public async Task<GetContractAdjustmentIndexByIdResponse?> GetContractAdjustmentIndexById(
        long referenceId,
        long id,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (id > 0)
            query = query.Where(index => index.Id == id);

        if (referenceId > 0)
            query = query.Where(index => index.ContractAdjustmentReferenceId == referenceId);

        query = query.AsNoTracking();

        return await query
            .Select(index => new GetContractAdjustmentIndexByIdResponse
            {
                Id = index.Id,
                ReferenceId = index.ContractAdjustmentReferenceId,
                Code = index.Code,
                ReferenceFaTitle = index.ContractAdjustmentReference.FaTitle,
                ReferenceEnTitle = index.ContractAdjustmentReference.EnTitle,
                FaTitle = index.FaTitle,
                EnTitle = index.EnTitle,
                Description = index.Description,
                IsActive = index.IsActive
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetContractAdjustmentIndexesModel> Data, int RowCount)> GetContractAdjustmentIndexes(
        long referenceId,
        bool? isActive,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (referenceId > 0)
            query = query.Where(index => index.ContractAdjustmentReferenceId == referenceId);

        if (isActive.HasValue)
            query = query.Where(index => index.IsActive == isActive.Value);

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            var pattern = filterData.MakeLikePattern();

            query = query.Where(index =>
                EF.Functions.Like(index.Code, pattern) ||
                EF.Functions.Like(index.FaTitle, pattern) ||
                EF.Functions.Like(index.EnTitle, pattern) ||
                (index.Description != null &&
                EF.Functions.Like(index.Description, pattern)));
        }

        query = query.AsNoTracking();

        var resultQuery = query.Select(index => new GetContractAdjustmentIndexesModel
        {
            Id = index.Id,
            ReferenceId = index.ContractAdjustmentReferenceId,
            Code = index.Code,
            FaTitle = index.FaTitle,
            EnTitle = index.EnTitle,
            Description = index.Description,
            IsActive = index.IsActive
        });

        var rowCount = await resultQuery.CountAsync(ct);

        if (orderBy is { Length: > 0 })
            resultQuery = resultQuery.SortBy(orderBy);
        else
            resultQuery = resultQuery.OrderBy(index => index.FaTitle);

        if (pageIndex > 0 && pageSize > 0)
            resultQuery = resultQuery.Page(pageIndex, pageSize);

        var data = await resultQuery.ToListAsync(ct);

        return (data, rowCount);
    }
}
