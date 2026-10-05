using Engineering.Application.Abstractions.Data.WorkflowRequests;
using Engineering.Domain.Entities.WorkflowRequests;
using Engineering.Domain.Entities.WorkflowRequests.Contracts;
using Engineering.Domain.Entities.WorkflowRequests.Enums;

namespace Engineering.Persistence.Repositories.WorkflowRequests;

/// <summary>
/// مدیریت درخواست های گردش کار و پیام های ورودی و خروجی
/// در بستر جاری، بدون انجام ذخیره نهایی.
/// </summary>
public sealed class WorkflowIntegrationRepository : IWorkflowIntegrationRepository
{
    private readonly EngineeringDBContext _context;

    private DbSet<WorkflowRequest> Requests => _context.Set<WorkflowRequest>();
    private DbSet<WorkflowOutbox> OutboxMessages => _context.Set<WorkflowOutbox>();
    private DbSet<WorkflowInbox> InboxMessages => _context.Set<WorkflowInbox>();

    /// <summary>Context مشترک با واحد کار جاری را دریافت می کند.</summary>
    public WorkflowIntegrationRepository(EngineeringDBContext context)
    {
        _context = context;
    }

    /// <summary>
    /// درخواست گردش کار و پیام شروع مربوط به آن را
    /// پس از بررسی برای ذخیره ثبت می کند.
    /// </summary>
    public void Add(WorkflowRequest request, WorkflowOutbox outbox)
    {
        ArgumentNullException.ThrowIfNull(request);
        ArgumentNullException.ThrowIfNull(outbox);

        ValidateStartWorkflowMessage(request, outbox);

        Requests.Add(request);
        OutboxMessages.Add(outbox);
    }

    /// <summary>
    /// درخواست گردش کار را بر اساس شرکت و شناسه درخواست دریافت می کند.
    /// </summary>
    public Task<WorkflowRequest?> Get(
        long companyId,
        Guid requestId,
        CT ct)
    {
        return Requests
            .AsTracking()
            .SingleOrDefaultAsync(
                x => x.CompanyId == companyId &&
                     x.RequestId == requestId,
                ct);
    }

    /// <summary>
    /// درخواست باز را بر اساس شرکت، نوع موجودیت،
    /// کلید کسب و کار و هدف دریافت می کند.
    /// </summary>
    public Task<WorkflowRequest?> GetOpen(
        long companyId,
        string entityType,
        string businessKey,
        string purpose,
        CT ct)
    {
        return Requests
            .AsTracking()
            .SingleOrDefaultAsync(
                x => x.CompanyId == companyId &&
                     x.EntityType == entityType &&
                     x.BusinessKey == businessKey &&
                     x.Purpose == purpose &&
                     x.IsOpen,
                ct);
    }

    /// <summary>
    /// اولین پیام آماده پردازش یا دارای قفل منقضی شده را دریافت می کند.
    /// </summary>
    public Task<WorkflowOutbox?> GetNext(
        DateTime nowUtc,
        CT ct)
    {
        return OutboxMessages
            .AsTracking()
            .Where(x =>
                x.ProcessedAtUtc == null &&
                !x.IsSuspended &&
                x.NextAttemptAtUtc <= nowUtc &&
                (x.LockedUntilUtc == null || x.LockedUntilUtc <= nowUtc))
            .OrderBy(x => x.NextAttemptAtUtc)
            .ThenBy(x => x.CreatedAtUtc)
            .FirstOrDefaultAsync(ct);
    }

    /// <summary>
    /// پیام خروجی را بر اساس شناسه پیام دریافت می کند.
    /// </summary>
    public Task<WorkflowOutbox?> GetMessage(
        Guid messageId,
        CT ct)
    {
        return OutboxMessages
            .AsTracking()
            .SingleOrDefaultAsync(
                x => x.MessageId == messageId,
                ct);
    }

    /// <summary>
    /// درخواست مربوط به نمونه فرایند مورد نظر را
    /// در شرکت مربوطه دریافت می کند.
    /// </summary>
    public Task<WorkflowRequest?> GetByInstance(
        long companyId,
        long instanceId,
        CT ct)
    {
        return Requests
            .AsTracking()
            .SingleOrDefaultAsync(
                x => x.CompanyId == companyId &&
                     x.WorkflowInstanceId == instanceId,
                ct);
    }

    /// <summary>
    /// رسید رویداد را حتی پس از حذف نرم، بدون نگهداری برای تغییر دریافت می کند تا تشخیص تکرار حفظ شود.
    /// </summary>
    public Task<WorkflowInbox?> GetReceipt(
        long eventId,
        CT ct)
    {
        return InboxMessages
            .IgnoreQueryFilters()
            .AsNoTracking()
            .SingleOrDefaultAsync(
                x => x.EventId == eventId,
                ct);
    }

    /// <summary>
    /// رسید پردازش رویداد را برای ذخیره ثبت می کند.
    /// </summary>
    public void AddReceipt(WorkflowInbox receipt)
    {
        ArgumentNullException.ThrowIfNull(receipt);

        InboxMessages.Add(receipt);
    }

    /// <summary>
    /// سازگاری درخواست گردش کار با پیام شروع آن را بررسی می کند.
    /// </summary>
    private static void ValidateStartWorkflowMessage(
        WorkflowRequest request,
        WorkflowOutbox outbox)
    {
        var hasSameRequest =
            request.RequestId == outbox.RequestId;

        var hasSameCompany =
            request.CompanyId == outbox.CompanyId;

        var isPendingRequest =
            request.IsOpen &&
            request.DispatchStatus == WorkflowDispatchStatus.Pending;

        var isStartWorkflowMessage =
            outbox.MessageType == WorkflowOutboxTypes.StartWorkflowV1;

        if (hasSameRequest &&
            hasSameCompany &&
            isPendingRequest &&
            isStartWorkflowMessage)
        {
            return;
        }

        throw new InvalidOperationException(
            "پیام شروع با درخواست گردش کار سازگار نیست.");
    }
    /// <summary>پیام تحویل نشده دارای خطا یا توقف را فقط در شرکت تعیین شده دریافت می کند.</summary>
    public Task<List<WorkflowOutbox>> GetFailures(long companyId, int page, int size, CT ct) =>
        OutboxMessages.AsNoTracking()
            .Where(x => x.CompanyId == companyId && x.ProcessedAtUtc == null &&
                (x.IsSuspended || x.LastError != null))
            .OrderByDescending(x => x.IsSuspended).ThenBy(x => x.NextAttemptAtUtc).ThenBy(x => x.Id)
            .Skip((page - 1) * size).Take(size).ToListAsync(ct);}
