using System.ComponentModel;

namespace Engineering.Application.Services.TransportationContractorMachines.Contracts.GetsTransportationContractorMachineExcelEnum;

public enum TransportationContractorMachineExcelEnum
{
    [Description("نوعش خص")] IsIndivisualTitle = 1,
    [Description("نام مدیر")] FullName = 2,
    [Description("شماره تماس")] PhoneNumber = 3,
    [Description("کدملی")] IdentityNo = 4,
    [Description("نام پیمانکار حمل")] CompanyName = 5,
    [Description("کد ثبت")] RegisterationNo = 6,
    [Description("عنوان ماشین")] MachineTypeName = 7,
    [Description("کد پیمانکار حمل")] MachineTypeCode = 8,
    [Description("پلاک وسیله")] NumberPlate = 9,
    [Description("شماره شاسی")] Vin = 10,
    [Description("رنگ")] Color = 11,
    [Description("وضعیت")] IsActive = 12,
    [Description("تاریخ ایجاد")] CreatedShamsi = 13,
}
