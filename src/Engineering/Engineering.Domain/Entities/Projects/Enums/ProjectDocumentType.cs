namespace Engineering.Domain.Entities.Projects.Enums;

public enum ProjectDocumentType
{
    [Description("مشخصات فنی")]
    TechnicalSpecification = 1,

    [Description("نقشه")]
    Drawing = 2,

    [Description("محاسبات")]
    Calculation = 3,

    [Description("گزارش")]
    Report = 4,

    [Description("روش اجرا")]
    MethodStatement = 5,

    [Description("دستورالعمل")]
    Procedure = 6,

    [Description("گزارش تست")]
    TestReport = 7,

    [Description("گزارش بازرسی")]
    InspectionReport = 8,

    // برنامه‌ریزی
    [Description("برنامه زمان‌بندی")]
    Schedule = 20,

    [Description("گزارش پیشرفت")]
    ProgressReport = 21,

    [Description("صورتجلسه")]
    MeetingMinutes = 22,

    // قرارداد
    [Description("قرارداد")]
    Contract = 40,

    [Description("الحاقیه قرارداد")]
    Amendment = 41,

    [Description("پیشنهاد فنی/مالی")]
    Proposal = 42,

    // مالی
    [Description("فاکتور")]
    Invoice = 60,

    [Description("اسناد پرداخت")]
    PaymentDocument = 61,

    [Description("برآورد هزینه")]
    CostEstimate = 62,

    // خرید
    [Description("درخواست خرید")]
    PurchaseRequest = 80,

    [Description("سفارش خرید")]
    PurchaseOrder = 81,

    [Description("مستندات تأمین‌کننده")]
    VendorDocument = 82,

    // کیفیت
    [Description("مستندات کیفیت")]
    QualityDocument = 100,

    [Description("گزارش عدم انطباق")]
    NCR = 101,

    [Description("مستندات HSE")]
    HSEDocument = 102,

    // تحویل پروژه
    [Description("نقشه‌های چون‌ساخت (As-Built)")]
    AsBuilt = 120,

    [Description("مستندات تحویل پروژه")]
    HandoverDocument = 121,

    [Description("دفترچه بهره‌برداری")]
    OperationManual = 122,

    [Description("دفترچه نگهداری و تعمیرات")]
    MaintenanceManual = 123,

    // سایر
    [Description("تصویر")]
    Image = 140,

    [Description("ویدئو")]
    Video = 141,

    [Description("سایر")]
    Other = 999
}