using Engineering.Domain.Entities.ContractorContracts.Enums;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

/// <summary>
/// شرح عملیات پروژه صورت وضعیت پیمانکار
/// </summary>
public class ContractorStatusStatementServiceThirdParty : AuditableEntity<ContractorStatusStatementServiceThirdParty>
{
    [Description(CSSCmts.ThirdPartyId)]
    public long ThirdPartyId { get; private set; }

    [Description(CSSCmts.SkillId)]
    public long SkillId { get; private set; }

    [Description(CSSCmts.Type)]
    public ContractorContractDetailCooperationBasis Type { get; private set; }

    [Description(CSSCmts.WorkingDay)]
    public DateTime WorkingDay { get; private set; }

    [Description(CSSCmts.Price)]
    public decimal Price { get; private set; }

    [Description(CSSCmts.ContractorStatusStatementService)]
    public long ContractorStatusStatementServiceId { get; private set; }
    public ContractorStatusStatementService ContractorStatusStatementService { get; private set; }

    public ContractorStatusStatementServiceThirdParty(
        ContractorStatusStatementService contractorStatusStatementService,
        long thirdPartyId,
        ContractorContractDetailCooperationBasis type,
        long skillId,
        DateTime workingDay,
        decimal price
        ) : this()
    {
        SetContractorStatusStatementService(contractorStatusStatementService);
        SetThirdPartyId(thirdPartyId);
        SetType(type);
        SetSkillId(skillId);
        SetWorkingDay(workingDay);
        SetPrice(price);
    }

    public static ContractorStatusStatementServiceThirdParty Create(
        ContractorStatusStatementService contractorStatusStatementService,
        long thirdPartyId,
        ContractorContractDetailCooperationBasis type,
        long skillId,
        DateTime workingDay,
        decimal price)
    {
        return new ContractorStatusStatementServiceThirdParty(
            contractorStatusStatementService,
            thirdPartyId,
            type,
            skillId,
            workingDay,
            price);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetContractorStatusStatementService(ContractorStatusStatementService value)
    {
        ContractorStatusStatementService = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetThirdPartyId(long value)
    {
        ThirdPartyId = Guard.Against.Null(value, nameof(value));
    }

    public void SetType(ContractorContractDetailCooperationBasis value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    public void SetSkillId(long value)
    {
        SkillId = Guard.Against.Null(value, nameof(value));
    }

    public void SetWorkingDay(DateTime value)
    {
        WorkingDay = Guard.Against.Null(value, nameof(value));
    }

    public void SetPrice(decimal value)
    {
        Price = Guard.Against.Null(value, nameof(value));
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorStatusStatementServiceThirdParty()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
