# اتصال Engineering به Workflow

## مسیر فعلی

1. `POST /api/engineering/v1/CostOver/SubmitForApproval` با بدنه `{"id": 123}`، شرکت و کاربر را از هویت جاری Engineering می‌گیرد.
2. Handler اختصاصی CostOver، وضعیت `PendingApproval`، یک `WorkflowRequest` و یک `WorkflowOutbox` را در Context مشترک ثبت می‌کند. تنها Commit در `CostOverLogic` انجام می‌شود.
3. Worker پیام را با رزرو دو دقیقه‌ای و `RowVersion` تحویل می‌گیرد. تماس HTTP بیرون از تراکنش پایگاه داده است.
4. پیام به `http://localhost:8081/api/workflow/v1/instances/StartWorkflow` ارسال می‌شود. فعلاً توکن ارسال نمی‌شود. `Idempotency-Key` در تمام تلاش‌ها همان `WorkflowRequest.RequestId` است. محتوای پیام نیز ثابت می‌ماند.
5. پاسخ `Result<RequestResponse>.Value` باید شناسه نمونه مثبت و `BusinessKey` متناظر داشته باشد. پذیرش درخواست و تکمیل Outbox با یک Commit در `WorkflowIntegrationLogic` ذخیره می‌شوند؛ پذیرش، پایان موفق موتور را نشان نمی‌دهد.
6. `POST /api/engineering/v1/workflow/result`، نتیجه امضاشده را به Logic مشترک و Handler موجودیت می‌رساند. شرکت، نمونه، کلید کسب‌وکار و تلاش جاری بررسی می‌شوند. رسید `WorkflowInbox` و تغییر موجودیت در یک Commit ثبت می‌شوند.

## تنظیمات

مقادیر زیر را می‌توان در بخش `WorkflowIntegration` تنظیمات یا متغیرهای محیطی وارد کرد. پیش‌فرض آدرس همان مقدار زیر است:

```json
{
  "WorkflowIntegration": {
    "Enabled": true,
    "BaseUrl": "http://localhost:8081/",
    "StartPath": "api/workflow/v1/instances/StartWorkflow",
    "PollIntervalSeconds": 2
  }
}
```

`WorkflowIntegration__CallbackSecret` باید از تنظیمات محیطی/Secret store تأمین شود و حداقل ۳۲ کاراکتر باشد. آن را با `ResultDestinations.Services[EngineeringServiceId].Secret` در Workflow یکسان کنید. Secret در کد، Outbox یا این فایل ذخیره نمی‌شود.

در تنظیمات مقصد نتیجه Workflow، URL را برابر نشانی HTTPS این endpoint در Engineering قرار دهید:

```text
https://<engineering-host>/api/engineering/v1/workflow/result
```

کد فعلی Workflow مقصد نتیجه را فقط با HTTPS می‌پذیرد. Callback بدون Secret تنظیم‌شده پاسخ 503 می‌دهد و بدون امضای معتبر پاسخ 401. `AllowAnonymous` فقط احراز هویت Bearer عمومی را کنار می‌گذارد؛ احراز اصالت این endpoint با HMAC انجام می‌شود. محتوای امضا دقیقاً `timestamp.eventId.body` و مهلت ارسال پنج دقیقه است.

**محدودیت فعلی مقصد:** کد موجود Workflow برای `StartWorkflow` هویت احرازشده، شرکت، شناسه سرویس و مجوز شروع می‌خواهد. ارسال بدون توکن Engineering مطابق درخواست فعلی پیاده شده است؛ اگر نسخه در حال اجرای مقصد نیز این الزام را داشته باشد، پاسخ 401/403 در `LastError` ثبت می‌شود. فعال شدن ارتباط واقعی به تنظیم دسترسی در Workflow وابسته است. Claimهای شرکت یا سرویس از طریق هدر جعلی فرستاده نمی‌شوند.

Workflow فعال و منتشرشده با کد `CostOverApproval` باید در شرکت مربوط وجود داشته باشد. ورودی شامل `WorkflowCode`، `BusinessKey`، `OwnerId` و تصویر ثابت `Variables` با اطلاعات CostOver است.

## پیگیری و تلاش مجدد

