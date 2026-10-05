using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.OperationLocations;
using Engineering.Domain.Entities.ProjectOperations;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.Projects.Junctions;
using Engineering.Domain.Entities.Projects.ProjectCostCenterRequests;
using Engineering.Domain.Entities.RequestGoodsSupplies;
using Engineering.Domain.Entities.RequestRewards;
using Engineering.Domain.Entities.TelegramChats;
using Engineering.Domain.Entities.Transportations;

namespace Engineering.Domain.Entities.CostCenters;

[Description(GlobalCmts.CostCenter)]
public class CostCenter : ActivateEntity<CostCenter, long>
{
    [Description(CCenterCmts.CostCenterCode)]
    public string CostCenterCode { get; private set; } = string.Empty;
    [Description(CCenterCmts.CostCenterName)]
    public string CostCenterName { get; private set; } = string.Empty;
    [Description(CCenterCmts.CostCenterName)]
    public string? CostCenterEnName { get; private set; } = string.Empty;
    [Description(CCenterCmts.NoOperationDays)]
    public int? NoOperationDays { get; private set; } = 0;
    [Description(GlobalCmts.CityId)]
    public long CityId { get; private set; }
    [Description(CCenterCmts.Address)]
    public string Address { get; private set; }
    [Description(CCenterCmts.PostalCode)]
    public string? PostalCode { get; private set; }
    [Description(CCenterCmts.Latitude)]
    public decimal Latitude { get; private set; } = decimal.Zero;
    [Description(CCenterCmts.Longitude)]
    public decimal Longitude { get; private set; } = decimal.Zero;
    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; } = string.Empty;
    [Description(GlobalCmts.Description)]
    public string? DescriptionEn { get; private set; } = string.Empty;
    [Description(CCenterCmts.WeatherState)]
    public bool? WeatherState { get; private set; } = false;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }
    [Description(CCenterCmts.PreferentialReferenceCode)]
    public Guid PreferentialReferenceCode { get; private set; }

    [Description(CCenterCmts.CostCenterType)]
    public long CostCenterTypesId { get; private set; }
    public CostCenterType CostCenterType { get; set; }

    [Description(CCenterCmts.IsDefault)]
    public bool IsDefault { get; private set; } = false;

    public CostCenter(
        CostCenterType costCenterType,
        string costCenterName,
        string? costCenterEnName,
        string costCenterCode,
        int? noOperationDays,
        long cityId,
        string address,
        string? postalCode,
        decimal? latitude,
        decimal? longitude,
        string? description,
        string? descriptionEn,
        bool? weatherState,
        bool isActive,
        bool isDefault,
        long? companyId,
        ProjectCostCenterRequest? pCR) : this()
    {
        SetName(costCenterName);
        SetEnName(costCenterEnName);
        SetCode(costCenterCode);
        SetNoOperationDays(noOperationDays);
        SetCostCenterType(costCenterType);
        SetCityId(cityId);
        SetAddress(address);
        SetPostalCode(postalCode);
        SetCompanyId(companyId);
        SetLatitude(latitude);
        SetLongitude(longitude);
        SetDescription(description);
        SetDescriptionEn(descriptionEn);
        SetWeatherState(weatherState);
        SetPreferentialReferenceCode(Guid.NewGuid());
        SetIsDefault(isDefault);
        if (isActive)
            SetActive();
        else
            SetDeactivate();

        if (pCR is not null)
        {
            _projectCostCenter.Add(new(pCR.Project, this, true));
            pCR.Complete(this);
        }

    }

    #region Set Date

    public void SetCostCenterType(CostCenterType value)
    {
        CostCenterType = Guard.Against.Null(value, nameof(value));
        CostCenterTypesId = Guard.Against.Null(value.Id, nameof(value.Id));
    }
    public void SetPreferentialReferenceCode(Guid value)
    {
        PreferentialReferenceCode = Guard.Against.Null(value, nameof(value));
    }
    public void SetIsDefault(bool value)
    {
        IsDefault = Guard.Against.Null(value, nameof(value));
    }
    public void SetName(string value)
    {
        CostCenterName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetDescriptionEn(string? value)
    {
        DescriptionEn = value;
    }
    public void SetEnName(string? value)
    {
        CostCenterEnName = value;
    }
    public void SetCode(string value)
    {
        CostCenterCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetNoOperationDays(int? value)
    {
        NoOperationDays = value ?? 0;
    }
    public void SetCityId(long value)
    {
        CityId = value;
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    public void SetAddress(string value)
    {
        Address = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    public void SetPostalCode(string? value)
    {
        PostalCode = value;
    }
    public void SetLatitude(decimal? value)
    {
        Latitude = value ?? 0;
    }
    public void SetLongitude(decimal? value)
    {
        Longitude = value ?? 0;
    }
    public void SetDescription(string? value)
    {
        Description = value;
    }
    public void SetWeatherState(bool? value)
    {
        WeatherState = value;
    }

    public void AddHistory()
    {
        _costCenterHistories.Add(new CostCenterHistory(
            this));
    }
    #endregion

    #region Methods 

    public void AddInformedUser(CostCenterInformedUser newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_informedUsers.Any(oo => oo.EmployeeId == newData.EmployeeId && oo.Created == newData.Created))
            return;

        _informedUsers.Add(newData);
    }

    public void AddCostCenterAuthorizedRole(CostCenterAuthorizedRole newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_costCenterAuthorizedRoles.Any(oo => oo.AuthorizedRoleId == newData.AuthorizedRoleId && oo.Created == newData.Created))
            return;

        _costCenterAuthorizedRoles.Add(newData);
    }

    public void AddCostCenterAuthorizedUser(CostCenterAuthorizedUser newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_costCenterAuthorizedUsers.Any(oo => oo.AuthorizedUserId == newData.AuthorizedUserId && oo.Created == newData.Created))
            return;

        _costCenterAuthorizedUsers.Add(newData);
    }

    public void AddProject(Project newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_projects.Any(oo => oo.ProjectName == newData.ProjectName && oo.Created == newData.Created))
            return;

        _projects.Add(newData);
    }

    public void AddCostCenterWarehouse(CostCenterWarehouse newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_costCenterWarehouses.Any(oo => oo.WarehouseId == newData.WarehouseId && oo.Created == newData.Created))
            return;

        _costCenterWarehouses.Add(newData);
    }

    #endregion

    public string GetPreferentialName() => CostCenterName;

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    [Description(CCenterCmts.EmployerContractHeads)]
    private readonly List<EmployerContractHead> _employerContractHeads;
    public IReadOnlyList<EmployerContractHead> EmployerContractHeads => _employerContractHeads;

    [Description(CCenterCmts.CostCenterInformedUser)]
    private readonly List<CostCenterInformedUser> _informedUsers;
    public IReadOnlyList<CostCenterInformedUser> InformedUsers => _informedUsers;

    [Description(CCenterCmts.CostCenterAuthorizedRole)]
    private readonly List<CostCenterAuthorizedRole> _costCenterAuthorizedRoles;
    public IReadOnlyList<CostCenterAuthorizedRole> CostCenterAuthorizedRoles => _costCenterAuthorizedRoles;

    [Description(CCenterCmts.CostCenterAuthorizedUser)]
    private readonly List<CostCenterAuthorizedUser> _costCenterAuthorizedUsers;
    public IReadOnlyList<CostCenterAuthorizedUser> CostCenterAuthorizedUsers => _costCenterAuthorizedUsers;

    [Description(GlobalCmts.Project)]
    private readonly List<Project> _projects;
    public IReadOnlyList<Project> Projects => _projects;

    [Description(CCenterCmts.OperationLocations)]
    private readonly List<OperationLocation> _operationLocations;
    public IReadOnlyList<OperationLocation> OperationLocations => _operationLocations;

    [Description(CCenterCmts.CostCenterWarehouses)]
    private readonly List<CostCenterWarehouse> _costCenterWarehouses;
    public IReadOnlyList<CostCenterWarehouse> CostCenterWarehouses => _costCenterWarehouses;

    [Description(CCenterCmts.CostCenterVirtualGroup)]
    private readonly List<CostCenterVirtualGroup> _groups;
    public IReadOnlyList<CostCenterVirtualGroup> Groups => _groups;

    [Description(CCenterCmts.RequestReward)]
    private readonly List<RequestReward> _requestRewards;
    public IReadOnlyList<RequestReward> RequestRewards => _requestRewards;

    [Description(CCenterCmts.TelegramChat)]
    private readonly List<TelegramChat> _telegramChats;
    public IReadOnlyList<TelegramChat> TelegramChats => _telegramChats;

    [Description(CCenterCmts.TransportationRequest)]
    private readonly List<TransportationRequest> _transportationRequest;
    public IReadOnlyList<TransportationRequest> TransportationRequests => _transportationRequest;

    [Description(CCenterCmts.ProjectOperationTemporaryDaily)]
    private readonly List<ProjectOperationTemporaryDaily> _projectOperationTemporaryDailies;
    public IReadOnlyList<ProjectOperationTemporaryDaily> ProjectOperationTemporaryDailies => _projectOperationTemporaryDailies;

    [Description(CCenterCmts.TransportationRequestCostCenter)]
    private readonly List<TransportationRequestCostCenter> _transportationRequestCostCenters;
    public IReadOnlyList<TransportationRequestCostCenter> TransportationRequestCostCenters => _transportationRequestCostCenters;

    [Description(CCenterCmts.ContractorContractHeader)]
    private readonly List<ContractorContractHeader> _contractorContractHeaders;
    public IReadOnlyList<ContractorContractHeader> ContractorContractHeaders => _contractorContractHeaders;

    private readonly List<CostCenterHistory> _costCenterHistories;
    public IReadOnlyList<CostCenterHistory> CostCenterHistories => _costCenterHistories;

    private readonly List<RequestGoodsSupplyDetail> _requestGoodsSupplyDetails;
    public IReadOnlyList<RequestGoodsSupplyDetail> RequestGoodsSupplyDetails => _requestGoodsSupplyDetails;

    private List<RequestGoodsSupplyTypeDetail> _requestGoodsSupplyTypeDetails;
    public IReadOnlyList<RequestGoodsSupplyTypeDetail> RequestGoodsSupplyTypeDetails => _requestGoodsSupplyTypeDetails;

    private readonly List<ProjectCostCenter> _projectCostCenter;
    public IReadOnlyList<ProjectCostCenter> ProjectCostCenters => _projectCostCenter;
    private CostCenter()
    {
        _employerContractHeads = [];
        _informedUsers = [];
        _costCenterAuthorizedRoles = [];
        _costCenterAuthorizedUsers = [];
        _projects = [];
        _operationLocations = [];
        _costCenterWarehouses = [];
        _groups = [];
        _requestRewards = [];
        _telegramChats = [];
        _transportationRequest = [];
        _projectOperationTemporaryDailies = [];
        _transportationRequestCostCenters = [];
        _contractorContractHeaders = [];
        _costCenterHistories = [];
        _requestGoodsSupplyDetails = [];
        _requestGoodsSupplyTypeDetails = [];
        _projectCostCenter = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

}
