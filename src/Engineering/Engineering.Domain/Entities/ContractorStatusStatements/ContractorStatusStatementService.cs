using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.ContractorContracts.Enums;
using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

/// <summary>
/// شرح عملیات پروژه صورت وضعیت پیمانکار
/// </summary>
public class ContractorStatusStatementService : AuditableEntity<ContractorStatusStatementService>
{


    [Description(CSSCmts.ThirdPartiesAmount)]
    public decimal? ThirdPartiesAmount { get; private set; } = 0;

    [Description(CSSCmts.ProjectManagerApprovalAmount)]
    public decimal? ProjectManagerApprovalAmount { get; private set; } = 0;

    [Description(CSSCmts.ManagementApprovalAmount)]
    public decimal? ManagementApprovalAmount { get; private set; } = 0;

    [Description(CSSCmts.ContractorStatusStatementDetail)]
    public ContractorStatusStatementDetail ContractorStatusStatementDetail { get; private set; }
    public long ContractorStatusStatementDetailId { get; private set; }

    [Description(CSSCmts.ContractorContractDetail)]
    public long? ContractorContractDetailId { get; private set; }
    public ContractorContractDetail? ContractorContractDetail { get; private set; }

    [Description(CSSCmts.DailyProjectOperation)]
    public long? DailyProjectOperationId { get; private set; }
    public DailyProjectOperation? DailyProjectOperation { get; private set; }

    public ContractorStatusStatementService(
        ContractorStatusStatementDetail contractorStatusStatementDetail,
        ContractorContractDetail? contractorContractDetail,
        DailyProjectOperation? dailyProjectOperation,
        decimal? thirdPartiesAmount
        ) : this()
    {
        SetContractorStatusStatementDetail(contractorStatusStatementDetail);
        SetContractorContractDetail(contractorContractDetail);
        SetDailyProjectOperation(dailyProjectOperation);
        SetThirdPartiesAmount(thirdPartiesAmount);
    }

    public void SetThirdPartiesAmount(decimal? value)
    {
        ThirdPartiesAmount = value ?? 0;
    }

    public void SetContractorStatusStatementDetail(ContractorStatusStatementDetail? value)
    {
        ContractorStatusStatementDetail = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementDetailId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetContractorContractDetail(ContractorContractDetail? value)
    {
        ContractorContractDetail = value;
        ContractorContractDetailId = value?.Id;
    }

    public void SetDailyProjectOperation(DailyProjectOperation? value)
    {
        DailyProjectOperation = value;
        DailyProjectOperationId = value?.Id;
    }

    public void SetProjectManagerApprovalAmount()
    {
        ProjectManagerApprovalAmount = _contractorStatusStatementServiceDailies.Sum(x => x.ProjectManagementApprovedPrice);
    }

    public void SetManagementApprovalAmount()
    {
        ManagementApprovalAmount = _contractorStatusStatementServiceDailies.Sum(x => x.ApprovedPrice);
    }

    public void ResetProjectManagerApprovalAmount()
    {
        ProjectManagerApprovalAmount = ThirdPartiesAmount;
    }

    public void ResettManagementApprovalAmount()
    {
        ManagementApprovalAmount = ThirdPartiesAmount;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void AddContractorStatusStatementServiceThirdParties(
        long thirdPartyId,
        ContractorContractDetailCooperationBasis type,
        long skillId,
        DateTime workingDay,
        decimal price)
    {
        _contractorStatusStatementServiceThirdParties.Add(ContractorStatusStatementServiceThirdParty.Create(
            this,
            thirdPartyId,
            type,
            skillId,
            workingDay,
            price));
    }

    public void AddContractorStatusStatementServiceDailies(
        DailyProjectOperationService dailyProjectOperationService,
        decimal volume,
        long? timeSpant,
        decimal unitPrice,
        decimal acceptablePercentage,
        string? acceptableDescription)
    {
        _contractorStatusStatementServiceDailies.Add(ContractorStatusStatementServiceDaily.Create(
            this,
            dailyProjectOperationService,
            volume,
            timeSpant,
            unitPrice,
            acceptablePercentage,
            acceptableDescription));
    }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<ContractorStatusStatementServiceThirdParty> _contractorStatusStatementServiceThirdParties;
    public IReadOnlyList<ContractorStatusStatementServiceThirdParty> ContractorStatusStatementServiceThirdParties => _contractorStatusStatementServiceThirdParties;
    private List<ContractorStatusStatementServiceDaily> _contractorStatusStatementServiceDailies;
    public IReadOnlyList<ContractorStatusStatementServiceDaily> ContractorStatusStatementServiceDailies => _contractorStatusStatementServiceDailies;
    private ContractorStatusStatementService()
    {
        _contractorStatusStatementServiceThirdParties = [];
        _contractorStatusStatementServiceDailies = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
