using NPOI.SS.Format;

namespace Engineering.Domain.Cmts;

public static class WbsCmts
{
    public const string WbsTemplate = "شکست کار";
    public const string ProjectScheduleImportStatus = "وضعیت های بارگذاری فایل";
    public const string ProjectScheduleDependencyType = "نوع های وابستگی";
    public const string WbsTemplateTitle = "عنوان شکست کار";
    public const string WbsTemplateCode = "کد شکست کار";
    public const string ProjectWbs = "شکست کار پروژه";
    public const string ProjectWbsId = "شناسه شکست کار پروژه";
    public const string ProjectWbsTitle = "عنوان شکست کار پروژه";
    public const string ProjectWbsCode = "کد شکست کار پروژه";
    public const string ProjectOperationWbs = "شکست کار شرح عملیات پروژه";
    public const string ProjectScheduleTaskValue = "مقدار برنامه زمانبندی";
    public const string DontShowParent = "بولین نشان دادن والد";

    // Project Schedule
    public const string ProjectSchedule = "زمان‌بندی پروژه";

    // Import
    public const string ProjectScheduleImport = "ورود برنامه زمان‌بندی پروژه";
    public const string ProjectScheduleImportId = "شناسه ورود برنامه زمان‌بندی پروژه";
    public const string FileId = "شناسه فایل";
    public const string FileName = "نام فایل";
    public const string ScheduleStartDate = "تاریخ شروع برنامه ای";
    public const string ImportStatus = "وضعیت ورود";
    public const string ImportedAt = "تاریخ ورود";
    public const string ImportedBy = "کاربر واردکننده";
    public const string ErrorMessage = "پیام خطای ورود";

    public const string StatusDate = "تاریخ وضعیتی";
    public const string RescheduleFromDate = "تغییر تاریخ از";

    // Schedule Task
    public const string ProjectScheduleTask = "فعالیت زمان‌بندی پروژه";
    public const string ProjectScheduleTaskId = "شناسه فعالیت زمان‌بندی پروژه";
    public const string ScheduleTaskTitle = "عنوان فعالیت زمان‌بندی";

    // Microsoft Project
    public const string MppUid = "شناسه یکتای فعالیت در Microsoft Project";
    public const string MppId = "شناسه فعالیت در Microsoft Project";

    // Structure
    public const string SortOrder = "ترتیب نمایش";
    public const string OutlineLevel = "سطح ساختاری";
    public const string OutlineNumber = "شماره ساختاری";

    // Planned
    public const string PlannedStart = "تاریخ شروع برنامه‌ریزی‌شده";
    public const string PlannedFinish = "تاریخ پایان برنامه‌ریزی‌شده";
    public const string PlannedDurationMinutes = "مدت زمان برنامه‌ریزی‌شده به دقیقه";

    // Progress
    public const string PercentComplete = "درصد پیشرفت";
    public const string PhysicalPercentComplete = "درصد پیشرفت واقعی";

    // Baseline
    public const string BaselineStart = "تاریخ شروع خط مبنا";
    public const string BaselineFinish = "تاریخ پایان خط مبنا";
    public const string BaselineDurationMinutes = "مدت زمان خط مبنا به دقیقه";

    // Actual
    public const string ActualStart = "تاریخ شروع واقعی";
    public const string ActualFinish = "تاریخ پایان واقعی";
    public const string ActualDurationMinutes = "مدت زمان واقعی به دقیقه";

    // Task Properties
    public const string IsMilestone = "نقطه عطف";
    public const string IsSummary = "فعالیت بر";
    public const string IsCritical = "فعالیت بحرانی";
    public const string IsEstimated = "زمان تخمینی";
    public const string IsManuallyScheduled = "وضعیت دستی";
    public const string Deadline = "تاریخ سررسید";
    public const string Note = "یاداشت";
    public const string Cost = "هزینه";
    public const string RemainingDurationMinutes = "زمان کاری باقی مانده";

    // Dependency
    public const string ProjectScheduleTaskDependency = "وابستگی فعالیت زمان‌بندی پروژه";
    public const string ProjectScheduleTaskDependencyId = "شناسه وابستگی فعالیت زمان‌بندی پروژه";

    public const string PredecessorTask = "فعالیت پیش‌نیاز";
    public const string PredecessorTaskId = "شناسه فعالیت پیش‌نیاز";

    public const string SuccessorTask = "فعالیت پس‌نیاز";
    public const string SuccessorTaskId = "شناسه فعالیت پس‌نیاز";

    public const string DependencyType = "نوع وابستگی";
    public const string LagMinutes = "تأخیر زمانی به دقیقه";

    // Dependency Types
    public const string FinishToStart = "پایان به شروع";
    public const string StartToStart = "شروع به شروع";
    public const string FinishToFinish = "پایان به پایان";
    public const string StartToFinish = "شروع به پایان";

    // Calendar
    public const string Calendar = "تقویم کاری";
    public const string CalendarId = "شناسه تقویم کاری";

    public const string CalendarWorkingDay = "روز کاری تقویم";
    public const string CalendarWorkingDayId = "شناسه روز کاری تقویم";

    public const string WorkingTime = "ساعت کاری";
    public const string WorkingTimeId = "شناسه ساعت کاری";

    public const string CalendarHoliday = "تعطیلات تقویم";
    public const string CalendarHolidayId = "شناسه تعطیلات تقویم";

    public const string IsWorkingDay = "روز کاری";
    public const string DayOfWeek = "روز هفته";
    public const string FromTime = "ساعت شروع کار";
    public const string ToTime = "ساعت پایان کار";
    public const string HolidayDate = "تاریخ تعطیلی";

    // Optional ProjectOperation Mapping
    public const string ProjectScheduleTaskOperation = "ارتباط فعالیت زمان‌بندی با شرح عملیات پروژه";

    public const string ProjectScheduleTaskOperationId = "شناسه ارتباط فعالیت زمان‌بندی با شرح عملیات پروژه";

    // columns
    public const string ColumnType = "نوع ستون";
    public const string DataType = "نوع داده";
    public const string ProjectScheduleColumn = "ستون برنامه زمانبندی";
    public const string ColumnIsCustom = "ستون اضافی";
    public const string ColumnCanEditName = "قابلیت ویرایش نام";
    public const string ColumnIsVisible = "ستون قابل نمایش";
    public const string ColumnMppFieldName = "نام فیلد در mpp";
    public const string StringValue = "نوع متن";
    public const string NumberValue = "نوع عدد";
    public const string DateTimeValue = "نوع تاریخ";
}