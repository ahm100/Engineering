namespace Engineering.Domain.Entities.ContractorEmployees;

/// <summary>
/// اطلاعات پرسنل
/// </summary>
public class ContractorEmployee : ActivateEntity<ContractorEmployee>
{
    #region Properties

    [Description(GlobalCmts.EmployeeId)]
    public long EmployeeId { get; private set; }
    [Description(GlobalCmts.ContractorId)]
    public long ContractorId { get; private set; }
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    #endregion

    #region Constructors

    public ContractorEmployee(long employeeId, long contractorId, bool isActive, long? companyId)
    {
        SetEmployeeId(employeeId);
        SetContractorId(contractorId);
        SetCompanyId(companyId);
        IsActive = isActive;
    }

    #endregion

    #region Commands

    public static ContractorEmployee Create(long thirdPartySkillId, long contractorId, bool isActive, long? companyId)
    {
        return new ContractorEmployee(thirdPartySkillId, contractorId, isActive, companyId);
    }

    public void SetIsActive(bool isActive)
    {
        this.IsActive = isActive;
    }

    public void SetIsDeleted()
    {
        IsDeleted = true;
    }

    public void SetEmployeeId(long employeeId)
    {
        EmployeeId = employeeId;
    }

    public void SetContractorId(long contractorId)
    {
        ContractorId = contractorId;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    #endregion

    private ContractorEmployee() { }
}