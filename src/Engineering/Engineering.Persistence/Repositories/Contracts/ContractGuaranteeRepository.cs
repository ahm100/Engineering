using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetContractGuaranteeById;
using Engineering.Application.Services.Contracts.Contracts.GetContractGuarantees;
using Engineering.Domain.Entities.Contracts;

using ContractEntity = Engineering.Domain.Entities.Contracts.Contract;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractGuaranteeRepository
    : BaseRepository<EngineeringDBContext, ContractGuarantee>,
        IContractGuaranteeRepository
{
    public ContractGuaranteeRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetContractGuaranteeByIdResponse?> GetContractGuaranteeById(
        long contractId,
        long guaranteeId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (guaranteeId > 0)
            query = query.Where(guarantee => guarantee.Id == guaranteeId);

        if (contractId > 0)
            query = query.Where(guarantee => guarantee.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(guarantee => guarantee.Contract.CompanyId == companyId);

        query = query.AsNoTracking();

        return await query
            .Select(guarantee => new GetContractGuaranteeByIdResponse
            {
                Id = guarantee.Id,
                ContractId = guarantee.ContractId,
                Type = guarantee.Type,
                Amount = guarantee.Amount,
                Percentage = guarantee.Percentage,
                Number = guarantee.Number,
                IssueDate = guarantee.IssueDate,
                ExpiryDate = guarantee.ExpiryDate,
                Status = guarantee.Status,
                FileUrl = guarantee.FileUrl
            })
            .FirstOrDefaultAsync(ct);
    }

    public async Task<List<GetContractGuaranteesModel>> GetContractGuarantees(
        long contractId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (contractId > 0)
            query = query.Where(guarantee => guarantee.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(guarantee => guarantee.Contract.CompanyId == companyId);

        query = query.AsNoTracking();

        var resultQuery = query
            .OrderByDescending(guarantee => guarantee.IssueDate)
            .ThenByDescending(guarantee => guarantee.Id)
            .Select(guarantee => new GetContractGuaranteesModel
            {
                Id = guarantee.Id,
                Type = guarantee.Type,
                Amount = guarantee.Amount,
                Percentage = guarantee.Percentage,
                Number = guarantee.Number,
                IssueDate = guarantee.IssueDate,
                ExpiryDate = guarantee.ExpiryDate,
                Status = guarantee.Status,
                FileUrl = guarantee.FileUrl
            });

        return await resultQuery.ToListAsync(ct);
    }

    public bool HasActiveGuarantee(
        ContractEntity contract,
        long guaranteeId) =>
        contract.Guarantees.Any(guarantee =>
            guarantee.Id == guaranteeId &&
            !guarantee.IsDeleted);
}
