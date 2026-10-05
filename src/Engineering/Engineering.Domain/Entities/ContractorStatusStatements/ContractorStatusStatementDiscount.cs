using Engineering.Domain.Entities.RequestRewards;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

[Description(CSSCmts.ContractorStatusStatementDiscount)]
public class ContractorStatusStatementDiscount : AuditableEntity<ContractorStatusStatementDiscount>
{
    [Description(CSSCmts.DiscountPrice)]
    public decimal DiscountPrice { get; private set; }

    [Description(CSSCmts.RegistrationDate)]
    public DateTime? RegistrationDate { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(GlobalCmts.ContractorStatusStatement)]
    public ContractorStatusStatement ContractorStatusStatement { get; private set; }
    public long ContractorStatusStatementId { get; private set; }
    [Description(GlobalCmts.RequestReward)]
    public RequestReward? RequestReward { get; private set; }
    public long? RequestRewardId { get; private set; }

    public ContractorStatusStatementDiscount(
        ContractorStatusStatement contractorStatusStatement,
        RequestReward? requestReward,
        DateTime? registrationDate,
        decimal discountPrice,
        string? description
        ) : this()
    {
        SetCSS(contractorStatusStatement);
        SetRequestReward(requestReward);
        SetRegistrationDate(registrationDate);
        SetDescription(description);
        SetDiscountPrice(discountPrice);
    }

    public static ContractorStatusStatementDiscount Create(
        ContractorStatusStatement contractorStatusStatement,
        RequestReward? requestReward,
        DateTime? registrationDate,
        decimal discountPrice,
        string? description)
    {
        return new ContractorStatusStatementDiscount(
            contractorStatusStatement,
            requestReward,
            registrationDate,
            discountPrice,
            description);
    }

    public void SetDiscountPrice(decimal value)
    {
        DiscountPrice = Guard.Against.Null(value, nameof(value));
    }

    public void SetCSS(ContractorStatusStatement value)
    {
        ContractorStatusStatement = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetRequestReward(RequestReward? value)
    {
        RequestReward = value;
        RequestRewardId = value?.Id;
    }

    public void SetRegistrationDate(DateTime? value)
    {
        RegistrationDate = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorStatusStatementDiscount()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
