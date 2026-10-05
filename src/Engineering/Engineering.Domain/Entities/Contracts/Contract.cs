using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Entities.SessionRecords;
using Engineering.Domain.Errors;
using Gita.Backend.Shared.Domain.Base.Results;
using System.Globalization;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.Contract)]
public class Contract : AuditableEntity<Contract, long>
{
    #region Properties

    [Description(ContractCmts.ContractNumber)]
    public long? ContractNumber { get; private set; }

    [Description(GlobalCmts.FaTitle)]
    public string FaTitle { get; private set; }

    [Description(GlobalCmts.EnTitle)]
    public string? EnTitle { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.ProjectId)]
    public long ProjectId { get; private set; }

    [Description(ContractCmts.ContractPartyId)]
    public long ContractPartyId { get; private set; }

    [Description(GlobalCmts.StartDate)]
    public DateTime StartDate { get; private set; }

    [Description(ContractCmts.Duration)]
    public int Duration { get; private set; }

    [Description(ContractCmts.DurationUnit)]
    public ContractDurationUnit DurationUnit { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime EndDate { get; private set; }

    [Description(GlobalCmts.Status)]
    public ContractStatus Status { get; private set; }

    public bool IsRegistrationPending { get; private set; }

    [Description(GlobalCmts.CompanyId)]
    public long CompanyId { get; private set; }

    [Description(GlobalCmts.ContractType)]
    private readonly List<ContractType> _contractTypes = [];

    public IReadOnlyList<ContractType> ContractTypes => _contractTypes;

    [Description(ContractCmts.ContractDocument)]
    private readonly List<ContractDocument> _contractDocuments = [];

    public IReadOnlyList<ContractDocument> ContractDocuments => _contractDocuments;

    [Description(ContractCmts.ContractFinancialInformation)]
    public ContractFinancialInformation? FinancialInformation { get; private set; }

    public ContractAdjustmentMethod? AdjustmentMethod { get; private set; }

    private readonly List<ContractAdjustmentConfiguration> _adjustmentConfigurations = [];

    public IReadOnlyList<ContractAdjustmentConfiguration> AdjustmentConfigurations =>
        _adjustmentConfigurations;

    [Description(ContractCmts.ContractChange)]
    private readonly List<ContractChange> _contractChanges = [];

    public IReadOnlyList<ContractChange> ContractChanges => _contractChanges;

    [Description(ContractCmts.ContractStatusHistory)]
    private readonly List<ContractStatusHistory> _statusHistories = [];

    public IReadOnlyList<ContractStatusHistory> StatusHistories => _statusHistories;

    [Description(ContractCmts.ContractLegalSnapshot)]
    public ContractLegalSnapshot? LegalSnapshot { get; private set; }

    [Description(ContractCmts.ContractGuarantee)]
    private readonly List<ContractGuarantee> _guarantees = [];
    public IReadOnlyList<ContractGuarantee> Guarantees => _guarantees;

    [Description(ContractCmts.ContractProceVerbals)]
    private readonly List<ProcesVerbal.ProcesVerbal> _procesVerbals = [];
    public IReadOnlyList<ProcesVerbal.ProcesVerbal> ProcesVerbals => _procesVerbals;

    [Description(SessionRecordCmts.SessionRecord)]
    private readonly List<SessionRecord> _sessionRecords = [];
    public IReadOnlyList<SessionRecord> SessionRecords => _sessionRecords;

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private Contract()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    {
    }

    public Contract(
        long companyId,
        long projectId,
        long contractPartyId,
        string faTitle,
        string? enTitle,
        string? description,
        DateTime startDate,
        int duration,
        ContractDurationUnit durationUnit) : this()
    {
        SetCompany(companyId);
        SetProject(projectId);
        SetContractParty(contractPartyId);
        SetFaTitle(faTitle);
        SetEnTitle(enTitle);
        SetDescription(description);
        SetStartDate(startDate);
        SetDuration(duration);
        SetDurationUnit(durationUnit);

        Status = ContractStatus.Draft;
        EndDate = CalculateEndDate(StartDate, Duration, DurationUnit);
    }

    #endregion

    #region Contract Header Commands

    public void UpdateReferenceInfo(
        string faTitle,
        string? enTitle,
        string? description)
    {
        SetFaTitle(faTitle);
        SetEnTitle(enTitle);
        SetDescription(description);
    }

    public void ChangeProject(long projectId)
    {
        SetProject(projectId);
    }

    public void ChangeContractParty(long contractPartyId)
    {
        SetContractParty(contractPartyId);
    }

    public void UpdateSchedule(
        DateTime startDate,
        int duration,
        ContractDurationUnit durationUnit)
    {
        SetStartDate(startDate);
        SetDuration(duration);
        SetDurationUnit(durationUnit);

        EndDate = CalculateEndDate(StartDate, Duration, DurationUnit);
    }

    public void AssignRegisteredContractNumber(long contractNumber)
        => SetContractNumber(contractNumber);

    #endregion

    #region Registration Commands

    public Result PrepareImportedRegistration()
    {
        if (Id != 0 || Status != ContractStatus.Draft ||
            _statusHistories.Any(oo => !oo.IsDeleted) || LegalSnapshot is not null ||
            IsRegistrationPending)
            return Result.Failure(ContractErrors.ContractStatusTransitionInvalid);

        IsRegistrationPending = true;
        return Result.Success();
    }

    public Result FinalizeImportedRegistration()
    {
        if (!IsRegistrationPending || Status != ContractStatus.Draft ||
            _statusHistories.Any(oo => !oo.IsDeleted) || LegalSnapshot is not null)
            return Result.Failure(ContractErrors.ContractRegistrationIsNotPending);

        IsRegistrationPending = false;
        return Result.Success();
    }

    #endregion

    #region Contract Lifecycle Commands

    public Result SubmitForReview()
        => ApplyStatusTransition(
            ContractStatus.UnderReview,
            ContractStatusTransitionType.SubmitForReview,
            DateTime.UtcNow);

    public Result ReturnToDraft()
        => ApplyStatusTransition(
            ContractStatus.Draft,
            ContractStatusTransitionType.ReturnToDraft,
            DateTime.UtcNow);

    public Result Approve()
    {
        if (DateTime.Today < StartDate.Date)
            return Result.Failure(ContractErrors.ContractActivationBeforeStartDate);

        return ApplyStatusTransition(
            ContractStatus.Active,
            ContractStatusTransitionType.Approve,
            DateTime.UtcNow,
            createLegalSnapshot: true);
    }

    public Result Suspend(
        DateTime effectiveDate,
        int durationMonths,
        string reason,
        string? description,
        IEnumerable<string> urls)
    {
        if (effectiveDate == default)
            return Result.Failure(ContractErrors.ContractStatusEffectiveDateRequired);
        if (durationMonths <= 0)
            return Result.Failure(ContractErrors.ContractSuspensionDurationInvalid);
        if (string.IsNullOrWhiteSpace(reason))
            return Result.Failure(ContractErrors.ContractStatusReasonRequired);

        return ApplyStatusTransition(
            ContractStatus.Suspended,
            ContractStatusTransitionType.Suspend,
            effectiveDate,
            reason,
            description,
            durationMonths,
            urls);
    }

    public Result Resume()
        => ApplyStatusTransition(
            ContractStatus.Active,
            ContractStatusTransitionType.Resume,
            DateTime.UtcNow);

    public Result Finish(
        DateTime effectiveDate,
        string reason,
        string? description,
        IEnumerable<string> urls)
    {
        var validation = ValidateLifecycleOperation(effectiveDate, reason);
        if (validation.IsFailure)
            return validation;

        return ApplyStatusTransition(
            ContractStatus.Finished,
            ContractStatusTransitionType.Finish,
            effectiveDate,
            reason,
            description,
            urls: urls);
    }

    public Result Terminate(
        DateTime effectiveDate,
        string reason,
        string? description,
        IEnumerable<string> urls)
    {
        var validation = ValidateLifecycleOperation(effectiveDate, reason);
        if (validation.IsFailure)
            return validation;

        return ApplyStatusTransition(
            ContractStatus.Terminated,
            ContractStatusTransitionType.Terminate,
            effectiveDate,
            reason,
            description,
            urls: urls);
    }

    public Result EnsureBaselineIsEditable()
    {
        if (Status is ContractStatus.Draft or
            ContractStatus.UnderReview)
        {
            return Result.Success();
        }

        return Result.Failure(
            ContractErrors.ContractBaselineCannotChangeInCurrentStatus);
    }

    public Result EnsureContractChangeCanBeCreated()
    {
        return CanCreateContractChange()
            ? Result.Success()
            : Result.Failure(
                ContractErrors.ContractChangeCannotBeCreatedInCurrentStatus);
    }

    public Result EnsureSummaryContractChangeCanBeCreated()
        => EnsureContractChangeCanBeCreated();

    public Result EnsureTypeDetailAdjustmentCanBeApplied()
    {
        return IsAdjustmentEnabledForContract()
            ? Result.Success()
            : Result.Failure(
                ContractErrors.ContractTypeDetailAdjustmentNotAllowed);
    }

    public Result EnsureTypeDetailAdjustmentEligibilityIsValid(
        bool? requestedEligibility,
        bool hasAdjustment)
    {
        return ValidateCreateTypeDetailAdjustmentEligibility(
            requestedEligibility,
            hasAdjustment);
    }

    public Result EnsureTypeDetailAdjustmentUpdateEligibilityIsValid(
        long contractTypeId,
        long contractTypeDetailId,
        bool? requestedEligibility,
        bool hasAdjustment)
    {
        var eligibilityResult = ValidateTypeDetailAdjustmentEligibility(
            requestedEligibility,
            hasAdjustment);

        if (eligibilityResult.IsFailure)
            return eligibilityResult;

        var contractType = GetActiveContractType(contractTypeId);

        return contractType.EnsureTypeDetailAdjustmentEligibilityIsValid(
            contractTypeDetailId,
            requestedEligibility,
            hasAdjustment);
    }

    public Result EnsureAdjustmentEligibilityIsValid(
        bool isSubjectToAdjustment)
    {
        if (!isSubjectToAdjustment && GetActiveAdjustmentConfiguration() is not null)
        {
            return Result.Failure(
                ContractErrors.ContractAdjustmentConfigurationMustBeRemovedBeforeDisabling);
        }

        if (isSubjectToAdjustment || !HasActiveTypeDetailAdjustments())
            return Result.Success();

        return Result.Failure(
            ContractErrors.ContractAdjustmentCannotBeDisabledWithExistingDetails);
    }

    #endregion

    #region ContractAdjustmentConfiguration Commands

    public Result<ContractAdjustmentConfiguration> AddAdjustmentConfiguration(
        ContractAdjustmentMethod method,
        ContractTypeDetailAdjustmentTerms terms,
        bool wholeContract,
        IReadOnlyCollection<ContractTypeKind> kinds,
        IReadOnlyCollection<long> detailIds)
    {
        if (!IsAdjustmentEnabledForContract())
            return Result.Failure<ContractAdjustmentConfiguration>(
                ContractErrors.ContractAdjustmentConfigurationNotAllowed)!;
        method = Guard.Against.EnumOutOfRange(method, nameof(method));
        var activeConfigurations = _adjustmentConfigurations
            .Where(configuration => !configuration.IsDeleted)
            .ToList();
        if (AdjustmentMethod.HasValue && AdjustmentMethod != method && activeConfigurations.Count > 0)
            return Result.Failure<ContractAdjustmentConfiguration>(
                ContractErrors.ContractAdjustmentConfigurationAlreadyExists)!;
        if (!HasValidAdjustmentScope(method, wholeContract, kinds, detailIds) ||
            method == ContractAdjustmentMethod.SingleBasis && activeConfigurations.Count > 0 ||
            method == ContractAdjustmentMethod.MultipleBasis &&
            HasOverlappingAdjustmentScope(null, kinds, detailIds))
            return Result.Failure<ContractAdjustmentConfiguration>(
                ContractErrors.ContractAdjustmentConfigurationScopeInvalid)!;

        AdjustmentMethod = method;
        var configuration = new ContractAdjustmentConfiguration(
            this, terms, wholeContract, kinds, detailIds);
        _adjustmentConfigurations.Add(configuration);
        SynchronizeAdjustmentConfiguration();
        return configuration;
    }

    public Result UpdateAdjustmentConfiguration(
        long id,
        ContractTypeDetailAdjustmentTerms terms,
        bool wholeContract,
        IReadOnlyCollection<ContractTypeKind> kinds,
        IReadOnlyCollection<long> detailIds)
    {
        if (!IsAdjustmentEnabledForContract())
            return Result.Failure(ContractErrors.ContractAdjustmentConfigurationNotAllowed);
        var configuration = _adjustmentConfigurations
            .Where(configuration => !configuration.IsDeleted)
            .FirstOrDefault(x => x.Id == id);
        if (configuration is null || configuration.Id != id)
            return Result.Failure(ContractErrors.ContractAdjustmentConfigurationNotFound);
        if (!AdjustmentMethod.HasValue ||
            !HasValidAdjustmentScope(AdjustmentMethod.Value, wholeContract, kinds, detailIds) ||
            HasOverlappingAdjustmentScope(id, kinds, detailIds))
            return Result.Failure(ContractErrors.ContractAdjustmentConfigurationScopeInvalid);

        configuration.Update(terms, wholeContract, kinds, detailIds);
        SynchronizeAdjustmentConfiguration();
        return Result.Success();
    }

    public Result RemoveAdjustmentConfiguration(long id)
    {
        var configuration = _adjustmentConfigurations
            .Where(configuration => !configuration.IsDeleted)
            .FirstOrDefault(x => x.Id == id);
        if (configuration is null || configuration.Id != id)
            return Result.Failure(ContractErrors.ContractAdjustmentConfigurationNotFound);

        foreach (var scope in configuration.Scopes.Where(scope => !scope.IsDeleted))
            scope.SoftDelete();
        configuration.SoftDelete();
        SynchronizeAdjustmentConfigurations();
        return Result.Success();
    }

    #endregion

    #region Contract Document Commands

    public void Delete()
    {
        foreach (var contractType in _contractTypes.Where(oo => !oo.IsDeleted))
            contractType.Delete();

        foreach (var contractDocument in _contractDocuments.Where(oo => !oo.IsDeleted))
            contractDocument.SoftDelete();

        if (FinancialInformation is not null && !FinancialInformation.IsDeleted)
            FinancialInformation.SoftDelete();

        foreach (var configuration in _adjustmentConfigurations.Where(
                     configuration => !configuration.IsDeleted))
        {
            foreach (var scope in configuration.Scopes.Where(scope => !scope.IsDeleted))
                scope.SoftDelete();
            configuration.SoftDelete();
        }

        foreach (var contractChange in _contractChanges.Where(oo => !oo.IsDeleted))
            contractChange.Delete();

        foreach (var guarantee in _guarantees.Where(oo => !oo.IsDeleted))
            guarantee.SoftDelete();

        SoftDelete();
    }

    public void AddDocuments(IEnumerable<string> urls)
    {
        foreach (var url in urls)
            _contractDocuments.Add(new ContractDocument(url, this));
    }

    public void ReplaceDocuments(IEnumerable<string> urls)
    {
        foreach (var contractDocument in _contractDocuments.Where(oo => !oo.IsDeleted))
            contractDocument.SoftDelete();

        AddDocuments(urls);
    }

    #endregion

    #region ContractType and ContractTypeDetail Commands

    public ContractType AddContractType(
        ContractTypeKind kind,
        PricingMethod pricingMethod)
    {
        if (HasActiveContractTypeKind(kind))
            throw new InvalidOperationException(
                $"An active ContractType with Kind {kind} already exists.");

        var contractType = new ContractType(
            this,
            kind,
            pricingMethod);

        _contractTypes.Add(contractType);

        return contractType;
    }

    public void UpdateContractType(
        long contractTypeId,
        ContractTypeKind kind,
        PricingMethod pricingMethod)
    {
        var contractType = GetActiveContractType(contractTypeId);

        if (HasActiveContractTypeKind(kind, contractTypeId))
            throw new InvalidOperationException(
                $"An active ContractType with Kind {kind} already exists.");

        contractType.Update(
            kind,
            pricingMethod);
    }

    public void RemoveContractType(long contractTypeId)
    {
        var contractType = _contractTypes.FirstOrDefault(
            oo => oo.Id == contractTypeId && !oo.IsDeleted);

        if (contractType is null)
            throw new InvalidOperationException(
                $"ContractType with Id {contractTypeId} not found.");

        var activeDetailIds = contractType.ContractTypeDetails
            .Where(oo => !oo.IsDeleted)
            .Select(oo => oo.Id)
            .ToList();

        foreach (var configuration in _adjustmentConfigurations.Where(
                     configuration => !configuration.IsDeleted))
        {
            foreach (var scope in configuration.Scopes.Where(scope =>
                         !scope.IsDeleted &&
                         scope.ScopeType == ContractAdjustmentScopeType.ContractTypeDetail &&
                         scope.ContractTypeDetailId.HasValue &&
                         activeDetailIds.Contains(scope.ContractTypeDetailId.Value)))
            {
                scope.SoftDelete();
            }
        }

        contractType.Delete();
    }

    public Result SynchronizeRegistrationStructure(
        IReadOnlyCollection<ContractTypeKind> kinds,
        PricingMethod pricingMethod)
    {
        if (kinds.Count == 0 || kinds.Count != kinds.Distinct().Count())
            return Result.Failure(ContractErrors.ContractRegistrationTypeInvalid);

        var activeTypes = _contractTypes.Where(oo => !oo.IsDeleted).ToList();
        if (activeTypes.Any(type =>
                (!kinds.Contains(type.Kind) || type.PricingMethod != pricingMethod) &&
                type.ContractTypeDetails.Any(detail => !detail.IsDeleted)))
            return Result.Failure(ContractErrors.ContractTypeStructureCannotChangeWithDetails);

        foreach (var type in activeTypes.Where(type => !kinds.Contains(type.Kind)).ToList())
            RemoveContractType(type.Id);

        foreach (var type in activeTypes.Where(type => kinds.Contains(type.Kind)))
            UpdateContractType(type.Id, type.Kind, pricingMethod);

        foreach (var kind in kinds.Where(kind => !activeTypes.Any(type => type.Kind == kind)))
            AddContractType(kind, pricingMethod);

        return Result.Success();
    }

    public Result<ContractTypeDetail> AddContractTypeDetail(
        long contractTypeId,
        long sourceId,
        decimal quantity,
        long? unitOfMeasurementId,
        decimal? unitPrice,
        decimal? fixedAmount,
        string? technicalSpecifications,
        string? expectedDeliverables,
        decimal? duration,
        ContractDurationUnit? durationUnit,
        bool? isSubjectToAdjustment,
        ContractTypeDetailAdjustmentTerms? adjustmentTerms)
    {
        var configuration = GetAdjustmentConfigurationForNewDetail(
            GetActiveContractType(contractTypeId).Kind);
        if (configuration is not null &&
            (isSubjectToAdjustment.HasValue || adjustmentTerms is not null))
        {
            return Result.Failure<ContractTypeDetail>(
                ContractErrors.ContractAdjustmentManagedByConfiguration)!;
        }

        var adjustmentEligibilityResult = ValidateCreateTypeDetailAdjustmentEligibility(
            isSubjectToAdjustment,
            adjustmentTerms is not null);

        if (adjustmentEligibilityResult.IsFailure)
        {
            return Result.Failure<ContractTypeDetail>(
                adjustmentEligibilityResult.Error!)!;
        }

        var contractType = GetActiveContractType(contractTypeId);
        var effectiveAdjustmentEligibility = configuration is not null
            ? configuration.CoversNewDetail(contractType.Kind)
            : isSubjectToAdjustment ?? false;
        var effectiveAdjustmentTerms = effectiveAdjustmentEligibility && configuration is not null
            ? configuration.GetAdjustmentTerms()
            : adjustmentTerms;

        return contractType.AddContractTypeDetail(
            sourceId,
            quantity,
            unitOfMeasurementId,
            unitPrice,
            fixedAmount,
            technicalSpecifications,
            expectedDeliverables,
            duration,
            durationUnit,
            effectiveAdjustmentEligibility,
            effectiveAdjustmentTerms);
    }

    public Result UpdateContractTypeDetail(
        long contractTypeId,
        long contractTypeDetailId,
        decimal quantity,
        long? unitOfMeasurementId,
        decimal? unitPrice,
        decimal? fixedAmount,
        string? technicalSpecifications,
        string? expectedDeliverables,
        decimal? duration,
        ContractDurationUnit? durationUnit,
        bool? isSubjectToAdjustment,
        ContractTypeDetailAdjustmentTerms? adjustmentTerms)
    {
        var configuration = GetAdjustmentConfigurationForDetail(contractTypeDetailId);
        if (configuration is not null &&
            (isSubjectToAdjustment.HasValue || adjustmentTerms is not null))
        {
            return Result.Failure(
                ContractErrors.ContractAdjustmentManagedByConfiguration);
        }

        var contractType = GetActiveContractType(contractTypeId);
        var detail = contractType.ContractTypeDetails.FirstOrDefault(oo =>
            oo.Id == contractTypeDetailId && !oo.IsDeleted);
        if (detail is null)
            throw new InvalidOperationException("ContractTypeDetail not found.");

        if (configuration is not null)
        {
            var covered = configuration.Covers(detail);
            return contractType.UpdateContractTypeDetail(
                contractTypeDetailId, quantity, unitOfMeasurementId, unitPrice,
                fixedAmount, technicalSpecifications, expectedDeliverables,
                duration, durationUnit, covered,
                covered ? configuration.GetAdjustmentTerms() : null);
        }

        var adjustmentEligibilityResult =
            EnsureTypeDetailAdjustmentUpdateEligibilityIsValid(
                contractTypeId,
                contractTypeDetailId,
                isSubjectToAdjustment,
                adjustmentTerms is not null);

        if (adjustmentEligibilityResult.IsFailure)
            return adjustmentEligibilityResult;

        return contractType.UpdateContractTypeDetail(
            contractTypeDetailId,
            quantity,
            unitOfMeasurementId,
            unitPrice,
            fixedAmount,
            technicalSpecifications,
            expectedDeliverables,
            duration,
            durationUnit,
            isSubjectToAdjustment,
            adjustmentTerms);
    }

    public void RemoveContractTypeDetail(
        long contractTypeId,
        long contractTypeDetailId)
    {
        var contractType = GetActiveContractType(contractTypeId);

        GetActiveAdjustmentConfiguration()?.RemoveDetailScope(contractTypeDetailId);

        contractType.RemoveContractTypeDetail(contractTypeDetailId);
    }

    #endregion

    #region ContractFinancialInformation Calculations

    public decimal CalculateInitialAmount()
    {
        if (FinancialInformation is { IsDeleted: false, RegisteredInitialAmount: not null })
            return FinancialInformation.RegisteredInitialAmount.Value;

        return ContractFinancialMath.NormalizeMoney(
            _contractTypes
                .Where(oo => !oo.IsDeleted)
                .SelectMany(oo =>
                    oo.ContractTypeDetails.Where(detail => !detail.IsDeleted))
                .Sum(oo => oo.CalculateAmount()));
    }

    public decimal CalculateFinalContractAmount()
    {
        return ContractFinancialMath.NormalizeMoney(
            CalculateInitialAmount() +
            _contractChanges
                .Where(oo => !oo.IsDeleted)
                .Sum(oo => oo.CalculateFinancialChangeAmount()));
    }

    public decimal CalculateCurrentLegalAmount()
    {
        var latestChange = GetLatestActiveContractChange();

        if (latestChange is not null)
            return latestChange.FinalContractAmount;

        return LegalSnapshot?.FinalAmount ?? CalculateFinalContractAmount();
    }

    public int CalculateCurrentDuration()
    {
        return Duration +
               _contractChanges
                   .Where(oo => !oo.IsDeleted)
                   .Sum(oo => oo.DurationChange ?? 0);
    }

    public DateTime CalculateCurrentEndDate()
    {
        var durationChange = _contractChanges
            .Where(oo => !oo.IsDeleted)
            .Sum(oo => oo.DurationChange ?? 0);

        var persianCalendar = new PersianCalendar();

        return DurationUnit == ContractDurationUnit.Month
            ? persianCalendar.AddMonths(EndDate, durationChange)
            : EndDate.AddDays(durationChange);
    }

    public DateTime CalculateEndDateWithDurationChange(int cumulativeDurationChange)
    {
        var persianCalendar = new PersianCalendar();
        return DurationUnit == ContractDurationUnit.Month
            ? persianCalendar.AddMonths(EndDate, cumulativeDurationChange)
            : EndDate.AddDays(cumulativeDurationChange);
    }

    #endregion

    #region ContractChange Commands

    public Result<ContractChange> AddContractChange(
        ContractChangeTerms terms)
    {
        var lifecycleResult = EnsureContractChangeCanBeCreated();

        if (lifecycleResult.IsFailure)
        {
            return Result.Failure<ContractChange>(lifecycleResult.Error!)!;
        }

        ValidateContractChangeNumber(terms.Number);

        var latestChange = GetLatestActiveContractChange();

        if (terms.Date < StartDate ||
            latestChange is not null &&
            terms.Date < latestChange.Date)
        {
            throw new InvalidOperationException(
                "ContractChange Date is not chronological.");
        }

        var contractChange = new ContractChange(this, terms);

        _contractChanges.Add(contractChange);

        ValidateEffectiveContractState();

        return contractChange;
    }

    public Result<ContractChange> AddSummaryContractChange(
        ContractSummaryChangeTerms terms)
    {
        var lifecycleResult = EnsureSummaryContractChangeCanBeCreated();
        if (lifecycleResult.IsFailure)
            return Result.Failure<ContractChange>(lifecycleResult.Error!)!;

        ValidateContractChangeNumber(terms.Number);
        var latestChange = GetLatestActiveContractChange();
        if (terms.Date < StartDate || latestChange is not null && terms.Date < latestChange.Date)
            return Result.Failure<ContractChange>(ContractErrors.ContractChangeDateInvalid)!;

        var contractChange = new ContractChange(this, terms);
        _contractChanges.Add(contractChange);
        ValidateEffectiveContractState();
        return contractChange;
    }

    public Result UpdateContractChange(
        long id,
        ContractChangeTerms terms)
    {
        var contractChange = GetActiveContractChange(id);
        var latestChange = GetLatestActiveContractChange();

        if (latestChange?.Id != contractChange.Id)
            throw new InvalidOperationException(
                "Only the latest ContractChange can be updated.");

        ValidateContractChangeNumber(terms.Number, id);

        var previousChange = _contractChanges
            .Where(oo => !oo.IsDeleted && oo.Id != id)
            .OrderByDescending(oo => oo.Date)
            .ThenByDescending(oo => oo.Id)
            .FirstOrDefault();

        if (terms.Date < StartDate ||
            previousChange is not null &&
            terms.Date < previousChange.Date)
        {
            throw new InvalidOperationException(
                "ContractChange Date is not chronological.");
        }

        var updateResult = contractChange.UpdateDetailed(terms);
        if (updateResult.IsFailure)
            return updateResult;

        ValidateEffectiveContractState();
        return Result.Success();
    }

    public Result UpdateSummaryContractChange(long id, ContractSummaryChangeTerms terms)
    {
        var contractChange = GetActiveContractChange(id);
        var latestChange = GetLatestActiveContractChange();
        if (latestChange?.Id != contractChange.Id)
            return Result.Failure(ContractErrors.ContractChangeOnlyLatestCanBeModified);

        ValidateContractChangeNumber(terms.Number, id);
        var previousChange = _contractChanges
            .Where(oo => !oo.IsDeleted && oo.Id != id)
            .OrderByDescending(oo => oo.Date).ThenByDescending(oo => oo.Id)
            .FirstOrDefault();
        if (terms.Date < StartDate || previousChange is not null && terms.Date < previousChange.Date)
            return Result.Failure(ContractErrors.ContractChangeDateInvalid);

        var updateResult = contractChange.UpdateSummary(terms);
        if (updateResult.IsFailure)
            return updateResult;

        ValidateEffectiveContractState();
        return Result.Success();
    }

    public void RemoveContractChange(long id)
    {
        var contractChange = GetActiveContractChange(id);
        var latestChange = GetLatestActiveContractChange();

        if (latestChange?.Id != contractChange.Id)
            throw new InvalidOperationException(
                "Only the latest ContractChange can be deleted.");

        contractChange.Delete();
    }

    #endregion

    #region ContractFinancialInformation Commands

    public Result<ContractFinancialInformation> AddFinancialInformation(
        decimal calculatedInitialAmount,
        decimal calculatedCurrentContractAmount,
        long currencyId,
        bool hasPrepayment,
        bool isSubjectToAdjustment,
        decimal? contractCeilingAmount,
        decimal? adjustmentLimitValue,
        ContractAdjustmentLimitType? adjustmentLimitType,
        decimal? prepaymentPercentage,
        PrepaymentAmortizationMethod? prepaymentAmortizationMethod,
        decimal? prepaymentAmortizationValue,
        int? prepaymentStartStatusStatementNumber,
        decimal? prepaymentStartProgressPercentage)
    {
        var adjustmentEligibilityResult =
            EnsureAdjustmentEligibilityIsValid(isSubjectToAdjustment);

        if (adjustmentEligibilityResult.IsFailure)
        {
            return Result.Failure<ContractFinancialInformation>(
                adjustmentEligibilityResult.Error!)!;
        }

        if (FinancialInformation is not null &&
            !FinancialInformation.IsDeleted)
        {
            throw new InvalidOperationException(
                "Contract financial information already exists.");
        }

        FinancialInformation = new ContractFinancialInformation(
            this,
            calculatedInitialAmount,
            calculatedCurrentContractAmount,
            currencyId,
            hasPrepayment,
            isSubjectToAdjustment,
            contractCeilingAmount,
            adjustmentLimitValue,
            adjustmentLimitType,
            prepaymentPercentage,
            prepaymentAmortizationMethod,
            prepaymentAmortizationValue,
            prepaymentStartStatusStatementNumber,
            prepaymentStartProgressPercentage);

        return FinancialInformation;
    }

    public Result<ContractFinancialInformation> AddRegisteredFinancialInformation(
        decimal registeredInitialAmount,
        long currencyId,
        bool hasPrepayment,
        bool isSubjectToAdjustment,
        decimal? contractCeilingAmount,
        decimal? adjustmentLimitValue,
        ContractAdjustmentLimitType? adjustmentLimitType,
        decimal? prepaymentPercentage,
        PrepaymentAmortizationMethod? prepaymentAmortizationMethod,
        decimal? prepaymentAmortizationValue,
        int? prepaymentStartStatusStatementNumber,
        decimal? prepaymentStartProgressPercentage)
    {
        var normalizedAmount = ContractFinancialMath.NormalizeMoney(registeredInitialAmount);
        if (normalizedAmount <= 0m)
            return Result.Failure<ContractFinancialInformation>(ContractErrors.ContractFinancialTermsInvalid)!;

        var adjustmentResult = EnsureAdjustmentEligibilityIsValid(isSubjectToAdjustment);
        if (adjustmentResult.IsFailure)
            return Result.Failure<ContractFinancialInformation>(adjustmentResult.Error!)!;
        if (FinancialInformation is not null && !FinancialInformation.IsDeleted)
            return Result.Failure<ContractFinancialInformation>(
                ContractErrors.ContractFinancialInformationAlreadyExists)!;

        FinancialInformation = new ContractFinancialInformation(
            this,
            normalizedAmount, normalizedAmount, currencyId, hasPrepayment,
            isSubjectToAdjustment, contractCeilingAmount, adjustmentLimitValue,
            adjustmentLimitType, prepaymentPercentage, prepaymentAmortizationMethod,
            prepaymentAmortizationValue, prepaymentStartStatusStatementNumber,
            prepaymentStartProgressPercentage, normalizedAmount);
        return FinancialInformation;
    }

    public Result UpdateRegisteredFinancialInformation(
        decimal registeredInitialAmount,
        long currencyId,
        bool hasPrepayment,
        bool isSubjectToAdjustment,
        decimal? contractCeilingAmount,
        decimal? adjustmentLimitValue,
        ContractAdjustmentLimitType? adjustmentLimitType,
        decimal? prepaymentPercentage,
        PrepaymentAmortizationMethod? prepaymentAmortizationMethod,
        decimal? prepaymentAmortizationValue,
        int? prepaymentStartStatusStatementNumber,
        decimal? prepaymentStartProgressPercentage)
    {
        var normalizedAmount = ContractFinancialMath.NormalizeMoney(registeredInitialAmount);
        if (normalizedAmount <= 0m)
            return Result.Failure(ContractErrors.ContractFinancialTermsInvalid);

        var adjustmentResult = EnsureAdjustmentEligibilityIsValid(isSubjectToAdjustment);
        if (adjustmentResult.IsFailure)
            return adjustmentResult;
        if (FinancialInformation is null || FinancialInformation.IsDeleted)
            return Result.Failure(ContractErrors.ContractFinancialInformationNotFound);

        if (!isSubjectToAdjustment)
        {
            var disableResult = DisableAdjustmentEligibilityForActiveDetails();
            if (disableResult.IsFailure)
                return disableResult;
        }

        FinancialInformation.Update(
            normalizedAmount, normalizedAmount, currencyId, hasPrepayment,
            isSubjectToAdjustment, contractCeilingAmount, adjustmentLimitValue,
            adjustmentLimitType, prepaymentPercentage, prepaymentAmortizationMethod,
            prepaymentAmortizationValue, prepaymentStartStatusStatementNumber,
            prepaymentStartProgressPercentage, normalizedAmount);
        return Result.Success();
    }

    public Result UpdateFinancialInformation(
        decimal calculatedInitialAmount,
        decimal calculatedCurrentContractAmount,
        long currencyId,
        bool hasPrepayment,
        bool isSubjectToAdjustment,
        decimal? contractCeilingAmount,
        decimal? adjustmentLimitValue,
        ContractAdjustmentLimitType? adjustmentLimitType,
        decimal? prepaymentPercentage,
        PrepaymentAmortizationMethod? prepaymentAmortizationMethod,
        decimal? prepaymentAmortizationValue,
        int? prepaymentStartStatusStatementNumber,
        decimal? prepaymentStartProgressPercentage)
    {
        var adjustmentEligibilityResult =
            EnsureAdjustmentEligibilityIsValid(isSubjectToAdjustment);

        if (adjustmentEligibilityResult.IsFailure)
            return adjustmentEligibilityResult;

        if (FinancialInformation is null ||
            FinancialInformation.IsDeleted)
        {
            throw new InvalidOperationException(
                "Contract financial information not found.");
        }

        if (!isSubjectToAdjustment)
        {
            var disableResult = DisableAdjustmentEligibilityForActiveDetails();

            if (disableResult.IsFailure)
                return disableResult;
        }

        FinancialInformation.Update(
            calculatedInitialAmount,
            calculatedCurrentContractAmount,
            currencyId,
            hasPrepayment,
            isSubjectToAdjustment,
            contractCeilingAmount,
            adjustmentLimitValue,
            adjustmentLimitType,
            prepaymentPercentage,
            prepaymentAmortizationMethod,
            prepaymentAmortizationValue,
            prepaymentStartStatusStatementNumber,
            prepaymentStartProgressPercentage,
            FinancialInformation.RegisteredInitialAmount);

        return Result.Success();
    }

    #endregion

    #region ContractGuarantee Commands

    public ContractGuarantee AddGuarantee(
        ContractGuaranteeType type,
        decimal amount,
        decimal? percentage,
        string number,
        DateTime issueDate,
        DateTime expiryDate,
        string? fileUrl)
    {
        var guarantee = new ContractGuarantee(
            this,
            type,
            amount,
            percentage,
            number,
            issueDate,
            expiryDate,
            fileUrl);

        _guarantees.Add(guarantee);

        return guarantee;
    }

    public void UpdateGuarantee(
        long guaranteeId,
        ContractGuaranteeType type,
        decimal amount,
        decimal? percentage,
        string number,
        DateTime issueDate,
        DateTime expiryDate,
        string? fileUrl)
    {
        var guarantee = GetActiveGuarantee(guaranteeId);

        guarantee.Update(
            type,
            amount,
            percentage,
            number,
            issueDate,
            expiryDate,
            fileUrl);
    }

    public void ChangeGuaranteeStatus(
        long guaranteeId,
        ContractGuaranteeStatus status)
    {
        var guarantee = GetActiveGuarantee(guaranteeId);

        guarantee.ChangeStatus(status);
    }

    public void RemoveGuarantee(long guaranteeId)
    {
        var guarantee = GetActiveGuarantee(guaranteeId);

        guarantee.SoftDelete();
    }

    #endregion

    #region Private Helpers

    private void SetCompany(long value)
        => CompanyId = Guard.Against.NegativeOrZero(
            value,
            nameof(value));

    private void SetContractNumber(long value)
        => ContractNumber = Guard.Against.NegativeOrZero(value, nameof(value));

    private void SetProject(long value)
        => ProjectId = Guard.Against.NegativeOrZero(
            value,
            nameof(value));

    private void SetContractParty(long value)
        => ContractPartyId = Guard.Against.NegativeOrZero(
            value,
            nameof(value));

    private void SetFaTitle(string value)
        => FaTitle = Guard.Against.NullOrWhiteSpace(
            value,
            nameof(value));

    private void SetEnTitle(string? value)
        => EnTitle = string.IsNullOrWhiteSpace(value)
            ? null
            : value;

    private void SetDescription(string? value)
        => Description = value;

    private void SetStartDate(DateTime value)
        => StartDate = Guard.Against.Null(
            value,
            nameof(value));

    private void SetDuration(int value)
        => Duration = Guard.Against.NegativeOrZero(
            value,
            nameof(value));

    private void SetDurationUnit(ContractDurationUnit value)
        => DurationUnit = Guard.Against.EnumOutOfRange(
            value,
            nameof(value));

    private static DateTime CalculateEndDate(
        DateTime startDate,
        int duration,
        ContractDurationUnit durationUnit)
    {
        var persianCalendar = new PersianCalendar();

        return durationUnit == ContractDurationUnit.Month
            ? persianCalendar.AddMonths(startDate, duration)
            : startDate.AddDays(duration);
    }

    private bool HasActiveContractTypeKind(
        ContractTypeKind kind,
        long? excludedContractTypeId = null)
    {
        return _contractTypes.Any(oo =>
            !oo.IsDeleted &&
            oo.Kind == kind &&
            (!excludedContractTypeId.HasValue ||
             oo.Id != excludedContractTypeId.Value));
    }

    private bool HasActiveTypeDetailAdjustments()
    {
        return _contractTypes.Any(contractType =>
            !contractType.IsDeleted &&
            contractType.ContractTypeDetails.Any(detail =>
                !detail.IsDeleted &&
                detail.Adjustment is { IsDeleted: false }));
    }

    private bool HasActiveContractChanges()
        => _contractChanges.Any(oo => !oo.IsDeleted);

    private bool IsAdjustmentEnabledForContract()
    {
        return FinancialInformation is
        {
            IsDeleted: false,
            IsSubjectToAdjustment: true
        };
    }

    private Result ValidateTypeDetailAdjustmentEligibility(
        bool? requestedEligibility,
        bool hasAdjustment)
    {
        if (GetActiveAdjustmentConfiguration() is not null &&
            (requestedEligibility.HasValue || hasAdjustment))
        {
            return Result.Failure(
                ContractErrors.ContractAdjustmentManagedByConfiguration);
        }

        var contractAllowsAdjustment = IsAdjustmentEnabledForContract();

        if (requestedEligibility == true && !contractAllowsAdjustment ||
            hasAdjustment && !contractAllowsAdjustment)
        {
            return Result.Failure(
                ContractErrors.ContractTypeDetailAdjustmentNotAllowed);
        }

        if (requestedEligibility == false && hasAdjustment)
        {
            return Result.Failure(
                ContractErrors.ContractTypeDetailAdjustmentEligibilityInvalid);
        }

        return Result.Success();
    }

    private Result ValidateCreateTypeDetailAdjustmentEligibility(
        bool? requestedEligibility,
        bool hasAdjustment)
    {
        var eligibilityResult = ValidateTypeDetailAdjustmentEligibility(
            requestedEligibility,
            hasAdjustment);

        if (eligibilityResult.IsFailure)
            return eligibilityResult;

        return hasAdjustment && requestedEligibility != true
            ? Result.Failure(
                ContractErrors.ContractTypeDetailAdjustmentEligibilityInvalid)
            : Result.Success();
    }

    private ContractAdjustmentConfiguration? GetActiveAdjustmentConfiguration()
    {
        return _adjustmentConfigurations.FirstOrDefault(x => !x.IsDeleted);
    }

    private bool HasValidAdjustmentScope(
        ContractAdjustmentMethod method,
        bool wholeContract,
        IReadOnlyCollection<ContractTypeKind> kinds,
        IReadOnlyCollection<long> detailIds)
    {
        if (wholeContract || kinds.Count != 1 || detailIds.Count != 0)
            return false;

        var kind = kinds.Single();
        if (kind is not (
            ContractTypeKind.Engineering or
            ContractTypeKind.Procurement or
            ContractTypeKind.Construction or
            ContractTypeKind.Services))
            return false;

        var activeKinds = _contractTypes
            .Where(type => !type.IsDeleted)
            .Select(type => type.Kind)
            .Where(activeKind => activeKind is
                ContractTypeKind.Engineering or
                ContractTypeKind.Procurement or
                ContractTypeKind.Construction or
                ContractTypeKind.Services)
            .Distinct()
            .ToList();

        if (!activeKinds.Contains(kind))
            return false;

        return method == ContractAdjustmentMethod.SingleBasis ||
               method == ContractAdjustmentMethod.MultipleBasis && activeKinds.Count > 1;
    }

    private bool HasOverlappingAdjustmentScope(
        long? excludedConfigurationId,
        IReadOnlyCollection<ContractTypeKind> kinds,
        IReadOnlyCollection<long> detailIds)
    {
        var activeDetails = _contractTypes.Where(type => !type.IsDeleted)
            .SelectMany(type => type.ContractTypeDetails)
            .Where(detail => !detail.IsDeleted)
            .ToList();
        var requestedDetailIds = activeDetails
            .Where(detail => kinds.Contains(detail.ContractType.Kind))
            .Select(detail => detail.Id)
            .Concat(detailIds)
            .ToHashSet();

        return _adjustmentConfigurations
            .Where(configuration => !configuration.IsDeleted)
            .Where(configuration => configuration.Id != excludedConfigurationId)
            .Any(configuration =>
                kinds.Any(configuration.CoversNewDetail) ||
                activeDetails.Any(detail =>
                    requestedDetailIds.Contains(detail.Id) && configuration.Covers(detail)));
    }

    private Result DisableAdjustmentEligibilityForActiveDetails()
    {
        foreach (var contractType in _contractTypes.Where(oo => !oo.IsDeleted))
        {
            foreach (var detail in contractType.ContractTypeDetails.Where(oo => !oo.IsDeleted))
            {
                var result = detail.ChangeAdjustmentEligibility(false);

                if (result.IsFailure)
                    return result;
            }
        }

        return Result.Success();
    }

    private void SynchronizeAdjustmentConfiguration()
    {
        SynchronizeAdjustmentConfigurations();
    }

    private void SynchronizeAdjustmentConfigurations()
    {
        foreach (var type in _contractTypes.Where(x => !x.IsDeleted))
            foreach (var detail in type.ContractTypeDetails.Where(x => !x.IsDeleted))
            {
                var configuration = _adjustmentConfigurations
                    .Where(configuration => !configuration.IsDeleted)
                    .SingleOrDefault(candidate => candidate.Covers(detail));
                if (configuration is not null)
                {
                    detail.ChangeAdjustmentEligibility(true);
                    detail.ConfigureAdjustment(configuration.GetAdjustmentTerms());
                }
                else
                {
                    detail.RemoveAdjustment();
                    detail.ChangeAdjustmentEligibility(false);
                }
            }
    }

    private ContractAdjustmentConfiguration? GetAdjustmentConfigurationForNewDetail(
        ContractTypeKind kind) => _adjustmentConfigurations
        .Where(configuration => !configuration.IsDeleted)
        .SingleOrDefault(configuration => configuration.CoversNewDetail(kind));

    private ContractAdjustmentConfiguration? GetAdjustmentConfigurationForDetail(long detailId)
    {
        var detail = _contractTypes.Where(type => !type.IsDeleted)
            .SelectMany(type => type.ContractTypeDetails)
            .Single(item => !item.IsDeleted && item.Id == detailId);
        return _adjustmentConfigurations
            .Where(configuration => !configuration.IsDeleted)
            .SingleOrDefault(configuration => configuration.Covers(detail));
    }

    private ContractType GetActiveContractType(
        long contractTypeId)
    {
        var contractType = _contractTypes.FirstOrDefault(
            oo =>
                oo.Id == contractTypeId &&
                !oo.IsDeleted);

        if (contractType is null)
        {
            throw new InvalidOperationException(
                $"ContractType with Id {contractTypeId} not found.");
        }

        return contractType;
    }

    private ContractChange GetActiveContractChange(long id)
    {
        return _contractChanges.FirstOrDefault(
                   oo => oo.Id == id && !oo.IsDeleted)
               ?? throw new InvalidOperationException(
                   $"ContractChange with Id {id} not found.");
    }

    private ContractChange? GetLatestActiveContractChange()
    {
        return _contractChanges
            .Where(oo => !oo.IsDeleted)
            .OrderByDescending(oo => oo.Date)
            .ThenByDescending(oo => oo.Id)
            .FirstOrDefault();
    }

    private ContractGuarantee GetActiveGuarantee(
        long guaranteeId)
    {
        return _guarantees.FirstOrDefault(
                   oo =>
                       oo.Id == guaranteeId &&
                       !oo.IsDeleted)
               ?? throw new InvalidOperationException(
                   $"ContractGuarantee with Id {guaranteeId} not found.");
    }

    private void ValidateContractChangeNumber(
        string number,
        long? excludedId = null)
    {
        if (_contractChanges.Any(oo =>
                !oo.IsDeleted &&
                oo.Number == number &&
                (!excludedId.HasValue ||
                 oo.Id != excludedId.Value)))
        {
            throw new InvalidOperationException(
                "ContractChange Number already exists.");
        }
    }

    private void ValidateEffectiveContractState()
    {
        if (CalculateCurrentDuration() <= 0)
        {
            throw new InvalidOperationException(
                "Effective Contract duration must be positive.");
        }

        if (CalculateFinalContractAmount() < 0)
        {
            throw new InvalidOperationException(
                "Final Contract amount cannot be negative.");
        }
    }

    private Result ApplyStatusTransition(
        ContractStatus newStatus,
        ContractStatusTransitionType transitionType,
        DateTime effectiveDate,
        string? reason = null,
        string? description = null,
        int? suspensionDurationMonths = null,
        IEnumerable<string>? urls = null,
        bool createLegalSnapshot = false)
    {
        if (IsRegistrationPending)
            return Result.Failure(
                ContractErrors.ContractRegistrationMustBeFinalizedBeforeStatusChange);

        if (!CanChangeStatusTo(newStatus))
            return Result.Failure(ContractErrors.ContractStatusTransitionInvalid);

        var previousStatus = Status;
        var statusHistory = new ContractStatusHistory(
            this,
            previousStatus,
            newStatus,
            transitionType,
            effectiveDate,
            reason,
            description,
            suspensionDurationMonths,
            urls ?? []);

        ContractLegalSnapshot? legalSnapshot = null;
        if (createLegalSnapshot && LegalSnapshot is null)
        {
            var initialAmount = CalculateInitialAmount();
            legalSnapshot = new ContractLegalSnapshot(
                this,
                newStatus,
                initialAmount,
                initialAmount,
                FinancialInformation is { IsDeleted: false }
                    ? FinancialInformation.CurrencyId
                    : null);
        }

        Status = newStatus;
        _statusHistories.Add(statusHistory);

        if (legalSnapshot is not null)
            LegalSnapshot = legalSnapshot;

        return Result.Success();
    }

    private static Result ValidateLifecycleOperation(
        DateTime effectiveDate,
        string reason)
    {
        if (effectiveDate == default)
            return Result.Failure(ContractErrors.ContractStatusEffectiveDateRequired);
        if (string.IsNullOrWhiteSpace(reason))
            return Result.Failure(ContractErrors.ContractStatusReasonRequired);
        return Result.Success();
    }

    private bool CanChangeStatusTo(ContractStatus newStatus)
    {
        return Status switch
        {
            ContractStatus.Draft =>
                newStatus == ContractStatus.UnderReview,

            ContractStatus.UnderReview =>
                newStatus is ContractStatus.Draft
                    or ContractStatus.Active,

            ContractStatus.Active =>
                newStatus is ContractStatus.Suspended
                    or ContractStatus.Finished
                    or ContractStatus.Terminated,

            ContractStatus.Suspended =>
                newStatus is ContractStatus.Active
                    or ContractStatus.Finished
                    or ContractStatus.Terminated,

            ContractStatus.Finished => false,
            ContractStatus.Terminated => false,

            _ => false
        };
    }

    private bool CanCreateContractChange()
    {
        return Status is ContractStatus.Active or ContractStatus.Suspended;
    }

    #endregion
}
