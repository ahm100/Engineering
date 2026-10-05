using Engineering.Domain.Entities.Contracts.Enums;
using Engineering.Domain.Errors;
using Gita.Backend.Shared.Domain.Base.Results;

namespace Engineering.Domain.Entities.Contracts;

[Description(GlobalCmts.ContractType)]
public class ContractType : AuditableEntity<ContractType, long>
{
    [Description(GlobalCmts.ContractTypeKind)]
    public ContractTypeKind Kind { get; private set; }

    [Description(GlobalCmts.PricingMethod)]
    public PricingMethod PricingMethod { get; private set; }

    [Description(GlobalCmts.ContractId)]
    public long ContractId { get; private set; }

    public Contract Contract { get; private set; } = null!;

    [Description(ContractCmts.ContractTypeDetail)]
    private readonly List<ContractTypeDetail> _contractTypeDetails = [];

    public IReadOnlyList<ContractTypeDetail> ContractTypeDetails => _contractTypeDetails;

    #region Constructors

    private ContractType()
    {
    }

    public ContractType(
    long contractId,
    ContractTypeKind kind,
    PricingMethod pricingMethod)
    {
        SetContractId(contractId);
        SetKind(kind);
        SetPricingMethod(pricingMethod);
    }

    public ContractType(
        Contract contract,
        ContractTypeKind kind,
        PricingMethod pricingMethod)
    {
        Contract = Guard.Against.Null(contract, nameof(contract));
        ContractId = contract.Id;
        SetKind(kind);
        SetPricingMethod(pricingMethod);
    }

    #endregion

    #region Commands

    public void Update(
    ContractTypeKind kind,
    PricingMethod pricingMethod)
    {
        var hasActiveDetails = _contractTypeDetails.Any(oo => !oo.IsDeleted);

        if (hasActiveDetails && Kind != kind)
            throw new InvalidOperationException(
                "ContractType Kind cannot be changed while active details exist.");

        if (hasActiveDetails && PricingMethod != pricingMethod)
            throw new InvalidOperationException(
                "ContractType PricingMethod cannot be changed while active details exist.");

        SetKind(kind);
        SetPricingMethod(pricingMethod);
    }

    public ContractTypeDetail AddContractTypeDetail(
    long sourceId,
    decimal quantity,
    long? unitOfMeasurementId,
    decimal? unitPrice,
    decimal? fixedAmount,
    string? technicalSpecifications,
    string? expectedDeliverables,
    decimal? duration,
    ContractDurationUnit? durationUnit,
    bool isSubjectToAdjustment,
    ContractTypeDetailAdjustmentTerms? adjustmentTerms)
    {
        if (HasActiveSource(sourceId))
            throw new InvalidOperationException(
                "The selected source already exists in this ContractType.");

        var detail = new ContractTypeDetail(
            this,
            sourceId,
            quantity,
            unitOfMeasurementId,
            unitPrice,
            fixedAmount,
            technicalSpecifications,
            expectedDeliverables,
            duration,
            durationUnit,
            isSubjectToAdjustment);

        if (adjustmentTerms is not null)
            detail.ConfigureAdjustment(adjustmentTerms);

        _contractTypeDetails.Add(detail);

        return detail;
    }

    public Result UpdateContractTypeDetail(
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
        var detail = GetActiveContractTypeDetail(contractTypeDetailId);
        var effectiveAdjustmentEligibility =
            isSubjectToAdjustment ?? detail.IsSubjectToAdjustment;

        if (!effectiveAdjustmentEligibility && adjustmentTerms is not null)
        {
            return Result.Failure(
                ContractErrors.ContractTypeDetailAdjustmentEligibilityInvalid);
        }

        detail.Update(
            quantity,
            unitOfMeasurementId,
            unitPrice,
            fixedAmount,
            technicalSpecifications,
            expectedDeliverables,
            duration,
            durationUnit);

        if (!effectiveAdjustmentEligibility)
        {
            detail.RemoveAdjustment();
            var eligibilityResult = detail.ChangeAdjustmentEligibility(false);

            if (eligibilityResult.IsFailure)
                return eligibilityResult;
        }
        else
        {
            var eligibilityResult = detail.ChangeAdjustmentEligibility(true);

            if (eligibilityResult.IsFailure)
                return eligibilityResult;

            if (adjustmentTerms is null)
                detail.RemoveAdjustment();
            else
                detail.ConfigureAdjustment(adjustmentTerms);
        }

        return Result.Success();
    }

    public Result EnsureTypeDetailAdjustmentEligibilityIsValid(
        long contractTypeDetailId,
        bool? requestedEligibility,
        bool hasAdjustment)
    {
        var detail = GetActiveContractTypeDetail(contractTypeDetailId);
        var effectiveEligibility =
            requestedEligibility ?? detail.IsSubjectToAdjustment;

        return !effectiveEligibility && hasAdjustment
            ? Result.Failure(
                ContractErrors.ContractTypeDetailAdjustmentEligibilityInvalid)
            : Result.Success();
    }

    public void RemoveContractTypeDetail(long contractTypeDetailId)
    {
        var detail = GetActiveContractTypeDetail(contractTypeDetailId);
        detail.Delete();
    }

    public void Delete()
    {
        foreach (var detail in _contractTypeDetails.Where(oo => !oo.IsDeleted))
            detail.Delete();

        SoftDelete();
    }

    #endregion

    #region Private Helpers

    private void SetContractId(long value)
        => ContractId = Guard.Against.NegativeOrZero(value, nameof(value));

    private void SetKind(ContractTypeKind value)
        => Kind = Guard.Against.EnumOutOfRange(value, nameof(value));

    private void SetPricingMethod(PricingMethod value)
        => PricingMethod = Guard.Against.EnumOutOfRange(value, nameof(value));

    private ContractTypeDetail GetActiveContractTypeDetail(long contractTypeDetailId)
    {
        var detail = _contractTypeDetails.FirstOrDefault(
            oo => oo.Id == contractTypeDetailId && !oo.IsDeleted);

        if (detail is null)
            throw new InvalidOperationException(
                $"ContractTypeDetail with Id {contractTypeDetailId} not found.");

        return detail;
    }

    private bool HasActiveSource(long sourceId)
    {
        sourceId = Guard.Against.NegativeOrZero(sourceId, nameof(sourceId));

        return Kind switch
        {
            ContractTypeKind.Procurement =>
                _contractTypeDetails.Any(oo =>
                    !oo.IsDeleted &&
                    oo.ConsumableVolumeProductId == sourceId),

            ContractTypeKind.Construction =>
                _contractTypeDetails.Any(oo =>
                    !oo.IsDeleted &&
                    oo.ProjectOperationDetailId == sourceId),

            ContractTypeKind.Engineering or ContractTypeKind.Services =>
                _contractTypeDetails.Any(oo =>
                    !oo.IsDeleted &&
                    oo.ProjectOperationDetailContractorServiceId == sourceId),

            _ => false
        };
    }

    #endregion
}
