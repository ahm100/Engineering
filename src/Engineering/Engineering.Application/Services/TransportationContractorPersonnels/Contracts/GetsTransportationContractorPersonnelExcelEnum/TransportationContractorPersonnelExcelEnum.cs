using System.ComponentModel;

namespace Engineering.Application.Services.TransportationContractorPersonnels.Contracts.GetsTransportationContractorPersonnelExcelEnum;

public enum TransportationContractorPersonnelExcelEnum
{
    [Description("نام شرکت")] CompanyName = 1,
    [Description("شماره ثبت")] RegisterationNo = 2,
    [Description("نام پرسنل")] PersonnelFirstName = 3,
    [Description("نام خانوادگی پرسنل")] PersonnelLastName = 4,
    [Description("نام کامل پرسنل")] PersonnelFullName = 5,
    [Description("شماره تماس پرسنل")] PersonnelPhoneNumber = 6,
    [Description("کدملی پرسنل")] PersonnelIdentityNo = 7,
    [Description("توضیحات پرسنل")] PersonnelDescription = 8,
    [Description("شهر پرسنل")] PersonnelCity = 9,
    [Description("آدرس پرسنل")] PersonnelAddress = 10,
    [Description("عنوان پرسنل")] PersonnelTitle = 11,
    [Description("تاریخ ایجاد شمسی")] CreatedShamsi = 12,
    [Description("شماره گواهینامه")] CertificateNumber = 13,
    [Description("ماشین ها")] PersonnelMachines = 14
}
