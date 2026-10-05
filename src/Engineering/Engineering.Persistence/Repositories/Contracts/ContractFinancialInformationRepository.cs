using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.GetContractFinancialInformation;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Entities.Synonyms.MetaData.Currencies;
using ContractEntity = Engineering.Domain.Entities.Contracts.Contract;
using ContractTypeDetailEntity = Engineering.Domain.Entities.Contracts.ContractTypeDetail;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractFinancialInformationRepository
    : BaseRepository<EngineeringDBContext, ContractFinancialInformation>,
        IContractFinancialInformationRepository
{
    public ContractFinancialInformationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetContractFinancialInformationResponse?> GetContractFinancialInformation(
        long contractId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (contractId > 0)
            query = query.Where(financial => financial.ContractId == contractId);

        if (companyId > 0)
            query = query.Where(financial => financial.Contract.CompanyId == companyId);

        query = query.AsNoTracking();

        var response = await BuildFinancialInformationQuery(query)
            .FirstOrDefaultAsync(ct);

        if (response is null)
            return null;

        var legalAmounts = await GetContractLegalAmounts(
            contractId,
            companyId,
            ct);

        if (legalAmounts is not null)
        {
            response.InitialAmount = legalAmounts.InitialAmount;
            response.FinalContractAmount = legalAmounts.FinalAmount;
            return response;
        }

        response.InitialAmount = response.RegisteredInitialAmount ??
            await GetContractBaselineAmount(contractId, companyId, ct);

        response.FinalContractAmount = ContractFinancialMath.NormalizeMoney(
            response.InitialAmount + response.FinalContractAmount);

        return response;
    }

    private IQueryable<GetContractFinancialInformationResponse> BuildFinancialInformationQuery(
        IQueryable<ContractFinancialInformation> query)
    {
        var currencies = DbContext
            .Set<ViewCurrency>()
            .AsQueryable()
            .AsNoTracking();

        return
            from financial in query
            join currency in currencies
                on financial.CurrencyId equals currency.Id
                into currencyGroup
            from currency in currencyGroup.DefaultIfEmpty()
            let changeAmount = financial.Contract.ContractChanges
                .Where(change => !change.IsDeleted)
                .Sum(change => (decimal?)change.FinancialChangeAmount) ?? 0m
            select new GetContractFinancialInformationResponse
            {
                Id = financial.Id,
                ContractId = financial.ContractId,
                FinalContractAmount = changeAmount,
                RegisteredInitialAmount = financial.RegisteredInitialAmount,
                CurrencyId = financial.CurrencyId,
                CurrencyName = currency == null ? string.Empty : currency.Name,
                CurrencyIso = currency == null ? string.Empty : currency.Iso,
                CurrencySymbol = currency == null ? null : currency.Symbol,
                HasPrepayment = financial.HasPrepayment,
                IsSubjectToAdjustment = financial.IsSubjectToAdjustment,
                ContractCeilingAmount = financial.ContractCeilingAmount,
                AdjustmentLimitValue = financial.AdjustmentLimitValue,
                AdjustmentLimitType = financial.AdjustmentLimitType,
                PrepaymentPercentage = financial.PrepaymentPercentage,
                PrepaymentAmortizationMethod = financial.PrepaymentAmortizationMethod,
                PrepaymentAmortizationValue = financial.PrepaymentAmortizationValue,
                PrepaymentStartStatusStatementNumber =
                    financial.PrepaymentStartStatusStatementNumber,
                PrepaymentStartProgressPercentage =
                    financial.PrepaymentStartProgressPercentage
            };
    }

    private async Task<ContractLegalAmountValues?> GetContractLegalAmounts(
        long contractId,
        long companyId,
        CT ct)
    {
        var query = DbContext
            .Set<ContractEntity>()
            .AsQueryable();

        if (contractId > 0)
            query = query.Where(contract => contract.Id == contractId);

        if (companyId > 0)
            query = query.Where(contract => contract.CompanyId == companyId);

        query = query.Where(contract => contract.LegalSnapshot != null);
        query = query.AsNoTracking();

        return await query
            .Select(contract => new ContractLegalAmountValues(
                contract.LegalSnapshot!.InitialAmount,
                contract.ContractChanges
                    .OrderByDescending(change => change.Date)
                    .ThenByDescending(change => change.Id)
                    .Select(change => (decimal?)change.FinalContractAmount)
                    .FirstOrDefault() ??
                contract.LegalSnapshot.FinalAmount))
            .FirstOrDefaultAsync(ct);
    }

    private async Task<decimal> GetContractBaselineAmount(
        long contractId,
        long companyId,
        CT ct)
    {
        var query = DbContext
            .Set<ContractTypeDetailEntity>()
            .AsQueryable();

        if (contractId > 0)
        {
            query = query.Where(detail =>
                detail.ContractType.ContractId == contractId);
        }

        if (companyId > 0)
        {
            query = query.Where(detail =>
                detail.ContractType.Contract.CompanyId == companyId);
        }

        query = query.Where(detail =>
            !detail.ContractType.IsDeleted &&
            !detail.IsDeleted);

        query = query.AsNoTracking();

        var pricingValues = await query
            .Select(detail => new ContractTypeDetailPricingValues(
                detail.ContractType.ContractId,
                detail.ContractType.PricingMethod,
                detail.Quantity,
                detail.UnitPrice,
                detail.FixedAmount,
                detail.Duration))
            .ToListAsync(ct);

        var amount = pricingValues.Sum(detail =>
            ContractFinancialMath.CalculateContractTypeDetailAmount(
                detail.PricingMethod,
                detail.Quantity,
                detail.UnitPrice,
                detail.FixedAmount,
                detail.Duration) ?? 0m);

        return ContractFinancialMath.NormalizeMoney(amount);
    }

}