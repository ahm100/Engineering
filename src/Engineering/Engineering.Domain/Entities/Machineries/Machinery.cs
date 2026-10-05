using Engineering.Domain.Entities.ContractorMachineries;
using Engineering.Domain.Entities.FixAssetMachineries;
using Engineering.Domain.Entities.ProjectOperationDetails.ConsumableVolumes;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;

namespace Engineering.Domain.Entities.Machineries;

[Description(GlobalCmts.Machineries)]
public class Machinery : ActivateEntity<Machinery, long>
{
    [Description(MachineriesCmts.MachineryName)]
    public string MachineryName { get; private set; } = string.Empty;
    [Description(MachineriesCmts.MachineryCode)]
    public string MachineryCode { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.MachineriesGroup)]
    public long GroupId { get; set; }
    public MachineriesGroup MachineriesGroup { get; set; }

    public Machinery(MachineriesGroup machineriesGroup,
        string machineryName,
        string machineryCode,
        bool isActive,
        long? companyId) : this()
    {
        SetMachineriesGroup(machineriesGroup);
        SetName(machineryName);
        SetCode(machineryCode);
        SetCompanyId(companyId);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
    }

    #region Set data

    public void SetMachineriesGroup(MachineriesGroup value)
    {
        MachineriesGroup = Guard.Against.Null(value, nameof(value));
        GroupId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetName(string value)
    {
        MachineryName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        MachineryCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(MachineriesCmts.ConsumableVolumeMachinery)]
    private List<ConsumableVolumeMachinery> _consumableVolumeMachineries;
    public IReadOnlyList<ConsumableVolumeMachinery> ConsumableVolumeMachineries => _consumableVolumeMachineries;
    [Description(MachineriesCmts.ConsumptionStandardMachinery)]
    private List<ConsumptionStandardMachinery> _consumptionStandardMachineries;
    public IReadOnlyList<ConsumptionStandardMachinery> ConsumptionStandardMachineries => _consumptionStandardMachineries;
    [Description(MachineriesCmts.RequestMachinery)]
    private List<RequestMachinery> _requestMachineries;
    public IReadOnlyList<RequestMachinery> RequestMachineries => _requestMachineries;
    [Description(MachineriesCmts.FixAssetMachinery)]
    private List<FixAssetMachinery> _fixAssetMachineries;
    public IReadOnlyList<FixAssetMachinery> FixAssetMachineries => _fixAssetMachineries;
    [Description(MachineriesCmts.ContractorMachinery)]
    private List<ContractorMachinery> _contractorMachineries;
    public IReadOnlyList<ContractorMachinery> ContractorMachineries => _contractorMachineries;
    [Description(MachineriesCmts.RequestMachineryStatusStatementDetail)]
    private List<RequestMachineryStatusStatementDetail> _requestMachineryStatusStatementDetails;
    public IReadOnlyList<RequestMachineryStatusStatementDetail> RequestMachineryStatusStatementDetails => _requestMachineryStatusStatementDetails;
    private Machinery()
    {
        _consumableVolumeMachineries = [];
        _consumptionStandardMachineries = [];
        _requestMachineries = [];
        _fixAssetMachineries = [];
        _contractorMachineries = [];
        _requestMachineryStatusStatementDetails = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
