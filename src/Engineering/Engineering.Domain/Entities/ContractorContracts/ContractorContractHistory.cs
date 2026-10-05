
namespace Engineering.Domain.Entities.ContractorContracts;

[Description(CCCmts.ContractorContractHistory)]
public class ContractorContractHistory : AuditableEntity<ContractorContractHistory, long>
{
    #region Properties
    [Description(CCCmts.WorkDonePercent)]
    public int? WorkDonePercent { get; private set; }

    [Description(CCCmts.WorkDeliveryPercent)]
    public int? WorkDeliveryPercent { get; private set; }

    [Description(CCCmts.WorkCompletionPercent)]
    public int? WorkCompletionPercent { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(CCCmts.DailyBaseHours)]
    public decimal? DailyBaseHours { get; private set; }

    [Description(CCCmts.MonthlyBaseHours)]
    public decimal? MonthlyBaseHours { get; private set; }

    [Description(GlobalCmts.ContractorContract)]
    public ContractorContract ContractorContract { get; private set; } = default!;

    [Description(GlobalCmts.ContractorContractId)]
    public long ContractorContractId { get; private set; }

    #endregion

    #region Constructors

    public ContractorContractHistory() { }

    public ContractorContractHistory(
        int? workDonePercent,
        int? workDeliveryPercent,
        int? workCompletionPercent,
        string? description,
        ContractorContract contractorContract) : this()
    {
        SetWorkDonePercent(workDonePercent);
        SetWorkDeliveryPercent(workDeliveryPercent);
        SetDescription(description);
        SetContractorContract(contractorContract);
        SetWorkCompletionPercent(workCompletionPercent);
        SetDailyBaseHours(contractorContract.DailyBaseHours);
        SetMonthlyBaseHours(contractorContract.MonthlyBaseHours);
    }

    #endregion

    #region Commands

    public static ContractorContractHistory Create(int? workDonePercent, int? workDeliveryPercent, int? workCompletionPercent, string? description, ContractorContract contractorContract)
    {
        return new ContractorContractHistory(workDonePercent, workDeliveryPercent, workCompletionPercent, description, contractorContract);
    }

    public void SetWorkDonePercent(int? value)
    {
        WorkDonePercent = value;
    }

    public void SetWorkDeliveryPercent(int? value)
    {
        WorkDeliveryPercent = value;
    }

    public void SetWorkCompletionPercent(int? value)
    {
        WorkCompletionPercent = value;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetDailyBaseHours(decimal? value)
    {
        DailyBaseHours = value;
    }

    public void SetMonthlyBaseHours(decimal? value)
    {
        MonthlyBaseHours = value;
    }

    public void SetContractorContract(ContractorContract value)
    {
        ContractorContract = Guard.Against.Null(value, nameof(value));
        ContractorContractId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    #endregion
}
