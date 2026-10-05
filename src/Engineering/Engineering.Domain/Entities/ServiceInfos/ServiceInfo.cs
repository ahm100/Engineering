using Engineering.Domain.Entities.EmployerContracts;
using Engineering.Domain.Entities.Projects;
using Engineering.Domain.Entities.RequestContractors;
using Engineering.Domain.Entities.ServiceInfos.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Engineering.Domain.Entities.ServiceInfos;

[Description(GlobalCmts.ServiceInfo)]
public class ServiceInfo : AuditableEntity<ServiceInfo>
{
    [Description(GlobalCmts.Title)]
    public string ServiceInfoName { get; private set; } = string.Empty;

    [Description(GlobalCmts.Code)]
    public string ServiceInfoCode { get; private set; } = string.Empty;

    [Description(AdvertisementCmts.TitleEn)]
    public string? ServiceInfoEnName { get; private set; }

    [Description(AdvertisementCmts.DescriptionFa)]
    public string? DescriptionFa { get; private set; }

    [Description(AdvertisementCmts.DescriptionEn)]
    public string? DescriptionEn { get; private set; }

    [Description(GlobalCmts.MeasureUnitId)]
    public long UnitOfMeasurementId { get; private set; }

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(GlobalCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(GlobalCmts.PreferentialReferenceCode)]
    public Guid? PreferentialReferenceCode { get; private set; }

    [Description(GlobalCmts.Type)]
    public ServiceInfoType Type { get; private set; }

    [NotMapped]
    public bool HavePreferentialReferenceCode => PreferentialReferenceCode != Guid.Empty || PreferentialReferenceCode != null;

    public ServiceInfo(
        string serviceName,
        string? serviceEnName,
        string serviceCode,
        string? desFa,
        string? desEn,
        long unitOfMeasurement,
        bool isActive,
        ServiceInfoType type,
        List<string>? urls,
        long? companyId) : this()
    {
        SetName(serviceName);
        SetCode(serviceCode);
        SetUnitOfMeasurement(unitOfMeasurement);
        SetServiceInfoNameEn(serviceEnName);
        SetDescriptionFa(desFa);
        SetDescriptionEn(desEn);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
        SetCompanyId(companyId);
        SetPreferentialCode();
        SetType(type);
        AddDocuments(urls);
    }

    #region Set data 

    public void SetName(string value)
    {
        ServiceInfoName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetType(ServiceInfoType value)
    {
        Type = Guard.Against.Null(value, nameof(value));
    }

    public void SetCode(string value)
    {
        ServiceInfoCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }

    public void SetUnitOfMeasurement(long value)
    {
        UnitOfMeasurementId = Guard.Against.Null(value, nameof(value));
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetServiceInfoNameEn(string? value)
    {
        ServiceInfoEnName = value;
    }

    public void SetDescriptionEn(string? value)
    {
        DescriptionEn = value;
    }

    public void SetDescriptionFa(string? value)
    {
        DescriptionFa = value;
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

    public void SetPreferentialCode()
    {
        PreferentialReferenceCode = Guid.NewGuid();
    }

    public void AddDocuments(List<string>? urls)
    {
        if (urls != null && urls.Count > 0)
        {
            _serviceInfoDocument.ForEach(c => c.SetIsDeleted());

            foreach (var url in urls)
                _serviceInfoDocument.Add(ServiceInfoDocument.Create(url, this));
        }
        else
            _serviceInfoDocument.ForEach(c => c.SetIsDeleted());
    }


    #endregion

    #region Methods 

    public void AddOperationInfoService(OperationInfoService value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (_operationInfoServices.Any(oo => oo.OperationInfo == value.OperationInfo && oo.Created == value.Created))
            return;

        _operationInfoServices.Add(value);
    }

    public void AddProjectService(ProjectService value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (_projectServices.Any(oo => oo.Project == value.Project && oo.Created == value.Created))
            return;

        _projectServices.Add(value);
    }

    public string GetPreferentialName() => ServiceInfoName;

    #endregion
    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<OperationInfoService> _operationInfoServices;
    public IReadOnlyList<OperationInfoService> OperationInfoServices => _operationInfoServices;

    private List<EmployerOperationService> _employerOperationService;
    public IReadOnlyList<EmployerOperationService> EmployerOperationServices => _employerOperationService;

    private List<ProjectService> _projectServices;
    public IReadOnlyList<ProjectService> ProjectServices => _projectServices;

    private List<RequestContractor> _requestContractors;
    public IReadOnlyList<RequestContractor> RequestContractors => _requestContractors;

    private List<ServiceInfoDocument> _serviceInfoDocument;
    public IReadOnlyList<ServiceInfoDocument> ServiceInfoDocuments => _serviceInfoDocument;

    public ServiceInfo()
    {
        _operationInfoServices = new List<OperationInfoService>();
        _employerOperationService = new List<EmployerOperationService>();
        _projectServices = new List<ProjectService>();
        _requestContractors = [];
        _serviceInfoDocument = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
