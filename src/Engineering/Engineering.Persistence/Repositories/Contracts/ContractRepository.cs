using Engineering.Application.Abstractions.Data.Contracts;
using Engineering.Application.Services.Contracts.Contracts.ContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.GetContractById;
using Engineering.Application.Services.Contracts.Contracts.GetContractForProcesVerbal;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationById;
using Engineering.Application.Services.Contracts.Contracts.GetContractRegistrationGrid;
using Engineering.Application.Services.Contracts.Contracts.GetContractsByStatus;
using Engineering.Application.Services.Contracts.Contracts.GetContractStructure;
using Engineering.Application.Services.Contracts.Contracts.GetFilteredContracts;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractRegistration;
using Engineering.Application.Services.Contracts.Contracts.UpdateContractStructure;
using Engineering.Application.Services.Contracts.Models;
using Engineering.Domain.Entities.Contracts;
using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Entities.ProcesVerbal.Enums;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Synonyms.MetaData.Currencies;
using ContractEntity = Engineering.Domain.Entities.Contracts.Contract;
using ContractTypeEntity = Engineering.Domain.Entities.Contracts.ContractType;
using ContractTypeDetailEntity = Engineering.Domain.Entities.Contracts.ContractTypeDetail;

namespace Engineering.Persistence.Repositories.Contracts;

