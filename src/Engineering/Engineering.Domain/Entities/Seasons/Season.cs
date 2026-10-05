using Engineering.Domain.Entities.ContractorStatusStatements;
using Engineering.Domain.Entities.RequestMachineryStatusStatements;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Domain.Entities.Seasons;

[Description(GlobalCmts.Season)]
public class Season : ActivateEntity<Season, long>
{
    [Description(SeasonCmts.SeasonName)]
    public string SeasonName { get; private set; } = string.Empty;
    [Description(SeasonCmts.SeasonCode)]
    public string SeasonCode { get; private set; } = string.Empty;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }
    [Description(GlobalCmts.PreferentialReferenceCode)]
    public Guid PreferentialReferenceCode { get; private set; }

    [Description(GlobalCmts.Branch)]
    public long BranchId { get; private set; }
    public Branch Branch { get; set; }

    public Season(Branch branch,
        string seasonName,
        string seasonCode,
        bool isActive,
        long? companyId) : this()
    {
        SetBranch(branch);
        SetName(seasonName);
        SetCode(seasonCode);
        SetCompanyId(companyId);
        if (isActive)
            SetActive();
        else
            SetDeactivate();
        SetPreferentialReferenceCode(Guid.NewGuid());
    }

    #region Set data 

    public void SetBranch(Branch value)
    {
        Branch = Guard.Against.Null(value, nameof(value));
        BranchId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetName(string value)
    {
        SeasonName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetCode(string value)
    {
        SeasonCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetPreferentialReferenceCode(Guid value)
    {
        PreferentialReferenceCode = value;
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }


    #endregion

    #region Methods 

    #endregion

    public string GetPreferentialName()
    {
        if (SeasonName.Contains(Branch.BranchName))
            return SeasonName;

        return $"{SeasonName}-{Branch.BranchName}-{Branch.Category.CategoryName}";
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(GlobalCmts.OperationInfoSeason)]
    private List<OperationInfoSeason> _operationInfoSeason;
    public IReadOnlyList<OperationInfoSeason> OperationInfoSeasons => _operationInfoSeason;

    [Description(GlobalCmts.ContractorStatusStatement)]
    private List<ContractorStatusStatement> _contractorStatusStatements;
    public IReadOnlyList<ContractorStatusStatement> ContractorStatusStatements => _contractorStatusStatements;

    [Description(SeasonCmts.RequestMachineryStatusStatement)]
    private List<RequestMachineryStatusStatement> _requestMachineryStatusStatements;
    public IReadOnlyList<RequestMachineryStatusStatement> RequestMachineryStatusStatements => _requestMachineryStatusStatements;

    [Description(GlobalCmts.TransportationRequest)]
    private List<TransportationRequest> _transportationRequests;
    public IReadOnlyList<TransportationRequest> TransportationRequests => _transportationRequests;
    public Season()
    {
        _operationInfoSeason = [];
        _contractorStatusStatements = [];
        _requestMachineryStatusStatements = [];
        _transportationRequests = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
