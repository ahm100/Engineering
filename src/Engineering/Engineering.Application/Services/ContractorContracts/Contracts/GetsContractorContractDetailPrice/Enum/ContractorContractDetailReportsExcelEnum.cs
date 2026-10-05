using System.ComponentModel;

namespace Engineering.Application.Services.ContractorContracts.Contracts.GetsContractorContractDetailPrice.Enum;

public enum ContractorContractDetailReportsExcelEnum
{
    [Description("شناسه قرارداد پیمانکار")]
    ContractorContractId = 1,
    [Description("شناسه")]
    Id = 5,
    [Description("حجم")]
    WorkLoad = 10,
    [Description("تاریخ شروع")]
    StartDate = 15,
    [Description("تاریخ پایان")]
    EndDate = 20,
    [Description("حجم واحد")]
    UnitAmount = 25,
    [Description("حجم کل")]
    TotalAmount = 30,
    [Description("شناسه شرح عملیات پروژه")]
    ProjectOperationId = 35,
    [Description("شناسه شرح عملیات")]
    OperationInfoId = 40,
    [Description("شرح عملیات")]
    OperationInfoName = 45,
    [Description("کد شرح عملیات")]
    OperationInfoCode = 50,
    [Description("شناسه پروژه")]
    ProjectId = 55,
    [Description("پروژه")]
    ProjectName = 60,
    [Description("کد پروژه")]
    ProjectCode = 65,
    [Description("شناسه مرکزهزینه")]
    CostCenterId = 70,
    [Description("مرکز هزینه")]
    CostCenterName = 75,
    [Description("کد مرکز هزینه")]
    CostCenterCode = 80,
    [Description("شناسه خدمات")]
    ServiceInfoId = 85,
    [Description("خدمات")]
    ServiceInfoName = 90,
    [Description("کد خدمات")]
    ServiceInfoCode = 95,
    [Description("شناسه واحد خدمت")]
    ServiceInfoUnitOfMeasurementId = 100,
    [Description("واحد خدمت")]
    ServiceInfoUnitOfMeasurement = 105,
    [Description("شناسه ریزمتره")]
    ProjectOperationDetailId = 110,
    [Description("شناسه آدرس")]
    OperationLocationId = 115,
    [Description("عنوان خصوصی")]
    PrivateName = 120,
    [Description("کد خصوصی")]
    PrivateCode = 125,
    [Description("عنوان عمومی")]
    PublicName = 130,
    [Description("کئ عمومی")]
    PublicCode = 135,
    [Description("وضعیت ریزمتره")]
    StatusDescription = 140,
    [Description("شناسه ایجاد کننده")]
    CreatorId = 145,
    [Description("ایجاد کننده")]
    Creator = 150,
    [Description("تاریخ ایجاد")]
    Created = 155,
}
