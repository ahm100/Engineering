using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.ContractAdjustmentConfigurations;
using Engineering.Application.Services.Contracts.Contracts.GetContractAdjustmentConfiguration;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using ContractEntity = Engineering.Domain.Entities.Contracts.Contract;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractAdjustmentConfigurationRepository
    : BaseRepository<EngineeringDBContext, ContractAdjustmentConfiguration>,
        IContractAdjustmentConfigurationRepository
{
    public ContractAdjustmentConfigurationRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<GetContractAdjustmentConfigurationResponse?> GetContractAdjustmentConfiguration(
        long contractId,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(configuration => configuration.Scopes)
            .Include(configuration => configuration.PriceIndex)
                .ThenInclude(priceIndex =>
                    priceIndex.ContractAdjustmentReference)
            .AsNoTracking();

        if (contractId > 0)
        {
            query = query.Where(configuration =>
                configuration.ContractId == contractId);
        }

        if (companyId > 0)
        {
            query = query.Where(configuration =>
                configuration.Contract.CompanyId == companyId);
        }
        var configurations = await query.ToListAsync(ct);

        if (configurations.Count == 0)
            return null;

        var method = await GetContractAdjustmentMethod(
            contractId,
            companyId,
            ct);

        return new GetContractAdjustmentConfigurationResponse
        {
            ContractId = configurations[0].ContractId,
            Method = method,
            Configurations = configurations
                .Select(BuildConfigurationResponse)
                .ToList()
        };
    }

    private static ContractAdjustmentConfigurationModel BuildConfigurationResponse(
        ContractAdjustmentConfiguration configuration)
    {
        return new ContractAdjustmentConfigurationModel
        {
            Id = configuration.Id,
            Scope = new ContractAdjustmentScopeResponse
            {
                WholeContract = configuration.Scopes.Any(scope =>
                    scope.ScopeType ==
                    ContractAdjustmentScopeType.WholeContract),

                ContractTypeKinds = configuration.Scopes
                    .Where(scope => scope.ContractTypeKind.HasValue)
                    .Select(scope => scope.ContractTypeKind!.Value)
                    .ToList(),

                ContractTypeDetailIds = configuration.Scopes
                    .Where(scope => scope.ContractTypeDetailId.HasValue)
                    .Select(scope => scope.ContractTypeDetailId!.Value)
                    .ToList()
            },
            Adjustment = BuildAdjustmentResponse(configuration)
        };
    }

    private static ContractAdjustmentConfigurationAdjustmentResponse BuildAdjustmentResponse(
        ContractAdjustmentConfiguration configuration)
    {
        return configuration.Type switch
        {
            ContractTypeDetailAdjustmentType.PriceIndex =>
                new ContractAdjustmentConfigurationAdjustmentResponse
                {
                    Type = configuration.Type,
                    PriceIndex =
                        new PriceIndexContractAdjustmentConfigurationResponse
                        {
                            BaseYear =
                                configuration.PriceIndexBaseYear!.Value,
                            BasePeriod =
                                configuration.PriceIndexBasePeriod!.Value,
                            ReferenceId =
                                configuration.PriceIndex!
                                    .ContractAdjustmentReferenceId,
                            IndexId =
                                configuration.PriceIndexId!.Value
                        }
                },

            ContractTypeDetailAdjustmentType.Currency =>
                new ContractAdjustmentConfigurationAdjustmentResponse
                {
                    Type = configuration.Type,
                    Currency =
                        new CurrencyContractAdjustmentConfigurationResponse
                        {
                            BaseDate =
                                configuration.CurrencyBaseDate!.Value,
                            BaseRate =
                                configuration.CurrencyBaseRate!.Value,
                            CurrencyId =
                                configuration.CurrencyId!.Value,
                            ReferenceType =
                                configuration.CurrencyReferenceType!.Value,
                            CustomReference =
                                configuration.CurrencyCustomReference
                        }
                },

            _ =>
                new ContractAdjustmentConfigurationAdjustmentResponse
                {
                    Type = configuration.Type,
                    Other =
                        new OtherContractAdjustmentConfigurationResponse
                        {
                            Basis = configuration.OtherBasis!,
                            Reference = configuration.OtherReference!,
                            Index = configuration.OtherIndex!,
                            Description = configuration.Description!
                        }
                }
        };
    }

    private async Task<ContractAdjustmentMethod> GetContractAdjustmentMethod(
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

        return await query
            .Select(contract =>
                contract.AdjustmentMethod ??
                ContractAdjustmentMethod.SingleBasis)
            .SingleAsync(ct);
    }
}
