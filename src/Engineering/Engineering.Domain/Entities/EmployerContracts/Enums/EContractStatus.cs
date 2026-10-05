namespace Engineering.Domain.Entities.EmployerContracts;

[Description(EContractCmts.ContractStatus)]
public enum EContractStatus
{
    [Description("ثبت اولیه")]
    New = 1,
    [Description("ارسال مجدد به مدیر پروژه")]
    ProjectManagerResend = 10,

    [Description("بررسی مدیر پروژه")]
    ProjectManagerPending = 20,
    [Description("برگشت از مدیر پروژه")]
    ProjectManagerReturned = 30,
    [Description("تایید مدیر پروژه")]
    ProjectManagerConfirmed = 40,

    [Description("بررسی کارفرما")]
    EmployerPending = 45,
    [Description("برگشت از کارفرما")]
    EmployerReturned = 50,
    [Description("تایید کارفرما")]
    EmployerConfirmed = 55,

    [Description("بررسی کارشناس امور قرارداد")]
    ContractExpertPending = 60,
    [Description("برگشت از کارشناس امور قرارداد")]
    ContractExpertReturned = 65,
    [Description("تایید کارشناس امور قرارداد")]
    ContractExpertConfirmed = 70,
    [Description("برگشت از کارشناس به درخواست دهنده")]
    ContractExpertReturnToUser = 75,

    [Description("بررسی سرپرست امور قرارداد")]
    ContractSupervisorPending = 80,
    [Description(" برگشت از سرپرست به درخواست دهنده به کاربر")]
    ContractSupervisorReturnedToUser = 90,
    [Description("برگشت از کارشناس امور قرارداد")]
    ReturnToContractExpert = 100,
    [Description("تایید سرپرست امور قرارداد")]
    ContractSupervisorConfirmed = 120,

    [Description("برگشت از مدیر اولیه")]
    PrimaryManagerReturned = 130,
    [Description("تایید مدیر اولیه")]
    PrimaryManagerConfirmed = 140,

    [Description("برگشت از مدیر نهایی")]
    FinalManagerReturned = 160,
    [Description("تایید مدیر نهایی")]
    FinalManagerConfirmed = 170,

    [Description("ارسال به بازرگانی کارفرما")]
    SendToEmployerBusiness = 180,
    [Description("تایید بازرگانی کارفرما")]
    EmployerBusinessConfirmed = 190,
}

public class EContractStatusRules
{
    public static List<EContractStatus> AllowForProjectManagerResend =
        [
        EContractStatus.New,
        EContractStatus.ProjectManagerReturned,
        EContractStatus.ContractSupervisorReturnedToUser,
    ];

    public static List<EContractStatus> NotAllowForEmployer =
        [
        EContractStatus.New,
        EContractStatus.ProjectManagerResend,
        EContractStatus.ProjectManagerPending,
        EContractStatus.ProjectManagerReturned,
    ];

    public static List<EContractStatus> AllowForEmployer =>
    Enum.GetValues(typeof(EContractStatus))
        .Cast<EContractStatus>()
        .Except(NotAllowForEmployer)
        .ToList();

    public static List<EContractStatus> AllowManager =
        [
        EContractStatus.PrimaryManagerConfirmed,
        EContractStatus.FinalManagerConfirmed,
    ];

    public static List<EContractStatus> AllowEmployer =
        [
        EContractStatus.EmployerConfirmed,
        EContractStatus.EmployerPending,
        EContractStatus.EmployerReturned,
    ];
    public static List<EContractStatus> AllowVolume =
        [
        EContractStatus.ProjectManagerConfirmed,
        EContractStatus.ProjectManagerPending,
    ];

    public static List<EContractStatus> AllowContractExpertCost =
        [
        EContractStatus.ContractExpertConfirmed
    ];

    public static List<EContractStatus> AllowFinance =
        [
        EContractStatus.ContractExpertConfirmed,
        EContractStatus.ContractSupervisorConfirmed
    ];

    public static List<EContractStatus> AllowContractExpertEditProduct =
        [
        EContractStatus.ContractExpertPending,
        EContractStatus.ContractExpertConfirmed
    ];

