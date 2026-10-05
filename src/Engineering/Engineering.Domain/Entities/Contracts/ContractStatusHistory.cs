using Engineering.Domain.Entities.Contracts.Enums;

namespace Engineering.Domain.Entities.Contracts;

[Description(ContractCmts.ContractStatusHistory)]
public class ContractStatusHistory : AuditableEntity<ContractStatusHistory, long>
{
    private readonly List<ContractStatusHistoryDocument> _documents = [];

    public long ContractId { get; private set; }
    public Contract Contract { get; private set; } = null!;

    [Description(ContractCmts.FromStatus)]
    public ContractStatus FromStatus { get; private set; }

    [Description(ContractCmts.ToStatus)]
    public ContractStatus ToStatus { get; private set; }

    [Description(ContractCmts.TransitionType)]
    public ContractStatusTransitionType TransitionType { get; private set; }

    [Description(ContractCmts.EffectiveDate)]
    public DateTime EffectiveDate { get; private set; }

    [Description(ContractCmts.Reason)]
    public string? Reason { get; private set; }

    [Description(ContractCmts.Description)]
    public string? Description { get; private set; }

    [Description(ContractCmts.SuspensionDurationMonths)]
    public int? SuspensionDurationMonths { get; private set; }

    [Description(ContractCmts.ContractStatusHistoryDocument)]
    public IReadOnlyList<ContractStatusHistoryDocument> Documents => _documents;

    private ContractStatusHistory()
    {
    }

    public ContractStatusHistory(
        Contract contract,
        ContractStatus fromStatus,
        ContractStatus toStatus,
        ContractStatusTransitionType transitionType,
        DateTime effectiveDate,
        string? reason = null,
        string? description = null,
        int? suspensionDurationMonths = null,
        IEnumerable<string>? urls = null)
    {
        SetContract(contract);
        SetFromStatus(fromStatus);
        SetToStatus(toStatus);
        SetTransitionType(transitionType);
        SetEffectiveDate(effectiveDate);
        SetReason(reason);
        SetDescription(description);
        SetSuspensionDurationMonths(suspensionDurationMonths);
        AddDocuments(urls ?? []);
        Validate();
    }

    private void SetContract(Contract value)
    {
        Contract = Guard.Against.Null(value, nameof(value));
        ContractId = value.Id;
    }

    private void SetFromStatus(ContractStatus value)
        => FromStatus = Guard.Against.EnumOutOfRange(value, nameof(value));

    private void SetToStatus(ContractStatus value)
        => ToStatus = Guard.Against.EnumOutOfRange(value, nameof(value));

    private void SetTransitionType(ContractStatusTransitionType value)
        => TransitionType = Guard.Against.EnumOutOfRange(value, nameof(value));

    private void SetEffectiveDate(DateTime value)
        => EffectiveDate = Guard.Against.Null(value, nameof(value));

    private void SetReason(string? value)
        => Reason = string.IsNullOrWhiteSpace(value) ? null : value;

    private void SetDescription(string? value)
        => Description = string.IsNullOrWhiteSpace(value) ? null : value;

    private void SetSuspensionDurationMonths(int? value)
        => SuspensionDurationMonths = value;

    private void AddDocuments(IEnumerable<string> urls)
    {
        foreach (var url in urls.Where(oo => !string.IsNullOrWhiteSpace(oo)))
            _documents.Add(new ContractStatusHistoryDocument(this, url));
    }

    private void Validate()
    {
        if (FromStatus == ToStatus)
            throw new InvalidOperationException(
                "Contract status history must represent an actual transition.");

        if (TransitionType == ContractStatusTransitionType.Suspend &&
            (!SuspensionDurationMonths.HasValue ||
             SuspensionDurationMonths.Value <= 0))
            throw new InvalidOperationException(
                "Suspension duration must be greater than zero.");

        if (TransitionType is (ContractStatusTransitionType.Suspend
                or ContractStatusTransitionType.Finish
                or ContractStatusTransitionType.Terminate))
        {
            if (EffectiveDate == default)
                throw new InvalidOperationException(
                    "Effective date is required for this contract status transition.");

            if (string.IsNullOrWhiteSpace(Reason))
                throw new InvalidOperationException(
                    "Reason is required for this contract status transition.");
        }
    }
}
