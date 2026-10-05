using System.ComponentModel;

namespace Engineering.Application.Services.FixAssetMachineries.Models.GetsFixAssetMachineryNotWorkExcelEnum;

public enum FixAssetMachineryNotworkExcelEnum
{
    [Description("شناسه عدم فعالیت")] Id = 1,
    [Description("شناسه ماشین آلات در اختیار شرکت")] FixAssetMachineryId = 2,
    [Description("ماشین آلات")] MachineryName = 3,
    [Description("کد ماشین آلات")] MachineryCode = 4,
    [Description("نوع موجودی")] TypeDesctiption = 5,
    [Description("تامین کننده")] Contractor = 6,
    [Description("از تاریخ")] FromDateShamsi = 7,
    [Description("تا تاریخ")] ToDateShamsi = 8,
    [Description("از زمان")] FromTime = 9,
    [Description("تا زمان")] ToTime = 10,
    [Description("توضیحات")] Description = 11,
    [Description("ثبت کننده")] Creator = 12,
    [Description("تاریخ ثبت")] CreatedShamsi = 13,
    [Description("کمپانی")] Company = 14,
}