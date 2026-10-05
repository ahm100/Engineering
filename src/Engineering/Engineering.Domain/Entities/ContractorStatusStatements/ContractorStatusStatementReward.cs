using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

/// <summary>
/// شرح عملیات پروژه صورت وضعیت پیمانکار
/// </summary>
public class ContractorStatusStatementReward : AuditableEntity<ContractorStatusStatementReward>
{
    [Description(CSSCmts.ConfirmedPrice)]
    public decimal ConfirmedPrice { get; private set; }

    [Description(CSSCmts.RegistrationDate)]
    public DateTime RegistrationDate { get; private set; }

    [Description(GlobalCmts.RequestReward)]
    public long RequestRewardId { get; private set; }
    public RequestReward RequestReward { get; private set; }

    [Description(CSSCmts.ContractorStatusStatement)]
    public long ContractorStatusStatementId { get; private set; }
    public ContractorStatusStatement ContractorStatusStatement { get; private set; }

    public ContractorStatusStatementReward(
        ContractorStatusStatement contractorStatusStatement,
        RequestReward requestReward,
        DateTime registrationDate,
        decimal confirmedPrice
    ) : this()
    {
        SetContractorStatusStatement(contractorStatusStatement);
        SetRequestReward(requestReward);
        SetRegistrationDate(registrationDate);
        SetConfirmedPrice(confirmedPrice);
    }

    public static ContractorStatusStatementReward Create(
        ContractorStatusStatement contractorStatusStatement,
        RequestReward requestReward,
        DateTime registrationDate,
        decimal confirmedPrice)
    {
        return new ContractorStatusStatementReward(
            contractorStatusStatement,
            requestReward,
            registrationDate,
            confirmedPrice);
    }

    public void SetContractorStatusStatement(ContractorStatusStatement value)
    {
        ContractorStatusStatement = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetRequestReward(RequestReward value)
    {
        RequestReward = Guard.Against.Null(value, nameof(value));
        RequestRewardId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetConfirmedPrice(decimal value)
    {
        ConfirmedPrice = Guard.Against.Null(value, nameof(value));
    }

    public void SetRegistrationDate(DateTime value)
    {
        RegistrationDate = Guard.Against.Null(value, nameof(value));
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorStatusStatementReward()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
