namespace Engineering.Domain.Entities.EmployerEmployees;

public class EmployerEmployee : ActivateEntity<EmployerEmployee>
{
    #region Properties

    [Description(GlobalCmts.EmployeeId)]
    public long EmployeeId { get; private set; }
    [Description(EContractCmts.EmployerId)]
    public long EmployerId { get; private set; }
    [Description(GlobalCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    #endregion

    #region Constructors

    public EmployerEmployee(long employeeId,
        long employerId,
        bool isActive,
        long? companyId) : this()
    {
        SetEmployeeId(employeeId);
        SetEmployerId(employerId);
        SetCompanyId(companyId);
        IsActive = isActive;
    }

    #endregion

    #region Commands

    public static EmployerEmployee Create(long thirdPartySkillId, long contractorId, bool isActive, long? companyId)
    {
        return new EmployerEmployee(thirdPartySkillId, contractorId, isActive, companyId);
    }

    public void Update(long? employerId, bool? isActive)
    {
        SetEmployerId(employerId ?? EmployerId);
        IsActive = isActive ?? IsActive;
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

    public void SetEmployerId(long employerId)
    {
        EmployerId = employerId;
    }

    public void SetCompanyId(long? value)
    {
        CompanyId = value;
    }
    #endregion

    private EmployerEmployee() { }
}