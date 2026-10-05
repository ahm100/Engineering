using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferenceById;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentReferences;
using Engineering.Domain.Entities.Contracts;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractAdjustmentReferenceRepository
    : BaseRepository<EngineeringDBContext, ContractAdjustmentReference>,
        IContractAdjustmentReferenceRepository
{
    public ContractAdjustmentReferenceRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task CreateContractAdjustmentReference(
        ContractAdjustmentReference entity,
        CT ct)
    {
        await DbSet.AddAsync(entity, ct);
    }

    public async Task<bool> IsContractAdjustmentReferenceCodeDuplicate(
        string code,
        long? excludedId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (!string.IsNullOrWhiteSpace(code))
            query = query.Where(reference => reference.Code == code);

        if (excludedId.HasValue)
            query = query.Where(reference => reference.Id != excludedId.Value);

        query = query.AsNoTracking();

        return await query.AnyAsync(ct);
    }

    public async Task<ContractAdjustmentReference?> GetContractAdjustmentReference(
        long id,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (id > 0)
            query = query.Where(reference => reference.Id == id);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractAdjustmentReference?> GetContractAdjustmentReferenceWithIndex(
        long referenceId,
        long indexId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query.Include(reference =>
            reference.Indexes.Where(index => index.Id == indexId));

        if (referenceId > 0)
            query = query.Where(reference => reference.Id == referenceId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<bool> ContractAdjustmentReferenceExists(
        long id,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (id > 0)
            query = query.Where(reference => reference.Id == id);

        query = query.AsNoTracking();

        return await query.AnyAsync(ct);
    }

    public async Task<GetContractAdjustmentReferenceByIdResponse?> GetContractAdjustmentReferenceById(
        long id,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (id > 0)
            query = query.Where(reference => reference.Id == id);

        query = query.AsNoTracking();

        return await query
            .Select(reference => new GetContractAdjustmentReferenceByIdResponse
            {
                Id = reference.Id,
                Code = reference.Code,
                FaTitle = reference.FaTitle,
                EnTitle = reference.EnTitle,
                Description = reference.Description,
                IsActive = reference.IsActive
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<(List<GetContractAdjustmentReferencesModel> Data, int RowCount)> GetContractAdjustmentReferences(
        bool? isActive,
        string? filterData,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (isActive.HasValue)
            query = query.Where(reference => reference.IsActive == isActive.Value);

        if (!string.IsNullOrWhiteSpace(filterData))
        {
            var pattern = filterData.MakeLikePattern();

            query = query.Where(reference =>
                EF.Functions.Like(reference.Code, pattern) ||
                EF.Functions.Like(reference.FaTitle, pattern) ||
                EF.Functions.Like(reference.EnTitle, pattern) ||
                (reference.Description != null &&
                 EF.Functions.Like(reference.Description, pattern)));
        }

        query = query.AsNoTracking();

        var resultQuery = query.Select(reference => new GetContractAdjustmentReferencesModel
        {
            Id = reference.Id,
            Code = reference.Code,
            FaTitle = reference.FaTitle,
            EnTitle = reference.EnTitle,
            Description = reference.Description,
            IsActive = reference.IsActive
        });

        var rowCount = await resultQuery.CountAsync(ct);

        if (orderBy is { Length: > 0 })
            resultQuery = resultQuery.SortBy(orderBy);
        else
            resultQuery = resultQuery.OrderBy(reference => reference.FaTitle);

        if (pageIndex > 0 && pageSize > 0)
            resultQuery = resultQuery.Page(pageIndex, pageSize);

        var data = await resultQuery.ToListAsync(ct);

        return (data, rowCount);
    }
}