- `GET /api/engineering/v1/workflow/requests/{requestId}`: وضعیت درخواست در شرکت احرازشده.
- وضعیت تأیید و شناسه تلاش جاری در پاسخ جزئیات و لیست CostOver نیز وجود دارند.
- `POST /api/engineering/v1/CostOver/ReturnToDraft` با بدنه `{"id": 123}`: فقط هزینه ردشده را به Draft برمی‌گرداند و تلاش قبلی را می‌بندد. ارسال بعدی یک شناسه جدید می‌گیرد.
- ارسال دوباره هزینه‌ای که درخواست باز متناظر دارد، همان رسید را برمی‌گرداند. ایندکس یکتای درخواست باز و RowVersion مانع ذخیره درخواست‌های هم‌زمان ناسازگار می‌شوند.
- پیام با Timeout یا پاسخ ناموفق حذف نمی‌شود. تأخیر تلاش مجدد افزایش می‌یابد و حداکثر پنج دقیقه است؛ خطاهای 401/403 و سایر 4xx هم نگه داشته می‌شوند تا تنظیمات قابل اصلاح باشند.
- اگر برنامه بعد از پذیرش مقصد و پیش از ثبت پاسخ متوقف شود، همان پیام با همان کلید دوباره ارسال می‌شود. تضمین ایجاد نشدن نمونه تکراری به حفظ رسید Idempotency در Workflow نیز وابسته است.
- Callback زودرس که هنوز شناسه نمونه‌اش در Engineering ثبت نشده، پاسخ 409 می‌گیرد؛ Outbox سمت Workflow باید آن را دوباره ارسال کند.
- رویداد تکراری هم‌محتوا بی‌اثر است؛ تکرار شناسه رویداد با محتوای متفاوت یا نتیجه متناقض رد می‌شود. نتیجه دیررس تلاش بسته‌شده، تلاش جدید را تغییر نمی‌دهد.
- فقط `Approved` و `Rejected` وضعیت تأیید CostOver را تغییر می‌دهند. `Cancelled` و `Failed` برای پیگیری ثبت می‌شوند؛ به معنی رد یا آزاد شدن خودکار درخواست نیستند. بازگشت خودکار از این دو حالت پیاده نشده است.
- در زمان انتظار تأیید، تغییر فعال‌بودن یا حذف CostOver مجاز نیست. ویرایش اطلاعات فقط در Draft انجام می‌شود.

## گسترش به موجودیت‌های دیگر و RabbitMQ

برای موجودیت جدید، Handler اختصاصی `IWorkflowEntityHandler` با `EntityType` و `Purpose` و Command ارسال همان موجودیت اضافه کنید. ایجاد درخواست و Payload با `WorkflowRequestFactory` و ثبت با `IWorkflowIntegrationRepository` مشترک است. Commit همچنان مسئولیت Logic است.

برای انتقال جدید، درگاه `IWorkflowTransport` را توسعه دهید. پیاده‌سازی فعلی رسید پذیرش عددی را از REST دریافت می‌کند؛ در RabbitMQ باید قرارداد acknowledgement پذیرش نمونه نیز تعریف شود و صرف تأیید Broker به‌جای پذیرش Workflow ثبت نشود. Consumer نتیجه می‌تواند بعد از اعتبارسنجی مبدا، همان `WorkflowIntegrationLogic.ApplyResult` را فراخوانی کند. Outbox/Inbox تازه‌ای برای هر موجودیت ایجاد نکنید.

## پایگاه داده و بررسی

Migration اتصال، سه جدول `WorkflowRequests`، `WorkflowOutboxes` و `WorkflowInbox`، کلیدهای ارتباطی و ایندکس‌های یکتا را اضافه می‌کند. در ساختار فعلی، هر سه موجودیت از `AuditableEntity` و `MetaConfiguration` پروژه استفاده می‌کنند. توضیحات از کلاس‌های `Cmts` خوانده می‌شوند. Worker برای تغییر Outbox و Callback برای ایجاد Inbox، `CheckUser` را فعال می‌کنند تا Audit به پروفایل HTTP وابسته نباشد؛ ایجاد رسید سیستمی با `CreatorId = 0` ثبت می‌شود. رسیدهای Inbox حتی پس از حذف نرم برای تشخیص تکرار خوانده می‌شوند.

Migration باید پیش از پردازش پیام‌ها اعمال شود. `Program.cs` فعلی پروژه هنگام اجرا `MigrateAsync` را فراخوانی می‌کند. در این تغییر، خود برنامه یا Migration روی پایگاه داده اجرا نشده است.

بررسی با Build و بازبینی Migration انجام می‌شود؛ تست جدید اضافه نشده و سناریوی زنده SQL Server/Workflow اجرا نشده است.

### نتیجه بازبینی ۲۰۲۶/۰۹/۱۹

مدل جدول‌های Workflow با Migrationهای ثبت‌شده منطبق بود. بررسی کل مدل با `has-pending-model-changes` اختلاف موجود در `ProjectScheduleTasks` را نشان داد: الزامی بودن `MppId` و `MppUid`، nullable بودن `IsSummary`، توضیحات `IsManuallyScheduled` و رفتار حذف رابطه `Parent`. این اختلاف خارج از اتصال Workflow است؛ Migration تشخیصی آن نگه داشته نشد و Snapshot اصلی حفظ شد.

در بررسی دسترسی، اتصال به `localhost:8081` رد شد؛ اعتبارسنجی سرتاسری تا زمان در دسترس بودن سرویس Workflow انجام نشده است.
