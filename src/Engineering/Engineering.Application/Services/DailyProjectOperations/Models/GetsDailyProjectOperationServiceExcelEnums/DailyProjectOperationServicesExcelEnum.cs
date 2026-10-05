using System.ComponentModel;

namespace Engineering.Application.Services.DailyProjectOperations.Models.GetsDailyProjectOperationServiceExcelEnums;

public enum DailyProjectOperationServicesExcelEnum
{
    [Description("شناسه خدمت")] Id = 1,
    [Description("شناسه کارکرد روزانه")] DailyProjectOperationId = 2,
    [Description("مرکز هزینه")] CostCenterName = 3,
    [Description("پروژه")] ProjectName = 4,
    [Description("شرح عملیات")] ProjectOperationName = 5,
    [Description("وضعیت")] ProjectOperationStatusDescription = 6,
    [Description("توضیحات ریزمتره")] ProjectOperationDetailDescription = 7,
    [Description("خدمت ریزمتره")] ProjectOperationDetailServiceInfoName = 8,
    [Description("حجم خدمت ریزمتره")] ProjectOperationDetailVolume = 9,
    [Description("پیمانکار خدمت ریزمتره")] ProjectOperationDetailContractor = 10,
    [Description("وضعیت ریزمتره")] ProjectOperationDetailStatusDescription = 11,
    [Description("تاریخ شروع ریزمتره")] ProjectOperationDetailStartDateShamsi = 12,
    [Description("تاریخ پایان ریزمتره")] ProjectOperationDetailEndDateShamsi = 13,
    [Description("تاریخ ایجاد ریزمتره")] ProjectOperationDetailCreateDateShamsi = 14,
    [Description("عنوان موقعیت جزیی")] PrivateName = 15,
    [Description("کد موقعیت جزیی")] PrivateCode = 16,
    [Description("عنوان موقعیت")] PublicName = 17,
    [Description("کد موقعیت")] PublicCode = 18,
    [Description("عنوان خدمت کارکرد")] ServiceInfoName = 19,
    [Description("حجم خدمت کارکرد")] Volume = 20,
    [Description("توضیحات")] Description = 21,
    [Description("وضعیت کارکرد")] StatusDescription = 22,
    [Description("تاریخ شروع کارکرد")] StartDateShamsi = 23,
    [Description("تاریخ پایان کارکرد")] EndDateShamsi = 24,
    [Description("پیمانکار خدمت کارکرد")] Contractor = 25,
    [Description("ایجاد کننده")] CreatorName = 26,
    [Description("تاریخ ایجاد")] CreatedShamsi = 27,
    [Description("حجم نهایی ریزمتره")] ProjectOperationDetailFinalAmount = 28,
    [Description("حجم نهایی کارکرد روزانه")] DailyProjectOperationFinalAmount = 29,
    [Description("حجم نهایی ریزمتره با احتساب کسورات")] TotalProjectOperationDetailFinalAmount = 30,
    [Description("شناسه ریزمتره")] ProjectOperationDetailId = 31,

}