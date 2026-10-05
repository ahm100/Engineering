using Engineering.Domain.Entities.OperationInfoHistorys;
using Engineering.Domain.Entities.ProjectOperationDetails;
using Engineering.Domain.Entities.ProjectOperations;

namespace Engineering.Domain.Entities.OperationInfos;

[Description(GlobalCmts.OperationInfo)]
public class OperationInfo : AuditableEntity<OperationInfo>
{
    [Description(OperationInfoCmts.OperationInfoName)]
    public string OperationInfoName { get; private set; } = string.Empty;
    [Description(OperationInfoCmts.OperationInfoCode)]
    public string OperationInfoCode { get; private set; } = string.Empty;
    [Description(OperationInfoCmts.OperationLatinName)]
    public string? OperationLatinName { get; private set; } = string.Empty;
    [Description(OperationInfoCmts.Priority)]
    public int? Priority { get; private set; }
    [Description(OperationInfoCmts.BasePrice)]
    public decimal BasePrice { get; private set; } = 0;
    [Description(OperationInfoCmts.UnitOfMeasurementId)]
    public long UnitOfMeasurementId { get; private set; }
    [Description(OperationInfoCmts.HaveStandard)]
    public bool HaveStandard { get; private set; } = false;
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }
    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;
    [Description(OperationInfoCmts.IsPriceList)]
    public bool IsPriceList { get; private set; } = false;
    [Description(OperationInfoCmts.HasChanged)]
    public bool HasChanged { get; private set; } = false;
    public long? YearId { get; private set; }


    public OperationInfo(string operationInfoName, string operationInfoCode, string? operationLatinName,
        int? priority, long unitOfMeasurement, bool isActive, bool isPriceList, decimal basePrice, long? companyId) : this()
    {
        SetName(operationInfoName);
        SetCode(operationInfoCode);
        SetPriority(priority ?? 1);
        SetUnitOfMeasurement(unitOfMeasurement);
        SetLatinName(operationLatinName);
        SetBasePrice(basePrice);
        SetIsPriceList(isPriceList);
        SetCompanyId(companyId);
        SetHasChanged(false);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
        AddHistory();
    }

    #region Set data 

    public void SetPriority(int? value)
    {
        Priority = value;
    }
    public void SetBasePrice(decimal value)
    {
        BasePrice = Guard.Against.Null(value, nameof(value));
        if (this.ProjectOperations.Any())
        {
            foreach (var pO in this.ProjectOperations)
            {
                pO.SetBasePrice(value);
                pO.SetChangedPrice(value);
            }
        }
    }
    public void SetName(string value)
    {
        OperationInfoName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetIsPriceList(bool value)
    {
        IsPriceList = Guard.Against.Null(value, nameof(value));
    }

    public void SetHasChanged(bool value)
    {
        HasChanged = Guard.Against.Null(value, nameof(value));
    }
    public void SetCode(string value)
    {
        OperationInfoCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetLatinName(string? value)
    {
        OperationLatinName = value;
    }
    public void SetUnitOfMeasurement(long value)
    {
        UnitOfMeasurementId = Guard.Against.Null(value, nameof(value));
    }
    public void SetActive()
    {
        IsActive = true;
    }
    public void SetInActive()
    {
        IsActive = false;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }
    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetIncreaseRate(decimal increaseRate)
    {
        foreach (var pOperation in this.ProjectOperations)
        {
            pOperation.SetIncreaseRate(increaseRate);
        }
    }

    public void SetStandard()
    {
        HaveStandard = _consumptionStandardProduct.Any(x => !x.IsDeleted) ||
            _consumptionStandardMachineries.Any(x => !x.IsDeleted) || _consumptionStandardExperts.Any(x => !x.IsDeleted);
    }

    public void SetYearId(long? value)
    {
        YearId = value;
    }

    #endregion

    #region Methods 

    public void AddMaterialStandard(ConsumptionStandardProduct newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_consumptionStandardProduct.Any(oo => oo.ProductUnitId == newData.ProductUnitId && oo.Created == newData.Created))
            return;

        _consumptionStandardProduct.Add(newData);
    }

    public void AddMachineryStandard(ConsumptionStandardMachinery newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_consumptionStandardMachineries.Any(oo => oo.Machinery.Id == newData.Machinery.Id && oo.Created == newData.Created))
            return;

        _consumptionStandardMachineries.Add(newData);
    }

    public void AddExpertStandard(ConsumptionStandardExpert newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_consumptionStandardExperts.Any(oo => oo.ExpertUnitId == newData.ExpertUnitId && oo.Created == newData.Created))
            return;

        _consumptionStandardExperts.Add(newData);
    }

    public void AddOperationInfoService(OperationInfoService newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_operationInfoServices.Any(oo => oo.ServiceInfo == newData.ServiceInfo && oo.Created == newData.Created))
            return;

        _operationInfoServices.Add(newData);
    }

    public void AddOperationInfoSeason(OperationInfoSeason newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_operationInfoSeason.Any(oo => oo.Season == newData.Season && oo.Created == newData.Created))
            return;

        _operationInfoSeason.Add(newData);
    }

    public void AddOperationInfoAction(OperationInfoAction newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_operationInfoActions.Any(oo => oo.Action == newData.Action && oo.Created == newData.Created))
            return;

        _operationInfoActions.Add(newData);
    }

    public void AddOperationInfoGroupRelation(OperationInfoGroupRelation newData)
    {
        ArgumentNullException.ThrowIfNull(newData);
        if (_operationInfoGroupRelations.Any(oo => oo.OperationInfoGroup == newData.OperationInfoGroup && oo.Created == newData.Created))
            return;

        _operationInfoGroupRelations.Add(newData);
    }

    public void AddOperationInfoDependency(OperationInfoDependency newData)
    {
        ArgumentNullException.ThrowIfNull(newData);

        _operationInfoDependency.Add(newData);
    }

    public void AddHistory()
    {
        _operationInfoHistories.Add(OperationInfoHistory.Create(
            this,
            OperationInfoName,
            OperationInfoCode,
            OperationLatinName,
            BasePrice,
            Priority,
            UnitOfMeasurementId,
            IsPriceList,
            IsActive
            ));
    }

    #endregion

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    private List<OperationInfoHistory> _operationInfoHistories;
    public IReadOnlyList<OperationInfoHistory> OperationInfoHistories => _operationInfoHistories.AsReadOnly();

    private List<ConsumptionStandardProduct> _consumptionStandardProduct;
    public IReadOnlyList<ConsumptionStandardProduct> ConsumptionStandardProduct => _consumptionStandardProduct.AsReadOnly();

    private List<ConsumptionStandardMachinery> _consumptionStandardMachineries;
    public IReadOnlyList<ConsumptionStandardMachinery> ConsumptionStandardMachineries => _consumptionStandardMachineries.AsReadOnly();

    private List<ConsumptionStandardExpert> _consumptionStandardExperts;
    public IReadOnlyList<ConsumptionStandardExpert> ConsumptionStandardExperts => _consumptionStandardExperts.AsReadOnly();

    private List<ProjectOperation> _projectOperations;
    public IReadOnlyList<ProjectOperation> ProjectOperations => _projectOperations;

    private List<OperationInfoService> _operationInfoServices;
    public IReadOnlyList<OperationInfoService> OperationInfoServices => _operationInfoServices;

    private List<OperationInfoDependency> _operationInfoDependency;
    public IReadOnlyList<OperationInfoDependency> OperationInfoDependencies => _operationInfoDependency;

    private List<OperationInfoSeason> _operationInfoSeason;
    public IReadOnlyList<OperationInfoSeason> OperationInfoSeasons => _operationInfoSeason;

    private List<OperationInfoGroupRelation> _operationInfoGroupRelations;
    public IReadOnlyList<OperationInfoGroupRelation> OperationInfoGroupRelations => _operationInfoGroupRelations;

    private List<ProjectOperationDetailInspection> _projectOperationDetailInspections;
    public IReadOnlyList<ProjectOperationDetailInspection> ProjectOperationDetailInspections => _projectOperationDetailInspections;
    private List<OperationInfoAction> _operationInfoActions;
    public IReadOnlyList<OperationInfoAction> OperationInfoActions => _operationInfoActions;

    private OperationInfo()
    {
        _consumptionStandardProduct = [];
        _consumptionStandardMachineries = [];
        _consumptionStandardExperts = [];
        _projectOperations = [];
        _operationInfoServices = [];
        _operationInfoDependency = [];
        _operationInfoSeason = [];
        _operationInfoGroupRelations = [];
        _projectOperationDetailInspections = [];
        _operationInfoHistories = [];
        _operationInfoActions = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
