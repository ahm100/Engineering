using Engineering.Domain.Entities.DailyProjectOperations;

namespace Engineering.Domain.Entities.ContractorStatusStatements;

[Description(CSSCmts.ContractorStatusStatementServiceDaily)]
public class ContractorStatusStatementServiceDaily : AuditableEntity<ContractorStatusStatementServiceDaily>
{
    [Description(CSSCmts.TimeSpant)]
    public long? TimeSpant { get; private set; } = 0;

    [Description(CSSCmts.Volume)]
    public decimal Volume { get; private set; }

    [Description(CSSCmts.UnitPrice)]
    public decimal UnitPrice { get; private set; }

    [Description(CSSCmts.TotalPrice)]
    public decimal TotalPrice { get; private set; }

    [Description(CSSCmts.AcceptablePercentage)]
    public decimal AcceptablePercentage { get; private set; } = 100;

    [Description(CSSCmts.AcceptableAmount)]
    public decimal AcceptableAmount { get; private set; }

    [Description(CSSCmts.AcceptableDescription)]
    public string? AcceptableDescription { get; private set; }

    [Description(CSSCmts.ProjectManagementApprovalPercentage)]
    public decimal ProjectManagementApprovalPercentage { get; private set; } = 100;

    [Description(CSSCmts.ProjectManagementApprovedPrice)]
    public decimal ProjectManagementApprovedPrice { get; private set; }

    [Description(CSSCmts.ProjectManagementApprovedDescription)]
    public string? ProjectManagementApprovedDescription { get; private set; }

    [Description(CSSCmts.ManagementApprovalPercentage)]
    public decimal ManagementApprovalPercentage { get; private set; } = 100;

    [Description(CSSCmts.ApprovedPrice)]
    public decimal ApprovedPrice { get; private set; }

    [Description(CSSCmts.ApprovedDescription)]
    public string? ApprovedDescription { get; private set; }

    [Description(CSSCmts.ContractorStatusStatementService)]
    public long ContractorStatusStatementServiceId { get; private set; }
    public ContractorStatusStatementService ContractorStatusStatementService { get; private set; }

    [Description(CSSCmts.DailyProjectOperationService)]
    public long DailyProjectOperationServiceId { get; private set; }
    public DailyProjectOperationService DailyProjectOperationService { get; private set; }

    public ContractorStatusStatementServiceDaily(
        ContractorStatusStatementService contractorStatusStatementService,
        DailyProjectOperationService dailyProjectOperationService,
        decimal volume,
        long? timeSpant,
        decimal unitPrice,
        decimal acceptablePercentage,
        string? acceptableDescription
        ) : this()
    {
        SetContractorStatusStatementService(contractorStatusStatementService);
        SetDailyProjectOperationService(dailyProjectOperationService);
        SetVolume(volume);
        SetUnitPrice(unitPrice);
        SetTimeSpant(timeSpant);
        SetTotalPrice(volume * unitPrice);
        SetAcceptablePercentage(acceptablePercentage);
        SetAcceptableDescription(acceptableDescription);
        SetAcceptableAmount(TotalPrice * (acceptablePercentage / 100));
        SetProjectManagementApprovedPrice(AcceptableAmount);
        SetApprovedPrice(ProjectManagementApprovedPrice);
    }

    public static ContractorStatusStatementServiceDaily Create(
        ContractorStatusStatementService contractorStatusStatementService,
        DailyProjectOperationService dailyProjectOperationService,
        decimal volume,
        long? timeSpant,
        decimal unitPrice,
        decimal acceptablePercentage,
        string? acceptableDescription
        )
    {
        return new ContractorStatusStatementServiceDaily(
            contractorStatusStatementService,
            dailyProjectOperationService,
            volume,
            timeSpant,
            unitPrice,
            acceptablePercentage,
            acceptableDescription);
    }

    public void ResetProjectManagement()
    {
        ProjectManagementApprovedPrice = 0;
        ProjectManagementApprovalPercentage = 0;
        ProjectManagementApprovedDescription = null;
    }

    public void ResetManagement()
    {
        ApprovedPrice = 0;
        ManagementApprovalPercentage = 0;
        ApprovedDescription = null;
    }

    public void SetTotalPrice(decimal value)
    {
        TotalPrice = Volume * UnitPrice;
    }

    public void SetContractorStatusStatementService(ContractorStatusStatementService value)
    {
        ContractorStatusStatementService = Guard.Against.Null(value, nameof(value));
        ContractorStatusStatementServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetDailyProjectOperationService(DailyProjectOperationService value)
    {
        DailyProjectOperationService = Guard.Against.Null(value, nameof(value));
        DailyProjectOperationServiceId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetVolume(decimal value)
    {
        Volume = Guard.Against.Null(value, nameof(value));
    }

    public void SetTimeSpant(long? value)
    {
        TimeSpant = value;
    }

    public void SetUnitPrice(decimal value)
    {
        UnitPrice = Guard.Against.Null(value, nameof(value));
    }

    public void SetAcceptablePercentage(decimal value)
    {
        AcceptablePercentage = value;
    }

    public void SetAcceptableAmount()
    {
        AcceptableAmount = TotalPrice * (AcceptablePercentage / 100m);
    }

    public void SetAcceptableAmount(decimal value)
    {
        AcceptableAmount = value;
    }

    public void SetProjectManagementApprovalPercentage(decimal value)
    {
        ProjectManagementApprovalPercentage = value;
    }

    public void SetAcceptableDescription(string? value)
    {
        AcceptableDescription = value;
    }

    public void SetProjectManagementApprovedDescription(string? value)
    {
        ProjectManagementApprovedDescription = value;
    }

    public void SetProjectManagementApprovedPrice()
    {
        ProjectManagementApprovedPrice = AcceptableAmount * (ProjectManagementApprovalPercentage / 100);
    }

    public void SetProjectManagementApprovedPrice(decimal value)
    {
        ProjectManagementApprovedPrice = value;
    }

    public void SetManagementApprovalPercentage(decimal value)
    {
        ManagementApprovalPercentage = value;
    }

    public void SetApprovedDescription(string? value)
    {
        ApprovedDescription = value;
    }

    public void SetApprovedPrice()
    {
        ApprovedPrice = ProjectManagementApprovedPrice * (ManagementApprovalPercentage / 100);
    }

    public void SetApprovedPrice(decimal value)
    {
        ApprovedPrice = value;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private ContractorStatusStatementServiceDaily()
    {

    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
