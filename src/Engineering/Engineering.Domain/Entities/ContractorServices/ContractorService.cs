using Engineering.Domain.Entities.ServiceInfos;

namespace Engineering.Domain.Entities.ContractorServices;

/// <summary>
/// خدمت پیمانکار
/// </summary>
public class ContractorService : ActivateEntity<ContractorService>
{
    #region Properties

    [Description(GlobalCmts.ContractorId)]
    public long ContractorId { get; private set; }

    [Description(EContractCmts.ServiceInfo)]
    public long ServiceInfoId { get; private set; }
    public ServiceInfo ServiceInfo { get; private set; } = null!;

    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    #endregion

    #region Constructor

    public ContractorService(long serviceInfoId, long contractorId, long? companyId)
    {
        ContractorId = Guard.Against.Null(contractorId, nameof(contractorId));
        ServiceInfoId = Guard.Against.Null(serviceInfoId, nameof(serviceInfoId));
        CompanyId = companyId;
        IsActive = true;
    }

    #endregion

    #region Commands

    public static ContractorService Create(long serviceInfoId, long contractorId, long? companyId)
    {
        return new ContractorService(serviceInfoId, contractorId, companyId);
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetContractorId(long contractorId)
    {
        ContractorId = contractorId;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }

    public void SetServiceInfoId(long serviceInfoId)
    {
        ServiceInfoId = serviceInfoId;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    #endregion

    private ContractorService() { }
}