public class ContractRepository
    : BaseRepository<EngineeringDBContext, ContractEntity>,
        IContractRepository
{
    public ContractRepository(EngineeringDBContext context) : base(context)
    {
    }

    public async Task<ContractEntity?> GetContract(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract => contract.CompanyId == companyId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractEntity?> GetContractWithDocuments(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query.Include(contract => contract.ContractDocuments);

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract => contract.CompanyId == companyId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractEntity?> GetContractWithTypes(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query.Include(contract => contract.ContractTypes);

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract => contract.CompanyId == companyId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractEntity?> GetContractForDelete(
        long id,
        long companyId,
        CT ct)
    {
        var query = BuildContractForDeleteQuery();

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract => contract.CompanyId == companyId);

        return await query.FirstOrDefaultAsync(ct);
    }

    private IQueryable<ContractEntity> BuildContractForDeleteQuery()
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(contract => contract.ContractTypes)
                .ThenInclude(type => type.ContractTypeDetails)
                    .ThenInclude(detail => detail.Adjustment);

        query = query.Include(contract => contract.ContractDocuments);
        query = query.Include(contract => contract.Guarantees);
        query = query.Include(contract => contract.FinancialInformation);

        query = query
            .Include(contract => contract.AdjustmentConfigurations)
                .ThenInclude(configuration => configuration.Scopes);

        query = query.Include(contract => contract.LegalSnapshot);

        query = query
            .Include(contract => contract.ContractChanges)
                .ThenInclude(change => change.Items);

        query = query
            .Include(contract => contract.ContractChanges)
                .ThenInclude(change => change.Documents);

        return query;
    }

    public async Task<ContractEntity?> GetContractById(
        long id,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        query = query.AsNoTracking();

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<GetContractByIdResponse?> GetContractByIdForResponse(
        long id,
        long companyId,
        CT ct)
    {
        var contracts = DbSet.AsQueryable();

        if (id > 0)
            contracts = contracts.Where(contract => contract.Id == id);

        if (companyId > 0)
            contracts = contracts.Where(contract => contract.CompanyId == companyId);

        contracts = contracts.AsNoTracking();

        var projects = DbContext
            .Set<Project>()
            .AsQueryable()
            .AsNoTracking();

        var query =
            from contract in contracts
            join project in projects
                on contract.ProjectId equals project.Id
            where companyId <= 0 || project.CompanyId == companyId
            select new GetContractByIdResponse
            {
                Id = contract.Id,
                ContractNumber = contract.ContractNumber,
                FaTitle = contract.FaTitle,
                EnTitle = contract.EnTitle,
                Description = contract.Description,
                ProjectId = contract.ProjectId,
                ProjectName = project.ProjectName,
                ProjectCode = project.ProjectCode,
                ContractPartyId = contract.ContractPartyId,
                StartDate = contract.StartDate,
                Duration = contract.Duration,
                DurationUnit = contract.DurationUnit,
                EndDate = contract.EndDate,
                Status = contract.Status,
                Urls = contract.ContractDocuments
                    .Select(document => document.Url)
                    .ToList(),
                Created = contract.Created,
                CreatorId = contract.CreatorId,
                Updated = contract.Updated,
                UpdaterId = contract.UpdaterId
            };

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task CreateContract(
        ContractEntity entity,
        CT ct)
    {
        await Create(entity, ct);
    }

    public async Task<bool> IsContractNumberDuplicate(
        long contractNumber,
        long? excludedContractId,
        CT ct)
    {
        var query = DbSet
            .IgnoreQueryFilters()
            .AsQueryable();

        if (contractNumber > 0)
            query = query.Where(contract =>
                contract.ContractNumber == contractNumber);

        if (excludedContractId.HasValue)
            query = query.Where(contract =>
                contract.Id != excludedContractId.Value);

        query = query.AsNoTracking();

        return await query.AnyAsync(ct);
    }

    public async Task EnsureContractNumberSequenceIsAfter(
        long contractNumber,
        CT ct)
    {
        // SQL is required here because the operation reads SQL Server sequence metadata
        // and invokes sys.sp_sequence_get_range. EF Core LINQ cannot perform this operation.
        await DbContext.Database.ExecuteSqlInterpolatedAsync($@"
            DECLARE @requestedNumber bigint = {contractNumber};
            DECLARE @currentValue bigint;

            SELECT @currentValue = COALESCE(
                CONVERT(bigint, current_value),
                CONVERT(bigint, start_value) - CONVERT(bigint, increment))
            FROM sys.sequences
            WHERE object_id = OBJECT_ID(N'[engineer].[Contract_ContractNumber]');

            IF @currentValue < @requestedNumber
            BEGIN
                DECLARE @rangeSize bigint = @requestedNumber - @currentValue;
                DECLARE @rangeFirstValue sql_variant;

                EXEC sys.sp_sequence_get_range
                    @sequence_name = N'engineer.Contract_ContractNumber',
                    @range_size = @rangeSize,
                    @range_first_value = @rangeFirstValue OUTPUT;
            END", ct);
    }

    public async Task<ContractEntity?> GetContractForRegistrationMutation(
        long id,
        long companyId,
        CT ct)
    {
        var query = BuildContractForRegistrationMutationQuery();

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract => contract.CompanyId == companyId);

        return await query.FirstOrDefaultAsync(ct);
    }

    private IQueryable<ContractEntity> BuildContractForRegistrationMutationQuery()
    {
        var query = DbSet.AsQueryable();

        query = query.Include(contract => contract.ContractDocuments);

        query = query
            .Include(contract => contract.ContractTypes)
                .ThenInclude(type => type.ContractTypeDetails)
                    .ThenInclude(detail => detail.Adjustment);

        query = query.Include(contract => contract.FinancialInformation);

        query = query
            .Include(contract => contract.AdjustmentConfigurations)
                .ThenInclude(configuration => configuration.Scopes);

        query = query.Include(contract => contract.LegalSnapshot);
        query = query.Include(contract => contract.StatusHistories);

        query = query
            .Include(contract => contract.ContractChanges)
                .ThenInclude(change => change.Items);

        query = query
            .Include(contract => contract.ContractChanges)
                .ThenInclude(change => change.Documents);

        return query;
    }

    public async Task<GetContractRegistrationByIdResponse?> GetContractRegistrationById(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(contract => contract.ContractDocuments)
            .Include(contract => contract.ContractTypes)
            .Include(contract => contract.FinancialInformation)
            .Include(contract => contract.LegalSnapshot)
            .Include(contract => contract.StatusHistories)
                .ThenInclude(history => history.Documents)
            .Include(contract => contract.ContractChanges);

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract =>
                contract.CompanyId == companyId);

        var contract = await query.FirstOrDefaultAsync(ct);

        if (contract is null)
            return null;

        var project = await GetContractProject(
            contract.ProjectId,
            companyId,
            ct);

        if (project is null)
            return null;

        var financial = contract.FinancialInformation;

        var currency = financial?.CurrencyId is long currencyId
            ? await GetCurrency(currencyId, ct)
            : null;

        var initialAmount = GetRegistrationInitialAmount(
            contract,
            financial);

        var activeFinancialChangeAmount = contract.ContractChanges
            .Where(change => !change.IsDeleted)
            .Sum(change => (decimal?)change.FinancialChangeAmount) ?? 0m;

        var finalAmount = GetRegistrationFinalAmount(
            contract,
            initialAmount,
            activeFinancialChangeAmount);

        var response = new GetContractRegistrationByIdResponse
        {
            Id = contract.Id,
            ContractNumber = contract.ContractNumber,
            FaTitle = contract.FaTitle,
            EnTitle = contract.EnTitle,
            Description = contract.Description,
            ProjectId = contract.ProjectId,
            ProjectName = project.ProjectName,
            ContractPartyId = contract.ContractPartyId,
            StartDate = contract.StartDate,
            Duration = contract.Duration,
            DurationUnit = contract.DurationUnit,
            EndDate = contract.EndDate,
            Status = contract.Status,
            IsRegistrationPending = contract.IsRegistrationPending,
            RequiresCalculatedInitialAmount =
                financial != null &&
                contract.LegalSnapshot == null &&
                !financial.RegisteredInitialAmount.HasValue,
            ActiveFinancialChangeAmount =
                activeFinancialChangeAmount,
            Urls = contract.ContractDocuments
                .Where(document => !document.IsDeleted)
                .Select(document => document.Url)
                .ToList(),
            StatusHistories = contract.StatusHistories
                .Where(history => !history.IsDeleted)
                .OrderBy(history => history.Created)
                .Select(history =>
                    new GetContractRegistrationStatusHistoryResponse
                    {
                        Id = history.Id,
                        FromStatus = history.FromStatus,
                        ToStatus = history.ToStatus,
                        Operation = history.TransitionType,
                        EffectiveDate = history.EffectiveDate,
                        Reason = history.Reason,
                        Description = history.Description,
                        SuspensionDurationMonths =
                            history.SuspensionDurationMonths,
                        Created = history.Created,
                        CreatorId = history.CreatorId,
                        Urls = history.Documents
                            .Where(document => !document.IsDeleted)
                            .Select(document => document.Url)
                            .ToList()
                    })
                .ToList(),
                Financial = financial == null
                ? null
                : new ContractRegistrationFinancialResponse
                {
                    InitialAmount = initialAmount,
                    FinalContractAmount = finalAmount,
                    CurrencyId = financial.CurrencyId,
                    CurrencyName = currency?.Name ?? string.Empty,
                    CurrencyIso = currency?.Iso ?? string.Empty,
                    CurrencySymbol = currency?.Symbol,
                    HasPrepayment = financial.HasPrepayment,
                    IsSubjectToAdjustment =
                        financial.IsSubjectToAdjustment,
                    ContractCeilingAmount =
                        financial.ContractCeilingAmount,
                    AdjustmentLimitValue =
                        financial.AdjustmentLimitValue,
                    AdjustmentLimitType =
                        financial.AdjustmentLimitType,
                    PrepaymentPercentage =
                        financial.PrepaymentPercentage,
                    PrepaymentAmortizationMethod =
                        financial.PrepaymentAmortizationMethod,
                    PrepaymentAmortizationValue =
                        financial.PrepaymentAmortizationValue,
                    PrepaymentStartStatusStatementNumber =
                        financial.PrepaymentStartStatusStatementNumber,
                    PrepaymentStartProgressPercentage =
                        financial.PrepaymentStartProgressPercentage
                }
        };

        ApplyRegistrationStructure(
            response,
            contract.ContractTypes
                .Where(type => !type.IsDeleted)
                .Select(type =>
                    new ContractRegistrationStructureValue(
                        type.Kind,
                        type.PricingMethod))
                .ToList());

        if (response.Financial is not null &&
            response.RequiresCalculatedInitialAmount)
        {
            response.Financial.InitialAmount =
                await GetContractBaselineAmount(
                    id,
                    companyId,
                    ct);

            response.Financial.FinalContractAmount =
                ContractFinancialMath.NormalizeMoney(
                    response.Financial.InitialAmount +
                    response.ActiveFinancialChangeAmount);
        }

        return response;
    }

    private async Task<Project?> GetContractProject(
        long projectId,
        long companyId,
        CT ct)
    {
        var query = DbContext
            .Set<Project>()
            .AsQueryable();

        if (projectId > 0)
            query = query.Where(project =>
                project.Id == projectId);

        if (companyId > 0)
            query = query.Where(project =>
                project.CompanyId == companyId);

        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);
    }

    private async Task<ViewCurrency?> GetCurrency(
        long currencyId,
        CT ct)
    {
        var query = DbContext
            .Set<ViewCurrency>()
            .AsQueryable();

        if (currencyId > 0)
            query = query.Where(currency =>
                currency.Id == currencyId);

        return await query
            .AsNoTracking()
            .FirstOrDefaultAsync(ct);
    }

    private static decimal GetRegistrationInitialAmount(
        ContractEntity contract,
        ContractFinancialInformation? financial)
    {
        if (contract.LegalSnapshot is not null)
            return contract.LegalSnapshot.InitialAmount;

        if (financial?.RegisteredInitialAmount.HasValue == true)
            return financial.RegisteredInitialAmount.Value;

        return 0m;
    }

    private static decimal GetRegistrationFinalAmount(
        ContractEntity contract,
        decimal initialAmount,
        decimal activeFinancialChangeAmount)
    {
        if (contract.LegalSnapshot is null)
            return initialAmount + activeFinancialChangeAmount;

        return contract.ContractChanges
            .OrderByDescending(change => change.Date)
            .ThenByDescending(change => change.Id)
            .Select(change => (decimal?)change.FinalContractAmount)
            .FirstOrDefault() ??
            contract.LegalSnapshot.FinalAmount;
    }

    private static void ApplyRegistrationStructure(
        GetContractRegistrationByIdResponse response,
        IReadOnlyCollection<ContractRegistrationStructureValue> structure)
    {
        var pricingMethods = structure
            .Select(value => value.PricingMethod)
            .Distinct()
            .ToList();

        response.IsRegistrationStructureCompatible =
            pricingMethods.Count <= 1;

        response.ContractTypeCode = structure.Count == 0
            ? null
            : ContractRegistrationTypeCode.Format(
                structure.Select(value => value.Kind));

        response.PricingMethod =
            pricingMethods.Count == 0
                ? null
                : pricingMethods[0];
    }

    public async Task<(List<GetContractRegistrationGridModel> Data, int RowCount)> GetContractRegistrationGrid(
        long? contractNumber,
        long? contractNumberFrom,
        long? contractNumberTo,
        ContractStatus? status,
        string? filterData,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = BuildRegistrationGridQuery(
            contractNumber,
            contractNumberFrom,
            contractNumberTo,
            status,
            filterData,
            companyId);

        var rowCount = await query.CountAsync(ct);

        var ordersByInitialAmount = orderBy?.Any(value =>
            string.Equals(
                value.Trim()
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault(),
                nameof(GetContractRegistrationGridModel.InitialAmount),
                StringComparison.OrdinalIgnoreCase)) == true;

        if (ordersByInitialAmount)
            return await GetRegistrationGridOrderedByInitialAmount(

                query,
                rowCount,
                companyId,
                orderBy,
                pageIndex,
                pageSize,
                ct);

        query = ApplyRegistrationGridOrdering(
            query,
            orderBy,
            pageIndex,
            pageSize);

        var rows = await query.ToListAsync(ct);

        await PopulateRegistrationInitialAmounts(
            rows,
            companyId,
            ct);

        return (rows, rowCount);
    }

    private IQueryable<GetContractRegistrationGridModel> BuildRegistrationGridQuery(
        long? contractNumber,
        long? contractNumberFrom,
        long? contractNumberTo,
        ContractStatus? status,
        string? filterData,
        long companyId)
    {
        var contracts = DbSet
            .AsQueryable()
            .AsNoTracking();

        if (companyId > 0)
            contracts = contracts.Where(contract =>
                contract.CompanyId == companyId);

        if (contractNumber.HasValue)
            contracts = contracts.Where(contract =>
                contract.ContractNumber == contractNumber.Value);

        if (contractNumberFrom.HasValue)
        {
            contracts = contracts.Where(contract =>
                contract.ContractNumber.HasValue &&
                contract.ContractNumber.Value >=
                contractNumberFrom.Value);
        }

        if (contractNumberTo.HasValue)
        {
            contracts = contracts.Where(contract =>
                contract.ContractNumber.HasValue &&
                contract.ContractNumber.Value <=
                contractNumberTo.Value);
        }

        if (status.HasValue)
            contracts = contracts.Where(contract =>
                contract.Status == status.Value);

        if (!string.IsNullOrWhiteSpace(filterData))
            contracts = ApplyRegistrationGridSearch(
                contracts,
                filterData);

        var projects = DbContext
            .Set<Project>()
            .AsQueryable()
            .AsNoTracking();

        var financials = DbContext
            .Set<ContractFinancialInformation>()
            .AsQueryable()
            .AsNoTracking();

        var currencies = DbContext
            .Set<ViewCurrency>()
            .AsQueryable()
            .AsNoTracking();

        return
            from contract in contracts
            join project in projects
                on contract.ProjectId equals project.Id
            from financial in financials
                .Where(value => value.ContractId == contract.Id)
                .DefaultIfEmpty()
            from currency in currencies
                .Where(value =>
                    financial != null &&
                    value.Id == financial.CurrencyId)
                .DefaultIfEmpty()
            where companyId <= 0 || project.CompanyId == companyId
            select new GetContractRegistrationGridModel
            {
                Id = contract.Id,
                ProjectName = project.ProjectName,
                ContractNumber = contract.ContractNumber,
                FaTitle = contract.FaTitle,
                ContractPartyId = contract.ContractPartyId,
                InitialAmount = financial == null
                    ? null
                    : contract.LegalSnapshot != null
                        ? contract.LegalSnapshot.InitialAmount
                        : financial.RegisteredInitialAmount,
                CurrencyId = financial == null
                    ? null
                    : financial.CurrencyId,
                CurrencyTitle = currency == null
                    ? null
                    : currency.Name,
                CurrencyIso = currency == null
                    ? null
                    : currency.Iso,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                HasAttachment = contract.ContractDocuments
                    .Any(document => !document.IsDeleted),
                Status = contract.Status,
                IsRegistrationPending =
                    contract.IsRegistrationPending,
                RequiresCalculatedInitialAmount =
                    financial != null &&
                    contract.LegalSnapshot == null &&
                    !financial.RegisteredInitialAmount.HasValue
            };
    }

    private static IQueryable<ContractEntity> ApplyRegistrationGridSearch(
        IQueryable<ContractEntity> query,
        string filterData)
    {
        return query.Where(contract =>
            contract.FaTitle.Contains(filterData) ||
            (contract.EnTitle != null &&
             contract.EnTitle.Contains(filterData)) ||
            (contract.ContractNumber.HasValue &&
             contract.ContractNumber.Value
                 .ToString()
                 .Contains(filterData)));
    }

    private async Task<(List<GetContractRegistrationGridModel> Data, int RowCount)> GetRegistrationGridOrderedByInitialAmount(
        IQueryable<GetContractRegistrationGridModel> query,
        int rowCount,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var rows = await query.ToListAsync(ct);

        await PopulateRegistrationInitialAmounts(
            rows,
            companyId,
            ct);

        IQueryable<GetContractRegistrationGridModel> orderedQuery =
            rows.AsQueryable();

        orderedQuery = orderedQuery
            .OrderByDescending(row => row.Id);

        if (orderBy is { Length: > 0 })
            orderedQuery = orderedQuery.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            orderedQuery = orderedQuery.Page(pageIndex, pageSize);

        return (orderedQuery.ToList(), rowCount);
    }

    private static IQueryable<GetContractRegistrationGridModel> ApplyRegistrationGridOrdering(
        IQueryable<GetContractRegistrationGridModel> query,
        string[]? orderBy,
        int pageIndex,
        int pageSize)
    {
        query = query.OrderByDescending(row => row.Id);

        if (orderBy is { Length: > 0 })
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        return query;
    }

    public async Task<(List<GetFilteredContractsModel> Data, int RowCount)> GetFilteredContracts(
        long? contractNumber,
        string? faTitle,
        string? enTitle,
        long? projectId,
        long? contractPartyId,
        ContractStatus? status,
        ContractDurationUnit? durationUnit,
        DateTime? startDateFrom,
        DateTime? startDateTo,
        DateTime? endDateFrom,
        DateTime? endDateTo,
        string? filterData,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = BuildFilteredContractsQuery(
            contractNumber,
            faTitle,
            enTitle,
            projectId,
            contractPartyId,
            status,
            durationUnit,
            startDateFrom,
            startDateTo,
            endDateFrom,
            endDateTo,
            filterData,
            companyId);

        query = query.OrderByDescending(contract => contract.Created);

        var rowCount = await query.CountAsync(ct);

        if (orderBy is { Length: > 0 })
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query.ToListAsync(ct);

        return (data, rowCount);
    }

    private IQueryable<GetFilteredContractsModel> BuildFilteredContractsQuery(
        long? contractNumber,
        string? faTitle,
        string? enTitle,
        long? projectId,
        long? contractPartyId,
        ContractStatus? status,
        ContractDurationUnit? durationUnit,
        DateTime? startDateFrom,
        DateTime? startDateTo,
        DateTime? endDateFrom,
        DateTime? endDateTo,
        string? filterData,
        long companyId)
    {
        var contracts = DbSet
            .AsQueryable()
            .AsNoTracking();

        if (contractNumber.HasValue)
            contracts = contracts.Where(contract =>
                contract.ContractNumber == contractNumber.Value);

        if (!string.IsNullOrWhiteSpace(faTitle))
            contracts = contracts.Where(contract =>
                contract.FaTitle.Contains(faTitle));

        if (!string.IsNullOrWhiteSpace(enTitle))
            contracts = contracts.Where(contract =>
                contract.EnTitle != null &&
                contract.EnTitle.Contains(enTitle));

        if (projectId.HasValue)
            contracts = contracts.Where(contract =>
                contract.ProjectId == projectId.Value);

        if (contractPartyId.HasValue)
            contracts = contracts.Where(contract =>
                contract.ContractPartyId == contractPartyId.Value);

        if (status.HasValue)
            contracts = contracts.Where(contract =>
                contract.Status == status.Value);

        if (durationUnit.HasValue)
            contracts = contracts.Where(contract =>
                contract.DurationUnit == durationUnit.Value);

        if (startDateFrom.HasValue)
            contracts = contracts.Where(contract =>
                contract.StartDate >= startDateFrom.Value);

        if (startDateTo.HasValue)
            contracts = contracts.Where(contract =>
                contract.StartDate <= startDateTo.Value);

        if (endDateFrom.HasValue)
            contracts = contracts.Where(contract =>
                contract.EndDate >= endDateFrom.Value);

        if (endDateTo.HasValue)
            contracts = contracts.Where(contract =>
                contract.EndDate <= endDateTo.Value);

        if (!string.IsNullOrWhiteSpace(filterData))
            contracts = contracts.Where(contract =>
                contract.FaTitle.Contains(filterData) ||
                (contract.EnTitle != null &&
                 contract.EnTitle.Contains(filterData)) ||
                (contract.ContractNumber.HasValue &&
                 contract.ContractNumber.Value
                     .ToString()
                     .Contains(filterData)));

        if (companyId > 0)
            contracts = contracts.Where(contract =>
                contract.CompanyId == companyId);

        var projects = DbContext
            .Set<Project>()
            .AsQueryable()
            .AsNoTracking();

        return
            from contract in contracts
            join project in projects
                on contract.ProjectId equals project.Id
            where companyId <= 0 || project.CompanyId == companyId
            select new GetFilteredContractsModel
            {
                Id = contract.Id,
                ContractNumber = contract.ContractNumber,
                FaTitle = contract.FaTitle,
                EnTitle = contract.EnTitle,
                Description = contract.Description,
                ProjectId = contract.ProjectId,
                ProjectName = project.ProjectName,
                ProjectCode = project.ProjectCode,
                ContractPartyId = contract.ContractPartyId,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Duration = contract.Duration,
                DurationUnit = contract.DurationUnit,
                Status = contract.Status,
                Created = contract.Created,
                CreatorId = contract.CreatorId,
                Updated = contract.Updated,
                UpdaterId = contract.UpdaterId
            };
    }

    public async Task<(List<GetContractsByStatusModel> Data, int RowCount)> GetContractsByStatus(
        ContractStatus status,
        long companyId,
        string[]? orderBy,
        int pageIndex,
        int pageSize,
        CT ct)
    {
        var query = BuildContractsByStatusQuery(
            status,
            companyId);

        query = query.OrderByDescending(contract => contract.Created);

        var rowCount = await query.CountAsync(ct);

        if (orderBy is { Length: > 0 })
            query = query.SortBy(orderBy);

        if (pageIndex > 0 && pageSize > 0)
            query = query.Page(pageIndex, pageSize);

        var data = await query.ToListAsync(ct);

        return (data, rowCount);
    }

    private IQueryable<GetContractsByStatusModel>
        BuildContractsByStatusQuery(
            ContractStatus status,
            long companyId)
    {
        var contracts = DbSet
            .AsQueryable()
            .AsNoTracking();

        if (companyId > 0)
            contracts = contracts.Where(contract =>
                contract.CompanyId == companyId);

        contracts = contracts.Where(contract =>
            contract.Status == status);

        var projects = DbContext
            .Set<Project>()
            .AsQueryable()
            .AsNoTracking();

        return
            from contract in contracts
            join project in projects
                on contract.ProjectId equals project.Id
            where companyId <= 0 || project.CompanyId == companyId
            select new GetContractsByStatusModel
            {
                Id = contract.Id,
                ContractNumber = contract.ContractNumber,
                FaTitle = contract.FaTitle,
                EnTitle = contract.EnTitle,
                Description = contract.Description,
                ProjectId = contract.ProjectId,
                ProjectName = project.ProjectName,
                ProjectCode = project.ProjectCode,
                ContractPartyId = contract.ContractPartyId,
                StartDate = contract.StartDate,
                EndDate = contract.EndDate,
                Duration = contract.Duration,
                DurationUnit = contract.DurationUnit,
                Status = contract.Status,
                Created = contract.Created,
                CreatorId = contract.CreatorId,
                Updated = contract.Updated,
                UpdaterId = contract.UpdaterId
            };
    }

    public async Task<GetContractStructureResponse?> GetContractStructure(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract => contract.CompanyId == companyId);

        query = query.AsNoTracking();

        return await query
            .Select(contract =>
                new GetContractStructureResponse(
                    contract.Id,
                    contract.ContractTypes
                        .Select(type =>
                            new GetContractStructureItemModel
                            {
                                Id = type.Id,
                                Kind = type.Kind,
                                PricingMethod = type.PricingMethod
                            })
                        .ToList()))
            .FirstOrDefaultAsync(ct);
    }

    public async Task<ContractEntity?> GetContractWithTypesAndDetails(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(contract => contract.ContractTypes)
                .ThenInclude(type => type.ContractTypeDetails)
                    .ThenInclude(detail => detail.Adjustment);

        query = query.Include(contract => contract.FinancialInformation);

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract =>
                contract.CompanyId == companyId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractEntity?> GetContractWithTypesDetailsAndFinancialInformation(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(contract => contract.ContractTypes)
                .ThenInclude(type => type.ContractTypeDetails)
                    .ThenInclude(detail => detail.Adjustment);

        query = query.Include(contract => contract.FinancialInformation);

        query = query
            .Include(contract => contract.AdjustmentConfigurations)
                .ThenInclude(configuration => configuration.Scopes);

        query = query.Include(contract => contract.LegalSnapshot);

        query = query
            .Include(contract => contract.ContractChanges)
                .ThenInclude(change => change.Items);

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract =>
                contract.CompanyId == companyId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractEntity?> GetContractWithTypesAndFinancialInformation(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(contract => contract.ContractTypes)
                .ThenInclude(type => type.ContractTypeDetails);

        query = query.Include(contract => contract.FinancialInformation);

        query = query
            .Include(contract => contract.ContractChanges)
                .ThenInclude(change => change.Items);

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract =>
                contract.CompanyId == companyId);
        query = query.Include(contract => contract.LegalSnapshot);
        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<List<GetContractForProcesVerbalResponse>>
        GetContractForProcesVerbal(
            long? projectId,
            CT ct)
    {
        var query = DbSet.AsQueryable();

        if (projectId.HasValue)
            query = query.Where(contract =>
                contract.ProjectId == projectId.Value);

        query = query.AsNoTracking();

        return await query
            .Select(contract =>
                new GetContractForProcesVerbalResponse
                {
                    Id = contract.Id,
                    CNumber = contract.ContractNumber,
                    TitleFa = contract.FaTitle,
                    IsTempDelivered = contract.ProcesVerbals.Any(pv =>
                        !pv.IsDeleted &&
                        pv.Type == ProcesVerbalType.TempDelivery)
                })
            .ToListAsync(ct);
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
            .Select(detail =>
                new ContractTypeDetailPricingValues(
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

    private async Task PopulateRegistrationInitialAmounts(
        IReadOnlyCollection<GetContractRegistrationGridModel> rows,
        long companyId,
        CT ct)
    {
        var contractIds = rows
            .Where(row => row.RequiresCalculatedInitialAmount)
            .Select(row => row.Id)
            .ToHashSet();

        if (contractIds.Count == 0)
            return;

        var query = DbContext
            .Set<ContractTypeDetailEntity>()
            .AsQueryable();

        query = query.Where(detail =>
            contractIds.Contains(detail.ContractType.ContractId));

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
            .Select(detail =>
                new ContractTypeDetailPricingValues(
                    detail.ContractType.ContractId,
                    detail.ContractType.PricingMethod,
                    detail.Quantity,
                    detail.UnitPrice,
                    detail.FixedAmount,
                    detail.Duration))
            .ToListAsync(ct);

        var amounts = pricingValues
            .GroupBy(detail => detail.ContractId)
            .ToDictionary(
                group => group.Key,
                group =>
                    ContractFinancialMath.NormalizeMoney(
                        group.Sum(detail =>
                            ContractFinancialMath
                                .CalculateContractTypeDetailAmount(
                                    detail.PricingMethod,
                                    detail.Quantity,
                                    detail.UnitPrice,
                                    detail.FixedAmount,
                                    detail.Duration) ?? 0m)));

        foreach (var row in rows
                     .Where(row => row.RequiresCalculatedInitialAmount))
        {
            row.InitialAmount =
                amounts.GetValueOrDefault(row.Id);
        }
    }

    public async Task<ContractEntity?> GetContractWithGuarantees(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query.Include(contract => contract.Guarantees);

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract =>
                contract.CompanyId == companyId);

        return await query.FirstOrDefaultAsync(ct);
    }

    public async Task<ContractEntity?> GetContractWithChangesForMutation(
        long id,
        long companyId,
        CT ct)
    {
        var query = DbSet.AsQueryable();

        query = query
            .Include(contract => contract.ContractTypes)
                .ThenInclude(type => type.ContractTypeDetails);

        query = query.Include(contract => contract.FinancialInformation);

        query = query
            .Include(contract => contract.ContractChanges)
                .ThenInclude(change => change.Items);

        query = query
            .Include(contract => contract.ContractChanges)
                .ThenInclude(change => change.Documents);

        if (id > 0)
            query = query.Where(contract => contract.Id == id);

        if (companyId > 0)
            query = query.Where(contract =>
                contract.CompanyId == companyId);
        query = query.Include(contract => contract.LegalSnapshot);
        return await query.FirstOrDefaultAsync(ct);
    }

    private sealed record ContractTypeDetailPricingValues(
        long ContractId,
        PricingMethod PricingMethod,
        decimal Quantity,
        decimal? UnitPrice,
        decimal? FixedAmount,
        decimal? Duration);

        private sealed record ContractRegistrationByIdReadValues(
        GetContractRegistrationByIdResponse Response,
        List<ContractRegistrationStructureValue> Structure);

        private sealed record ContractRegistrationStructureValue(
        ContractTypeKind Kind,
        PricingMethod PricingMethod);

        
    public ContractStructureMutationProjection GetContractStructureMutationProjection(
        ContractEntity contract,
        UpdateContractStructureRequest request)
    {
        var currentTypeIds = contract.ContractTypes
            .Select(type => type.Id)
            .ToHashSet();

        var requestedTypeIds = request.Items
            .Where(item => item.Id.HasValue)
            .Select(item => item.Id!.Value)
            .ToHashSet();

        var typeIdsToRemove = currentTypeIds
            .Except(requestedTypeIds)
            .ToList();

        var hasUnknownContractType = requestedTypeIds
            .Any(id => !currentTypeIds.Contains(id));

        var hasActiveDetailsConflict = request.Items
            .Where(item => item.Id.HasValue)
            .Any(item =>
            {
                var type = contract.ContractTypes
                    .FirstOrDefault(value => value.Id == item.Id!.Value);

                return type is not null &&
                    type.ContractTypeDetails.Any(detail => !detail.IsDeleted) &&
                    (type.Kind != item.Kind ||
                     type.PricingMethod != item.PricingMethod);
            });

        var containsCostPlus = request.Items
            .Any(item => item.PricingMethod == PricingMethod.CostPlus);

        var requiresContractCeilingAmount = request.Items
            .Any(item =>
                item.PricingMethod is
                    PricingMethod.CostPlus or
                    PricingMethod.TimeAndMaterial);

        return new ContractStructureMutationProjection(
            typeIdsToRemove,
            hasUnknownContractType,
            hasActiveDetailsConflict,
            containsCostPlus,
            requiresContractCeilingAmount);
    }

    public ContractRegistrationMutationProjection GetContractRegistrationMutationProjection(
        ContractEntity contract,
        UpdateContractRegistrationRequest request)
    {
        var structure = ContractRegistrationTypeCode.TryParse(
            request.ContractTypeCode,
            out var kinds)
            ? kinds
            : [];

        var activeTypes = contract.ContractTypes
            .Where(type => !type.IsDeleted)
            .ToList();

        var activeKinds = activeTypes
            .Select(type => type.Kind)
            .ToHashSet();

        var structureChanged =
            !activeKinds.SetEquals(structure) ||
            activeTypes.Any(type =>
                type.PricingMethod != request.PricingMethod);

        var headerChanged =
            contract.ContractNumber != request.ContractNumber ||
            contract.FaTitle != request.FaTitle ||
            contract.EnTitle != NormalizeOptional(request.EnTitle) ||
            contract.Description != request.Description ||
            contract.ProjectId != request.ProjectId ||
            contract.ContractPartyId != request.ContractPartyId ||
            contract.StartDate != request.StartDate ||
            contract.Duration != request.Duration ||
            contract.DurationUnit != request.DurationUnit;

        var documentsChanged =
            request.Urls is not null &&
            !AreRegistrationDocumentsEqual(contract, request.Urls);

        var financialChanged =
            contract.FinancialInformation is null ||
            contract.FinancialInformation.IsDeleted ||
            ContractFinancialMath.NormalizeMoney(contract.CalculateInitialAmount()) !=
            ContractFinancialMath.NormalizeMoney(request.Financial.InitialAmount) ||
            contract.FinancialInformation.CurrencyId != request.Financial.CurrencyId ||
            contract.FinancialInformation.HasPrepayment != request.Financial.HasPrepayment ||
            contract.FinancialInformation.IsSubjectToAdjustment != request.Financial.IsSubjectToAdjustment ||
            contract.FinancialInformation.ContractCeilingAmount != request.Financial.ContractCeilingAmount ||
            contract.FinancialInformation.AdjustmentLimitValue != request.Financial.AdjustmentLimitValue ||
            contract.FinancialInformation.AdjustmentLimitType != request.Financial.AdjustmentLimitType ||
            contract.FinancialInformation.PrepaymentPercentage != request.Financial.PrepaymentPercentage ||
            contract.FinancialInformation.PrepaymentAmortizationMethod != request.Financial.PrepaymentAmortizationMethod ||
            contract.FinancialInformation.PrepaymentAmortizationValue != request.Financial.PrepaymentAmortizationValue ||
            contract.FinancialInformation.PrepaymentStartStatusStatementNumber != request.Financial.PrepaymentStartStatusStatementNumber ||
            contract.FinancialInformation.PrepaymentStartProgressPercentage != request.Financial.PrepaymentStartProgressPercentage;

        return new ContractRegistrationMutationProjection(
            structureChanged,
            headerChanged,
            documentsChanged,
            financialChanged,
            activeTypes.Any(type =>
                type.PricingMethod is
                    PricingMethod.CostPlus or
                    PricingMethod.TimeAndMaterial),
            GetActiveFinancialChangeAmount(contract));
    }

    public bool HasActiveContractTypes(ContractEntity contract) =>
        contract.ContractTypes.Any(type => !type.IsDeleted);

    public bool HasContractTypeKind(
        ContractEntity contract,
        ContractTypeKind kind,
        long? excludedContractTypeId = null) =>
        contract.ContractTypes.Any(type =>
            type.Id != excludedContractTypeId &&
            type.Kind == kind);

    public bool RequiresContractCeilingAmount(ContractEntity contract) =>
        contract.ContractTypes.Any(type =>
            !type.IsDeleted &&
            type.PricingMethod is
                PricingMethod.CostPlus or
                PricingMethod.TimeAndMaterial);

    public bool HasPricingMethodRequiringCeiling(
        ContractEntity contract,
        PricingMethod pricingMethod,
        long? excludedContractTypeId = null) =>
        pricingMethod is PricingMethod.CostPlus or PricingMethod.TimeAndMaterial ||
        contract.ContractTypes.Any(type =>
            !type.IsDeleted &&
            type.Id != excludedContractTypeId &&
            type.PricingMethod is
                PricingMethod.CostPlus or
                PricingMethod.TimeAndMaterial);

    public ContractTypeEntity? GetContractTypeForMutation(
        ContractEntity contract,
        long contractTypeId) =>
        contract.ContractTypes.FirstOrDefault(type => type.Id == contractTypeId);

    public bool HasActiveContractTypeDetails(ContractTypeEntity contractType) =>
        contractType.ContractTypeDetails.Any(detail => !detail.IsDeleted);

    public decimal GetActiveFinancialChangeAmount(ContractEntity contract) =>
        contract.ContractChanges
            .Where(change => !change.IsDeleted)
            .Sum(change => change.FinancialChangeAmount);

    public bool AreRegistrationDocumentsEqual(
        ContractEntity contract,
        IReadOnlyCollection<string> urls) =>
        contract.ContractDocuments
            .Where(document => !document.IsDeleted)
            .Select(document => document.Url)
            .SequenceEqual(urls);

    private static string? NormalizeOptional(string? value) =>
        string.IsNullOrWhiteSpace(value) ? null : value;
}
