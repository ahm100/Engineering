using Engineering.Domain.Entities.FixAssetMachineries.Enums;
using Engineering.Domain.Entities.Machineries;

namespace Engineering.Domain.Entities.FixAssetMachineries;

/// <summary>
/// موجودی ماشین آلات
/// </summary>
public class FixAssetMachinery : AuditableEntity<FixAssetMachinery>
{
    #region Properties
    [Description(GlobalCmts.StartDate)]
    public DateTime? StartDate { get; private set; }

    [Description(GlobalCmts.EndDate)]
    public DateTime? EndDate { get; private set; }

    [Description(GlobalCmts.Description)]
    public string? Description { get; private set; }

    [Description(FixAssetMachineryCmts.DriverId)]
    public long? DriverId { get; private set; }

    [Description(FixAssetMachineryCmts.DriverName)]
    public string? DriverName { get; private set; }

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(FixAssetMachineryCmts.MachinerySpecification)]
    public string? MachinerySpecification { get; private set; }

    [Description(FixAssetMachineryCmts.NumberPlates)]
    public string? NumberPlates { get; private set; }

    [Description(FixAssetMachineryCmts.MachineryPrice)]
    public decimal? MachineryPrice { get; private set; }

    [Description(FixAssetMachineryCmts.HourlyRate)]
    public decimal? HourlyRate { get; private set; }

    [Description(FixAssetMachineryCmts.DailyRate)]
    public decimal? DailyRate { get; private set; }

    [Description(FixAssetMachineryCmts.ServiceRate)]
    public decimal? ServiceRate { get; private set; }

    [Description(FixAssetMachineryCmts.VolumeRate)]
    public decimal? VolumeRate { get; private set; }

    [Description(FixAssetMachineryCmts.ContractorId)]
    public long? ContractorId { get; private set; }

    [Description(FixAssetMachineryCmts.FixAssetMachineryType)]
    public FixAssetMachineryType FixAssetMachineryType { get; private set; }

    [Description(FixAssetMachineryCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(FixAssetMachineryCmts.Machinery)]
    public long MachineryId { get; private set; }
    public Machinery Machinery { get; private set; }

    #endregion

    public FixAssetMachinery(
        string? machinerySpecification,
        string? numberPlates,
        FixAssetMachineryType fixAssetMachineryType,
        decimal? machineryPrice,
        bool isActive,
        long? contractorId,
        long? driverId,
        string? driverName,
        DateTime? startDate,
        DateTime? endDate,
        string? description,
        long? companyId,
        Machinery machinery) : this()
    {
        SetStartDate(startDate);
        SetEndDate(endDate);
        SetDescription(description);
        SetDriver(driverId, driverName);
        SetCompany(companyId);
        SetMachinerySpecification(machinerySpecification);
        SetNumberPlates(numberPlates);
        SetFixAssetMachineryType(fixAssetMachineryType);
        SetMachinery(machinery);
        SetMachineryPrice(machineryPrice);
        SetContractor(contractorId);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
    }

    #region Command

    public void SetFixAssetMachineryType(FixAssetMachineryType value)
    {
        FixAssetMachineryType = Guard.Against.Null(value, nameof(value));
    }

    public void SetDriver(long? driverId, string? driverName)
    {
        DriverId = driverId;
        DriverName = driverName;
    }

    public void SetCompany(long? companyId)
    {
        CompanyId = companyId;
    }
    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetDescription(string? value)
    {
        Description = value;
    }

    public void SetDriverId(long? value)
    {
        DriverId = value;
    }

    public void SetDriverName(string? value)
    {
        DriverName = value;
    }

    public void SetMachinerySpecification(string? value)
    {
        MachinerySpecification = value;
    }

    public void SetStartDate(DateTime? value)
    {
        StartDate = value;
    }

    public void SetEndDate(DateTime? value)
    {
        EndDate = value;
    }

    public void SetMachinery(Machinery value)
    {
        Machinery = Guard.Against.Null(value, nameof(value));
        MachineryId = Guard.Against.Null(value.Id, nameof(value.Id));
    }

    public void SetNumberPlates(string? value)
    {
        NumberPlates = value;
    }

    public void SetMachineryPrice(decimal? value)
    {
        MachineryPrice = value;
    }

    public void SetHourlyRate(decimal? value)
    {
        HourlyRate = value;
    }

    public void SetDailyRate(decimal? value)
    {
        DailyRate = value;
    }

    public void SetServiceRate(decimal? value)
    {
        ServiceRate = value;
    }

    public void SetVolumeRate(decimal? value)
    {
        VolumeRate = value;
    }

    public void SetContractorId(long? value)
    {
        ContractorId = value;
    }

    public void SetActive()
    {
        IsActive = true;
    }

    public void SetInActive()
    {
        IsActive = false;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetContractor(long? contractorId)
    {
        ContractorId = contractorId;
    }

    #endregion

    #region Constructors

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private readonly List<FixAssetMachineryNotWork> _FixAssetMachineryNotWorks;
    public IReadOnlyList<FixAssetMachineryNotWork> FixAssetMachineryNotWorks => _FixAssetMachineryNotWorks;
    private readonly List<MachineryReservation> _machineryReservations;
    public IReadOnlyList<MachineryReservation> MachineryReservations => _machineryReservations;

    private readonly List<FixAssetMachineryDocument> _fixAssetMachineryDocuments;
    public IReadOnlyList<FixAssetMachineryDocument> FixAssetMachineryDocuments => _fixAssetMachineryDocuments;

    private readonly List<FixAssetMachineryRate> _fixAssetMachineryRates;
    public IReadOnlyList<FixAssetMachineryRate> FixAssetMachineryRates => _fixAssetMachineryRates;

    private FixAssetMachinery()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
        _FixAssetMachineryNotWorks = [];
        _machineryReservations = [];
        _fixAssetMachineryDocuments = [];
        _fixAssetMachineryRates = [];
    }

    #endregion
}
