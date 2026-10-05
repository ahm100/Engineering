using Engineering.Domain.Entities.ContractorContracts;
using Engineering.Domain.Entities.EmployerContracts;

namespace Engineering.Domain.Entities.CostOvers;

[Description(GlobalCmts.CostOver)]

public class CostOver : AuditableEntity<CostOver>
{

    [Description(CostOverCmts.CostOverName)]
    public string CostOverName { get; private set; } = string.Empty;

    [Description(CostOverCmts.CostOverCode)]
    public string CostOverCode { get; private set; } = string.Empty;

    [Description(CostOverCmts.CompanyId)]
    public long? CompanyId { get; private set; }

    [Description(CostOverCmts.IsActive)]
    public bool IsActive { get; private set; } = true;

    [Description(CostOverCmts.ApprovalStatus)]
    public CostOverApprovalStatus ApprovalStatus { get; private set; } = CostOverApprovalStatus.Draft;

    [Description(CostOverCmts.CurrentApprovalAttemptId)]
    public Guid? CurrentApprovalAttemptId { get; private set; }

    public CostOver(string costOverName, string costOverCode, bool isActive, long? companyId) : this()
    {
        SetName(costOverName);
        SetCode(costOverCode);
        SetCompanyId(companyId);
        IsActive = Guard.Against.Null(isActive, nameof(isActive));
    }

    #region Set data

    /// <summary>نام را فقط در وضعیت پیش نویس تغییر می دهد.</summary>
    public void SetName(string value)
    {
        EnsureDraft();
        CostOverName = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    /// <summary>کد را فقط در وضعیت پیش نویس تغییر می دهد.</summary>
    public void SetCode(string value)
    {
        EnsureDraft();
        CostOverCode = Guard.Against.NullOrWhiteSpace(value, nameof(value));
    }
    /// <summary>رکورد را در صورت نداشتن تأیید جاری فعال می کند.</summary>
    public void SetActive()
    {
        EnsureNotPending();
        IsActive = true;
    }
    /// <summary>رکورد را در صورت نداشتن تأیید جاری غیرفعال می کند.</summary>
    public void SetInActive()
    {
        EnsureNotPending();
        IsActive = false;
    }
    /// <summary>حذف نرم را در صورت نداشتن تأیید جاری اعمال می کند.</summary>
    public void SetIsDeleted()
    {
        EnsureNotPending();
        IsDeleted = true;
    }
    /// <summary>شرکت را فقط در وضعیت پیش نویس تنظیم می کند.</summary>
    public void SetCompanyId(long? value)
    {
        EnsureDraft();
        CompanyId = value;
    }
    #endregion

    #region Methods


    #endregion



    //TOWork
    /// <summary>
    /// رکورد پیش نویس را برای تأیید ارسال و تلاش جاری را مشخص می کند.
    /// </summary>
    public void SubmitForApproval(Guid attemptId)
    {
        if (attemptId == Guid.Empty)
            throw new InvalidOperationException("شناسه تلاش تأیید معتبر نیست.");

        if (ApprovalStatus != CostOverApprovalStatus.Draft)
            throw new InvalidOperationException(
                "فقط رکورد پیش نویس قابل ارسال برای تأیید است.");

        CurrentApprovalAttemptId = attemptId;
        ApprovalStatus = CostOverApprovalStatus.PendingApproval;
    }

    //TOWork
    /// <summary>
    /// نتیجه تأیید یا رد را فقط برای تلاش جاری و در انتظار ثبت می کند.
    /// </summary>
    public void ApplyApprovalResult(Guid attemptId, bool approved)
    {
        if (ApprovalStatus != CostOverApprovalStatus.PendingApproval ||
            CurrentApprovalAttemptId != attemptId)
        {
            throw new InvalidOperationException(
                "نتیجه دریافتی متعلق به تلاش جاری در انتظار تأیید نیست.");
        }

        ApprovalStatus = approved
            ? CostOverApprovalStatus.Approved
            : CostOverApprovalStatus.Rejected;
    }



    //TOWork
    /// <summary>
    /// رکورد ردشده را برای اصلاح و ارسال مجدد به پیش نویس برمی گرداند.
    /// </summary>
    public void ReturnToDraft()
    {
        if (ApprovalStatus != CostOverApprovalStatus.Rejected)
            throw new InvalidOperationException(
                "فقط رکورد ردشده قابل بازگشت برای اصلاح است.");

        ApprovalStatus = CostOverApprovalStatus.Draft;
        CurrentApprovalAttemptId = null;
    }

    /// <summary>پس از تایید پایان ناموفق درخواست در لایه کاربرد، فقط همان تلاش جاری را برای اصلاح به پیش نویس برمی گرداند.</summary>
    public void RecoverApproval(Guid attemptId)
    {
        if (attemptId == Guid.Empty || CurrentApprovalAttemptId != attemptId ||
            ApprovalStatus != CostOverApprovalStatus.PendingApproval)
            throw new InvalidOperationException("تلاش جاری هزینه بالاسری برای بازیابی معتبر نیست.");

        ApprovalStatus = CostOverApprovalStatus.Draft;
        CurrentApprovalAttemptId = null;
    }
    /// <summary>ویرایش اطلاعات درخواستی را تا بازگشت صریح به پیش نویس متوقف می کند.</summary>
    private void EnsureDraft()
    {
        if (ApprovalStatus != CostOverApprovalStatus.Draft)
            throw new InvalidOperationException("ویرایش اطلاعات فقط در وضعیت پیش نویس مجاز است.");
    }

    /// <summary>از تغییر یا حذف رکورد در طول تأیید جاری جلوگیری می کند.</summary>
    private void EnsureNotPending()
    {
        if (ApprovalStatus == CostOverApprovalStatus.PendingApproval)
            throw new InvalidOperationException("رکورد در انتظار تأیید قابل تغییر یا حذف نیست.");
    }

    /// <summary>
    ///  For EF core, never thoch this
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private List<EmployerCostOver> _contractCostOver;
    public IReadOnlyList<EmployerCostOver> EmployerCostOvers => _contractCostOver;

    private List<ContractorContractDetailCostOver> _contractorContractDetailCostOvers;
    public IReadOnlyList<ContractorContractDetailCostOver> ContractorContractDetailCostOvers => _contractorContractDetailCostOvers;
    private CostOver()
    {
        _contractCostOver = [];
        _contractorContractDetailCostOvers = [];
    }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}
