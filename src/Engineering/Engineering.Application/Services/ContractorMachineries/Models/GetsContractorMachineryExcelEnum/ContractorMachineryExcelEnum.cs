using System.ComponentModel;

namespace Engineering.Application.Services.ContractorMachineries.Models.GetsContractorMachineryExcelEnum;

public enum ContractorMachineryExcelEnum
{
    [Description("شناسه ماشین پیمانکاری")] Id = 1,
    [Description("پیمانکار")] Contractor = 2,
    [Description("نام مستعار پیمانکار")] ContractorNickName = 3,
    [Description("گروه ماشین آلات")] MachineryGroupName = 4,
    [Description("کد گروه")] MachineryGroupCode = 5,
    [Description("ماشین آلات")] MachineryName = 6,
    [Description("کد ماشین آلات")] MachineryCode = 7,
    [Description("واحد")] UnitDesctiption = 8,
    [Description("قیمت")] MachineryPrice = 9,
    [Description("واحد")] Currency = 10,
    [Description("شناسه ماشین آلات")] MachineryIdentifier = 11,
    [Description("پلاک")] NumberPlates = 12,
    [Description("وضعیت")] IsActive = 13,
    [Description("توضیحات")] Description = 14,
    [Description("ثبت کننده")] Creator = 15,
    [Description("تاریخ ثبت")] CreatedShamsi = 16,
}