    public static List<EContractStatus> AllowContractSupervisorEditProduct =
        [
        EContractStatus.ContractSupervisorPending,
        EContractStatus.ContractSupervisorConfirmed
    ];

    public static List<EContractStatus> AllowContractSupervisorCost =
        [
        EContractStatus.ContractSupervisorConfirmed
    ];

    public static List<EContractStatus> AllowForProjectManagerPending =
        [
        EContractStatus.New,
        EContractStatus.ProjectManagerResend,
        EContractStatus.ReturnToContractExpert,
        EContractStatus.ProjectManagerPending,
    ];

    public static List<EContractStatus> AllowForProjectManagerReturned =
        [
        EContractStatus.New,
        EContractStatus.ProjectManagerResend,
        EContractStatus.ProjectManagerPending,
    ];

    public static List<EContractStatus> AllowForProjectManagerRejected =
        [
        EContractStatus.New,
        EContractStatus.ProjectManagerResend,
        EContractStatus.ProjectManagerPending,
    ];

    public static List<EContractStatus> AllowForProjectManagerConfirmed =
        [
        EContractStatus.New,
        EContractStatus.ProjectManagerResend,
        EContractStatus.ProjectManagerPending,
    ];

    public static List<EContractStatus> AllowForContractExpertPending =
        [
        EContractStatus.ProjectManagerConfirmed,
        EContractStatus.ContractExpertPending,
        EContractStatus.ReturnToContractExpert,
    ];

    public static List<EContractStatus> AllowForContractExpertReturned =
        [
        EContractStatus.ProjectManagerConfirmed,
        EContractStatus.ContractExpertPending,
        EContractStatus.ReturnToContractExpert
    ];

    public static List<EContractStatus> AllowForContractExpertConfirmed =
        [
        EContractStatus.ContractExpertPending,
        EContractStatus.ProjectManagerConfirmed,
        EContractStatus.ReturnToContractExpert,
    ];

    public static List<EContractStatus> AllowForContractExpertReturnToUSer =
        [
        EContractStatus.ContractExpertPending,
        EContractStatus.ProjectManagerConfirmed,
        EContractStatus.ReturnToContractExpert,
    ];

    public static List<EContractStatus> AllowForContractSupervisorPending =
        [
        EContractStatus.ContractExpertConfirmed,
        EContractStatus.ContractSupervisorPending,
    ];

    public static List<EContractStatus> AllowForContractSupervisorReturned =
        [
        EContractStatus.ContractSupervisorPending,
        EContractStatus.ContractExpertConfirmed,
    ];

    public static List<EContractStatus> AllowForContractSupervisorRejected =
        [
        EContractStatus.ContractSupervisorPending,
        EContractStatus.ContractExpertConfirmed,
    ];

    public static List<EContractStatus> AllowForContractSupervisorConfirmed =
        [
        EContractStatus.ContractSupervisorPending,
        EContractStatus.ContractExpertConfirmed
    ];

    public static List<EContractStatus> AllowForPrimaryManagerReturned =
        [
        EContractStatus.ContractSupervisorConfirmed
    ];

    public static List<EContractStatus> AllowForPrimaryManagerConfirmed =
        [
        EContractStatus.ContractSupervisorConfirmed
    ];

    public static List<EContractStatus> AllowForFinalManagerReturned =
        [
        EContractStatus.ContractSupervisorConfirmed
    ];

    public static List<EContractStatus> AllowForFinalManagerConfirmed =
        [
        EContractStatus.ContractSupervisorConfirmed
    ];

    public static List<EContractStatus> AllowForPaymentConfirmation =
        [
        EContractStatus.ContractSupervisorConfirmed
    ];

    public static List<EContractStatus> AllowForArchived =
        [
        EContractStatus.New,
        EContractStatus.ProjectManagerResend,
    ];

    public static List<EContractStatus> AllowForReturnToProjectManager =
        [
        EContractStatus.ProjectManagerConfirmed,
        EContractStatus.ContractSupervisorPending,
    ];

}