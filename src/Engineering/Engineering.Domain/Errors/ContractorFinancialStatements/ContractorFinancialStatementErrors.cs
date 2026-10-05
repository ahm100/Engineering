
namespace Engineering.Domain.Errors;

public static class ContractorFinancialStatementErrors
{
    public static Error InValidCostCenter = new("InvalidArguments", "مرکز معتبر نیست.", 422);
    public static Error InValidProject = new("InvalidArguments", "پروژه نا معتبر است.", 422);
    public static Error ProjectNotInCostCenter = new("InvalidArguments", "پروژه مدنظر در مرکزهزینه انتخابی نیست.", 422);
    public static Error InValidContractorContract = new("InvalidArguments", "قرارداد پیمانکار نا معتبر است.", 422);
    public static Error InValidContractor = new("InvalidArguments", "پیمانکار نا معتبر است.", 422);
    public static Error ContractorContractTypeNotFound = new("InvalidArguments", "هیچ نوع قراردادی در بازه مشخص شده یافت نشد.", 422);
    public static Error InValidContractorContractStatus = new("InvalidArguments", "قرارداد پیمانکار باید در وضعیت تایید یا بایگانی باشد.", 422);
    public static Error ContractorNoInContractorContract = new("InvalidArguments", "پیمانکار مد نظر در این قراردادپیمانکار نمیباشد.", 422);
    public static Error InvalidRequestDate = new("InvalidArguments", "تاریخ وارد شده در بازه قرارداد پیمانکار نیست.", 422);
    public static Error ContractorFinancialStatementWithIdNotFound = new("NotFound", "شرح عملیات پروژه ای برای این صورت وضعیت پیمانکار یافت نشد.", 404);
    public static Error PriceNotFound = new("NotFound", "قیمتی برای این شرح عملیات در بازه مشخص شده یافت نشد.", 404);
    public static Error DuplicatePrice = new("Duplicate", "در بازه مشخص شده بیش از یک قیمت در قرارداد پیمانکار وجود دارد.", 409);




    public static Error ThirdPartyNotFound = new("NotFound", "شخص یافت نشد.", 404);
    public static Error ContractProjectOperationNotFound = new("NotFound", "شرح عملیات پروژه ای در این بازه و این اطلاعات یافت نشد.", 404);
    public static Error ServicesNotFound = new("NotFound", "کارکرد روزانه ای با این خدمت یافت نشد.", 404);

    public static Error InvalidContractorFinancialStatement = new("InvalidArguments", "صورت وضعیت پیمانکار نا معتبر است.", 422);
    public static Error InvalidContractorFinancialStatementStatus = new("InvalidArguments", "وضیعت صورت وضعیت پیمانکار نا معتبر است.", 422);
    public static Error InvalidStartDate = new("InvalidArguments", "تاریخ شروع نا معتبر است.", 422);
    public static Error InvalidEndDate = new("InvalidArguments", "تاریخ پایان نا معتبر است.", 422);
    public static Error InvalidTotalPrice = new("InvalidArguments", "قیمت صورت وضعیت نا معتبر است.", 422);
    public static Error InvalidWorkDoneValue = new("InvalidArguments", "درصد انجام کار نا معتبر است.", 422);
    public static Error InvalidContractorContract = new("InvalidArguments", "قرارداد پیمانکار نا معتبر است.", 422);
    public static Error InvalidProjectOperationDetailId = new("InvalidArguments", "ریز متر نا معتبر است.", 422);
    public static Error InvalidProduct = new("InvalidArguments", "کالا نا معتبر است.", 422);
    public static Error InvalidProductGroup = new("InvalidArguments", "گروه محصول نا معتبر است.", 422);
    public static Error InvalidCurrency = new("InvalidArguments", "ارز نا معتبر است.", 422);
    public static Error InvalidHeaderInfo = new("InvalidArguments", "اطلاعات هدر معتبر نیست.", 422);
    public static Error InvalidMeasureunit = new("InvalidArguments", "اطلاعات هدر معتبر نیست.", 422);
    public static Error InValidProjectOperation = new("InvalidArguments", "شرح عملیات پروژه نا معتبر است.", 422);
    public static Error InValidStatus = new("InvalidArguments", "وضعیت نا معتبر است.", 422);

    public static Error InValidTransportationRequest = new("InvalidArguments", "درخواست ترابری نامعتبر است.", 422);

}
